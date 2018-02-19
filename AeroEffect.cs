using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Drawing;
using Microsoft.Win32;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Theme.WPF
{
    /// <summary>
    /// Helpmethods for OS Version checks
    /// </summary>
    /// 

    public class Aero
    {
        public static void AutoApply(Window sender)
        {
            if (VersionHelper.IsWindows10orHigher)
            {
                Int16 CurrentBuildNumber = 0;
                Int16.TryParse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber", "").ToString(), out CurrentBuildNumber);
                //MainWindow.main.WindowLable.Content = CurrentBuildNumber;

                if (CurrentBuildNumber >= 16353)
                {

                }
                else
                {
                    //MainWindow.main.WindowLable.Content = "WIN 10 ENB BLUR";
                    AeroEffect.Win10.EnableBlur(sender);
                }
            }
            else
            {
                AeroEffect.Win7.EnableBlur(sender);
            }
        }
    }

        public class VersionHelper
    {
        /// <summary>
        /// OS is at least Windows Vista
        /// </summary>
        public static bool IsAtLeastVista
        {
            get
            {
                if (Environment.OSVersion.Version.Major < 6)
                {
                    //Debug.WriteLine("How about trying this on Vista?");
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// OS is Windows 7 or higher
        /// </summary>
        /// 
        public static bool IsWindows10orHigher
        {
            get
            {
                //MainWindow.main.WindowLable.Content = Environment.OSVersion.Version.Minor;

                if (Environment.OSVersion.Version.Major == 6 &&
                    Environment.OSVersion.Version.Minor >= 2)
                {
                    return true;
                }
                else if (Environment.OSVersion.Version.Major > 6)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool IsWindows7orHigher
        {
            get
            {
                if (Environment.OSVersion.Version.Major == 6 &&
                    Environment.OSVersion.Version.Minor >= 1)
                {
                    return true;
                }
                else if (Environment.OSVersion.Version.Major > 6)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public static class GlassEffectHelper
    {

        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern bool DwmIsCompositionEnabled();

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public MARGINS(int a)
            {
                cxLeftWidth = a;
                cxRightWidth = a;
                cyTopHeight = a;
                cyBottomHeight = a;
            }
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        public static bool EnableGlassEffect(Window window)
        {
            window.MouseLeftButtonDown += (s, e) =>
            {
                window.DragMove();
            };
            return EnableGlassEffect(window, true);
        }

        public static bool EnableGlassEffect(Window window, bool enabled)
        {
            return EnableGlassEffect(window, enabled, -1);
        }

        public static bool EnableGlassEffect(Window window, bool enabled, Int32 margin)
        {
            if (!VersionHelper.IsAtLeastVista)
            {
                // Go and buy Windows 7
                return false;
            }

            if (!DwmIsCompositionEnabled())
            {
                return false;
            }

            if (enabled)
            {
                IntPtr hwnd = new WindowInteropHelper(window).Handle;

                // Hintergrundfarbe von Fenster Transparent darstellen
                window.Background = System.Windows.Media.Brushes.Transparent;

                // Die Farbe festlegen auf die den Glaseffekt bekommt
                HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor =
                    Colors.Transparent;

                // Den Bereich für den Glaseffekt definieren
                MARGINS margins = new MARGINS(margin);

                // Glasseffekt aktivieren
                DwmExtendFrameIntoClientArea(hwnd, ref margins);
            }
            else
            {
                // Hintergrundfarbe des Fensters zurück auf die
                // Systemfarbe stellen
                window.Background = System.Windows.SystemColors.WindowBrush;
            }

            return true;
        }

    }
    public static class AeroEffect
    {
        public static class Win7
        {
            [DllImport("DwmApi.dll")]
            public static extern int DwmExtendFrameIntoClientArea(
                                                    IntPtr hwnd,
                                                    ref Margins pMarInset);

            [StructLayout(LayoutKind.Sequential)]
            public struct Margins
            {
                public int cxLeftWidth;
                public int cxRightWidth;
                public int cyTopHeight;
                public int cyBottomHeight;
            }
            public static Margins GetDpiAdjustedMargins(IntPtr windowHandle, int left, int right, int top, int bottom)
            {
                // Получить Dpi системы
                System.Drawing.Graphics desktop = System.Drawing.Graphics.FromHwnd(windowHandle);
                float DesktopDpiX = desktop.DpiX;
                float DesktopDpiY = desktop.DpiY;

                // Установка полей
                AeroEffect.Win7.Margins margins = new AeroEffect.Win7.Margins();

                margins.cxLeftWidth = Convert.ToInt32(left * (DesktopDpiX / 96));
                margins.cxRightWidth = Convert.ToInt32(right * (DesktopDpiX / 96));
                margins.cyTopHeight = Convert.ToInt32(top * (DesktopDpiX / 96));
                margins.cyBottomHeight = Convert.ToInt32(right * (DesktopDpiX / 96));

                return margins;
            }
            public static void EnableBlur(Window win, int left, int right, int top, int bottom)
            {
                // Получение Win32-дескриптора для окна WPF
                WindowInteropHelper windowInterop = new WindowInteropHelper(win);
                IntPtr windowHandle = windowInterop.Handle;
                HwndSource mainWindowSrc = HwndSource.FromHwnd(windowHandle);
                mainWindowSrc.CompositionTarget.BackgroundColor = Colors.Transparent;

                AeroEffect.Win7.Margins margins =
                    AeroEffect.Win7.GetDpiAdjustedMargins(windowHandle, left, right, top, bottom);

                int returnVal = AeroEffect.Win7.DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);

                if (returnVal < 0)
                {
                    throw new NotSupportedException("Operation failed.");
                }
            }

            public static void EnableBlur(Window win)
            {
                try {
                    // Obtain the window handle for WPF application  
                    IntPtr mainWindowPtr = new WindowInteropHelper(win).Handle;
                    HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
                    mainWindowSrc.CompositionTarget.BackgroundColor = System.Windows.Media.Color.FromArgb(0, 0, 0, 0);

                    // Get System Dpi  
                    System.Drawing.Graphics desktop = System.Drawing.Graphics.FromHwnd(mainWindowPtr);
                    float DesktopDpiX = desktop.DpiX;
                    float DesktopDpiY = desktop.DpiY;

                    // Set Margins  
                    AeroEffect.Win7.Margins margins = new AeroEffect.Win7.Margins();

                    // Extend glass frame into client area  
                    // Note that the default desktop Dpi is 96dpi. The  margins are  
                    // adjusted for the system Dpi.  
                    margins.cxLeftWidth = -1; //   Convert.ToInt32(5 * (DesktopDpiX / 96));
                    margins.cxRightWidth = -1; //  Convert.ToInt32(5 * (DesktopDpiX / 96));
                    margins.cyTopHeight = -1; //   Convert.ToInt32(5 * (DesktopDpiX / 96));
                    margins.cyBottomHeight = -1; //Convert.ToInt32(5 * (DesktopDpiX / 96));

                    int hr = AeroEffect.Win7.DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
                    //  
                    if (hr < 0)
                    {
                        //DwmExtendFrameIntoClientArea Failed  
                    }
                }  
                // If not Vista, paint background white.  
                catch (DllNotFoundException)  
                {  
                    Application.Current.MainWindow.Background = System.Windows.Media.Brushes.White;  
                }
            }
        }

        public static class Win10
        {
            internal enum AccentState
            {
                ACCENT_DISABLED = 0,
                ACCENT_ENABLE_GRADIENT = 1,
                ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
                ACCENT_ENABLE_BLURBEHIND = 3,
                ACCENT_INVALID_STATE = 4
            }

            [StructLayout(LayoutKind.Sequential)]
            internal struct AccentPolicy
            {
                public AccentState AccentState;
                public int AccentFlags;
                public int GradientColor;
                public int AnimationId;
            }

            [StructLayout(LayoutKind.Sequential)]
            internal struct WindowCompositionAttributeData
            {
                public WindowCompositionAttribute Attribute;
                public IntPtr Data;
                public int SizeOfData;
            }

            internal enum WindowCompositionAttribute
            {
                // ...
                WCA_ACCENT_POLICY = 19
                // ...
            }

            [DllImport("user32.dll")]
            internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

            internal static void EnableBlur(Window w)
            {
                var windowHelper = new WindowInteropHelper(w);

                var accent = new AccentPolicy();
                accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

                var accentStructSize = Marshal.SizeOf(accent);

                var accentPtr = Marshal.AllocHGlobal(accentStructSize);
                Marshal.StructureToPtr(accent, accentPtr, false);

                var data = new WindowCompositionAttributeData();
                data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
                data.SizeOfData = accentStructSize;
                data.Data = accentPtr;

                SetWindowCompositionAttribute(windowHelper.Handle, ref data);

                Marshal.FreeHGlobal(accentPtr);
            }
        }
    }
}
