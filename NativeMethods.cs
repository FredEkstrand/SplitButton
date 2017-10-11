/*
 * Source code from Microsoft System.Windows.Forms.NaiveMethods.cs
 * This source file is not a complete copy of the original source file.
 * Only items needed per project requirements were copied over to this file.
 * 
 * Most of the items could be obtained through public domain sources but, getting it 
 * from the MS team development source code just ensures proper identification and referencing.
 * 
 * Update Dates
 * 8/12/2017 Original file elements copied over.
 */
namespace Ekstrand.Windows.Forms
{
    //using Accessibility;
    using System.Runtime.InteropServices;
    using System;
    using System.Security.Permissions;
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Microsoft.Win32;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.Versioning;
    using System.Windows.Forms;

    public static class NativeMethods
    {
        public static IntPtr InvalidIntPtr = (IntPtr)(-1);
        public static IntPtr LPSTR_TEXTCALLBACK = (IntPtr)(-1);
        public static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

        public const int BITMAPINFO_MAX_COLORSIZE = 256;
        public const int BI_BITFIELDS = 3;

        #region Windows Messages

        public const int 
        WH_JOURNALPLAYBACK = 1,
        WH_GETMESSAGE = 3,
        WH_MOUSE = 7,
        WSF_VISIBLE = 0x0001,
        WM_NULL = 0x0000,
        WM_CREATE = 0x0001,
        WM_DELETEITEM = 0x002D,
        WM_DESTROY = 0x0002,
        WM_MOVE = 0x0003,
        WM_SIZE = 0x0005,
        WM_ACTIVATE = 0x0006,
        WA_INACTIVE = 0,
        WA_ACTIVE = 1,
        WA_CLICKACTIVE = 2,
        WM_SETFOCUS = 0x0007,
        WM_KILLFOCUS = 0x0008,
        WM_ENABLE = 0x000A,
        WM_SETREDRAW = 0x000B,
        WM_SETTEXT = 0x000C,
        WM_GETTEXT = 0x000D,
        WM_GETTEXTLENGTH = 0x000E,
        WM_PAINT = 0x000F,
        WM_CLOSE = 0x0010,
        WM_QUERYENDSESSION = 0x0011,
        WM_QUIT = 0x0012,
        WM_QUERYOPEN = 0x0013,
        WM_ERASEBKGND = 0x0014,
        WM_SYSCOLORCHANGE = 0x0015,
        WM_ENDSESSION = 0x0016,
        WM_SHOWWINDOW = 0x0018,
        WM_WININICHANGE = 0x001A,
        WM_SETTINGCHANGE = 0x001A,
        WM_DEVMODECHANGE = 0x001B,
        WM_ACTIVATEAPP = 0x001C,
        WM_FONTCHANGE = 0x001D,
        WM_TIMECHANGE = 0x001E,
        WM_CANCELMODE = 0x001F,
        WM_SETCURSOR = 0x0020,
        WM_MOUSEACTIVATE = 0x0021,
        WM_CHILDACTIVATE = 0x0022,
        WM_QUEUESYNC = 0x0023,
        WM_GETMINMAXINFO = 0x0024,
        WM_PAINTICON = 0x0026,
        WM_ICONERASEBKGND = 0x0027,
        WM_NEXTDLGCTL = 0x0028,
        WM_SPOOLERSTATUS = 0x002A,
        WM_DRAWITEM = 0x002B,
        WM_MEASUREITEM = 0x002C,
        WM_VKEYTOITEM = 0x002E,
        WM_CHARTOITEM = 0x002F,
        WM_SETFONT = 0x0030,
        WM_GETFONT = 0x0031,
        WM_SETHOTKEY = 0x0032,
        WM_GETHOTKEY = 0x0033,
        WM_QUERYDRAGICON = 0x0037,
        WM_COMPAREITEM = 0x0039,
        WM_GETOBJECT = 0x003D,
        WM_COMPACTING = 0x0041,
        WM_COMMNOTIFY = 0x0044,
        WM_WINDOWPOSCHANGING = 0x0046,
        WM_WINDOWPOSCHANGED = 0x0047,
        WM_POWER = 0x0048,
        WM_COPYDATA = 0x004A,
        WM_CANCELJOURNAL = 0x004B,
        WM_NOTIFY = 0x004E,
        WM_INPUTLANGCHANGEREQUEST = 0x0050,
        WM_INPUTLANGCHANGE = 0x0051,
        WM_TCARD = 0x0052,
        WM_HELP = 0x0053,
        WM_USERCHANGED = 0x0054,
        WM_NOTIFYFORMAT = 0x0055,
        WM_CONTEXTMENU = 0x007B,
        WM_STYLECHANGING = 0x007C,
        WM_STYLECHANGED = 0x007D,
        WM_DISPLAYCHANGE = 0x007E,
        WM_GETICON = 0x007F,
        WM_SETICON = 0x0080,
        WM_NCCREATE = 0x0081,
        WM_NCDESTROY = 0x0082,
        WM_NCCALCSIZE = 0x0083,
        WM_NCHITTEST = 0x0084,
        WM_NCPAINT = 0x0085,
        WM_NCACTIVATE = 0x0086,
        WM_GETDLGCODE = 0x0087,
        WM_NCMOUSEMOVE = 0x00A0,
        WM_NCMOUSELEAVE = 0x02A2,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_NCLBUTTONUP = 0x00A2,
        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_NCRBUTTONDOWN = 0x00A4,
        WM_NCRBUTTONUP = 0x00A5,
        WM_NCRBUTTONDBLCLK = 0x00A6,
        WM_NCMBUTTONDOWN = 0x00A7,
        WM_NCMBUTTONUP = 0x00A8,
        WM_NCMBUTTONDBLCLK = 0x00A9,
        WM_NCXBUTTONDOWN = 0x00AB,
        WM_NCXBUTTONUP = 0x00AC,
        WM_NCXBUTTONDBLCLK = 0x00AD,
        WM_KEYFIRST = 0x0100,
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_CHAR = 0x0102,
        WM_DEADCHAR = 0x0103,
        WM_CTLCOLOR = 0x0019,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
        WM_SYSCHAR = 0x0106,
        WM_SYSDEADCHAR = 0x0107,
        WM_KEYLAST = 0x0108,
        WM_IME_STARTCOMPOSITION = 0x010D,
        WM_IME_ENDCOMPOSITION = 0x010E,
        WM_IME_COMPOSITION = 0x010F,
        WM_IME_KEYLAST = 0x010F,
        WM_INITDIALOG = 0x0110,
        WM_COMMAND = 0x0111,
        WM_SYSCOMMAND = 0x0112,
        WM_TIMER = 0x0113,
        WM_HSCROLL = 0x0114,
        WM_VSCROLL = 0x0115,
        WM_INITMENU = 0x0116,
        WM_INITMENUPOPUP = 0x0117,
        WM_MENUSELECT = 0x011F,
        WM_MENUCHAR = 0x0120,
        WM_ENTERIDLE = 0x0121,
        WM_UNINITMENUPOPUP = 0x0125,
        WM_CHANGEUISTATE = 0x0127,
        WM_UPDATEUISTATE = 0x0128,
        WM_QUERYUISTATE = 0x0129,
        WM_CTLCOLORMSGBOX = 0x0132,
        WM_CTLCOLOREDIT = 0x0133,
        WM_CTLCOLORLISTBOX = 0x0134,
        WM_CTLCOLORBTN = 0x0135,
        WM_CTLCOLORDLG = 0x0136,
        WM_CTLCOLORSCROLLBAR = 0x0137,
        WM_CTLCOLORSTATIC = 0x0138,
        WM_MOUSEFIRST = 0x0200,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_XBUTTONDOWN = 0x020B,
        WM_XBUTTONUP = 0x020C,
        WM_XBUTTONDBLCLK = 0x020D,
        WM_MOUSEWHEEL = 0x020A,
        WM_MOUSELAST = 0x020A;

