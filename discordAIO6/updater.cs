using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace discordAIO6
{
    public partial class updater : Form
    {
        private static string version = "0.7.0";

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public updater()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            Check();
        }

        string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string dDir = Path.GetTempPath();
        private void Check()
        {
            if (!Directory.Exists(dDir + "\\uAIO"))
            {
                Directory.CreateDirectory(dDir + "\\uAIO");
            }
            if (!Directory.Exists(dDir + "\\uAIO\\uDwn"))
            {
                Directory.CreateDirectory(dDir + "\\uAIO\\uDwn");
            }
            if (!Directory.Exists(dDir + "\\uAIO\\uOld"))
            {
                Directory.CreateDirectory(dDir + "\\uAIO\\uOld");
            }

            if (File.Exists(dDir + "\\uAIO\\uOld\\Discord AIO"))
            {
                File.Delete(dDir + "\\uAIO\\uOld\\Discord AIO");
            }

            WebClient vClient = new WebClient();
            string downloadedVersion = vClient.DownloadString("https://pastebin.com/raw/hxxHrBni");

            if (version == downloadedVersion)
            {
                label1.Text = "Launching...";

                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                Visible = false;

                label1.Text = "gj, hiding is broken";

                dAIOmain dForm = new dAIOmain();
                dForm.Show();
            }
            else
            {
                label1.Text = "Updating...";
                try
                {
                    if (File.Exists(dDir + "\\uAIO\\uDwn\\Discord AIO"))
                    {
                        File.Delete(dDir + "\\uAIO\\uDwn\\Discord AIO");
                    }

                    new WebClient().DownloadFile("https://github.com/Nyxonn/Discord-AIO/releases/download/" + version + "/Discord.AIO.exe", dDir + "\\uAIO\\uDwn\\Discord AIO.exe");
                    File.Move(dir + "\\Discord AIO.exe", dDir + "\\uAIO\\uOld\\Discord AIO.exe");
                    File.Copy(dDir + "\\uAIO\\uDwn\\Discord AIO.exe", dir + "\\Discord AIO.exe");

                    notify nfForm = new notify("Update completed.\nPlease restart Discord AIO.", true);
                    nfForm.Show();
                }
                catch { }
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
    }
}
