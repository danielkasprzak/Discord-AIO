using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace daioHandler
{
    public partial class daioHandler : Form
    {
        private protected string _TK {get; set;}
        public daioHandler(string[] args)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.Visible = false;
            _TK = args[0];
            Start();
        }

        private void Start()
        {
            var timer = new System.Timers.Timer(60000);
            timer.Elapsed += Tick;
            timer.Start();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
        }

        private async void Tick(Object source, ElapsedEventArgs e)
        {
            var processes = Process.GetProcessesByName("Discord AIO");
            if (!(processes.Length > 0)) {
                HttpClient c = new HttpClient();
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _TK);
                var response = await c.PostAsync("APIURL", null);
                Environment.Exit(0);
            }
        }
    }
}
