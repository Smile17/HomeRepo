
using System.Drawing;
using System.Windows.Forms;

namespace KnowledgeTester
{
    partial class Tester
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnUploadTest = new System.Windows.Forms.Button();
            this.lblTestName = new System.Windows.Forms.Label();
            this.pnlTest = new System.Windows.Forms.Panel();
            this.btnCheck = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(377, 12);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(144, 25);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate Sample Test";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnUploadTest
            // 
            this.btnUploadTest.Location = new System.Drawing.Point(12, 13);
            this.btnUploadTest.Name = "btnUploadTest";
            this.btnUploadTest.Size = new System.Drawing.Size(89, 24);
            this.btnUploadTest.TabIndex = 1;
            this.btnUploadTest.Text = "Upload Test";
            this.btnUploadTest.UseVisualStyleBackColor = true;
            this.btnUploadTest.Click += new System.EventHandler(this.btnUploadTest_Click);
            // 
            // lblTestName
            // 
            this.lblTestName.AutoSize = true;
            this.lblTestName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTestName.Location = new System.Drawing.Point(12, 51);
            this.lblTestName.Name = "lblTestName";
            this.lblTestName.Size = new System.Drawing.Size(16, 21);
            this.lblTestName.TabIndex = 2;
            this.lblTestName.Text = "-";
            // 
            // pnlTest
            // 
            this.pnlTest.AutoScroll = true;
            this.pnlTest.Location = new System.Drawing.Point(12, 75);
            this.pnlTest.Name = "pnlTest";
            this.pnlTest.Size = new System.Drawing.Size(508, 306);
            this.pnlTest.TabIndex = 4;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(107, 13);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(82, 25);
            this.btnCheck.TabIndex = 5;
            this.btnCheck.Text = "Check Test";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblResult.Location = new System.Drawing.Point(488, 51);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(16, 21);
            this.lblResult.TabIndex = 6;
            this.lblResult.Text = "-";
            // 
            // Tester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 393);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.pnlTest);
            this.Controls.Add(this.lblTestName);
            this.Controls.Add(this.btnUploadTest);
            this.Controls.Add(this.btnGenerate);
            this.Name = "Tester";
            this.Text = "Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnUploadTest;
        private System.Windows.Forms.Label lblTestName;
        private System.Windows.Forms.Panel pnlTest;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblResult;
    }
}

