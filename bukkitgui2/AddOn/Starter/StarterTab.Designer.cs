using System.Windows.Forms;
using MetroFramework.Controls;
using Net.Bertware.Bukkitgui2.Core;

namespace Net.Bertware.Bukkitgui2.AddOn.Starter
{
	partial class StarterTab
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.GBServer = new System.Windows.Forms.GroupBox();
            this.BtnBrowseJarFile = new MetroFramework.Controls.MetroButton();
            this.BtnLaunch = new MetroFramework.Controls.MetroButton();
            this.label7 = new MetroFramework.Controls.MetroLabel();
            this.TxtOptFlag = new MetroFramework.Controls.MetroTextBox();
            this.label6 = new MetroFramework.Controls.MetroLabel();
            this.TxtOptArg = new MetroFramework.Controls.MetroTextBox();
            this.label5 = new MetroFramework.Controls.MetroLabel();
            this.NumMaxRam = new System.Windows.Forms.NumericUpDown();
            this.TBMaxRam = new MetroFramework.Controls.MetroTrackBar();
            this.label4 = new MetroFramework.Controls.MetroLabel();
            this.NumMinRam = new System.Windows.Forms.NumericUpDown();
            this.TBMinRam = new MetroFramework.Controls.MetroTrackBar();
            this.label3 = new MetroFramework.Controls.MetroLabel();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.CBJavaVersion = new MetroFramework.Controls.MetroComboBox();
            this.TxtJarFile = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.CBServerType = new MetroFramework.Controls.MetroComboBox();
            this.GBCustomSettings = new System.Windows.Forms.GroupBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.metroStyleExtender = new MetroFramework.Components.MetroStyleExtender(this.components);
            this.metroToolTip = new MetroFramework.Components.MetroToolTip();
            this.GBServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumMaxRam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinRam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // GBServer
            // 
            this.metroStyleExtender.SetApplyMetroTheme(this.GBServer, true);
            this.GBServer.BackColor = System.Drawing.Color.Transparent;
            this.GBServer.Controls.Add(this.BtnBrowseJarFile);
            this.GBServer.Controls.Add(this.BtnLaunch);
            this.GBServer.Controls.Add(this.label7);
            this.GBServer.Controls.Add(this.TxtOptFlag);
            this.GBServer.Controls.Add(this.label6);
            this.GBServer.Controls.Add(this.TxtOptArg);
            this.GBServer.Controls.Add(this.label5);
            this.GBServer.Controls.Add(this.NumMaxRam);
            this.GBServer.Controls.Add(this.TBMaxRam);
            this.GBServer.Controls.Add(this.label4);
            this.GBServer.Controls.Add(this.NumMinRam);
            this.GBServer.Controls.Add(this.TBMinRam);
            this.GBServer.Controls.Add(this.label3);
            this.GBServer.Controls.Add(this.label2);
            this.GBServer.Controls.Add(this.CBJavaVersion);
            this.GBServer.Controls.Add(this.TxtJarFile);
            this.GBServer.Controls.Add(this.label1);
            this.GBServer.Controls.Add(this.CBServerType);
            this.GBServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GBServer.Location = new System.Drawing.Point(0, 0);
            this.GBServer.Name = "GBServer";
            this.GBServer.Size = new System.Drawing.Size(800, 500);
            this.GBServer.TabIndex = 0;
            this.GBServer.TabStop = false;
            this.GBServer.Text = "Server";
            // 
            // BtnBrowseJarFile
            // 
            this.BtnBrowseJarFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBrowseJarFile.Location = new System.Drawing.Point(764, 90);
            this.BtnBrowseJarFile.Name = "BtnBrowseJarFile";
            this.BtnBrowseJarFile.Size = new System.Drawing.Size(30, 20);
            this.BtnBrowseJarFile.TabIndex = 17;
            this.BtnBrowseJarFile.Text = "...";
            this.BtnBrowseJarFile.UseSelectable = true;
            this.BtnBrowseJarFile.Click += new System.EventHandler(this.BtnBrowseJarFile_Click);
            // 
            // BtnLaunch
            // 
            this.BtnLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconAlignment(this.BtnLaunch, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.BtnLaunch.Location = new System.Drawing.Point(152, 270);
            this.BtnLaunch.Name = "BtnLaunch";
            this.BtnLaunch.Size = new System.Drawing.Size(642, 23);
            this.BtnLaunch.TabIndex = 16;
            this.BtnLaunch.Text = "Launch Server";
            this.BtnLaunch.UseSelectable = true;
            this.BtnLaunch.Click += new System.EventHandler(this.BtnLaunch_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Optional flags:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtOptFlag
            // 
            this.TxtOptFlag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconAlignment(this.TxtOptFlag, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.TxtOptFlag.Lines = new string[0];
            this.TxtOptFlag.Location = new System.Drawing.Point(152, 244);
            this.TxtOptFlag.MaxLength = 32767;
            this.TxtOptFlag.Name = "TxtOptFlag";
            this.TxtOptFlag.PasswordChar = '\0';
            this.TxtOptFlag.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtOptFlag.SelectedText = "";
            this.TxtOptFlag.Size = new System.Drawing.Size(642, 20);
            this.TxtOptFlag.TabIndex = 14;
            this.TxtOptFlag.UseSelectable = true;
            this.TxtOptFlag.TextChanged += new System.EventHandler(this.TxtOptFlag_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Optional arguments:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtOptArg
            // 
            this.TxtOptArg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconAlignment(this.TxtOptArg, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.TxtOptArg.Lines = new string[0];
            this.TxtOptArg.Location = new System.Drawing.Point(152, 218);
            this.TxtOptArg.MaxLength = 32767;
            this.TxtOptArg.Name = "TxtOptArg";
            this.TxtOptArg.PasswordChar = '\0';
            this.TxtOptArg.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtOptArg.SelectedText = "";
            this.TxtOptArg.Size = new System.Drawing.Size(642, 20);
            this.TxtOptArg.TabIndex = 12;
            this.TxtOptArg.UseSelectable = true;
            this.TxtOptArg.TextChanged += new System.EventHandler(this.TxtOptArg_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Maximum Ram [Mb] :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NumMaxRam
            // 
            this.errorProvider.SetIconAlignment(this.NumMaxRam, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.NumMaxRam.Location = new System.Drawing.Point(152, 176);
            this.NumMaxRam.Margin = new System.Windows.Forms.Padding(3, 12, 3, 12);
            this.NumMaxRam.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.NumMaxRam.Name = "NumMaxRam";
            this.NumMaxRam.Size = new System.Drawing.Size(73, 20);
            this.NumMaxRam.TabIndex = 10;
            this.metroToolTip.SetToolTip(this.NumMaxRam, "Recommended value: 1024mb");
            this.NumMaxRam.ValueChanged += new System.EventHandler(this.NumMaxRam_ValueChanged);
            // 
            // TBMaxRam
            // 
            this.TBMaxRam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBMaxRam.BackColor = System.Drawing.Color.Transparent;
            this.errorProvider.SetIconAlignment(this.TBMaxRam, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.TBMaxRam.LargeChange = 512;
            this.TBMaxRam.Location = new System.Drawing.Point(231, 167);
            this.TBMaxRam.Maximum = 4096;
            this.TBMaxRam.Name = "TBMaxRam";
            this.TBMaxRam.Size = new System.Drawing.Size(563, 45);
            this.TBMaxRam.SmallChange = 256;
            this.TBMaxRam.TabIndex = 9;
            this.metroToolTip.SetToolTip(this.TBMaxRam, "Recommended value: 1024mb");
            this.TBMaxRam.ValueChanged += new System.EventHandler(this.TbMaxRamScroll);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Minimum Ram [Mb] :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NumMinRam
            // 
            this.errorProvider.SetIconAlignment(this.NumMinRam, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.NumMinRam.Location = new System.Drawing.Point(152, 125);
            this.NumMinRam.Margin = new System.Windows.Forms.Padding(3, 12, 3, 12);
            this.NumMinRam.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.NumMinRam.Name = "NumMinRam";
            this.NumMinRam.Size = new System.Drawing.Size(73, 20);
            this.NumMinRam.TabIndex = 7;
            this.metroToolTip.SetToolTip(this.NumMinRam, "Recommended value: 128mb");
            this.NumMinRam.ValueChanged += new System.EventHandler(this.NumMinRam_ValueChanged);
            // 
            // TBMinRam
            // 
            this.TBMinRam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBMinRam.BackColor = System.Drawing.Color.Transparent;
            this.errorProvider.SetIconAlignment(this.TBMinRam, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.TBMinRam.LargeChange = 512;
            this.TBMinRam.Location = new System.Drawing.Point(231, 116);
            this.TBMinRam.Maximum = 4096;
            this.TBMinRam.Name = "TBMinRam";
            this.TBMinRam.Size = new System.Drawing.Size(563, 45);
            this.TBMinRam.SmallChange = 256;
            this.TBMinRam.TabIndex = 6;
            this.metroToolTip.SetToolTip(this.TBMinRam, "Recommended value: 128mb");
            this.TBMinRam.ValueChanged += new System.EventHandler(this.TbMinRamScroll);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Jar file:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Java version:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBJavaVersion
            // 
            this.CBJavaVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBJavaVersion.FormattingEnabled = true;
            this.CBJavaVersion.ItemHeight = 23;
            this.CBJavaVersion.Location = new System.Drawing.Point(152, 55);
            this.CBJavaVersion.Name = "CBJavaVersion";
            this.CBJavaVersion.Size = new System.Drawing.Size(642, 29);
            this.CBJavaVersion.TabIndex = 3;
            this.metroToolTip.SetToolTip(this.CBJavaVersion, "The java version to use.\r\nOnly installed versions are selectable.");
            this.CBJavaVersion.UseSelectable = true;
            this.CBJavaVersion.SelectedIndexChanged += new System.EventHandler(this.CbJavaVersionSelectedIndexChanged);
            // 
            // TxtJarFile
            // 
            this.TxtJarFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconAlignment(this.TxtJarFile, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.TxtJarFile.Lines = new string[0];
            this.TxtJarFile.Location = new System.Drawing.Point(152, 90);
            this.TxtJarFile.MaxLength = 32767;
            this.TxtJarFile.Name = "TxtJarFile";
            this.TxtJarFile.PasswordChar = '\0';
            this.TxtJarFile.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtJarFile.SelectedText = "";
            this.TxtJarFile.Size = new System.Drawing.Size(606, 20);
            this.TxtJarFile.TabIndex = 2;
            this.metroToolTip.SetToolTip(this.TxtJarFile, "The server file (java .jar file).\r\nIf you don\'t have one, download one using the " +
        "buttons on the right.");
            this.TxtJarFile.UseSelectable = true;
            this.TxtJarFile.TextChanged += new System.EventHandler(this.TxtJarFile_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server type:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBServerType
            // 
            this.CBServerType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBServerType.FormattingEnabled = true;
            this.CBServerType.ItemHeight = 23;
            this.CBServerType.Location = new System.Drawing.Point(152, 20);
            this.CBServerType.Name = "CBServerType";
            this.CBServerType.Size = new System.Drawing.Size(642, 29);
            this.CBServerType.TabIndex = 0;
            this.metroToolTip.SetToolTip(this.CBServerType, "The minecraft server type you want to use");
            this.CBServerType.UseSelectable = true;
            this.CBServerType.SelectedIndexChanged += new System.EventHandler(this.CbServerTypeSelectedIndexChanged);
            // 
            // GBCustomSettings
            // 
            this.metroStyleExtender.SetApplyMetroTheme(this.GBCustomSettings, true);
            this.GBCustomSettings.BackColor = System.Drawing.Color.Transparent;
            this.GBCustomSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GBCustomSettings.Location = new System.Drawing.Point(0, 383);
            this.GBCustomSettings.Name = "GBCustomSettings";
            this.GBCustomSettings.Size = new System.Drawing.Size(800, 117);
            this.GBCustomSettings.TabIndex = 2;
            this.GBCustomSettings.TabStop = false;
            this.GBCustomSettings.Text = "Server specific settings";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // metroToolTip
            // 
            this.metroToolTip.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip.StyleManager = null;
            this.metroToolTip.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // StarterTab
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.GBCustomSettings);
            this.Controls.Add(this.GBServer);
            this.Name = "StarterTab";
            this.Size = new System.Drawing.Size(800, 500);
            this.GBServer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumMaxRam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinRam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.GroupBox GBServer;
        private MetroLabel label1;
        private MetroComboBox CBServerType;
        private System.Windows.Forms.GroupBox GBCustomSettings;
        private MetroButton BtnLaunch;
        private MetroLabel label7;
        private MetroTextBox TxtOptFlag;
        private MetroLabel label6;
        private MetroTextBox TxtOptArg;
        private MetroLabel label5;
        private System.Windows.Forms.NumericUpDown NumMaxRam;
        private MetroTrackBar TBMaxRam;
        private MetroLabel label4;
        private System.Windows.Forms.NumericUpDown NumMinRam;
        private MetroTrackBar TBMinRam;
        private MetroLabel label3;
        private MetroLabel label2;
        private MetroComboBox CBJavaVersion;
        private MetroTextBox TxtJarFile;
        private MetroButton BtnBrowseJarFile;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private MetroFramework.Components.MetroStyleExtender metroStyleExtender;
		private MetroFramework.Components.MetroToolTip metroToolTip;
	}
}
