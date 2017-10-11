using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Design;

namespace Ekstrand.Windows.Forms
{
    public class SplitButton : Button
    {
        #region Fields

        private const int SplitButtonWidth = 18;
        private const int WM_PAINT = 0x000F;
        private static int BorderSize = SystemInformation.Border3DSize.Width * 2;
        private Image m_Chevron1;
        private Image m_Chevron2;
        private Image m_Chevron3;
        private bool m_InternalPaint = false;
        // private Rectangle dropDownRectangle = new Rectangle();
        private Rectangle m_SplitButtonRectangle;        
        private SplitButtonSide m_SplitButtonSide = SplitButtonSide.Right;

        private PushButtonState m_State;
        private string m_Text = string.Empty;
        private TextFormatFlags m_TextFormatFlags;
        private Rectangle m_TextRectangle;
        private float m_XPos;
        private float m_YPos;
        private bool skipNextOpen = false;
        private bool m_MenuOpen = false;

        #endregion Fields

        #region Constructors

        public SplitButton()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                                 
            m_State = PushButtonState.Normal;
            m_Chevron1 = new Bitmap(Ekstrand.Windows.Forms.Properties.Resources.Chevron1);
            m_Chevron2 = new Bitmap(Ekstrand.Windows.Forms.Properties.Resources.Chevron2);
            m_Chevron3 = new Bitmap(Ekstrand.Windows.Forms.Properties.Resources.Chevron3);

            UpdateLayout();
        }

        #endregion Constructors

        #region Properties

