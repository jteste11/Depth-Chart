namespace BaseballDepthChart
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxTeams = new ComboBox();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            label1 = new Label();
            radioButtonFull = new RadioButton();
            radioButton40 = new RadioButton();
            radioButton25 = new RadioButton();
            ButtonExport = new Button();
            ButtonViewGames = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // comboBoxTeams
            // 
            comboBoxTeams.FormattingEnabled = true;
            comboBoxTeams.Location = new Point(12, 90);
            comboBoxTeams.Name = "comboBoxTeams";
            comboBoxTeams.Size = new Size(358, 23);
            comboBoxTeams.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 150);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1052, 841);
            dataGridView1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(376, 90);
            button1.Name = "button1";
            button1.Size = new Size(78, 23);
            button1.TabIndex = 2;
            button1.Text = "Show Team!";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(99, 68);
            label1.Name = "label1";
            label1.Size = new Size(171, 19);
            label1.TabIndex = 3;
            label1.Text = "Baseball Depth Chart";
            // 
            // radioButtonFull
            // 
            radioButtonFull.AutoSize = true;
            radioButtonFull.BackColor = SystemColors.WindowText;
            radioButtonFull.ForeColor = SystemColors.Window;
            radioButtonFull.Location = new Point(12, 122);
            radioButtonFull.Name = "radioButtonFull";
            radioButtonFull.Size = new Size(80, 19);
            radioButtonFull.TabIndex = 4;
            radioButtonFull.TabStop = true;
            radioButtonFull.Text = "Full Roster";
            radioButtonFull.UseVisualStyleBackColor = false;
            // 
            // radioButton40
            // 
            radioButton40.AutoSize = true;
            radioButton40.ForeColor = SystemColors.Window;
            radioButton40.Location = new Point(98, 122);
            radioButton40.Name = "radioButton40";
            radioButton40.Size = new Size(102, 19);
            radioButton40.TabIndex = 5;
            radioButton40.TabStop = true;
            radioButton40.Text = "40-Man Roster";
            radioButton40.UseVisualStyleBackColor = true;
            // 
            // radioButton25
            // 
            radioButton25.AutoSize = true;
            radioButton25.ForeColor = SystemColors.Window;
            radioButton25.Location = new Point(206, 124);
            radioButton25.Name = "radioButton25";
            radioButton25.Size = new Size(102, 19);
            radioButton25.TabIndex = 6;
            radioButton25.TabStop = true;
            radioButton25.Text = "25-Man Roster";
            radioButton25.UseVisualStyleBackColor = true;
            // 
            // ButtonExport
            // 
            ButtonExport.Location = new Point(989, 997);
            ButtonExport.Name = "ButtonExport";
            ButtonExport.Size = new Size(75, 23);
            ButtonExport.TabIndex = 7;
            ButtonExport.Text = "Export";
            ButtonExport.UseVisualStyleBackColor = true;
            ButtonExport.Click += ButtonExport_Click;
            // 
            // ButtonViewGames
            // 
            ButtonViewGames.Location = new Point(314, 118);
            ButtonViewGames.Name = "ButtonViewGames";
            ButtonViewGames.Size = new Size(140, 23);
            ButtonViewGames.TabIndex = 8;
            ButtonViewGames.Text = "View Today's Matchups";
            ButtonViewGames.UseVisualStyleBackColor = true;
            ButtonViewGames.Click += ButtonViewGames_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Logo2;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(75, 75);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowText;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1076, 1026);
            Controls.Add(pictureBox1);
            Controls.Add(ButtonViewGames);
            Controls.Add(ButtonExport);
            Controls.Add(radioButton25);
            Controls.Add(radioButton40);
            Controls.Add(radioButtonFull);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(comboBoxTeams);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Depth Chart";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxTeams;
        private DataGridView dataGridView1;
        private Button button1;
        private Label label1;
        private RadioButton radioButtonFull;
        private RadioButton radioButton40;
        private RadioButton radioButton25;
        private Button ButtonExport;
        private Button ButtonViewGames;
        private PictureBox pictureBox1;
    }
}
