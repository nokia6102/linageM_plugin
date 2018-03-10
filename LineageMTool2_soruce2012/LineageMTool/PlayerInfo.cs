using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

namespace LineageMTool
{
	public class PlayerInfo
	{
		private RoleState _state;

		private SimulatorInfo _simulatorInfo;

		private Stopwatch _stopwatchSpacialError = new Stopwatch();

		private Config _config;

		public int Hp
		{
			get;
			set;
		}

		public int Mp
		{
			get;
			set;
		}

		public RoleState State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
				switch (value)
				{
					case RoleState.Normal:
					case RoleState.Detoxification:
					{
						this._stopwatchSpacialError.Stop();
						return;
					}
					case RoleState.Error:
					case RoleState.Die:
					case RoleState.OutOfArrow:
					case RoleState.BackHome:
					{
						if (!this._stopwatchSpacialError.IsRunning)
						{
							this._stopwatchSpacialError.Start();
						}
						if (this._stopwatchSpacialError.Elapsed.TotalMinutes > 5)
						{
							this._stopwatchSpacialError.Restart();
							(new LineMessage()).SendMessageToLine(this._config.Uid, this.GetRoleStateMessage(), this._simulatorInfo.GetImage((CaptureMode)this._config.comboBoxCaptureSettingSelectIndex));
						}
						return;
					}
					default:
					{
						return;
					}
				}
			}
		}

		public PlayerInfo(Config config, SimulatorInfo simulatorInfo)
		{
			this._config = config;
			this._simulatorInfo = simulatorInfo;
		}

		public void CalculateHpPercent(Image image)
		{
			List<Color> colors = new List<Color>();
			int num = int.Parse(this._config.HpRect.Left);
			int num1 = int.Parse(this._config.HpRect.Right);
			int num2 = int.Parse(this._config.HpRect.Top);
			int num3 = int.Parse(this._config.HpRect.Down);
			int num4 = 0;
			using (Bitmap bitmap = new Bitmap(image))
			{
				for (int i = num; i < num1; i++)
				{
					int r = 0;
					int g = 0;
					int b = 0;
					for (int j = num2; j < num3; j++)
					{
						Color pixel = bitmap.GetPixel(i, j);
						g += pixel.G;
						b += pixel.B;
						r += pixel.R;
					}
					r = r / (num3 - num2);
					g = g / (num3 - num2);
					b = b / (num3 - num2);
					if (r > 100 && g < 50 && b < 50)
					{
						this.State = RoleState.Detoxification;
						colors.Add(Color.FromArgb(r, g, b));
						num4--;
					}
					else if (r < 20 && g > 50 && b < 20)
					{
						colors.Add(Color.FromArgb(r, g, b));
						num4++;
					}
				}
			}
			if (num4 >= 0)
			{
				this.State = RoleState.Detoxification;
			}
			else
			{
				this.State = RoleState.Normal;
			}
			int count = colors.Count * 100 / (num1 - num);
			this.Hp = count;
		}

		public void CalculateMpPercent(Image image)
		{
			List<Color> colors = new List<Color>();
			int num = int.Parse(this._config.MpRect.Left);
			int num1 = int.Parse(this._config.MpRect.Right);
			int num2 = int.Parse(this._config.MpRect.Top);
			int num3 = int.Parse(this._config.MpRect.Down);
			using (Bitmap bitmap = new Bitmap(image))
			{
				for (int i = num; i < num1; i++)
				{
					int r = 0;
					int g = 0;
					int b = 0;
					for (int j = num2; j < num3; j++)
					{
						Color pixel = bitmap.GetPixel(i, j);
						r += pixel.R;
						g += pixel.G;
						b += pixel.B;
					}
					r = r / (num3 - num2);
					g = g / (num3 - num2);
					b = b / (num3 - num2);
					if (b > 100 && r < 20)
					{
						colors.Add(Color.FromArgb(r, g, b));
					}
				}
			}
			int count = colors.Count * 100 / (num1 - num);
			this.Mp = count;
		}

		private int GetGrayNumColor(Color posClr)
		{
			return posClr.R * 19595 + posClr.G * 38469 + posClr.B * 7472 >> 16;
		}

		private int GetMoney(List<Bitmap> splitImageList)
		{
			return 0;
		}

		private Bitmap GetMoneyArea(Image image)
		{
			Bitmap bitmap = new Bitmap(87, 21, PixelFormat.Format24bppRgb);
			Bitmap bitmap1 = new Bitmap(image);
			for (int i = 613; i <= 699; i++)
			{
				for (int j = 60; j <= 80; j++)
				{
					Color pixel = bitmap1.GetPixel(i, j);
					bitmap.SetPixel(i - 613, j - 60, pixel);
				}
			}
			return bitmap;
		}

		public string GetRoleStateMessage()
		{
			switch (this.State)
			{
				case RoleState.Error:
				{
					return "外掛發生錯誤";
				}
				case RoleState.Die:
				{
					return "角色死亡";
				}
				case RoleState.OutOfArrow:
				{
					return "箭矢用完，使用回捲回村";
				}
				case RoleState.BackHome:
				{
					return "使用回捲回村";
				}
				case RoleState.Detoxification:
				{
					return "角色中毒，嘗試使用解毒藥水";
				}
			}
			return "外掛正常運行中";
		}

		private bool IsArrowRunOut(Image image)
		{
			return false;
		}

		private List<Bitmap> SplitImg(Bitmap image)
		{
			List<Bitmap> bitmaps = new List<Bitmap>();
			List<int> nums = new List<int>();
			for (int i = 0; i < image.Width; i++)
			{
				int num = 0;
				for (int j = 0; j < image.Height; j++)
				{
					if (image.GetPixel(i, j).R > 180)
					{
						num++;
					}
				}
				nums.Add(num);
			}
			bool flag = false;
			int num1 = 0;
			int num2 = 0;
			for (int k = 0; k < image.Width; k++)
			{
				if (!flag && nums[k] > 0)
				{
					flag = true;
					num1 = k;
				}
				if (flag && nums[k] == 0)
				{
					flag = false;
					num2 = k;
					Bitmap bitmap = new Bitmap(num2 - num1 + 1, image.Height, PixelFormat.Format24bppRgb);
					int num3 = num1;
					while (k <= num2)
					{
						for (int l = 0; l < image.Height; l++)
						{
							bitmap.SetPixel(num3 - num1, l, image.GetPixel(num3, l));
						}
						k++;
					}
					bitmaps.Add(bitmap);
				}
			}
			return bitmaps;
		}

		private Bitmap TranslateToGrayImage(Bitmap image)
		{
			Bitmap bitmap = new Bitmap(image);
			for (int i = 0; i < image.Width; i++)
			{
				for (int j = 0; j < image.Height; j++)
				{
					int grayNumColor = this.GetGrayNumColor(bitmap.GetPixel(i, j));
					bitmap.SetPixel(i, j, Color.FromArgb(grayNumColor, grayNumColor, grayNumColor));
				}
			}
			return bitmap;
		}
	}
}