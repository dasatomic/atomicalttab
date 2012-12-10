using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AtomicAltTabl
{
	/// <summary>
	/// Main form that contains only search box.
	/// On every pressed key we search all the open windows based on the pattern
	/// extracted from the aforementined textbox.
	/// </summary>
	public partial class AtomicAltTabForm : Form
	{
		/// <summary>
		/// The brain behind the form.
		/// </summary>
		private readonly WindowManager windowManager = new WindowManager();

		/// <summary>
		/// Form that shows all the windows that match given pattern.
		/// </summary>
		private readonly ActiveProcessForm activeProcessForm = new ActiveProcessForm();

		private readonly StartupTutorial tutorialForm = new StartupTutorial();

		/// <summary>
		/// Global hot key hook.
		/// </summary>
		private readonly GlobalHotkey globalKeyHook;

		/// <summary>
		/// Const that hides the system menu.
		/// </summary>
		private const int WS_SYSMENU = 0x80000;

		private bool textBoxShowsTip = false;

		private const string UsageTipKeyCombination = "Press CTRL + ALT + A to switch to AtomicAltBox";

		private readonly string[] UsageTips = new[]
		{
			"Press Up/Down arrow keys or Alt + W/Alt + S to move through the available programs in the grid",
			"Press Alt + C and selected window will be closed",
			"Press Enter at any time and selected window will gain focus"
		};

		/// <summary>
		/// Initialize the component, put it in the upper left corner and register the hot key.
		/// </summary>
		public AtomicAltTabForm()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point(50, 50);
			activeProcessForm.Visible = false;
			this._textboxProcessInput.HideSelection = false;

			// TODO: Make the hotkey configurable (XML file?)
			globalKeyHook = new GlobalHotkey(Constants.ALT | Constants.CTRL, Keys.A, this);
			globalKeyHook.Register();

			// Make the form transparent.
			//
			this.TransparencyKey = Color.Turquoise;
			this.BackColor = Color.Turquoise;

			// Perform autostart check.
			//
			AutoStartCheck();

			// Show tip.
			//
			SetTextBoxTipText(UsageTipKeyCombination);
		}

		/// <summary>
		/// Method that puts auto start registry key to the proper value
		/// so the program always starts automatically with windows.
		/// </summary>
		/// <returns></returns>
		private void AutoStartCheck()
		{
			// The path to the key where Windows looks for startup applications.
			//
			RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

			// Add current path to the registry.
			//
			rkApp.SetValue("AtomicAltTab", Application.ExecutablePath);
		}
		
		/// <summary>
		/// Show tutorial window if needed.
		/// </summary>
		private void ShowTutorial()
		{
			// We will just print readme file in the dialog box.
			//
			Point tutorialFormLocation = new Point(Location.X, Location.Y);
			tutorialFormLocation.Y += this.Height + 20;
			tutorialForm.Location = tutorialFormLocation;

			tutorialForm.StartPosition = FormStartPosition.Manual;

			tutorialForm.Show();
		}
		

		/// <summary>
		/// Override the CreateParams in order to hide SYSMENU and remove the program from alt-tab choices.
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.Style &= ~WS_SYSMENU;
				cp.ExStyle |= 0x80; 
				return cp;
			}
		}

		/// <summary>
		/// When hotkey combination is pressed we clear the textbox and put form in the focus.
		/// </summary>
		private void HandleHotkey()
		{
			SetTextBoxTipText(UsageTipKeyCombination);
			this.Visible = true;
			this._textboxProcessInput.Focus();
			this.Activate();
		}

		/// <summary>
		/// Method that handles hotkey's combination.
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
			{
				HandleHotkey();
			}

			base.WndProc(ref m);
		}

		private void SetTextBoxTipText(string text)
		{
			_textboxProcessInput.Text = string.Format("Tip: ({0})", text);
			textBoxShowsTip = true;
			_textboxProcessInput.SelectionLength = 0;
		}

		private void SetTextBoxTipText()
		{
			Random gen = new Random();

			if (gen.NextDouble() < 0.5)
			{
				// Show default key combination tip.
				//
				SetTextBoxTipText(UsageTipKeyCombination);
			}
			else
			{
				// Pick a random text to show.
				//
				SetTextBoxTipText(UsageTips[gen.Next(UsageTips.Length)]);
			}
		}

		private void HandleNewTypedCharacter(object sender, KeyPressEventArgs e)
		{
			if (char.IsLetterOrDigit(e.KeyChar))
			{
				// Refresh the active process form with newly typed pattern.
				//
				List<WindowDescriptor> windowDescriptors = windowManager.GetOpenWindows(_textboxProcessInput.Text);
				activeProcessForm.RefreshList(windowDescriptors);
			}
				
			if (_textboxProcessInput.Text.Length == 1)
			{
				// This is the first input character so we refetch input list and draw the active process form.
				//
				windowManager.RefreshOpenWidnowsList();
				windowManager.RefreshProcessList();

				// Draw the active process form next to the text box.
				//
				Point processInfoLocation = new Point(Location.X, Location.Y);
				processInfoLocation.X += Size.Width;
				activeProcessForm.Visible = true;
				activeProcessForm.Location = processInfoLocation;

				this.Focus();
				this._textboxProcessInput.SelectionLength = 0;
				this._textboxProcessInput.SelectionStart = this._textboxProcessInput.Text.Length;
			}
		}

		/// <summary>
		/// TextBox key pressed handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TextboxProcessInputTextChanged(object sender, KeyEventArgs e)
		{
			if (textBoxShowsTip)
			{
				this._textboxProcessInput.Text = string.Empty;
				textBoxShowsTip = false;
			}
			else if (string.IsNullOrEmpty(_textboxProcessInput.Text))
			{
				// We don't have anything to show, hide the active process form.
				//
				activeProcessForm.Visible = false;
			}
			else
			{
				if (e.KeyCode == Keys.Down || (e.KeyCode == Keys.S && e.Modifiers == Keys.Alt))
				{
					// We increment selected item in the grid.
					//
					activeProcessForm.ClearSelected();
					activeProcessForm.IncrementSelected();

					// Put the currently selected window to the from but keep the focus on the AtomicAltTab.
					//
					activeProcessForm.FocusSelected();
					this.Visible = true;
					this._textboxProcessInput.Focus();
					this.Activate();
					activeProcessForm.Activate();
					activeProcessForm.Focus();
					this.Focus();
				}
				else if (e.KeyCode == Keys.Up || (e.KeyCode == Keys.W && e.Modifiers == Keys.Alt))
				{
					// We decrement selected item in the grid.
					//
					activeProcessForm.ClearSelected();
					activeProcessForm.DecrementSelected();

					// Put the currently selected window to the from but keep the focus on the AtomicAltTab.
					//
					activeProcessForm.FocusSelected();
					this.Visible = true;
					this._textboxProcessInput.Focus();
					this.Activate();
					activeProcessForm.Activate();
					activeProcessForm.Focus();
					this.Focus();
				}
				else if (e.KeyCode == Keys.Enter)
				{
					// Put selected window to the focus and hide the AtomicAltTab form.
					//
					activeProcessForm.FocusSelected();

					activeProcessForm.Visible = false;
					this.Visible = false;
				}
				else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Alt)
				{
					// Close the selected window.
					//
					activeProcessForm.CloseSelectedWindow();
					activeProcessForm.ClearSelected();

					// Refresh the list.
					//
					windowManager.RefreshOpenWidnowsList();
					windowManager.RefreshProcessList();

					List<WindowDescriptor> windowDescriptors = windowManager.GetOpenWindows(_textboxProcessInput.Text);
					activeProcessForm.RefreshList(windowDescriptors);
				}
			}
		}
	}
}
