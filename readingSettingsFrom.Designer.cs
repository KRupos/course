namespace course
{
    partial class readingSettingsFrom
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Lite = new System.Windows.Forms.RadioButton();
            this.Sepia = new System.Windows.Forms.RadioButton();
            this.Gray = new System.Windows.Forms.RadioButton();
            this.Black = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тема читалки:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(142, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 34);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(223, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 34);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.GrayText;
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(304, 34);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(55, 34);
            this.button3.TabIndex = 3;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Desktop;
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(385, 34);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(55, 34);
            this.button4.TabIndex = 4;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(12, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Размер символов:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(145, 93);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(45, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(400, 326);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(497, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // Lite
            // 
            this.Lite.AutoSize = true;
            this.Lite.BackColor = System.Drawing.Color.Transparent;
            this.Lite.ForeColor = System.Drawing.Color.White;
            this.Lite.Location = new System.Drawing.Point(122, 43);
            this.Lite.Name = "Lite";
            this.Lite.Size = new System.Drawing.Size(14, 13);
            this.Lite.TabIndex = 9;
            this.Lite.UseVisualStyleBackColor = false;
            this.Lite.CheckedChanged += new System.EventHandler(this.Lite_CheckedChanged);
            // 
            // Sepia
            // 
            this.Sepia.AutoSize = true;
            this.Sepia.BackColor = System.Drawing.SystemColors.Control;
            this.Sepia.Location = new System.Drawing.Point(203, 43);
            this.Sepia.Name = "Sepia";
            this.Sepia.Size = new System.Drawing.Size(14, 13);
            this.Sepia.TabIndex = 10;
            this.Sepia.UseVisualStyleBackColor = false;
            this.Sepia.CheckedChanged += new System.EventHandler(this.Sepia_CheckedChanged);
            // 
            // Gray
            // 
            this.Gray.AutoSize = true;
            this.Gray.BackColor = System.Drawing.SystemColors.Control;
            this.Gray.Location = new System.Drawing.Point(284, 43);
            this.Gray.Name = "Gray";
            this.Gray.Size = new System.Drawing.Size(14, 13);
            this.Gray.TabIndex = 11;
            this.Gray.UseVisualStyleBackColor = false;
            this.Gray.CheckedChanged += new System.EventHandler(this.Gray_CheckedChanged);
            // 
            // Black
            // 
            this.Black.AutoSize = true;
            this.Black.BackColor = System.Drawing.SystemColors.Control;
            this.Black.Location = new System.Drawing.Point(365, 45);
            this.Black.Name = "Black";
            this.Black.Size = new System.Drawing.Size(14, 13);
            this.Black.TabIndex = 12;
            this.Black.UseVisualStyleBackColor = false;
            this.Black.CheckedChanged += new System.EventHandler(this.Black_CheckedChanged);
            // 
            // readingSettingsFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.Black);
            this.Controls.Add(this.Gray);
            this.Controls.Add(this.Sepia);
            this.Controls.Add(this.Lite);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(600, 400);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "readingSettingsFrom";
            this.Text = "readingSettingsFrom";
            this.Load += new System.EventHandler(this.readingSettingsFrom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton Lite;
        private System.Windows.Forms.RadioButton Sepia;
        private System.Windows.Forms.RadioButton Gray;
        private System.Windows.Forms.RadioButton Black;
    }
}