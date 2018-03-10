using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace LineageMTool.Spazzarama.ScreenCapture
{
	[SuppressUnmanagedCodeSecurity]
	internal sealed class NativeMethods
	{
		public NativeMethods()
		{
		}

		internal static Rectangle GetAbsoluteClientRect(IntPtr hWnd)
		{
			Rectangle windowRect = LineageMTool.Spazzarama.ScreenCapture.NativeMethods.GetWindowRect(hWnd);
			Rectangle clientRect = LineageMTool.Spazzarama.ScreenCapture.NativeMethods.GetClientRect(hWnd);
			int width = (windowRect.Width - clientRect.Width) / 2;
			return new Rectangle(new Point(windowRect.X + width, windowRect.Y + (windowRect.Height - clientRect.Height - width)), clientRect.Size);
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

		internal static Rectangle GetClientRect(IntPtr hwnd)
		{
			RECT rECT = new RECT();
			LineageMTool.Spazzarama.ScreenCapture.NativeMethods.GetClientRect(hwnd, out rECT);
			return rECT.AsRectangle;
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		internal static Rectangle GetWindowRect(IntPtr hwnd)
		{
			RECT rECT = new RECT();
			LineageMTool.Spazzarama.ScreenCapture.NativeMethods.GetWindowRect(hwnd, out rECT);
			return rECT.AsRectangle;
		}
	}
}