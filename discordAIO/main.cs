using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace discordAIO
{
    public partial class main : Form
    {
        private protected string email = "fmediolanek@gmail.com";
        public main()
        {
            InitializeComponent();
            CheckProtection();
        }

        private void CheckProtection()
        {
            string ip = new WebClient().DownloadString("http://ipv4bot.whatismyipaddress.com/");

            WebClient userIP = new WebClient();
            string resposne = userIP.DownloadString("http://check.getipintel.net/check.php?ip=" + ip + "&contact=" + email + "&flags=m");

            int responseNum = Int32.Parse(resposne);

            if (responseNum < 1)
            {
                label2.Text = ip + " > unprotected";
            } 
            else if (responseNum == -5) {
                MessageBox.Show("Error.\nContact email incorrect or blocked.", "Discord AIO");
            }
            else if (responseNum == -6)
            {
                MessageBox.Show("Error.\nContact email incorrect.", "Discord AIO");
            }
            else if (responseNum == -4)
            {
                MessageBox.Show("Error.\nUnable to reach database.\n\nProbably it is being updated.", "Discord AIO");
            }
            else
            {
                label2.Text = ip + " > protected";
            }

        }

        private void Solid_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            float width = (float)1.0;
            Color color = Color.DarkRed;
            Pen pen = new Pen(color, width);
            pen.DashStyle = DashStyle.Solid;
            e.Graphics.DrawLine(pen, 0, 0, 0, panel.Height - 0);
            e.Graphics.DrawLine(pen, 0, 0, panel.Width - 0, 0);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, 0, panel.Height - 1);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, panel.Width - 1, 0);
        }


        private void Dot_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            float width = (float)1.0;
            Color color = Color.DarkRed;
            Pen pen = new Pen(color, width);
            pen.DashStyle = DashStyle.Dash;
            e.Graphics.DrawLine(pen, 0, 0, 0, panel.Height - 0);
            e.Graphics.DrawLine(pen, 0, 0, panel.Width - 0, 0);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, 0, panel.Height - 1);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, panel.Width - 1, 0);
        }
    }
}
