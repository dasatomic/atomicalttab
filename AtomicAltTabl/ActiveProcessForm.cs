using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtomicAltTabl
{
	internal class CustomGridTransparendBackground : DataGridView
	{
		//protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
		//{
		//	base.PaintBackground(graphics, clipBounds, gridBounds);
		//	Rectangle rectSource = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
		//	Rectangle rectDest = new Rectangle(0, 0, rectSource.Width, rectSource.Height);

		//	Bitmap b = new Bitmap(Parent.ClientRectangle.Width, Parent.ClientRectangle.Height);
		//	Graphics.FromImage(b).DrawImage(this.Parent.BackgroundImage, Parent.ClientRectangle);


		//	graphics.DrawImage(b, rectDest, rectSource, GraphicsUnit.Pixel);
		//	//SetCellsTransparent();
		//}

		//public void SetCellsTransparent()
		//{
		//	this.EnableHeadersVisualStyles = false;
		//	this.ColumnHeadersDefaultCellStyle.BackColor = Color.Transparent;
		//	this.RowHeadersDefaultCellStyle.BackColor = Color.Transparent;


		//	//foreach (DataGridViewColumn col in this.Columns)
		//	//{
		//	//	col.DefaultCellStyle.BackColor = Color.Transparent;
		//	//	col.DefaultCellStyle.SelectionBackColor = Color.Transparent;
		//	//}
		//}
	}

	public partial class ActiveProcessForm : Form
	{
		/// <summary>
		/// Save the currently selected element.
		/// </summary>
		private int _selectedGridItem = 0;

		/// <summary>
		/// SYSMENU override used to remove the menu ribbon.
		/// </summary>
		private const int WS_SYSMENU = 0x80000;

		/// <summary>
		/// Process close constant.
		/// </summary>
		private const int WM_CLOSE = 0x0010;

		/// <summary>
		/// In memory grid structure.
		/// </summary>
		private List<WindowDescriptor> _privateGrid;

        protected override bool ShowWithoutActivation
        {
            get { return false; }
        }

		/// <summary>
		/// Active the form.
		/// </summary>
		public ActiveProcessForm()
		{
			InitializeComponent();

			// Make the form transparent.
			//
			this.TransparencyKey = Color.Turquoise;
			this.BackColor = Color.Turquoise;

			//this.BackgroundImage = System.Drawing.Image.FromFile("transparent.gif");
		}

		/// <summary>
		/// Override create params in order to remove SYSMENU.
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.Style &= ~WS_SYSMENU;

                //cp.ExStyle |= (int)(
                //Microsoft.Win32.ExtendedWindowStyles.WS_EX_NOACTIVATE |
                //Microsoft.Win32.ExtendedWindowStyles.WS_EX_TOOLWINDOW);
				return cp;
			}
		}

		/// <summary>
		/// Update the grid with the newest window descriptors.
		/// </summary>
		/// <param name="refreshItems"></param>
		public void RefreshList(List<WindowDescriptor> refreshItems)
		{
			_privateGrid = refreshItems;

			_dataGridActiveProcesses.Rows.Clear();
			refreshItems.ForEach(windowDescriptor => 
				_dataGridActiveProcesses.Rows.Add(windowDescriptor.ProcessDescription, windowDescriptor.ProcessId));

			if (refreshItems.Any())
			{
				_selectedGridItem = 0;
				_dataGridActiveProcesses.Rows[_selectedGridItem].Selected = true;
			}

			// Update the size based on the number of rows.
			//
			int totalSize = 
				_dataGridActiveProcesses.ColumnHeadersHeight + _dataGridActiveProcesses.Rows.Cast<DataGridViewRow>().Sum(row => row.Height + 1);

			_dataGridActiveProcesses.Height = totalSize;
		}

		public void ClearSelected()
		{
			foreach (DataGridViewRow item in _dataGridActiveProcesses.Rows)
			{
				item.Selected = false;
			}
		}

		/// <summary>
		/// Put focus to the selected item in the grid.
		/// </summary>
		public void FocusSelected()
		{
			int counter = 0;

			foreach (DataGridViewRow item in _dataGridActiveProcesses.Rows)
			{
				if (item.Selected)
				{
					WindowManager.SetWindowToForeGround(_privateGrid[counter].WindowHandleId);
				}

				counter++;
			}
		}

		/// <summary>
		/// Increment selected item in the grid.
		/// Fired on the key up.
		/// </summary>
		public void IncrementSelected()
		{
			if (_dataGridActiveProcesses.Rows.Count == 0)
			{
				return;
			}

			_selectedGridItem = (_selectedGridItem + 1)%_dataGridActiveProcesses.Rows.Count;
			_dataGridActiveProcesses.Rows[_selectedGridItem].Selected = true;
		}

		/// <summary>
		/// Decrement the selected item in the grid.
		/// Fired on the key down.
		/// </summary>
		public void DecrementSelected()
		{
			if (_dataGridActiveProcesses.Rows.Count == 0)
			{
				return;
			}

			_selectedGridItem--;

			if (_selectedGridItem < 0)
			{
				_selectedGridItem = _dataGridActiveProcesses.Rows.Count - 1;
			}

			_dataGridActiveProcesses.Rows[_selectedGridItem].Selected = true;
		}

		/// <summary>
		/// Close selected window.
		/// </summary>
		public void CloseSelectedWindow()
		{
			IntPtr windowHandle = new IntPtr(_privateGrid[_selectedGridItem].WindowHandleId);
			WindowManager.SendMessage(windowHandle.ToInt32(), WM_CLOSE, 0, 0);
		}
	}
}
