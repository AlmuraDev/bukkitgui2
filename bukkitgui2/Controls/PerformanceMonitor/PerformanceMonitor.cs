// PerformanceMonitor.cs in bukkitgui2/bukkitgui2
// Created 2014/01/30
// 
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.
// 
// ©Bertware, visit http://bertware.net

using System;
using System.Globalization;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using MetroFramework.Controls;
using Net.Bertware.Bukkitgui2.Core.Util.Performance;
using Net.Bertware.Bukkitgui2.MinecraftInterop.ProcessHandler;
using Timer = System.Timers.Timer;

namespace Net.Bertware.Bukkitgui2.Controls.PerformanceMonitor
{
	public partial class PerformanceMonitor : MetroUserControl
	{
		private Timer _tmrRefresh = new Timer(1000);

		public PerformanceMonitor()
		{
			InitializeComponent();
		}

		private void PerformanceMonitor_Load(object sender, EventArgs e)
		{
			// do not draw in design mode!
			if (DesignMode) return;

			_tmrRefresh = new Timer(1000);
			_tmrRefresh.Elapsed += RefreshData;
		}

		private void PerformanceMonitor_VisibleChanged(object sender, EventArgs e)
		{
			// do not draw in design mode!
			if (DesignMode)
			{
				_tmrRefresh.Enabled = false;
				return;
			}

			if (_tmrRefresh != null) _tmrRefresh.Enabled = Visible; //Do not refresh if invisible
		}

		/// <summary>
		///     1 while a refresh is running, so slow refreshes don't pile up
		/// </summary>
		private int _refreshing;

		private void RefreshData(object sender, ElapsedEventArgs e)
		{
			if (Interlocked.Exchange(ref _refreshing, 1) == 1) return;
			try
			{
				// Read the counters here, on the timer thread. The first access creates the performance counters
				// and runs a WMI query, which takes seconds; doing that on the UI thread froze the window at startup.
				MemoryCounter guiRam = PerformanceMonitorDataSource.GuiRamCounter;
				MemoryCounter totalRam = PerformanceMonitorDataSource.TotalRamCounter;
				MemoryCounter serverRam = PerformanceMonitorDataSource.ServerRamCounter;
				CpuCounter guiCpu = PerformanceMonitorDataSource.GuiCpuCounter;
				CpuCounter totalCpu = PerformanceMonitorDataSource.TotalCpuCounter;
				CpuCounter serverCpu = PerformanceMonitorDataSource.ServerCpuCounter;

				int guiRamPct = guiRam.MemoryUsagePct;
				string guiRamText = ConstructRamLabelText(guiRam);
				int totalRamPct = totalRam.MemoryUsagePct;
				string totalRamText = ConstructRamLabelText(totalRam);
				int serverRamPct = serverRam.MemoryUsagePct;
				string serverRamText = ConstructRamLabelText(serverRam);
				int guiCpuPct = guiCpu.CpuUsage;
				string guiCpuText = ConstructCpuLabelText(guiCpu);
				int totalCpuPct = totalCpu.CpuUsage;
				string totalCpuText = ConstructCpuLabelText(totalCpu);
				int serverCpuPct = serverCpu.CpuUsage;
				string serverCpuText = ConstructCpuLabelText(serverCpu);

				TimeSpan uptime = ProcessHandler.Uptime;
				string uptimeText = uptime.Hours.ToString().PadLeft(2, '0') + ":" +
				                    uptime.Minutes.ToString().PadLeft(2, '0') + ":" +
				                    uptime.Seconds.ToString().PadLeft(2, '0');

				if (!IsHandleCreated || IsDisposed) return;
				BeginInvoke(new MethodInvoker(() =>
				{
					ProgBarGuiRam.Value = guiRamPct;
					LblGuiRamUsageValue.Text = guiRamText;
					ProgBarTotalRam.Value = totalRamPct;
					LblTotalRamUsageValue.Text = totalRamText;
					ProgBarServerRam.Value = serverRamPct;
					LblServerRamUsageValue.Text = serverRamText;
					ProgBarGuiCpu.Value = guiCpuPct;
					lblGuiCpuUsageValue.Text = guiCpuText;
					ProgBarTotalCpu.Value = totalCpuPct;
					lblTotalCpuUsageValue.Text = totalCpuText;
					ProgBarServerCpu.Value = serverCpuPct;
					lblServerCpuUsageValue.Text = serverCpuText;
					LblUptimeValue.Text = uptimeText;
				}));
			}
			catch (InvalidOperationException)
			{
				// control was disposed while we were reading the counters
			}
			finally
			{
				Interlocked.Exchange(ref _refreshing, 0);
			}
		}

		private static string ConstructRamLabelText(MemoryCounter counter)
		{
			return counter.MemoryUsageMb + "Mb (" +
			       counter.MemoryUsagePct.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') +
			       "%)";
		}

		private static string ConstructCpuLabelText(CpuCounter counter)
		{
			return counter.CpuUsage + "%";
		}
	}
}