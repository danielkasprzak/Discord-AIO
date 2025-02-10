using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO.Compression;

namespace discord_aio_release
{
    public partial class UpdaterWindow : Window
    {
        private protected string _HWID { get; set; }
        private protected string _DAIO { get; set; }
        private protected string _V = "b1.1.1";
        private protected string _P = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private protected string _T = System.IO.Path.GetTempPath();
        private protected string daio_path { get; set; }

        public UpdaterWindow()
        {
            InitializeComponent();
            _HWID = string.Empty;
            _DAIO = string.Empty;
            daio_path = string.Empty;
            performMainCheck().ConfigureAwait(false);
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

            if (!File.Exists(_DAIO + "\\daioCompiler.exe") || !Directory.Exists(System.IO.Path.Combine(_DAIO, "roslyn")))
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync("http://162.19.227.17/daio/compiler/compiler.zip");
                    var fileBytes = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync(_DAIO + "\\compiler.zip", fileBytes);
                }

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
            string url = "https://pastebin.com/raw/KZq5X31D";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    string responseString = await response.Content.ReadAsStringAsync();
                    updaterLabel.Content = "CONNECTION ESTABLISHED";
                    updaterBar.Value = 10;
                    updaterLabel.Content = "CHECKING FOR UPDATES";
                    if (responseString != _V) 
                    {
                        vIsOk = false;
                        updaterBar.Value = 20;
                        updaterLabel.Content = "DOWNLOADING NEW VERSION";

                        string durl = "https://github.com/szajjch/Discord-AIO/releases/download/" + responseString + "/Discord.AIO.exe";
                        string tempPath = Path.Combine(_T, "Discord.AIO.exe");
                        string daioUpdaterPath = Path.Combine(_DAIO, "daioUpdater.exe");

                        using (HttpClient downloadClient = new HttpClient())
                        {
                            var downloadResponse = await downloadClient.GetAsync(durl);
                            var fileBytes = await downloadResponse.Content.ReadAsByteArrayAsync();
                            await File.WriteAllBytesAsync(tempPath, fileBytes);

                            if (!File.Exists(daioUpdaterPath))
                            {
                                var updaterResponse = await downloadClient.GetAsync("http://162.19.227.17/daio/updater/daioUpdater.exe");
                                var updaterBytes = await updaterResponse.Content.ReadAsByteArrayAsync();
                                await File.WriteAllBytesAsync(daioUpdaterPath, updaterBytes);
                            }
                        }

                        if (File.Exists(daioUpdaterPath))
                        {
                            if (!string.IsNullOrEmpty(daio_path))
                            {
                                Process.Start(daioUpdaterPath, daio_path);
                                Application.Current.Shutdown();
                            }
                            else
                            {
                                MessageBox.Show("Updater couldn't find your application path.\nPlease download the new version manually.", "Discord AIO");
                                Application.Current.Shutdown();
                            }
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
                    HttpClient cli = new HttpClient();
                    var content = new StringContent($"\"{_HWID}\"", Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await cli.PostAsync("https://localhost:7118/user/check", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var reponseContent = await response.Content.ReadAsStringAsync();
                        if (reponseContent.Contains("User is banned"))
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
                if (vIsOk)
                {
                    //if (File.Exists(_DAIO + "\\Program.cs"))
                    //    File.Delete(_DAIO + "\\Program.cs");

                    //WebClient compilerClient = new WebClient();
                    //compilerClient.DownloadFile("http://162.19.227.17/daio/stub/Program.cs", _DAIO + "\\Program.cs");
                    //if (!File.Exists(_DAIO + "\\BouncyCastle.Crypto.dll"))
                    //    compilerClient.DownloadFile("https://cdn.discordapp.com/attachments/1167109154855456768/1167883584577753230/BouncyCastle.Crypto.dll", _DAIO + "\\BouncyCastle.Crypto.dll");

                    updaterBar.Value = 95;
                    updaterLabel.Content = "LOADING DISCORD AIO";
                    updaterBar.Value = 100;
                    MainWindow launch = new MainWindow(_V, _HWID, _DAIO);
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
