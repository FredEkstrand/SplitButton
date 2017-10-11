
/*
 * Source code from Microsoft System.Windows.Forms.UnsafeNativeMethods.cs
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
    using System.Runtime.InteropServices.ComTypes;
    using System.Runtime.ConstrainedExecution;
    using System;
    using System.Security.Permissions;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Security;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Versioning;

    //using System.Drawing;

    [ SuppressUnmanagedCodeSecurity()]
    public static class UnsafeNativeMethods
    {
        #region SendMessage

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, bool wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int[] lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int[] wParam, int[] lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, ref int wParam, ref int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, StringBuilder lParam);

        /* for future need
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TOOLINFO_T lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TOOLINFO_TOOLTIP lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TBBUTTON lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TBBUTTONINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TV_ITEM lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TV_INSERTSTRUCT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TV_HITTESTINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVBKIMAGE lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.LVHITTESTINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TCITEM_T lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.HDLAYOUT hdlayout);
        */
        #endregion

        #region DefWindowProc

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        #endregion

        #region SetWindowLong
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        [ResourceExposure(ResourceScope.None)]

        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, NativeMethods.WndProc wndproc);
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, NativeMethods.WndProc wndproc);

        public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, NativeMethods.WndProc wndproc)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, wndproc);
            }
            return SetWindowLongPtr64(hWnd, nIndex, wndproc);
        }

        //SetWindowLong won't work correctly for 64-bit: we should use SetWindowLongPtr instead.  On
        //32-bit, SetWindowLongPtr is just #defined as SetWindowLong.  SetWindowLong really should 
        //take/return int instead of IntPtr/HandleRef, but since we're running this only for 32-bit
        //it'll be OK.
        public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }
        #endregion

        #region GetActiveWindow

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern IntPtr GetActiveWindow();

        #endregion

        #region SetParent

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SetParent(HandleRef hWnd, HandleRef hWndParent);

        #endregion

        #region Paint

        [DllImport("user32.dll", ExactSpelling = true, EntryPoint = "BeginPaint", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        private static extern IntPtr IntBeginPaint(HandleRef hWnd, [In, Out] ref NativeMethods.PAINTSTRUCT lpPaint);
        public static IntPtr BeginPaint(HandleRef hWnd, [In, Out, MarshalAs(UnmanagedType.LPStruct)] ref NativeMethods.PAINTSTRUCT lpPaint)
        {
            return Ekstrand.Internal.HandleCollector.Add(IntBeginPaint(hWnd, ref lpPaint), NativeMethods.CommonHandles.HDC);
        }

        [DllImport("user32.dll", ExactSpelling = true, EntryPoint = "EndPaint", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        private static extern bool IntEndPaint(HandleRef hWnd, ref NativeMethods.PAINTSTRUCT lpPaint);
        public static bool EndPaint(HandleRef hWnd, [In, MarshalAs(UnmanagedType.LPStruct)] ref NativeMethods.PAINTSTRUCT lpPaint)
        {
            Ekstrand.Internal.HandleCollector.Remove(lpPaint.hdc, NativeMethods.CommonHandles.HDC);
            return IntEndPaint(hWnd, ref lpPaint);
        }

        #endregion
    }
}
