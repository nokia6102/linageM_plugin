using Imgur.API;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using isRock.LineBot;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LineageMTool
{
	internal class LineMessage
	{
		public Action<string> ErrorCallBack;

		public LineMessage()
		{
		}

		private string DownloadImage(int imageId)
		{
			string link;
			try
			{
				TaskAwaiter<IImage> awaiter = (new ImageEndpoint(new ImgurClient("a7d89efd3720246", "f67e11527f51f7dae687900806901fed26b07e42"))).GetImageAsync("IMAGE_ID").GetAwaiter();
				link = awaiter.GetResult().Link;
			}
			catch (ImgurException imgurException)
			{
				link = string.Empty;
			}
			return link;
		}

		public void SendMessageToLine(string uid, string message, Image image)
		{
			try
			{
				Utility.PushMessage(uid, message, "XfzPgOG9PcPqQj38QNOWkAtpSC8M7K2TJGPe0erfeogRIOr/6Xh5Hdl+CDwt0KUgkd0PvTLQ5ebqCyzYNT9kbJshTDy54NgKZG/9tFaRTQPmWH4x/l7xpGXTWTdLSdLVx9aKtSYvLVFJoUd0vPbfvAdB04t89/1O/w1cDnyilFU=");
				if (image != null)
				{
					string str = this.UploadImage(image);
					if (!string.IsNullOrWhiteSpace(str))
					{
						Task.Run(() => Utility.PushImageMessage(uid, str, str, "XfzPgOG9PcPqQj38QNOWkAtpSC8M7K2TJGPe0erfeogRIOr/6Xh5Hdl+CDwt0KUgkd0PvTLQ5ebqCyzYNT9kbJshTDy54NgKZG/9tFaRTQPmWH4x/l7xpGXTWTdLSdLVx9aKtSYvLVFJoUd0vPbfvAdB04t89/1O/w1cDnyilFU="));
					}
				}
			}
			catch
			{
				Action<string> errorCallBack = this.ErrorCallBack;
				if (errorCallBack != null)
				{
					errorCallBack("請確認Line uid設定正確");
				}
				else
				{
				}
			}
		}

		private string UploadImage(Image gameImage)
		{
			IImage result;
			string link;
			try
			{
				ImageEndpoint imageEndpoint = new ImageEndpoint(new ImgurClient("6ef47358e9c4197", "4803d40aec622afd20d5409696a624c13fcc716e"));
				gameImage.Save("screenCapture.jpg", ImageFormat.Jpeg);
				using (FileStream fileStream = new FileStream("screenCapture.jpg", FileMode.Open))
				{
					TaskAwaiter<IImage> awaiter = imageEndpoint.UploadImageStreamAsync(fileStream, null, null, null).GetAwaiter();
					result = awaiter.GetResult();
				}
				link = result.Link;
			}
			catch (ImgurException imgurException)
			{
				link = string.Empty;
			}
			return link;
		}
	}
}