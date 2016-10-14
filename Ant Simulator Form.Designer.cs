namespace Ant_Simulator
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nestRadioButton = new System.Windows.Forms.RadioButton();
            this.foodRadioButton = new System.Windows.Forms.RadioButton();
            this.simpleTimer = new System.Windows.Forms.Timer(this.components);
            this.clearDroppablesButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.highlightEnemyAntCheckBox = new System.Windows.Forms.CheckBox();
            this.highlightNestAntCheckBox = new System.Windows.Forms.CheckBox();
            this.highlightFoodAntCheckBox = new System.Windows.Forms.CheckBox();
            this.showAntCommsCheckBox = new System.Windows.Forms.CheckBox();
            this.showNestRangeCheckBox = new System.Windows.Forms.CheckBox();
            this.showFoodRangeCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.antTeamSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.mainPanel.Location = new System.Drawing.Point(12, 12);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(720, 650);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nestRadioButton);
            this.groupBox1.Controls.Add(this.foodRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(738, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 67);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Droppable Object";
            // 
            // nestRadioButton
            // 
            this.nestRadioButton.AutoSize = true;
            this.nestRadioButton.Location = new System.Drawing.Point(9, 42);
            this.nestRadioButton.Name = "nestRadioButton";
            this.nestRadioButton.Size = new System.Drawing.Size(47, 17);
            this.nestRadioButton.TabIndex = 1;
            this.nestRadioButton.Text = "Nest";
            this.nestRadioButton.UseVisualStyleBackColor = true;
            // 
            // foodRadioButton
            // 
            this.foodRadioButton.AutoSize = true;
            this.foodRadioButton.Checked = true;
            this.foodRadioButton.Location = new System.Drawing.Point(9, 19);
            this.foodRadioButton.Name = "foodRadioButton";
            this.foodRadioButton.Size = new System.Drawing.Size(49, 17);
            this.foodRadioButton.TabIndex = 0;
            this.foodRadioButton.TabStop = true;
            this.foodRadioButton.Text = "Food";
            this.foodRadioButton.UseVisualStyleBackColor = true;
            // 
            // simpleTimer
            // 
            this.simpleTimer.Interval = 16;
            this.simpleTimer.Tick += new System.EventHandler(this.simpleTimer_Tick);
            // 
            // clearDroppablesButton
            // 
            this.clearDroppablesButton.Location = new System.Drawing.Point(738, 112);
            this.clearDroppablesButton.Name = "clearDroppablesButton";
            this.clearDroppablesButton.Size = new System.Drawing.Size(132, 49);
            this.clearDroppablesButton.TabIndex = 0;
            this.clearDroppablesButton.Text = "Clear Droppables";
            this.clearDroppablesButton.UseVisualStyleBackColor = true;
            this.clearDroppablesButton.Click += new System.EventHandler(this.clearDroppablesButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.highlightEnemyAntCheckBox);
            this.groupBox2.Controls.Add(this.highlightNestAntCheckBox);
            this.groupBox2.Controls.Add(this.highlightFoodAntCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(738, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(132, 92);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Highlight Settings";
            // 
            // highlightEnemyAntCheckBox
            // 
            this.highlightEnemyAntCheckBox.AutoSize = true;
            this.highlightEnemyAntCheckBox.Location = new System.Drawing.Point(9, 65);
            this.highlightEnemyAntCheckBox.Name = "highlightEnemyAntCheckBox";
            this.highlightEnemyAntCheckBox.Size = new System.Drawing.Size(126, 17);
            this.highlightEnemyAntCheckBox.TabIndex = 9;
            this.highlightEnemyAntCheckBox.Text = "Highlight Enemy Ants";
            this.highlightEnemyAntCheckBox.UseVisualStyleBackColor = true;
            // 
            // highlightNestAntCheckBox
            // 
            this.highlightNestAntCheckBox.AutoSize = true;
            this.highlightNestAntCheckBox.Location = new System.Drawing.Point(9, 42);
            this.highlightNestAntCheckBox.Name = "highlightNestAntCheckBox";
            this.highlightNestAntCheckBox.Size = new System.Drawing.Size(116, 17);
            this.highlightNestAntCheckBox.TabIndex = 8;
            this.highlightNestAntCheckBox.Text = "Highlight Nest Ants";
            this.highlightNestAntCheckBox.UseVisualStyleBackColor = true;
            // 
            // highlightFoodAntCheckBox
            // 
            this.highlightFoodAntCheckBox.AutoSize = true;
            this.highlightFoodAntCheckBox.Location = new System.Drawing.Point(9, 19);
            this.highlightFoodAntCheckBox.Name = "highlightFoodAntCheckBox";
            this.highlightFoodAntCheckBox.Size = new System.Drawing.Size(118, 17);
            this.highlightFoodAntCheckBox.TabIndex = 7;
            this.highlightFoodAntCheckBox.Text = "Highlight Food Ants";
            this.highlightFoodAntCheckBox.UseVisualStyleBackColor = true;
            // 
            // showAntCommsCheckBox
            // 
            this.showAntCommsCheckBox.AutoSize = true;
            this.showAntCommsCheckBox.Location = new System.Drawing.Point(6, 19);
            this.showAntCommsCheckBox.Name = "showAntCommsCheckBox";
            this.showAntCommsCheckBox.Size = new System.Drawing.Size(109, 17);
            this.showAntCommsCheckBox.TabIndex = 4;
            this.showAntCommsCheckBox.Text = "Show Ant Comms";
            this.showAntCommsCheckBox.UseVisualStyleBackColor = true;
            // 
            // showNestRangeCheckBox
            // 
            this.showNestRangeCheckBox.AutoSize = true;
            this.showNestRangeCheckBox.Location = new System.Drawing.Point(6, 42);
            this.showNestRangeCheckBox.Name = "showNestRangeCheckBox";
            this.showNestRangeCheckBox.Size = new System.Drawing.Size(113, 17);
            this.showNestRangeCheckBox.TabIndex = 5;
            this.showNestRangeCheckBox.Text = "Show Nest Range";
            this.showNestRangeCheckBox.UseVisualStyleBackColor = true;
            // 
            // showFoodRangeCheckBox
            // 
            this.showFoodRangeCheckBox.AutoSize = true;
            this.showFoodRangeCheckBox.Location = new System.Drawing.Point(6, 65);
            this.showFoodRangeCheckBox.Name = "showFoodRangeCheckBox";
            this.showFoodRangeCheckBox.Size = new System.Drawing.Size(115, 17);
            this.showFoodRangeCheckBox.TabIndex = 6;
            this.showFoodRangeCheckBox.Text = "Show Food Range";
            this.showFoodRangeCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.showAntCommsCheckBox);
            this.groupBox3.Controls.Add(this.showNestRangeCheckBox);
            this.groupBox3.Controls.Add(this.showFoodRangeCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(738, 299);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(132, 99);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Show Range Settings";
            // 
            // antTeamSelectionComboBox
            // 
            this.antTeamSelectionComboBox.FormattingEnabled = true;
            this.antTeamSelectionComboBox.Items.AddRange(new object[] {
            "A",
            "B"});
            this.antTeamSelectionComboBox.Location = new System.Drawing.Point(738, 85);
            this.antTeamSelectionComboBox.Name = "antTeamSelectionComboBox";
            this.antTeamSelectionComboBox.Size = new System.Drawing.Size(132, 21);
            this.antTeamSelectionComboBox.TabIndex = 7;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 670);
            this.Controls.Add(this.antTeamSelectionComboBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.clearDroppablesButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "mainForm";
            this.Text = "Ant Simulator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton nestRadioButton;
        private System.Windows.Forms.RadioButton foodRadioButton;
        private System.Windows.Forms.Timer simpleTimer;
        private System.Windows.Forms.Button clearDroppablesButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox showAntCommsCheckBox;
        private System.Windows.Forms.CheckBox highlightNestAntCheckBox;
        private System.Windows.Forms.CheckBox highlightFoodAntCheckBox;
        private System.Windows.Forms.CheckBox showNestRangeCheckBox;
        private System.Windows.Forms.CheckBox showFoodRangeCheckBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox antTeamSelectionComboBox;
        private System.Windows.Forms.CheckBox highlightEnemyAntCheckBox;
    }
}

