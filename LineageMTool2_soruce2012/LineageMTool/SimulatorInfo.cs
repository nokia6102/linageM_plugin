using LineageMTool.Spazzarama.ScreenCapture;
using ScreenShotDemo;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LineageMTool
{
	public class SimulatorInfo
	{
		public static string HotKeyList;

		private uint WM_KEYDOWN = 256;

		private uint WM_KEYUP = 257;

		private uint WM_LBUTTONDOWN = 513;

		private uint WM_LBUTTONUP = 514;

		private Config _config;

		public string Name
		{
			get
			{
				return this._config.SimulatorName;
			}
		}

		static SimulatorInfo()
		{
			SimulatorInfo.HotKeyList = "鍵盤7,鍵盤8,鍵盤9,鍵盤0,鍵盤U,鍵盤I,鍵盤O,鍵盤P";
		}

		public SimulatorInfo(Config config)
		{
			this._config = config;
		}

		public Image GetImage(CaptureMode captureMode)
		{
			IntPtr intPtr = WinApi.FindWindow(null, this.Name);
			Image image = null;
			if (captureMode == CaptureMode.DirectX)
			{
				image = Direct3DCapture.CaptureWindow(intPtr);
			}
			else
			{
				if (captureMode != CaptureMode.Gdi)
				{
					throw new NotImplementedException("尚未實作的擷取畫面模式");
				}
				image = (new ScreenCapture()).CaptureWindow(intPtr);
			}
			return image;
		}

		public bool IsSimulatorOpen()
		{
			return WinApi.FindWindow(null, this.Name) != IntPtr.Zero;
		}

		public void SendMessage(string action)
		{
			IntPtr intPtr = WinApi.FindWindow(null, this.Name);
			string[] strArrays = SimulatorInfo.HotKeyList.Split(new char[] { ',' });
			IntPtr zero = IntPtr.Zero;
			zero = (!this.Name.Contains("雷電") ? intPtr : WinApi.FindWindowEx(intPtr, 0, null, null));
			if (action == strArrays[0])
			{
				WinApi.SendMessage(zero, this.WM_KEYDOWN, Convert.ToInt32(Keys.D7), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, this.WM_KEYUP, Convert.ToInt32(Keys.D7), 0);
				return;
			}
			if (action == strArrays[1])
			{
				WinApi.SendMessage(zero, this.WM_KEYDOWN, Convert.ToInt32(Keys.D8), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, this.WM_KEYUP, Convert.ToInt32(Keys.D8), 0);
				return;
			}
			if (action == strArrays[2])
			{
				WinApi.SendMessage(zero, this.WM_KEYDOWN, Convert.ToInt32(Keys.D9), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, this.WM_KEYUP, Convert.ToInt32(Keys.D9), 0);
				return;
			}
			if (action == strArrays[3])
			{
				WinApi.SendMessage(zero, this.WM_KEYDOWN, Convert.ToInt32(Keys.D0), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, this.WM_KEYUP, Convert.ToInt32(Keys.D0), 0);
				return;
			}
			if (action == strArrays[4])
			{
				WinApi.SendMessage(zero, this.WM_KEYDOWN, Convert.ToInt32(Keys.U), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, this.WM_KEYUP, Convert.ToInt32(Keys.U), 0);
				return;
			}
			if (action == strArrays[5])
			{
				WinApi.SendMessage(zero, this.WM_KEYDOWN, Convert.ToInt32(Keys.I), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, this.WM_KEYUP, Convert.ToInt32(Keys.I), 0);
				return;
			}
			if (action == strArrays[6])
			{
				WinApi.SendMessage(zero, this.WM_KEYDOWN, Convert.ToInt32(Keys.O), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, this.WM_KEYUP, Convert.ToInt32(Keys.O), 0);
				return;
			}
			if (action == strArrays[7])
			{
				WinApi.SendMessage(zero, this.WM_KEYDOWN, Convert.ToInt32(Keys.P), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, this.WM_KEYUP, Convert.ToInt32(Keys.P), 0);
			}
		}
	}
}