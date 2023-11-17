using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace daioUpdater
{
    public partial class daioUpdater : Form
    {
        public daioUpdater(string[] args)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.Visible = false;
            Update(args[0]);
        }

        private void Update(string app_path)
        {
            string _DP = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Discord AIO");
            string correctPath = Path.Combine(app_path, "Discord AIO.exe");
            string tempPath = Path.Combine(Path.GetTempPath(), "Discord.AIO.exe");
            try
            {
                if (File.Exists(tempPath))
                {
                    File.Copy(tempPath, correctPath, true);
                    File.Delete(tempPath);
                    MessageBox.Show("Application has been updated.\nPlease restart the application.", "Discord AIO");
                }
            }
            catch {}
            Environment.Exit(0);
        }
    }
}
