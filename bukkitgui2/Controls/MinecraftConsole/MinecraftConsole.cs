// MinecraftConsole.cs in bukkitgui2/bukkitgui2
// Created 2014/01/17
// 
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.
// 
// ©Bertware, visit http://bertware.net

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Net.Bertware.Bukkitgui2.MinecraftInterop.OutputHandler;

namespace Net.Bertware.Bukkitgui2.Controls.MinecraftConsole
{
	/// <summary>
	///     RichTextBox which has markup for Minecraft server output
	/// </summary>
	public class MinecraftConsole : RichTextBox
	{
		/// <summary>
		///     Gets or sets the message color for informative text.
		/// </summary>
		public Color MessageColorInfo { get; set; }

		/// <summary>
		///     Gets or sets the message color for text describing a player action (join, disconnect, ...).
		/// </summary>
		public Color MessageColorPlayerAction { get; set; }

		/// <summary>
		///     Gets or sets the message color for player names.
		/// </summary>
		public Color MessageColorPlayerTag { get; set; }

		/// <summary>
		///     Gets or sets the message color for plugin tags.
		/// </summary>
		public Color MessageColorPluginTag { get; set; }

		/// <summary>
		///     Gets or sets the message color for text containeing severe errors.
		/// </summary>
		public Color MessageColorSevere { get; set; }

		/// <summary>
		///     Gets or sets the message color for unknown or other text typess.
		/// </summary>
		public Color MessageColorUnknown { get; set; }

		/// <summary>
		///     Gets or sets the message color for text containing warnings.
		/// </summary>
		public Color MessageColorWarning { get; set; }

		/// <summary>
		///     Wether or not the date should be shown before each message
		/// </summary>
		public Boolean ShowDate { get; set; }

		/// <summary>
		///     Wether or not the time should be shown before each message
		/// </summary>
		public Boolean ShowTime { get; set; }

		/// <summary>
		///     Autoscroll down or not
		/// </summary>
		public Boolean Autoscroll { get; set; }

		private readonly Dictionary<MessageType, Color> _colorCache;

		/// <summary>
		///     Lines waiting to be written by the flush timer
		/// </summary>
		private readonly List<KeyValuePair<MessageType, string>> _pendingOutput =
			new List<KeyValuePair<MessageType, string>>();

		private readonly Timer _flushTimer;

		private const int MaxLines = 10000;

		private const int WmSetredraw = 0x000B;

		private const int EmGetscrollpos = 0x04DD;

		private const int EmSetscrollpos = 0x04DE;

		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref Point lParam);

		public MinecraftConsole()
		{
			_flushTimer = new Timer {Interval = 100};
			_flushTimer.Tick += FlushTimer_Tick;
			_flushTimer.Start();

			KeyDown += MinecraftConsole_KeyDown;
            LinkClicked += MinecraftConsole_LinkClicked;
			MessageColorInfo = Color.Blue;
			MessageColorPlayerAction = Color.DarkGreen;
			MessageColorSevere = Color.DarkRed;
			MessageColorWarning = Color.DarkOrange;

			CreateContextMenu();


			string[] names = Enum.GetNames(typeof (MessageType));

			_colorCache = new Dictionary<MessageType, Color>();
			foreach (string name in names)
			{
				MessageType t = (MessageType) Enum.Parse(typeof (MessageType), name);
				switch (t)
				{
					case MessageType.Info:
						_colorCache.Add(t, MessageColorInfo);
						break;
					case MessageType.JavaStatus:
					case MessageType.Warning:
						_colorCache.Add(t, MessageColorWarning);
						break;
					case MessageType.Severe:
					case MessageType.JavaStackTrace:
						_colorCache.Add(t, MessageColorSevere);
						break;
					case MessageType.PlayerJoin:
					case MessageType.PlayerLeave:
					case MessageType.PlayerKick:
					case MessageType.PlayerBan:
					case MessageType.PlayerIpBan:
						_colorCache.Add(t, MessageColorPlayerAction);
						break;
					default:
						_colorCache.Add(t, MessageColorUnknown);
						break;
				}
			}
		}

