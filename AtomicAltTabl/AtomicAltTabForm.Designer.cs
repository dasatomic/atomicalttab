using System.Windows.Forms;

namespace AtomicAltTabl
{
	partial class AtomicAltTabForm
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
            this._textboxProcessInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _textboxProcessInput
            // 
            this._textboxProcessInput.BackColor = System.Drawing.SystemColors.MenuBar;
            this._textboxProcessInput.Location = new System.Drawing.Point(12, 12);
            this._textboxProcessInput.Name = "_textboxProcessInput";
            this._textboxProcessInput.Size = new System.Drawing.Size(481, 20);
            this._textboxProcessInput.TabIndex = 0;
            //this._textboxProcessInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyPressed);
            this._textboxProcessInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextboxProcessInputTextChanged);
		    this._textboxProcessInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HandleNewTypedCharacter);

            // 
            // AtomicAltTabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(503, 50);
            this.Controls.Add(this._textboxProcessInput);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "AtomicAltTabForm";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "AtomicAltTab";
            this.ResumeLayout(false);
            this.PerformLayout();
		}

		#endregion

        private System.Windows.Forms.TextBox _textboxProcessInput;
	}
}