        public new FlatStyle FlatStyle
        {
            get
            {
                return base.FlatStyle;
            }

            set
            {
                if (base.FlatStyle != value)
                {
                    base.FlatStyle = value;
                    m_InternalPaint = base.FlatStyle == FlatStyle.System ? true : false;
                }
            }
        }

        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }

            set
            {
                base.RightToLeft = value;
                UpdateLayout();
                Invalidate();
            }
        }

        [
                           Category("Appearance"),
           Description("Split Button Side")
           Browsable(true),
           SettingsBindable(true),
           EditorBrowsable(EditorBrowsableState.Always),
           DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
        ]
        public SplitButtonSide SplitButtonSide
        {
            get
            {
                return m_SplitButtonSide;
            }

            set
            {
                if (m_SplitButtonSide != value)
                {
                    m_SplitButtonSide = value;
                    UpdateLayout();
                    Invalidate();
                }
            }
        }
        [
           Browsable(false),
           SettingsBindable(false),
           Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor)),
           EditorBrowsable(EditorBrowsableState.Never),
           DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
            }
        }

        [
           Category("Appearance"),
           Description("Text to be displayed to user")
           Browsable(true),
           Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor)),
           SettingsBindable(true),
           EditorBrowsable(EditorBrowsableState.Always),
           DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
       ]
        public string TextDisplayed
        {
            get
            {
                return m_Text;
            }

            set
            {
                if (!m_Text.Equals(value))
                {
                    m_Text = value;
                    Invalidate();
                }
            }
        }

        protected override Size DefaultMinimumSize
        {
            get
            {
                return new Size(20, 20);
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(109, 23);
            }
        }

        /*
         * Hide Control.Text to prevent painting of text on button using controls text formatting.
         * This derived button can now control the text formatting when rendered on button face.
         */
        #endregion

        #region Methods

        protected override void OnGotFocus(EventArgs e)
        {
            if (m_State != PushButtonState.Pressed && m_State != PushButtonState.Disabled)
            {
                m_State = PushButtonState.Default;
                Invalidate();
            }
        }

        protected override void OnKeyDown(KeyEventArgs kevent)
        {

            if (kevent.KeyCode.Equals(Keys.Down))
            {
                ShowContextMenuStrip();
            }
            else if (kevent.KeyCode.Equals(Keys.Space) && kevent.Modifiers == Keys.None)
            {
                m_State = PushButtonState.Pressed;
                Invalidate();
            }

            base.OnKeyDown(kevent);
        }

        protected override void OnKeyUp(KeyEventArgs kevent)
        {
            if (kevent.KeyCode.Equals(Keys.Space))
            {
                if (Control.MouseButtons == MouseButtons.None)
                {
                    m_State = PushButtonState.Normal;
                    Invalidate();
                }
            }
            base.OnKeyUp(kevent);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (m_State != PushButtonState.Pressed && m_State != PushButtonState.Disabled)
            {
                m_State = PushButtonState.Normal;
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (m_SplitButtonRectangle.Contains(this.PointToClient(Cursor.Position)))
                {
                    ShowContextMenuStrip();                    
                }
                else
                {
                    m_State = PushButtonState.Pressed;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (m_State != PushButtonState.Pressed && m_State != PushButtonState.Disabled)
            {
                m_State = PushButtonState.Hot;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!m_MenuOpen)
            {
                if (m_State != PushButtonState.Pressed && m_State != PushButtonState.Disabled)
                {
                    if (Focused)
                    {
                        m_State = PushButtonState.Default;
                        Invalidate();
                    }
                    else
                    {
                        m_State = PushButtonState.Normal;
                        Invalidate();
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (ContextMenuStrip == null || !ContextMenuStrip.Visible)
            {
                if (Bounds.Contains(Parent.PointToClient(Cursor.Position)))
                {
                    m_State = PushButtonState.Hot;
                }
                else if (Focused)
                {
                    m_State = PushButtonState.Default;
                }
                else
                {
                    m_State = PushButtonState.Normal;
                }

                if (Bounds.Contains(Parent.PointToClient(Cursor.Position)) && !m_SplitButtonRectangle.Contains(mevent.Location))
                {
                    OnClick(new EventArgs());
                }
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (!m_InternalPaint)
            {
                base.OnPaint(pevent);
            }
            else if (m_InternalPaint)
            {
                DrawSystemFlatstyle(pevent.Graphics);
                return;
            }

            m_InternalPaint = false;

            if (!Enabled)
            {
                m_State = PushButtonState.Disabled;
            }

            Graphics g = pevent.Graphics;

            ButtonRenderer.DrawButton(g, this.ClientRectangle, m_State);
            DrawSplitButton(g);

            if (!string.IsNullOrEmpty(m_Text))
            {
                TextRenderer.DrawText(g, m_Text, Font, m_TextRectangle, ForeColor, m_TextFormatFlags);
            }

            if (m_State == PushButtonState.Normal && Focused)
            {
                ControlPaint.DrawFocusRectangle(g, m_TextRectangle);
            }

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();
        }

        void ContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            ContextMenuStrip cms = sender as ContextMenuStrip;
            if (cms != null)
            {
                cms.Closing -= new ToolStripDropDownClosingEventHandler(ContextMenuStrip_Closing);
            }

            if (Bounds.Contains(Parent.PointToClient(Cursor.Position)))
            {
                m_State = PushButtonState.Hot;
            }
            else if (Focused)
            {
                m_State = PushButtonState.Default;
            }
            else
            {
                m_State = PushButtonState.Normal;
            }

            m_MenuOpen = false;
            Invalidate();
        }

        private void DrawHighlight(Graphics g)
        {
            Color hightlight = Color.FromArgb(20, SystemColors.Highlight);
            g.FillRectangle(new SolidBrush(hightlight), m_SplitButtonRectangle);
        }

        private void DrawSplitButton(Graphics g)
        {
            if (m_State == PushButtonState.Disabled)
            {
                g.DrawImage(m_Chevron3, m_SplitButtonRectangle.X + m_XPos, m_YPos);
                DrawSplitLine(g, m_SplitButtonRectangle, false);
            }
            else if (m_State == PushButtonState.Hot)
            {

                DrawHighlight(g);
                g.DrawImage(m_Chevron2, m_SplitButtonRectangle.X + m_XPos, m_YPos);
                DrawSplitLine(g, m_SplitButtonRectangle, true);
            }
            else if (m_State == PushButtonState.Normal || m_State == PushButtonState.Default || m_State == PushButtonState.Pressed)
            {
                g.DrawImage(m_Chevron1, m_SplitButtonRectangle.X + m_XPos, m_YPos);
                DrawSplitLine(g, m_SplitButtonRectangle, false);
            }
        }

        private void DrawSplitLine(Graphics g, RectangleF bounds, bool isHot)
        {
            PointF p1 = new PointF();
            PointF p2 = new PointF();

            p1.Y = bounds.Y + 5;
            p2.Y = bounds.Height - 5;
            p1.X = SplitButtonSide == SplitButtonSide.Right ? p1.X = bounds.X : p1.X = SplitButtonWidth;
            p2.X = SplitButtonSide == SplitButtonSide.Right ? p2.X = bounds.X : p2.X = SplitButtonWidth;

            if (!Enabled)
            {
                using (Pen pen = new Pen(SystemColors.GrayText, 1))
                {
                    g.DrawLine(pen, p1, p2);
                }
            }
            else
            {
                using (Pen pen = new Pen(isHot ? SystemColors.Highlight : SystemColors.ControlText, 1))
                {
                    g.DrawLine(pen, p1, p2);
                }
            }
        }

        private void DrawSystemFlatstyle(Graphics g)
        {
            DrawSplitButton(g);
            if (!string.IsNullOrEmpty(m_Text))
            {
                // change to graphics.drawstring using rectangle for location
                
                g.DrawString(m_Text, Font, SystemBrushes.ControlText, m_TextRectangle);
                //TextRenderer.DrawText(g, m_Text, Font, m_TextRectangle, ForeColor, m_TextFormatFlags);
            }
        }

        private void ShowContextMenuStrip()
        {
            if (skipNextOpen)
            {   // we were called because we're closing the context menu strip
                // when clicking the dropdown button.
                skipNextOpen = false;
                return;
            }

            m_State = PushButtonState.Pressed;
            m_MenuOpen = true;
            if (ContextMenuStrip != null)
            {
                ContextMenuStrip.Closing += new ToolStripDropDownClosingEventHandler(ContextMenuStrip_Closing);
                ContextMenuStrip.Show(this, new Point(0, Height), ToolStripDropDownDirection.BelowRight);
            }
        }
        private void UpdateLayout()
        {
            Rectangle bounds = Rectangle.Truncate(this.CreateGraphics().VisibleClipBounds);

            // set splitbutton rectangle
            m_SplitButtonRectangle = new Rectangle(this.Width - 18, bounds.Y + 2, 18, bounds.Height - 4);

            // set up text bounds
            int internalBorder = BorderSize;
            m_TextRectangle = new Rectangle(internalBorder,
                                            (internalBorder * 2) / 2,
                                            bounds.Width - m_SplitButtonRectangle.Width - internalBorder * 2,
                                            bounds.Height - (internalBorder * 2));

            if (this.SplitButtonSide == SplitButtonSide.Left)
            {
                // set splitbutton rectangle
                m_SplitButtonRectangle = new Rectangle(2, bounds.Y + 2, 18, bounds.Height - 4);
                m_TextRectangle.X = m_SplitButtonRectangle.Width + internalBorder;
                m_TextRectangle.Width = m_TextRectangle.Width; // - internalBorder;
            }

            // x,y offset to center image chevron on split button
            m_XPos = ((m_SplitButtonRectangle.Width / 2) - (m_Chevron1.Width / 2)) - 1;
            m_YPos = (m_SplitButtonRectangle.Height / 2) - (m_Chevron1.Height / 2) + 1;



            m_TextFormatFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;

            // If we dont' use mnemonic, set formatFlag to NoPrefix as this will show ampersand.
            if (!UseMnemonic)
            {
                m_TextFormatFlags = m_TextFormatFlags | TextFormatFlags.NoPrefix;
            }
            else if (!ShowKeyboardCues)
            {
                m_TextFormatFlags = m_TextFormatFlags | TextFormatFlags.HidePrefix;
            }

            if (RightToLeft == RightToLeft.Yes)
            {
                m_TextFormatFlags = m_TextFormatFlags | TextFormatFlags.RightToLeft | TextFormatFlags.SingleLine | TextFormatFlags.Right;
            }
        }


        #endregion

        #region Win32


        private const short PaintLayerBackground = 1;

        private const short PaintLayerForeground = 2;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case WM_PAINT:
                    WmPaint(ref m);
                    break;
            }
        }


        private void WmPaint(ref Message m)
        {
            if(FlatStyle != FlatStyle.System)
            {
                return;
            }
            IntPtr hWnd = IntPtr.Zero;
            IntPtr dc;
            Rectangle clip;
            NativeMethods.PAINTSTRUCT ps = new NativeMethods.PAINTSTRUCT();
            bool needDisposeDC = false;

            try
            {
                if (m.WParam == IntPtr.Zero)
                {
                    // Cache Handle not only for perf but to avoid object disposed exception in case the window
                    // is destroyed in an event handler (VSW#261657).
                    hWnd = this.Handle;
                    dc = UnsafeNativeMethods.BeginPaint(new HandleRef(this, hWnd), ref ps);
                    if (dc == IntPtr.Zero)
                    {
                        return;
                    }
                    needDisposeDC = true;
                    clip = new Rectangle(ps.rcPaint_left, ps.rcPaint_top,
                                         ps.rcPaint_right - ps.rcPaint_left,
                                         ps.rcPaint_bottom - ps.rcPaint_top);
                    m_InternalPaint = true;
                    PaintEventArgs pevent = new PaintEventArgs(CreateGraphics(), clip);
                    OnPaint(pevent);
                }
                else
                {
                    dc = m.WParam;
                    clip = ClientRectangle;
                }

            }
            finally
            {
                if (needDisposeDC)
                {
                    UnsafeNativeMethods.EndPaint(new HandleRef(this, hWnd), ref ps);
                }
            }
        }

        #endregion
    }


    #region Enumerations

    public enum ButtonBehavior
    {
        [Description("Split Button Only")]
        SplitButton = 0,
        [Description("As One Button")]
        AsOneButton = 1,
        [Description("Split Button & Button")]
        SplitButton_Button = 2,

    }

    public enum SplitButtonSide
    {
        Left = 7,
        Right = 6
    }
    #endregion
}
