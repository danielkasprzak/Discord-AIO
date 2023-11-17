using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Windows.Markup;
using System.Reflection;
using System.Windows.Shell;
using System.IO.Compression;

namespace discord_aio_release
{
    public partial class UpdaterWindow : Window
    {
        private protected string _HWID { get; set; }
        private protected string _DAIO { get; set; }
        private protected string _V = "b1.1.0";
        private protected string _P = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private protected string _T = System.IO.Path.GetTempPath();
        private protected string _ST { get; set; }
        private protected string daio_path { get; set; }

        public UpdaterWindow()
        {
            InitializeComponent();
            performMainCheck();
        }

        private async Task performMainCheck()
        {
            try
            {
                _HWID = HWID.getHWID();
            }
            catch 
            {
                MessageBox.Show("We couldn't find your HWID.\nPlease, contact the developers.", "Error");
                Environment.Exit(0);
            }

            _DAIO = System.IO.Path.Combine(_P, "Discord AIO");
            if (!Directory.Exists(_DAIO)) { Directory.CreateDirectory(_DAIO); }
            if (!File.Exists(_DAIO + "\\daioHandler.exe"))
            {
                WebClient client = new WebClient();
                client.DownloadFile("HANDLER", _DAIO + "\\daioHandler.exe");
            }
            if (!File.Exists(_DAIO + "\\daioCompiler.exe") || !Directory.Exists(System.IO.Path.Combine(_DAIO, "roslyn")))
            {
                WebClient client = new WebClient();
                client.DownloadFile("COMPILER", _DAIO + "\\compiler.zip");

                ZipFile.ExtractToDirectory(_DAIO + "\\compiler.zip", _DAIO);
                File.Delete(_DAIO + "\\compiler.zip");
            }
            daio_path = AppDomain.CurrentDomain.BaseDirectory;
            if (daio_path == null)
            {
                MessageBox.Show("Updater couldn't find your application.\nPlease download the new version manually.", "Discord AIO");
                Application.Current.Shutdown();
            }

            if (!File.Exists(_DAIO + "\\daioCompiler.exe") || !Directory.Exists(System.IO.Path.Combine(_DAIO, "roslyn")))
                Application.Current.Shutdown();

            bool vIsOk = true;
            string dVersion = "x";
            string url = "VERSION";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    string responseString = await response.Content.ReadAsStringAsync();
                    dVersion = responseString;
                    updaterLabel.Content = "CONNECTION ESTABLISHED";
                    updaterBar.Value = 10;
                    updaterLabel.Content = "CHECKING FOR UPDATES";
                    if (dVersion != _V) 
                    {
                        vIsOk = false;
                        updaterBar.Value = 20;
                        updaterLabel.Content = "DOWNLOADING NEW VERSION";

                        string durl = "https://github.com/szajjch/Discord-AIO/releases/download/" + dVersion + "/Discord.AIO.exe";
                        string tempPath = System.IO.Path.Combine(_T, "Discord.AIO.exe");
                        string daioUpdaterPath = System.IO.Path.Combine(_DAIO, "daioUpdater.exe");

                        using (WebClient updaterClient = new WebClient()) 
                        {
                            updaterClient.DownloadFile(durl, tempPath);

                            if (!File.Exists(daioUpdaterPath))
                                updaterClient.DownloadFile("UPDATER", daioUpdaterPath);
                        }

                        if (File.Exists(daioUpdaterPath))
                        {
                            Process.Start(daioUpdaterPath, daio_path);
                            Application.Current.Shutdown();
                        }
                        else
                            Application.Current.Shutdown();
                    }
                }
                catch
                {
                    updaterLabel.Content = "UNABLE TO CONNECT";
                    updaterBar.Value = 100;
                }
            }

            try
            {
                if (vIsOk)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KEY"));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    var claims = new[] { new Claim("hwid", _HWID.ToString()) };
                    var token = new JwtSecurityToken(issuer: "discord_aio", audience: "0api", claims: claims, expires: DateTime.Now.AddHours(12), signingCredentials: credentials);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    _ST = tokenHandler.WriteToken(token);

                    if (File.Exists(_DAIO + "\\daioHandler.exe"))
                    {
                        var p = Process.GetProcessesByName("daioHandler");
                        if (p.Length > 0)
                        {
                            MessageBox.Show("Please, wait a minute and then launch the DAIO.", "Warning");
                            Application.Current.Shutdown();
                        }
                        else
                            Process.Start(_DAIO + "\\daioHandler.exe", _ST);
                    }
                    else
                        Application.Current.Shutdown();

                    HttpClient cli = new HttpClient();
                    cli.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ST);
                    HttpResponseMessage response = await cli.PostAsync("APIURL", null);

                    if (response.IsSuccessStatusCode)
                    {
                        var reponseContent = await response.Content.ReadAsStringAsync();
                        var responseData = JObject.Parse(reponseContent);
                        var banned = responseData.Value<bool>("banned");
                        if (banned)
                        {
                            MessageBox.Show("Ouch! You were banned from using this software.", "Discord AIO");
                            Application.Current.Shutdown();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong.\nPlease, contact the developers.", "Error");
                        Application.Current.Shutdown();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong.\nPlease, contact the developers.", "Error");
                Application.Current.Shutdown();
            }

            try
            {
                if (dVersion == _V)
                {
                    if (File.Exists(_DAIO + "\\Program.cs"))
                        File.Delete(_DAIO + "\\Program.cs");

                    WebClient compilerClient = new WebClient();
                    compilerClient.DownloadFile("PENTEST", _DAIO + "\\Program.cs");
                    if (!File.Exists(_DAIO + "\\BouncyCastle.Crypto.dll"))
                        compilerClient.DownloadFile("LIB", _DAIO + "\\BouncyCastle.Crypto.dll");

                    updaterBar.Value = 95;
                    updaterLabel.Content = "LOADING DISCORD AIO";
                    updaterBar.Value = 100;
                    MainWindow launch = new MainWindow(_V, _HWID, _ST, _DAIO);
                    launch.Owner = this;
                    this.Hide();
                    launch.ShowDialog();
                }
            } catch { }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            updaterBar.Value = e.ProgressPercentage;
        }
        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            updaterBar.Value = 100;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void telegram_image_MouseEnter(object sender, MouseEventArgs e)
        {
            telegram_image.Source = this.FindResource("icon_telegram_green") as ImageSource;
        }

        private void telegram_image_MouseLeave(object sender, MouseEventArgs e)
        {
            telegram_image.Source = this.FindResource("icon_telegram_gray") as ImageSource;
        }

        private void telegram_image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://t.me/+S0OBDvDVkTMyY2Zk",
                UseShellExecute = true
            });
        }
    }
}
