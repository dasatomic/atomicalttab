using System.Windows.Forms;

namespace AtomicAltTabl
{
	partial class ActiveProcessForm
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
			//this._dataGridActiveProcesses = new System.Windows.Forms.DataGridView();
			this._dataGridActiveProcesses = new DataGridView();
			this.dataGridColumnProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridColumnProcessId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this._dataGridActiveProcesses)).BeginInit();
			this.SuspendLayout();
			// 
			// _dataGridActiveProcesses
			// 
			this._dataGridActiveProcesses.AllowUserToAddRows = false;
			this._dataGridActiveProcesses.AllowUserToDeleteRows = false;
			this._dataGridActiveProcesses.AllowUserToOrderColumns = true;
			this._dataGridActiveProcesses.AllowUserToResizeColumns = false;
			this._dataGridActiveProcesses.AllowUserToResizeRows = false;
			//this._dataGridActiveProcesses.BackgroundColor = System.Drawing.SystemColors.ControlDark;
			this._dataGridActiveProcesses.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._dataGridActiveProcesses.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenVertical;
			this._dataGridActiveProcesses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dataGridActiveProcesses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridColumnProcessName,
            this.dataGridColumnProcessId});
			this._dataGridActiveProcesses.Location = new System.Drawing.Point(12, 12);
			this._dataGridActiveProcesses.Name = "_dataGridActiveProcesses";
			this._dataGridActiveProcesses.ReadOnly = true;
			this._dataGridActiveProcesses.Size = new System.Drawing.Size(743, 396);
			this._dataGridActiveProcesses.TabIndex = 1;

			this.AutoSize = true;
			// 
			// dataGridColumnProcessName
			// 
			this.dataGridColumnProcessName.HeaderText = "ProcessName";
			this.dataGridColumnProcessName.Name = "dataGridColumnProcessName";
			this.dataGridColumnProcessName.ReadOnly = true;
			this.dataGridColumnProcessName.Width = 600;
			// 
			// dataGridColumnProcessId
			// 
			this.dataGridColumnProcessId.HeaderText = "ProcessId";
			this.dataGridColumnProcessId.Name = "dataGridColumnProcessId";
			this.dataGridColumnProcessId.ReadOnly = true;
			// 
			// ActiveProcessForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.ClientSize = new System.Drawing.Size(767, 418);
			this.Controls.Add(this._dataGridActiveProcesses);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.Name = "ActiveProcessForm";
			this.Opacity = 0.8D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "ActiveProcessForm";
			((System.ComponentModel.ISupportInitialize)(this._dataGridActiveProcesses)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DataGridView _dataGridActiveProcesses;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridColumnProcessName;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridColumnProcessId;
	}
}