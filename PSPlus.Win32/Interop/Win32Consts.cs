using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSPlus.Win32.Interop
{
    public static class Win32Consts
    {
        // GetWindowLong / SetWindowLong
        public const int GWL_WNDPROC = -4;
        public const int GWL_HINSTANCE = -6;
        public const int GWL_HWNDPARENT = -8;
        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;
        public const int GWL_USERDATA = -21;
        public const int GWL_ID = -12;
        public const int GWLP_WNDPROC = -4;
        public const int GWLP_HINSTANCE = -6;
        public const int GWLP_HWNDPARENT = -8;
        public const int GWLP_USERDATA = -21;
        public const int GWLP_ID = -12;

        // GetWindow
        public const uint GW_HWNDFIRST = 0;
        public const uint GW_HWNDLAST = 1;
        public const uint GW_HWNDNEXT = 2;
        public const uint GW_HWNDPREV = 3;
        public const uint GW_OWNER = 4;
        public const uint GW_CHILD = 5;
        public const uint GW_ENABLEDPOPUP = 6;

        // GetAncestor
        public const uint GA_PARENT = 1;
        public const uint GA_ROOT = 2;
        public const uint GA_ROOTOWNER = 3;

        // SetWindowPos Flags
        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_NOREDRAW = 0x0008;
        public const uint SWP_NOACTIVATE = 0x0010;
        public const uint SWP_FRAMECHANGED = 0x0020;
        public const uint SWP_SHOWWINDOW = 0x0040;
        public const uint SWP_HIDEWINDOW = 0x0080;
        public const uint SWP_NOCOPYBITS = 0x0100;
        public const uint SWP_NOOWNERZORDER = 0x0200;
        public const uint SWP_NOSENDCHANGING = 0x0400;
        public const uint SWP_DRAWFRAME = SWP_FRAMECHANGED;
        public const uint SWP_NOREPOSITION = SWP_NOOWNERZORDER;
        public const uint SWP_DEFERERASE = 0x2000;
        public const uint SWP_ASYNCWINDOWPOS = 0x4000;

        // HWND insert positions
        public static readonly IntPtr HWND_TOP = (IntPtr)0;
        public static readonly IntPtr HWND_BOTTOM = (IntPtr)1;
        public static readonly IntPtr HWND_TOPMOST = (IntPtr)(-1);
        public static readonly IntPtr HWND_NOTOPMOST = (IntPtr)(-2);

        // Window styles
        public const int WS_OVERLAPPED = 0x00000000;
        public const int WS_POPUP = unchecked((int)0x80000000);
        public const int WS_CHILD = 0x40000000;
        public const int WS_MINIMIZE = 0x20000000;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_DISABLED = 0x08000000;
        public const int WS_CLIPSIBLINGS = 0x04000000;
        public const int WS_CLIPCHILDREN = 0x02000000;
        public const int WS_MAXIMIZE = 0x01000000;
        public const int WS_BORDER = 0x00800000;
        public const int WS_DLGFRAME = 0x00400000;
        public const int WS_VSCROLL = 0x00200000;
        public const int WS_HSCROLL = 0x00100000;
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_THICKFRAME = 0x00040000;
        public const int WS_GROUP = 0x00020000;
        public const int WS_TABSTOP = 0x00010000;
        public const int WS_MINIMIZEBOX = 0x00020000;
        public const int WS_MAXIMIZEBOX = 0x00010000;

        public const int WS_CAPTION = WS_BORDER | WS_DLGFRAME;
        public const int WS_TILED = WS_OVERLAPPED;
        public const int WS_ICONIC = WS_MINIMIZE;
        public const int WS_SIZEBOX = WS_THICKFRAME;

        public const int WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX;
        public const int WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU;
        public const int WS_CHILDWINDOW = WS_CHILD;
        public const int WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW;

        // Extended Window Styles
        public const int WS_EX_DLGMODALFRAME = 0x00000001;
        public const int WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const int WS_EX_TOPMOST = 0x00000008;
        public const int WS_EX_ACCEPTFILES = 0x00000010;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int WS_EX_MDICHILD = 0x00000040;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_WINDOWEDGE = 0x00000100;
        public const int WS_EX_CLIENTEDGE = 0x00000200;
        public const int WS_EX_CONTEXTHELP = 0x00000400;
        public const int WS_EX_RIGHT = 0x00001000;
        public const int WS_EX_LEFT = 0x00000000;
        public const int WS_EX_RTLREADING = 0x00002000;
        public const int WS_EX_LTRREADING = 0x00000000;
        public const int WS_EX_LEFTSCROLLBAR = 0x00004000;
        public const int WS_EX_RIGHTSCROLLBAR = 0x00000000;
        public const int WS_EX_CONTROLPARENT = 0x00010000;
        public const int WS_EX_STATICEDGE = 0x00020000;
        public const int WS_EX_APPWINDOW = 0x00040000;
        public const int WS_EX_LAYERED = 0x00080000;
        public const int WS_EX_NOINHERITLAYOUT = 0x00100000;
        public const int WS_EX_NOREDIRECTIONBITMAP = 0x00200000;
        public const int WS_EX_LAYOUTRTL = 0x00400000;
        public const int WS_EX_COMPOSITED = 0x02000000;
        public const int WS_EX_NOACTIVATE = 0x08000000;

        public const int WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE;
        public const int WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST;

        // Show window
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;

        // ChildWindowFromPointEx flags
        public const uint CWP_ALL = 0x0000;
        public const uint CWP_SKIPINVISIBLE = 0x0001;
        public const uint CWP_SKIPDISABLED = 0x0002;
        public const uint CWP_SKIPTRANSPARENT = 0x0004;

        // ScrollInfo flags
        public const uint SIF_RANGE = 0x0001;
        public const uint SIF_PAGE = 0x0002;
        public const uint SIF_POS = 0x0004;
        public const uint SIF_DISABLENOSCROLL = 0x0008;
        public const uint SIF_TRACKPOS = 0x0010;
        public const uint SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);
    }
}
