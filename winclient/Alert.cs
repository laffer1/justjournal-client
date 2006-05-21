using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace JustJournal
{
    partial class Alert 
    {
        [DllImport("User32.dll")]
        public extern static int ShowWindow(IntPtr hWnd, Int32 cmdShow);

        const Int32 SW_SHOWNOACTIVATE = 4;

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool SetWindowPos(
            IntPtr hWnd,             // handle to window
            Int32 hWndInsertAfter,  // placement-order handle
            Int32 X,                 // horizontal position
            Int32 Y,                 // vertical position
            Int32 cx,                // width
            Int32 cy,                // height
            Int32 uFlags            // window-positioning options
            );

        const Int32 SWP_NOACTIVATE = 0x0010;
        const Int32 HWND_TOPMOST = -1;

        [DllImport("winmm.dll")]
        private static extern Int32 PlaySound(String lpszName, Int32 hModule, Int32 dwFlags);
        private static int SND_ALIAS = 0x00010000;

        private string linked;

        public Alert(string Message, string link)
        {
            InitializeComponent();
            msg.Text = Message;
            linked = link;
            int h = 128;
            int w = 168;
            int cx = Screen.PrimaryScreen.WorkingArea.Width - (w + 25);
            int cy = Screen.PrimaryScreen.WorkingArea.Height - h;
            this.SetBounds(cx, cy, w, h, BoundsSpecified.All);
            ShowWindow(this.Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(this.Handle, HWND_TOPMOST, cx, cy, this.Width, this.Height, SWP_NOACTIVATE);
            timer1.Start();
        }

        private void Alert_Load(object sender, System.EventArgs e)
        {
            PlaySound("SystemNotification", 0, SND_ALIAS);
        }

        private void msg_MouseEnter(object sender, System.EventArgs e)
        {
            if (!String.IsNullOrEmpty(linked))
            {
                Font ft = msg.Font;
                msg.ForeColor = Color.Blue;
                msg.Font = new Font(ft.FontFamily, ft.Size, FontStyle.Underline);
                msg.Cursor = Cursors.Hand;
            }
        }

        private void msg_MouseLeave(object sender, System.EventArgs e)
        {
            Font ft = msg.Font;
            msg.ForeColor = this.ForeColor;
            msg.Font = new Font(ft.FontFamily, ft.Size, FontStyle.Regular);
            msg.Cursor = Cursors.Default;
        }

        private void msg_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(linked))
                {
                    System.Diagnostics.Process.Start(linked);
                    timer1.Stop();
                    this.Close();
                }
            }
            catch (Win32Exception xc)
            {
                MessageBox.Show(xc.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

   }
}