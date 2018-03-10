using SlimDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LineageMTool.Spazzarama.ScreenCapture
{
	public static class Direct3DCapture
	{
		private static Direct3D _direct3D9;

		private static Dictionary<IntPtr, Device> _direct3DDeviceCache;

		static Direct3DCapture()
		{
			Direct3DCapture._direct3D9 = new Direct3D();
			Direct3DCapture._direct3DDeviceCache = new Dictionary<IntPtr, Device>();
		}

		public static Bitmap CaptureRegionDirect3D(IntPtr handle, Rectangle region)
		{
			Device device;
			IntPtr intPtr = handle;
			Bitmap bitmap = null;
			AdapterInformation defaultAdapter = Direct3DCapture._direct3D9.Adapters.DefaultAdapter;
			if (!Direct3DCapture._direct3DDeviceCache.ContainsKey(intPtr))
			{
				PresentParameters presentParameter = new PresentParameters()
				{
					BackBufferFormat = defaultAdapter.CurrentDisplayMode.Format
				};
				Rectangle absoluteClientRect = LineageMTool.Spazzarama.ScreenCapture.NativeMethods.GetAbsoluteClientRect(intPtr);
				presentParameter.BackBufferHeight = absoluteClientRect.Height;
				presentParameter.BackBufferWidth = absoluteClientRect.Width;
				presentParameter.Multisample = MultisampleType.None;
				presentParameter.SwapEffect = SwapEffect.Discard;
				presentParameter.DeviceWindowHandle = intPtr;
				presentParameter.PresentationInterval = PresentInterval.Default;
				presentParameter.FullScreenRefreshRateInHertz = 0;
				device = new Device(Direct3DCapture._direct3D9, defaultAdapter.Adapter, DeviceType.Hardware, intPtr, CreateFlags.SoftwareVertexProcessing, new PresentParameters[] { presentParameter });
				Direct3DCapture._direct3DDeviceCache.Add(intPtr, device);
			}
			else
			{
				device = Direct3DCapture._direct3DDeviceCache[intPtr];
			}
			int width = defaultAdapter.CurrentDisplayMode.Width;
			DisplayMode currentDisplayMode = defaultAdapter.CurrentDisplayMode;
			using (Surface surface = Surface.CreateOffscreenPlain(device, width, currentDisplayMode.Height, Format.A8R8G8B8, Pool.SystemMemory))
			{
				device.GetFrontBufferData(0, surface);
				bitmap = new Bitmap(Surface.ToStream(surface, ImageFileFormat.Bmp, new Rectangle(region.Left, region.Top, region.Width, region.Height)));
			}
			return bitmap;
		}

		public static Bitmap CaptureWindow(IntPtr hWnd)
		{
			return Direct3DCapture.CaptureRegionDirect3D(hWnd, LineageMTool.Spazzarama.ScreenCapture.NativeMethods.GetAbsoluteClientRect(hWnd));
		}
	}
}