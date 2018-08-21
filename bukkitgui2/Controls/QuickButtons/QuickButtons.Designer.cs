using MetroFramework.Controls;
using Net.Bertware.Bukkitgui2.Core;

namespace Net.Bertware.Bukkitgui2.Controls.QuickButtons
{
	partial class QuickButtons
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
            this.btnCustom = new MetroFramework.Controls.MetroButton();
            this.metroToolTip = new MetroFramework.Components.MetroToolTip();
            this.btnKill = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCustom
            // 
            this.btnCustom.Location = new System.Drawing.Point(3, 49);
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Size = new System.Drawing.Size(132, 23);
            this.btnCustom.TabIndex = 2;
            this.btnCustom.Text = "Run Task";
            this.metroToolTip.SetToolTip(this.btnCustom, "Run a task.\r\nNote: you should create a task in the task manager first!\r\nUse the \"" +
        "task button\" trigger to associate a task with this trigger.\r\n");
            this.btnCustom.UseSelectable = true;
            this.btnCustom.Click += new System.EventHandler(this.btnCustom_Click);
            // 
            // metroToolTip
            // 
            this.metroToolTip.AutoPopDelay = 5000;
            this.metroToolTip.InitialDelay = 250;
            this.metroToolTip.ReshowDelay = 100;
            this.metroToolTip.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip.StyleManager = null;
            this.metroToolTip.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btnKill
            // 
            this.btnKill.Enabled = false;
            this.btnKill.Image = global::Net.Bertware.Bukkitgui2.Properties.Resources.kill;
            this.btnKill.Location = new System.Drawing.Point(95, 3);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(40, 40);
            this.btnKill.TabIndex = 5;
            this.btnKill.UseVisualStyleBackColor = true;
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Image = global::Net.Bertware.Bukkitgui2.Properties.Resources.restart;
            this.btnRestart.Location = new System.Drawing.Point(49, 3);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(40, 40);
            this.btnRestart.TabIndex = 4;
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnStart
            // 
            this.btnStart.Image = global::Net.Bertware.Bukkitgui2.Properties.Resources.start;
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(40, 40);
            this.btnStart.TabIndex = 3;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStartStopClick);
            // 
            // QuickButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnKill);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnCustom);
            this.Name = "QuickButtons";
            this.Size = new System.Drawing.Size(139, 78);
            this.ResumeLayout(false);

		}

		#endregion
		private MetroButton btnCustom;
		private MetroFramework.Components.MetroToolTip metroToolTip;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnKill;
    }
}