        public const int 
        WHEEL_DELTA = 120,
        WM_PARENTNOTIFY = 0x0210,
        WM_ENTERMENULOOP = 0x0211,
        WM_EXITMENULOOP = 0x0212,
        WM_NEXTMENU = 0x0213,
        WM_SIZING = 0x0214,
        WM_CAPTURECHANGED = 0x0215,
        WM_MOVING = 0x0216,
        WM_POWERBROADCAST = 0x0218,
        WM_DEVICECHANGE = 0x0219,
        WM_IME_SETCONTEXT = 0x0281,
        WM_IME_NOTIFY = 0x0282,
        WM_IME_CONTROL = 0x0283,
        WM_IME_COMPOSITIONFULL = 0x0284,
        WM_IME_SELECT = 0x0285,
        WM_IME_CHAR = 0x0286,
        WM_IME_KEYDOWN = 0x0290,
        WM_IME_KEYUP = 0x0291,
        WM_MDICREATE = 0x0220,
        WM_MDIDESTROY = 0x0221,
        WM_MDIACTIVATE = 0x0222,
        WM_MDIRESTORE = 0x0223,
        WM_MDINEXT = 0x0224,
        WM_MDIMAXIMIZE = 0x0225,
        WM_MDITILE = 0x0226,
        WM_MDICASCADE = 0x0227,
        WM_MDIICONARRANGE = 0x0228,
        WM_MDIGETACTIVE = 0x0229,
        WM_MDISETMENU = 0x0230,
        WM_ENTERSIZEMOVE = 0x0231,
        WM_EXITSIZEMOVE = 0x0232,
        WM_DROPFILES = 0x0233,
        WM_MDIREFRESHMENU = 0x0234,
        WM_MOUSEHOVER = 0x02A1,
        WM_MOUSELEAVE = 0x02A3,
        WM_CUT = 0x0300,
        WM_COPY = 0x0301,
        WM_PASTE = 0x0302,
        WM_CLEAR = 0x0303,
        WM_UNDO = 0x0304,
        WM_RENDERFORMAT = 0x0305,
        WM_RENDERALLFORMATS = 0x0306,
        WM_DESTROYCLIPBOARD = 0x0307,
        WM_DRAWCLIPBOARD = 0x0308,
        WM_PAINTCLIPBOARD = 0x0309,
        WM_VSCROLLCLIPBOARD = 0x030A,
        WM_SIZECLIPBOARD = 0x030B,
        WM_ASKCBFORMATNAME = 0x030C,
        WM_CHANGECBCHAIN = 0x030D,
        WM_HSCROLLCLIPBOARD = 0x030E,
        WM_QUERYNEWPALETTE = 0x030F,
        WM_PALETTEISCHANGING = 0x0310,
        WM_PALETTECHANGED = 0x0311,
        WM_HOTKEY = 0x0312,
        WM_PRINT = 0x0317,
        WM_PRINTCLIENT = 0x0318,
        WM_THEMECHANGED = 0x031A,
        WM_HANDHELDFIRST = 0x0358,
        WM_HANDHELDLAST = 0x035F,
        WM_AFXFIRST = 0x0360,
        WM_AFXLAST = 0x037F,
        WM_PENWINFIRST = 0x0380,
        WM_PENWINLAST = 0x038F,
        WM_APP = unchecked((int)0x8000),
        WM_USER = 0x0400,
        WM_REFLECT = NativeMethods.WM_USER + 0x1C00,
        WM_CHOOSEFONT_GETLOGFONT = (0x0400 + 1);

