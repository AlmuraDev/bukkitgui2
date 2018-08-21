// QuickButtons.cs in bukkitgui2/bukkitgui2
// Created 2014/01/17
// 
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.
// 
// ©Bertware, visit http://bertware.net

using System;
using System.Windows.Forms;
using MetroFramework.Controls;
using Net.Bertware.Bukkitgui2.AddOn.Starter;
using Net.Bertware.Bukkitgui2.MinecraftInterop.ProcessHandler;

namespace Net.Bertware.Bukkitgui2.Controls.QuickButtons
{
	public partial class QuickButtons : MetroUserControl
	{
		public QuickButtons()
		{
			InitializeComponent();
            new ToolTip().SetToolTip(btnKill, "Kills the server instance");
            new ToolTip().SetToolTip(btnRestart, "Restarts the server");
			ProcessHandler.ServerStatusChanged += HandleServerStatusChange;
		}

		public event EventHandler TaskButtonPressed;

		protected virtual void OnTaskButtonPressed()
		{
			var handler = TaskButtonPressed;
		    handler?.Invoke(this, EventArgs.Empty);
		}

		private void HandleServerStatusChange(ServerState currentState)
		{
			//suport for calls from other threads
			if (InvokeRequired)
			{
				Invoke((MethodInvoker) (() => HandleServerStatusChange(currentState)));
			}
			else
			{
			    var tooltip = new ToolTip();
                switch (currentState)
				{
					case ServerState.Starting:
						btnStart.Enabled = false;
						btnRestart.Enabled = false;
					    btnKill.Enabled = true;
					    btnStart.Image = Properties.Resources.hourglass;

                        break;
					case ServerState.Running:
					    tooltip.SetToolTip(btnStart, ProcessHandler.Server.IsLocal ? "Stop the server" : "Disconnect from the server");
					    btnStart.Enabled = true;
					    btnStart.Image = Properties.Resources.stop;
                        btnKill.Enabled = true;
						btnRestart.Enabled = true;

						break;
					case ServerState.Stopping:
						btnStart.Enabled = false;
						btnRestart.Enabled = false;
					    btnKill.Enabled = true;
					    btnStart.Image = Properties.Resources.hourglass;

						break;
					case ServerState.Stopped:
					    tooltip.SetToolTip(btnStart, ProcessHandler.Server.IsLocal ? "Start the server" : "Connect to the server");
                        btnStart.Enabled = true;
					    btnStart.Image = Properties.Resources.start;
                        btnRestart.Enabled = false;
					    btnKill.Enabled = false;

						break;
				}
			}
		}

		private void BtnStartStopClick(object sender, EventArgs e)
		{
			//suport for calls from other threads
			if (InvokeRequired)
			{
				Invoke((MethodInvoker) (() => BtnStartStopClick(sender, e)));
			}
			else
			{
				if (ProcessHandler.IsRunning)
				{
					if (ProcessHandler.Server.IsLocal)
					{
						ProcessHandler.StopServer(); // stop running server
					}
					else
					{
						ProcessHandler.StopServerProcess(); // stop the running process without stopping the server
					}
				}
				else
				{
					Starter.StartServer(); // Launch with tab settings
				}
			}
		}

		private void btnCustom_Click(object sender, EventArgs e)
		{
			OnTaskButtonPressed();
		}

		private void btnRestart_Click(object sender, EventArgs e)
		{
			Starter.RestartServer(); //TODO: Fix restart for jsonapi
		}

        private void btnKill_Click(object sender, EventArgs e)
        {
            var result =
                MessageBox.Show(
                    "Are you sure you want to kill the server instance? This could lead to data corruption among other issues.",
                    "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

            if (result == DialogResult.Yes)
            {
                ProcessHandler.KillServer();
            }
        }
    }
}