using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ScreenShotDemo
{
	public class ScreenCapture
	{
		private static Image img;

		static ScreenCapture()
		{
		}

		public ScreenCapture()
		{
		}

		public Image CaptureScreen()
		{
			return this.CaptureWindow(ScreenCapture.User32.GetDesktopWindow());
		}

		public void CaptureScreenToFile(string filename, ImageFormat format)
		{
			this.CaptureScreen().Save(filename, format);
		}

		public Image CaptureWindow(IntPtr handle)
		{
			ScreenCapture.img = null;
			try
			{
				IntPtr windowDC = ScreenCapture.User32.GetWindowDC(handle);
				ScreenCapture.User32.RECT rECT = new ScreenCapture.User32.RECT();
				ScreenCapture.User32.GetWindowRect(handle, ref rECT);
				int num = rECT.right - rECT.left;
				int num1 = rECT.bottom - rECT.top;
				IntPtr intPtr = ScreenCapture.GDI32.CreateCompatibleDC(windowDC);
				IntPtr intPtr1 = ScreenCapture.GDI32.CreateCompatibleBitmap(windowDC, num, num1);
				IntPtr intPtr2 = ScreenCapture.GDI32.SelectObject(intPtr, intPtr1);
				ScreenCapture.GDI32.BitBlt(intPtr, 0, 0, num, num1, windowDC, 0, 0, 13369376);
				ScreenCapture.GDI32.SelectObject(intPtr, intPtr2);
				ScreenCapture.GDI32.DeleteDC(intPtr);
				ScreenCapture.User32.ReleaseDC(handle, windowDC);
				ScreenCapture.img = Image.FromHbitmap(intPtr1);
				ScreenCapture.GDI32.DeleteObject(intPtr1);
			}
			finally
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			return ScreenCapture.img;
		}

		public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
		{
			this.CaptureWindow(handle).Save(filename, format);
		}

		private class GDI32
		{
			public const int SRCCOPY = 13369376;

			public GDI32()
			{
			}

			[DllImport("gdi32.dll", CharSet=CharSet.None, ExactSpelling=false)]
			public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);

			[DllImport("gdi32.dll", CharSet=CharSet.None, ExactSpelling=false)]
			public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

			[DllImport("gdi32.dll", CharSet=CharSet.None, ExactSpelling=false)]
			public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

			[DllImport("gdi32.dll", CharSet=CharSet.None, ExactSpelling=false)]
			public static extern bool DeleteDC(IntPtr hDC);

			[DllImport("gdi32.dll", CharSet=CharSet.None, ExactSpelling=false)]
			public static extern bool DeleteObject(IntPtr hObject);

			[DllImport("gdi32.dll", CharSet=CharSet.None, ExactSpelling=false)]
			public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
		}

		private class User32
		{
			public User32()
			{
			}

			[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
			public static extern IntPtr GetDesktopWindow();

			[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
			public static extern IntPtr GetWindowDC(IntPtr hWnd);

			[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
			public static extern IntPtr GetWindowRect(IntPtr hWnd, ref ScreenCapture.User32.RECT rect);

			[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
			public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

			public struct RECT
			{
				public int left;

				public int top;

				public int right;

				public int bottom;
			}
		}
	}
}