        #endregion

        #region Windows Styles

        public const int
        WS_OVERLAPPED = 0x00000000,
        WS_POPUP = unchecked((int)0x80000000),
        WS_CHILD = 0x40000000,
        WS_MINIMIZE = 0x20000000,
        WS_VISIBLE = 0x10000000,
        WS_DISABLED = 0x08000000,
        WS_CLIPSIBLINGS = 0x04000000,
        WS_CLIPCHILDREN = 0x02000000,
        WS_MAXIMIZE = 0x01000000,
        WS_CAPTION = 0x00C00000,
        WS_BORDER = 0x00800000,
        WS_DLGFRAME = 0x00400000,
        WS_VSCROLL = 0x00200000,
        WS_HSCROLL = 0x00100000,
        WS_SYSMENU = 0x00080000,
        WS_THICKFRAME = 0x00040000,
        WS_TABSTOP = 0x00010000,
        WS_MINIMIZEBOX = 0x00020000,
        WS_MAXIMIZEBOX = 0x00010000;

        #endregion

        #region Windows Extended Styles

        public const int
        WS_EX_DLGMODALFRAME = 0x00000001,
        WS_EX_MDICHILD = 0x00000040,
        WS_EX_TOOLWINDOW = 0x00000080,
        WS_EX_CLIENTEDGE = 0x00000200,
        WS_EX_CONTEXTHELP = 0x00000400,
        WS_EX_RIGHT = 0x00001000,
        WS_EX_LEFT = 0x00000000,
        WS_EX_RTLREADING = 0x00002000,
        WS_EX_LEFTSCROLLBAR = 0x00004000,
        WS_EX_CONTROLPARENT = 0x00010000,
        WS_EX_STATICEDGE = 0x00020000,
        WS_EX_APPWINDOW = 0x00040000,
        WS_EX_LAYERED = 0x00080000,
        WS_EX_TOPMOST = 0x00000008,
        WS_EX_LAYOUTRTL = 0x00400000,
        WS_EX_NOINHERITLAYOUT = 0x00100000,
        WPF_SETMINPOSITION = 0x0001;

