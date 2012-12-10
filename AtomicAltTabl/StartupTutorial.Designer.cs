namespace AtomicAltTabl
{
    partial class StartupTutorial
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
            this.txtReadme = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtReadme
            // 
            this.txtReadme.Location = new System.Drawing.Point(12, 12);
            this.txtReadme.Multiline = true;
            this.txtReadme.Name = "txtReadme";
            this.txtReadme.ReadOnly = true;
            this.txtReadme.Size = new System.Drawing.Size(349, 276);
            this.txtReadme.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(191, 292);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(169, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close and don\'t show me again";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // StartupTutorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 327);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtReadme);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartupTutorial";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "StartupTutorial";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReadme;
        private System.Windows.Forms.Button btnClose;
    }
}