        void MinecraftConsole_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string link = e.LinkText;
            if (link.StartsWith("http")) Process.Start(e.LinkText);
        }

		/// <summary>
		///     Writes text to the Console Control
		/// </summary>
		/// <param name="type"> The message type. </param>
		/// <param name="text"> The message text. </param>
		public void WriteOutput(MessageType type, string text)
		{
			// Queue the line; the flush timer writes queued lines in batches on the UI thread,
			// so callers on other threads never wait for the UI.
			text = AddTimeStamp(text);
			lock (_pendingOutput)
			{
				_pendingOutput.Add(new KeyValuePair<MessageType, string>(type, text));
			}
		}

		private void FlushTimer_Tick(object sender, EventArgs e)
		{
			List<KeyValuePair<MessageType, string>> lines;
			lock (_pendingOutput)
			{
				if (_pendingOutput.Count == 0) return;
				lines = new List<KeyValuePair<MessageType, string>>(_pendingOutput);
				_pendingOutput.Clear();
			}

			// Appending moves the caret to the end, and the box scrolls to follow the caret.
			// Without autoscroll, or while the user has text selected (e.g. to copy it),
			// remember where the user was so we can put the view and selection back.
			bool keepView = !Autoscroll || SelectionLength > 0;
			int selectionStart = SelectionStart;
			int selectionLength = SelectionLength;
			Point scrollPos = Point.Empty;
			if (keepView) SendMessage(Handle, EmGetscrollpos, IntPtr.Zero, ref scrollPos);

			// Don't redraw for every line in the batch
			SendMessage(Handle, WmSetredraw, IntPtr.Zero, IntPtr.Zero);
			try
			{
				foreach (KeyValuePair<MessageType, string> line in lines)
				{
					SelectionStart = TextLength;
					SelectionLength = 0;
					SelectionColor = _colorCache[line.Key];
					AppendText(line.Value + Environment.NewLine);
				}

				// GetLineFromCharIndex is a single window message, Lines.Length copies the whole text
				bool cleared = false;
				if (GetLineFromCharIndex(TextLength) > MaxLines)
				{
					Clear();
					cleared = true;
				}

				if (!keepView)
				{
					Scrolldown();
				}
				else if (!cleared)
				{
					Select(selectionStart, selectionLength);
					SendMessage(Handle, EmSetscrollpos, IntPtr.Zero, ref scrollPos);
				}
			}
			finally
			{
				SendMessage(Handle, WmSetredraw, new IntPtr(1), IntPtr.Zero);
				Invalidate();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) _flushTimer.Dispose();
			base.Dispose(disposing);
		}

		/// <summary>
		///     Add a timestamp before a text message
		/// </summary>
		/// <param name="text">the text to alter</param>
		/// <returns></returns>
		private string AddTimeStamp(string text)
		{
			if (ShowTime) text = DateTime.Now.ToLongTimeString() + ' ' + text;
			if (ShowDate) text = DateTime.Now.ToShortDateString() + ' ' + text;
			return text;
		}

		/// <summary>
		///     Scroll to the bottom of the text
		/// </summary>
		private void Scrolldown()
		{
			SelectionStart = TextLength;
			ScrollToCaret();
		}


		private void CreateContextMenu()
		{
			var menuItem = new MenuItem[1];
			menuItem[0] = new MenuItem("Copy", CopySelectedText) {Enabled = true};
			ContextMenu = new ContextMenu(menuItem);
        }

	    private void CopySelectedText(object sender, EventArgs e)
	    {
	        if (string.IsNullOrEmpty(SelectedText)) return;
	        try
	        {
	            // retries for about a second if another program has the clipboard open
	            Clipboard.SetDataObject(SelectedText, true, 10, 100);
	        }
	        catch (ExternalException)
	        {
	            // clipboard stayed busy; nothing more we can do
	        }
	    }

	    private void MinecraftConsole_KeyDown(object sender, KeyEventArgs e)
		{
			// Copy on key down: on key up, Ctrl is often already released, so the first Ctrl+C was missed
			if (e.KeyCode == Keys.C && e.Control) CopySelectedText(sender, e);
		}
    }
}