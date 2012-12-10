using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AtomicAltTabl
{
	/// <summary>
	/// Class that represents a single window and it's parrent process.
	/// </summary>
	public class WindowDescriptor
	{
		public string ProcessDescription { get; set; }
		public int WindowHandleId { get; set; }
		public int ProcessId { get; set; }
	}

	/// <summary>
	/// Main window handler class.
	/// WindowManager provides interfaces which allow fetching window and process collections
	/// and searching for a wanted pattern in the window lists.
	/// Also this class encapsulates some of the window API calls used for window manipulation.
	/// </summary>
	class WindowManager
	{
		/// <summary>
		/// List of all open windows.
		/// </summary>
		private List<Window> _openWindows = new List<Window>();

		/// <summary>
		/// List of all active processes.
		/// </summary>
		private Dictionary<int, string> _processList = new Dictionary<int, string>(); 

		/// <summary>
		/// Refreshes the list of windows.
		/// </summary>
		public void RefreshOpenWidnowsList()
		{
			Windows windowsCollection = new Windows(false, false);

			_openWindows = windowsCollection.Cast<Window>().ToList();
		}

		/// <summary>
		/// Refresh the list of active processes excluding the AtomicAltTab process.
		/// </summary>
		public void RefreshProcessList()
		{
			_processList = 
				Process.GetProcesses()
				.Where(p => p.Id != 0)
				.ToDictionary(k => k.Id, p => p.ProcessName);
		}

		/// <summary>
		/// Returns list of open windows that satisfies given pattern.
		/// </summary>
		/// <param name="pattern">Pattern that open windows has to satisfy in either process name or windows title.</param>
		/// <returns>List of all windows that satisfy given pattern.</returns>
		public List<WindowDescriptor> GetOpenWindows(string pattern)
		{
			// A function that returns process id for given window handle.
			//
			Func<int, int> getProcId = handle =>
			{
				uint procId;
				GetWindowThreadProcessId(new IntPtr(handle), out procId);
				return (int)procId;
			};

			// If open window list is null or empty we have to refresh our collections.
			//
			if (_openWindows == null || !_openWindows.Any())
			{
				RefreshOpenWidnowsList();
				RefreshProcessList();
			}

			// Now we return all the windows that satisfy given pattern.
			//
			return _openWindows
				.Where(window => _processList.ContainsKey(getProcId(window.hWnd.ToInt32())))
				.Select(window => new WindowDescriptor()
					{
						ProcessDescription = string.Format("{0} => {1}", _processList[getProcId(window.hWnd.ToInt32())], window.Title),
						ProcessId = getProcId(window.hWnd.ToInt32()),
						WindowHandleId = window.hWnd.ToInt32()
					})
				.Where(windowDescriptor => pattern.ToLower().Split(null).All(ptr => windowDescriptor.ProcessDescription.ToLower().Contains(ptr)))
				.Where(windowDescriptor => windowDescriptor.ProcessId != Process.GetCurrentProcess().Id)
				.OrderBy(windowDescriptor => windowDescriptor.ProcessDescription)
				.ToList();
		}

		#region STATIC_METHODS
		/// <summary>
		/// Set given window to the foreground.
		/// </summary>
		/// <param name="windowHandle">Window handle.</param>
		public static void SetWindowToForeGround(int windowHandle)
		{
			// We first need to check if process is minimized.
			//
			WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
			GetWindowPlacement(new IntPtr(windowHandle), ref placement);
			const int minimizedFlag = 2;
			const int restoreWindowFlag = 9;

			if (placement.showCmd == minimizedFlag)
			{
				// Window is minimized we need to restore it first.
				//
				ShowWindowAsync(windowHandle, restoreWindowFlag);
			}

			SetForegroundWindow(windowHandle);
		}
		#endregion

		#region WINAPI IMPORTS
		[DllImport("User32.dll")]
		public static extern Int32 SetForegroundWindow(int hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		[DllImport("user32.dll")]
		public static extern bool CloseWindow(IntPtr hWnd);

		// Destroy selected window.
		//
		[DllImport("user32.dll")]
		public static extern bool DestroyWindow(IntPtr hWnd);

		[DllImport("User32.dll", EntryPoint = "SendMessage")]
		public static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

		[DllImport("User32.dll", EntryPoint = "PostMessage")]
		public static extern int PostMessage(int hWnd, int Msg, int wParam, int lParam);

		[DllImport("User32.dll")]
		public static extern bool ShowWindowAsync(int hWnd, int nCmdShow);

		private struct WINDOWPLACEMENT
		{
			public int length;
			public int flags;
			public int showCmd;
			public System.Drawing.Point ptMinPosition;
			public System.Drawing.Point ptMaxPosition;
			public System.Drawing.Rectangle rcNormalPosition;
		}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		#endregion
	}
}
