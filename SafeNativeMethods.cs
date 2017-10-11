/*
 * Source code from Microsoft System.Windows.Forms.SafeNativeMethods.cs
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
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using System;
    using System.Security;
    using System.Security.Permissions;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Drawing;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Versioning;

    public static class SafeNativeMethods
    {

        [DllImport("user32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool RedrawWindow(HandleRef hwnd, ref NativeMethods.RECT rcUpdate, HandleRef hrgnUpdate, int flags);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool RedrawWindow(HandleRef hwnd, NativeMethods.COMRECT rcUpdate, HandleRef hrgnUpdate, int flags);

    }
}
