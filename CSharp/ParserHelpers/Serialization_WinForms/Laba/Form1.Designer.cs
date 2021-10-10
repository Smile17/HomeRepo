namespace Laba
{
    partial class Form1
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
            this.btInput = new System.Windows.Forms.Button();
            this.rtbFileContent = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFName = new System.Windows.Forms.TextBox();
            this.tbLName = new System.Windows.Forms.TextBox();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.btSave = new System.Windows.Forms.Button();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.cbSaveFormat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btInput
            // 
            this.btInput.Location = new System.Drawing.Point(15, 12);
            this.btInput.Name = "btInput";
            this.btInput.Size = new System.Drawing.Size(143, 23);
            this.btInput.TabIndex = 1;
            this.btInput.Text = "Upload";
            this.btInput.UseVisualStyleBackColor = true;
            this.btInput.Click += new System.EventHandler(this.btInput_Click);
            // 
            // rtbFileContent
            // 
            this.rtbFileContent.Location = new System.Drawing.Point(15, 54);
            this.rtbFileContent.Name = "rtbFileContent";
            this.rtbFileContent.Size = new System.Drawing.Size(258, 328);
            this.rtbFileContent.TabIndex = 2;
            this.rtbFileContent.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "First name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Last name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(332, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Year";
            // 
            // tbFName
            // 
            this.tbFName.Location = new System.Drawing.Point(364, 54);
            this.tbFName.Name = "tbFName";
            this.tbFName.Size = new System.Drawing.Size(162, 20);
            this.tbFName.TabIndex = 7;
            // 
            // tbLName
            // 
            this.tbLName.Location = new System.Drawing.Point(364, 79);
            this.tbLName.Name = "tbLName";
            this.tbLName.Size = new System.Drawing.Size(162, 20);
            this.tbLName.TabIndex = 8;
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(364, 105);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(162, 20);
            this.tbYear.TabIndex = 9;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(308, 12);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(119, 23);
            this.btSave.TabIndex = 10;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // cbFormat
            // 
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Items.AddRange(new object[] {
            "XML",
            "Binary format"});
            this.cbFormat.Location = new System.Drawing.Point(164, 14);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(109, 21);
            this.cbFormat.TabIndex = 11;
            this.cbFormat.Text = "XML";
            // 
            // cbSaveFormat
            // 
            this.cbSaveFormat.FormattingEnabled = true;
            this.cbSaveFormat.Items.AddRange(new object[] {
            "XML",
            "Binary format"});
            this.cbSaveFormat.Location = new System.Drawing.Point(433, 14);
            this.cbSaveFormat.Name = "cbSaveFormat";
            this.cbSaveFormat.Size = new System.Drawing.Size(109, 21);
            this.cbSaveFormat.TabIndex = 12;
            this.cbSaveFormat.Text = "XML";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(582, 400);
            this.Controls.Add(this.cbSaveFormat);
            this.Controls.Add(this.cbFormat);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.tbLName);
            this.Controls.Add(this.tbFName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbFileContent);
            this.Controls.Add(this.btInput);
            this.Name = "Form1";
            this.Text = "Program";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btInput;
        private System.Windows.Forms.RichTextBox rtbFileContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFName;
        private System.Windows.Forms.TextBox tbLName;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ComboBox cbFormat;
        private System.Windows.Forms.ComboBox cbSaveFormat;
    }
}