        #endregion

        #region Class Styles Windows

        public const int 
        CS_DBLCLKS = 0x0008,
        CS_DROPSHADOW = 0x00020000,
        CS_SAVEBITS = 0x0800,
        CS_BYTEALIGNCLIENT = 0x1000,
        CS_BYTEALIGNWINDOW = 0x2000,
        CS_CLASSDC = 0x0040,
        CS_GLOBALCLASS = 0x4000,
        CS_HREDRAW = 0x0002,
        CS_NOCLOSE = 0x0200,
        CS_OWNDC = 0x0020,
        CS_PARENTDC = 0x0080,
        CS_VREDRAW = 0x0001;

        #endregion

        #region CASW
        // SetWindowLong function Changes an attribute of the specified window (CASW).

        public const int 
        GWL_EXSTYLE = -20,
        GWL_HINSTANCE = -6,
        GWL_ID = -12,
        GWL_STYLE = -16,
        GWL_USERDATA = -21,
        GWL_WNDPROC = -4,
        GWL_HWNDPARENT = -8;

        #endregion

        public const int 
        RPC_E_CHANGED_MODE = unchecked((int)0x80010106),
        RPC_E_CANTCALLOUT_ININPUTSYNCCALL = unchecked((int)0x8001010D),
        RGN_AND = 1,
        RGN_XOR = 3,
        RGN_DIFF = 4,
        RDW_INVALIDATE = 0x0001,
        RDW_ERASE = 0x0004,
        RDW_ALLCHILDREN = 0x0080,
        RDW_ERASENOW = 0x0200,
        RDW_UPDATENOW = 0x0100,
        RDW_FRAME = 0x0400,
        RB_INSERTBANDA = (0x0400 + 1),
        RB_INSERTBANDW = (0x0400 + 10);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TOOLINFO_T
        {
            public int cbSize = Marshal.SizeOf(typeof(TOOLINFO_T));
            public int uFlags;
            public IntPtr hwnd;
            public IntPtr uId;
            public RECT rect;
            public IntPtr hinst = IntPtr.Zero;
            public string lpszText;
            public IntPtr lParam = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TOOLINFO_TOOLTIP
        {
            public int cbSize = Marshal.SizeOf(typeof(TOOLINFO_TOOLTIP));
            public int uFlags;
            public IntPtr hwnd;
            public IntPtr uId;
            public RECT rect;
            public IntPtr hinst = IntPtr.Zero;
            public IntPtr lpszText;
            public IntPtr lParam = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(System.Drawing.Rectangle r)
            {
                this.left = r.Left;
                this.top = r.Top;
                this.right = r.Right;
                this.bottom = r.Bottom;
            }

            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            public System.Drawing.Size Size
            {
                get
                {
                    return new System.Drawing.Size(this.right - this.left, this.bottom - this.top);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class COMRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public COMRECT()
            {
            }

            public COMRECT(System.Drawing.Rectangle r)
            {
                this.left = r.X;
                this.top = r.Y;
                this.right = r.Right;
                this.bottom = r.Bottom;
            }


            public COMRECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            /* Unused
            public RECT ToRECT() {
                return new RECT(left, top, right, bottom);
            }
            */

            public static COMRECT FromXYWH(int x, int y, int width, int height)
            {
                return new COMRECT(x, y, x + width, y + height);
            }

            public override string ToString()
            {
                return "Left = " + left + " Top " + top + " Right = " + right + " Bottom = " + bottom;
            }
        }

        public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            // rcPaint was a by-value RECT structure
            public int rcPaint_left;
            public int rcPaint_top;
            public int rcPaint_right;
            public int rcPaint_bottom;
            public bool fRestore;
            public bool fIncUpdate;
            public int reserved1;
            public int reserved2;
            public int reserved3;
            public int reserved4;
            public int reserved5;
            public int reserved6;
            public int reserved7;
            public int reserved8;
        }

        #region CommonHandles

        public sealed class CommonHandles
        {
            static CommonHandles()
            {

            }

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.CommonHandles.Accelerator"]/*' />
            /// <devdoc>
            ///     Handle type for accelerator tables.
            /// </devdoc>
            public static readonly int Accelerator = Ekstrand.Internal.HandleCollector.RegisterType("Accelerator", 80, 50);

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.CommonHandles.Cursor"]/*' />
            /// <devdoc>
            ///     handle type for cursors.
            /// </devdoc>
            public static readonly int Cursor = Ekstrand.Internal.HandleCollector.RegisterType("Cursor", 20, 500);

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.CommonHandles.EMF"]/*' />
            /// <devdoc>
            ///     Handle type for enhanced metafiles.
            /// </devdoc>
            public static readonly int EMF = Ekstrand.Internal.HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.CommonHandles.Find"]/*' />
            /// <devdoc>
            ///     Handle type for file find handles.
            /// </devdoc>
            public static readonly int Find = Ekstrand.Internal.HandleCollector.RegisterType("Find", 0, 1000);

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.CommonHandles.GDI"]/*' />
            /// <devdoc>
            ///     Handle type for GDI objects.
            /// </devdoc>
            public static readonly int GDI = Ekstrand.Internal.HandleCollector.RegisterType("GDI", 50, 500);

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.CommonHandles.HDC"]/*' />
            /// <devdoc>
            ///     Handle type for HDC's that count against the Win98 limit of five DC's.  HDC's
            ///     which are not scarce, such as HDC's for bitmaps, are counted as GDIHANDLE's.
            /// </devdoc>
            public static readonly int HDC = Ekstrand.Internal.HandleCollector.RegisterType("HDC", 100, 2); // wait for 2 dc's before collecting

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.CommonHandles.HDC"]/*' />
            /// <devdoc>
            ///     Handle type for Compatible HDC's used for ToolStrips
            /// </devdoc>
            public static readonly int CompatibleHDC = Ekstrand.Internal.HandleCollector.RegisterType("ComptibleHDC", 50, 50); // wait for 2 dc's before collecting

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.CommonHandles.Icon"]/*' />
            /// <devdoc>
            ///     Handle type for icons.
            /// </devdoc>
            public static readonly int Icon = Ekstrand.Internal.HandleCollector.RegisterType("Icon", 20, 500);

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.CommonHandles.Kernel"]/*' />
            /// <devdoc>
            ///     Handle type for kernel objects.
            /// </devdoc>
            public static readonly int Kernel = Ekstrand.Internal.HandleCollector.RegisterType("Kernel", 0, 1000);

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.CommonHandles.Menu"]/*' />
            /// <devdoc>
            ///     Handle type for files.
            /// </devdoc>
            public static readonly int Menu = Ekstrand.Internal.HandleCollector.RegisterType("Menu", 30, 1000);

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.CommonHandles.Window"]/*' />
            /// <devdoc>
            ///     Handle type for windows.
            /// </devdoc>
            public static readonly int Window = Ekstrand.Internal.HandleCollector.RegisterType("Window", 5, 1000);

        }

        #endregion
    }

    #region Animation 

    /// <summary>
    /// Animate type from https://msdn.microsoft.com/en-us/library/windows/desktop/ms632669(v=vs.85).aspx
    /// </summary>
    [Flags]
    public enum AnimationTypes
    {
        ///<summary>Activates the window. Do not use this value with AW_HIDE. </summary> 
        ACTIVATE = 0x00020000, // 131072

        ///<summary>Uses a fade effect. This flag can be used only if hwnd is a top-level window. </summary> 
        BLEND = 0x00080000, // 524288

        ///<summary>
        ///Makes the window appear to collapse inward if AW_HIDE is used or expand outward if
        /// the AW_HIDE is not used. The various direction flags have no effect. 
        ///</summary> 
        CENTER = 0x00000010, // 16

        ///<summary>Hides the window. By default, the window is shown. </summary>
        HIDE = 0x00010000, // 65536

        /// <summary>
        /// Animates the window from left to right. This flag can be used with roll or slide animation. 
        /// It is ignored when used with AW_CENTER or AW_BLEND.       
        /// </summary>
        HOR_POSITIVE = 0x00000001, // 1

        /// <summary>
        /// Animates the window from right to left. This flag can be used with roll or slide animation. 
        /// It is ignored when used with AW_CENTER or AW_BLEND.
        /// </summary>
        HOR_NEGATIVE = 0x00000002, // 2

        /// <summary>
        /// Uses slide animation. By default, roll animation is used. 
        /// This flag is ignored when used with AW_CENTER. 
        /// </summary>
        SLIDE = 0x00040000, // 262144

        /// <summary>
        /// Animates the window from top to bottom. This flag can be used with roll or slide animation. 
        /// It is ignored when used with AW_CENTER or AW_BLEND. 
        /// </summary>
        VER_POSITIVE = 0x00000004, // 4

        /// <summary>
        ///  Animates the window from bottom to top. This flag can be used with roll or slide animation. 
        ///  It is ignored when used with AW_CENTER or AW_BLEND. 
        /// </summary>
        VER_NEGATIVE = 0x00000008, // 8

        /// <summary>
        /// Uses roll animation which is default.
        /// Added enum for completeness.
        /// </summary>
        ROLL = 0x0000000 // 0
    }

    /// <summary>
    /// Win32 Animate Window function wrapper class.
    /// </summary>
    public sealed class WinOSAnimation
    {
        private WinOSAnimation() { }

        public static bool AnimateControl(Control control, int msec, AnimationTypes flags)
        {
            bool success = false;

            if (control.Visible)
            {
                flags |= AnimationTypes.HIDE;
            }

            // Are we a top level control
            if (control.TopLevelControl == control)
            {
                flags |= AnimationTypes.ACTIVATE;
            }

            success = AnimateWindow(control.Handle, msec, (int)flags);
            if (success)
            {
                control.Visible = !control.Visible;
            }

            return success;
        }

        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);
    }

    #region Utils
    // Taken directly from .Net source NativeMethods.cs
    public static class Util
    {
        public static int HIWORD(int n)
        {
            return (n >> 16) & 0xffff;
        }

        public static int LOWORD(int n)
        {
            return n & 0xffff;
        }

        public static IntPtr GETWINDOWLONGPTR(IntPtr hWnd, int index)
        {
            return GetWindowLongPtr(hWnd, index);
        }

        public static IntPtr GETWINDOWLONG(IntPtr hWind, int index)
        {
            return GetWindowLong(hWind, index);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int index);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowLong(IntPtr hWnd, int index);

    }

    #endregion

    #endregion


}
