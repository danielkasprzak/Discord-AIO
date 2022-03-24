using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace discordAIO6
{
    public partial class notify : Form
    {
        bool areWeClosing = false;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public notify(string message, bool exHmmm = false)
        {
            InitializeComponent();
            label1.Text = message;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            pictureBox1.Image = Properties.Resources.d1;

            if (exHmmm)
            {
                areWeClosing = true;
            }
            else
            {
                areWeClosing = false;
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void notify_Load(object sender, EventArgs e)
        {

        }

        private void webhookCheck_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (areWeClosing)
            {
                Environment.Exit(0);
            }
        }

        private void webhookCheck_MouseHover(object sender, EventArgs e)
        {
            webhookCheck.FlatAppearance.BorderSize = 1;
        }

        private void webhookCheck_MouseLeave(object sender, EventArgs e)
        {
            webhookCheck.FlatAppearance.BorderSize = 0;
        }
    }
}
