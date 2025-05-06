using System;
using System.Net;
using System.Threading.Tasks;
using System.Numerics;
using System.Reflection;
using System.Drawing;
using ClosedXML.Excel;
using BaseballSharp;
using BaseballSharp.Enums;
using BaseballSharp.Models;

//jon tester
//jteste11@stumail.northeaststate.edu

//this is a project that will use an api wrapper to pull the current depth charts of MLB teams
namespace BaseballDepthChart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadImage();
        }
        private void LoadImage()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Logo2.png";

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                pictureBox1.Image = Image.FromStream(stream);
            }
        }
        /// <summary>
        /// loads form. loads team using LoadTeamsAsync. adds team list to comboBox. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false; //start dataGridView1 invisible
            ButtonExport.Visible = false; //start button export invisible
            try
            {
                var teamsList = await LoadTeamsAsync();
                if (teamsList == null || !teamsList.Any()) //make sure teams load
                {
                    MessageBox.Show("No teams were retrieved.");
                    return;
                }
                var sortedTeamsList = teamsList.OrderBy(team => team.FullName).ToList(); //put teams in alphabetical order

                comboBoxTeams.DataSource = sortedTeamsList.ToList();
                comboBoxTeams.DisplayMember = "FullName"; // adjust based on team name property
                comboBoxTeams.ValueMember = "Id"; // adjust based on team ID property
                comboBoxTeams.Refresh();
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load teams: {ex.Message}");
            }
        }
        /// <summary>
        /// event that loads the depth chart into dataGridView1 after user selects show team button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button1_Click(object sender, EventArgs e)
        {
            if (comboBoxTeams.SelectedItem == null) return;

            int teamId = (int)comboBoxTeams.SelectedValue; //changes team info based on user selection

            //allows user to select full roster, 40-man, or 25-man using radio buttons
            BaseballSharp.Enums.rosterType selectedRosterType;

            if (radioButtonFull.Checked)
            {
                selectedRosterType = BaseballSharp.Enums.rosterType.rosterFull;
            }
            else if (radioButton40.Checked)
            {
                selectedRosterType = BaseballSharp.Enums.rosterType.roster40;
            }
            else if (radioButton25.Checked)
            {
                selectedRosterType = BaseballSharp.Enums.rosterType.roster25;
            }
            else
            {
                MessageBox.Show("Please select a roster type."); //if user tries to run without selecting a roster type, will get messagebox
                return;
            }


            try
            {
                var teamRoster = await LoadRosterAsync(teamId, selectedRosterType);
                dataGridView1.DataSource = teamRoster; //loads depth chart into dataGridView1
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load roster: {ex.Message}");
            }
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Visible = true; //dataGridView1 becomes visible as rows are added
                ButtonExport.Visible = true; //export button becomes visible
            }
        }
        /// <summary>
        /// Fetches team data using baseballsharp API.
        /// </summary>
        /// <returns></returns>
        private static async Task<IEnumerable<Team>> LoadTeamsAsync()
        {
            var mlbClient = new BaseballSharp.MLBClient();
            return await mlbClient.GetTeamDataAsync();
        }
        /// <summary>
        /// Fetches team roster from baseballsharp API. returns roster as list.
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        private static async Task<IEnumerable<TeamRoster>> LoadRosterAsync(int teamId, BaseballSharp.Enums.rosterType rosterType)
        {
            var mlbClient = new BaseballSharp.MLBClient();
            return (List<TeamRoster>)await mlbClient.GetTeamRosterAsync(teamId, 2025, DateTime.Now, rosterType);
        }

        private static async Task<List<Schedule>> GetScheduleAsync()
        {
            var mlbClient = new BaseballSharp.MLBClient();
            var upcomingGames = await mlbClient.GetScheduleAsync(DateTime.Now);
            return upcomingGames.ToList();
        }

        private static async Task<List<PitchingReport>> GetTodayPitchingReportsAsync()
        {
            var mlbClient = new BaseballSharp.MLBClient();
            var pitchingReports = await mlbClient.GetPitchingReportsAsync(DateTime.Now);
            return pitchingReports.ToList();
        }

        /// <summary>
        /// autogenerates columns in DataGridView
        /// </summary>
        private void FormatDataGridView()
        {
            dataGridView1.AutoGenerateColumns = true;
        }
        /// <summary>
        /// allows user to export datagridview info to a spreadsheet. Used Copilot to help with this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExport_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.");
                return;
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Depth Chart");

            // Add column headers
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = dataGridView1.Columns[i].HeaderText;
            }

            // Add rows
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cell(i + 2, j + 1).Value = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                }
                //Save the file
            }
            // Get the selected team's name
            var selectedTeam = comboBoxTeams.SelectedItem as Team;
            string teamName = selectedTeam?.FullName ?? "Team";

            // Format the current date
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            // Prefill the save file name
            string defaultFileName = $"{teamName}_DepthChart_{currentDate}.xlsx";

            using var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save Depth Chart";
            saveFileDialog.FileName = defaultFileName;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialog.FileName);
                MessageBox.Show("Data exported successfully.");
            }
        }
        /// <summary>
        /// opens dialog box with today's current games
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonViewGames_Click(object sender, EventArgs e)
        {
            var upcomingGames = await GetScheduleAsync();
            var pitchingReports = await GetTodayPitchingReportsAsync();

            using var gamesDialog = new Form();
            gamesDialog.Text = "Today's Games";

            var listBoxGames = new ListBox
            {
                Dock = DockStyle.Top,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ItemHeight = 40
            };
            listBoxGames.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxGames.DrawItem += ListBoxGames_DrawItem;

            gamesDialog.Controls.Add(listBoxGames);

            //adds the games to listbox, showing away vs home at what field
            foreach (var game in upcomingGames)
            {
                listBoxGames.Items.Add($"{game.AwayTeam} vs {game.HomeTeam} at {game.Ballpark}");
            }

            int requiredHeight = (listBoxGames.ItemHeight * listBoxGames.Items.Count); //multiplies item height in list box by count
            int requiredWidth = 800;

            listBoxGames.Size = new Size(requiredWidth + 20, requiredHeight + 40); //makes listbox match height and width required to show all games without scrolling
            gamesDialog.Size = new Size(requiredWidth + 30, requiredHeight + 50); //makes gamesdialog large enough to fit listbox and look nice

            //allows user to click game and see the pitching matchup, if posted
            listBoxGames.SelectedIndexChanged += (s, ev) =>
            {
                if (listBoxGames.SelectedIndex == -1) return;

                var selectedGame = upcomingGames[listBoxGames.SelectedIndex];
                var pitchingReport = pitchingReports.FirstOrDefault(p => p.HomeTeam == selectedGame.HomeTeam && p.AwayTeam == selectedGame.AwayTeam);

                if (pitchingReport != null)
                {
                    MessageBox.Show($"Home pitcher: {pitchingReport.HomeProbablePitcherName}, Away pitcher: {pitchingReport.AwayProbablePitcherName}", "Pitching Matchup");
                }
                else
                {
                    MessageBox.Show("Pitching matchup not available.", "Pitching Matchup");
                }
            };

            gamesDialog.ShowDialog();
        }

        //used copilot to help with this, draws borders around items in listbox
        private void ListBoxGames_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var listBox = sender as ListBox;
            var itemText = listBox.Items[e.Index].ToString();

            // Draw background
            e.DrawBackground();

            // Draw border
            using (var pen = new Pen(Color.White, 2))
            {
                e.Graphics.DrawRectangle(pen, e.Bounds);
            }

            // Draw text
            using (var brush = new SolidBrush(listBox.ForeColor))
            {
                e.Graphics.DrawString(itemText, e.Font, brush, e.Bounds.X + 5, e.Bounds.Y + 5);
            }

            e.DrawFocusRectangle();
        }
    }
}