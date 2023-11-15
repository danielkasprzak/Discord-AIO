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
        private protected string _V = "b1.0.0";
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
                client.DownloadFile("HANDLERURL", _DAIO + "\\daioHandler.exe");
            }
            if (!File.Exists(_DAIO + "\\daioCompiler.exe") || !Directory.Exists(System.IO.Path.Combine(_DAIO, "roslyn")))
            {
                WebClient client = new WebClient();
                client.DownloadFile("COMPILERURL", _DAIO + "\\compiler.zip");

                ZipFile.ExtractToDirectory(_DAIO + "\\compiler.zip", _DAIO);
                File.Delete(_DAIO + "\\compiler.zip");
            }
            daio_path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!File.Exists(_DAIO + "\\daioCompiler.exe") || !Directory.Exists(System.IO.Path.Combine(_DAIO, "roslyn")))
                Application.Current.Shutdown();

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SECRETKEY"));
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
            catch
            {
                MessageBox.Show("Something went wrong.\nPlease, contact the developers.", "Error");
                Application.Current.Shutdown();
            }

            string url = "VERSIONURL";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    string responseString = await response.Content.ReadAsStringAsync();
                    updaterLabel.Content = "CONNECTION ESTABLISHED";
                    updaterBar.Value = 10;
                    updaterLabel.Content = "CHECKING FOR UPDATES";
                    if (responseString == _V)
                    {
                        if (File.Exists(_DAIO + "\\Program.cs"))
                            File.Delete(_DAIO + "\\Program.cs");

                        WebClient compilerClient = new WebClient();
                        compilerClient.DownloadFile("PENTESTURL", _DAIO + "\\Program.cs");
                        if (!File.Exists(_DAIO + "\\BouncyCastle.Crypto.dll"))
                            compilerClient.DownloadFile("LIBURL", _DAIO + "\\BouncyCastle.Crypto.dll");

                        updaterBar.Value = 95;
                        updaterLabel.Content = "LOADING DISCORD AIO";
                        updaterBar.Value = 100;
                        MainWindow launch = new MainWindow(_V, _HWID, _ST, _DAIO);
                        launch.Owner = this;
                        this.Hide();
                        launch.ShowDialog();
                    }
                    else
                    {
                        updaterBar.Value = 20;
                        updaterLabel.Content = "DOWNLOADING NEW VERSION";


                        string durl = "https://github.com/szajjch/Discord-AIO/releases/download/" + responseString + "/Discord.AIO.exe";
                        string tempPath = System.IO.Path.Combine(_T, "Discord.AIO.exe");
                        if (File.Exists(tempPath))
                            File.Delete(tempPath);
                        DownloadFile(durl, tempPath);

                        if (!File.Exists(_DAIO + "\\daioUpdater.exe"))
                        {
                            WebClient cl = new WebClient();
                            cl.DownloadFile("UPDATERURL", _DAIO + "\\daioUpdater.exe");
                        }
                        if (File.Exists(_DAIO + "\\daioUpdater.exe"))
                        {
                            Process.Start(_DAIO + "\\daioUpdater.exe", daio_path);
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
        }

        private void DownloadFile(string url, string path)
        {
            try 
            {
                WebClient client = new WebClient();
                client.DownloadFileAsync(new Uri(url), path);
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
            }
            catch 
            {
                updaterLabel.Content = "UNABLE TO CONNECT";
                updaterBar.Value = 100;
            }
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
