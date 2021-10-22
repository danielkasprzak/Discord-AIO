using DiscordRPC;
using ScintillaNET;
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
        private string version = "v1.0";
        private protected string email = "fmediolanek@gmail.com";
        private protected static string dAIOPass = "ceDUsRGbwAdcjUqHWkMLgARDrkx4hNJ28WbS6fPjr7cYQ87BKJTRFcUKRbJMMxeWDTEy3k9jsLSVFFV9heDeuWYfECHDzpmgYrttCMuLZRBtuPzuSJ846YBZhCa88Qj2fAaNJuuw6fhZBZfANEbRkXvaupN8rtDQVqrvLKnKW5ESAueQ2pz4QJnTdkDdM3rapFk9mU5DNP9MGAX8zymZW2MxLfj5C4p7PkPRYBxbTyawuQR9uaJZCr4bsSDhnCh2XT5CSUzHBFcVgWS7W5W9Z7SkUe4ehtymPawpwg58mpnN36sgWVSGeFXVzbQcXjfqjcUFcR8T2gBc6Ajm9wMjhCMgMNbmCMBjHKhcLghkeGWhw5wHWNZXhnDDFxnA8U4SzqdMkfTZCeaqTBaTASzKsRmGzeBGpdA9CHkLLxtJfxwj9LQ4vafzercmLJWPWGjpyeuM7CyN7ZgRbP9J6w4wzT3bxdtQXtm8RPVq5AKA6y66pSt5rdBaGbZxUjAHD2se";

        public DiscordRpcClient client;

        public main()
        {
            InitializeComponent();
            CheckProtection();


        //    Scintilla();
            DiscordRPC();
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

        protected void DiscordRPC()
        {
            client = new DiscordRpcClient("811586717433331732");
            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Details = "Running the best virus builder in the world...",
                State = "github.com/Nyxonn",
                Assets = new Assets()
                {
                    LargeImageKey = "aio3",
                    LargeImageText = "Discord AIO " + version
                }

            });
        }

      /*  void Scintilla()
        {
            scintilla1.Lexer = Lexer.Cpp;
            scintilla1.StyleResetDefault();
            scintilla1.Styles[Style.Default].Font = "Consolas";
            scintilla1.Styles[Style.Default].Size = 10;
            scintilla1.StyleClearAll();
            scintilla1.Styles[Style.Cpp.Default].ForeColor = System.Drawing.Color.Silver;
            scintilla1.Styles[Style.Cpp.Comment].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            scintilla1.Styles[Style.Cpp.CommentLine].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            scintilla1.Styles[Style.Cpp.CommentLineDoc].ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            scintilla1.Styles[Style.Cpp.Number].ForeColor = System.Drawing.Color.Olive;
            scintilla1.Styles[Style.Cpp.Word].ForeColor = System.Drawing.Color.Blue;
            scintilla1.Styles[Style.Cpp.Word2].ForeColor = System.Drawing.Color.Blue;
            scintilla1.Styles[Style.Cpp.String].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            scintilla1.Styles[Style.Cpp.Character].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            scintilla1.Styles[Style.Cpp.Verbatim].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            scintilla1.Styles[Style.Cpp.StringEol].BackColor = System.Drawing.Color.Pink;
            scintilla1.Styles[Style.Cpp.Operator].ForeColor = System.Drawing.Color.Purple;
            scintilla1.Styles[Style.Cpp.Preprocessor].ForeColor = System.Drawing.Color.Maroon;
            scintilla1.Margins[0].Width = 16;
        } */

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
