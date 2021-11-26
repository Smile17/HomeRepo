
namespace ImageMapGame
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.btnUploadImageMap = new System.Windows.Forms.Button();
            this.lblObjectName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMain
            // 
            this.pbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMain.Image = ((System.Drawing.Image)(resources.GetObject("pbMain.Image")));
            this.pbMain.Location = new System.Drawing.Point(12, 33);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(732, 386);
            this.pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMain.TabIndex = 0;
            this.pbMain.TabStop = false;
            this.pbMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseDown);
            // 
            // btnUploadImageMap
            // 
            this.btnUploadImageMap.Location = new System.Drawing.Point(12, 3);
            this.btnUploadImageMap.Name = "btnUploadImageMap";
            this.btnUploadImageMap.Size = new System.Drawing.Size(88, 24);
            this.btnUploadImageMap.TabIndex = 6;
            this.btnUploadImageMap.Text = "Upload Map";
            this.btnUploadImageMap.UseVisualStyleBackColor = true;
            this.btnUploadImageMap.Click += new System.EventHandler(this.btnUploadImageMap_Click);
            // 
            // lblObjectName
            // 
            this.lblObjectName.AutoSize = true;
            this.lblObjectName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblObjectName.Location = new System.Drawing.Point(171, 3);
            this.lblObjectName.Name = "lblObjectName";
            this.lblObjectName.Size = new System.Drawing.Size(122, 25);
            this.lblObjectName.TabIndex = 7;
            this.lblObjectName.Text = "Object Name";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 444);
            this.Controls.Add(this.lblObjectName);
            this.Controls.Add(this.btnUploadImageMap);
            this.Controls.Add(this.pbMain);
            this.Name = "Main";
            this.Text = "Play Game";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Button btnUploadImageMap;
        private System.Windows.Forms.Label lblObjectName;
    }
}

