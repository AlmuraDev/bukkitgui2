﻿// MinecraftServerBase.cs in bukkitgui2/bukkitgui2
// Created 2014/01/17
// 
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.
// 
// ©Bertware, visit http://bertware.net

using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Net.Bertware.Bukkitgui2.Core.Logging;
using Net.Bertware.Bukkitgui2.MinecraftInterop.OutputHandler;
using Net.Bertware.Bukkitgui2.MinecraftInterop.OutputHandler.PlayerActions;
using Net.Bertware.Bukkitgui2.MinecraftServers.Tools;
using Net.Bertware.Bukkitgui2.Properties;

namespace Net.Bertware.Bukkitgui2.MinecraftServers
{
	/// <summary>
	///     The base for a minecraft server. This should contain all parsing code for a vanilla server.
	/// </summary>
	public class MinecraftServerBase : IMinecraftServer
	{

		// message tags
		/// <summary>
		///     Info message tag at line start
		/// </summary>
		public const string RG_INFO = "^\\[INFO\\]";

		/// <summary>
		///     Warning message tag at line start
		/// </summary>
		public const string RG_WARN = "^\\[WARN(ING)?\\]";

		/// <summary>
		///     Severe/Error message tag at line start
		/// </summary>
		public const string RG_SEV = "^\\[(SEVERE|ERROR)\\]";

		// ip's and player names
		/// <summary>
		///     Ipv4 Ip address, without any extra's
		/// </summary>
		public const string RG_IP_NOPORT = "\\d{1,3}.\\d{1,3}.\\d{1,3}.\\d{1,3}";

		/// <summary>
		///     Ipv4 Ip address, with optional port. this is the typical ip for a minecraft login
		/// </summary>
		public const string RG_IP = RG_IP_NOPORT + "(:\\d{2,5})?";

		/// <summary>
		///     Ipv4 Ip with optional port, between right brackets
		/// </summary>
		public const string RG_IP_BRACKET = "\\[/" + RG_IP + "\\]";

		/// <summary>
		///     Player name
		/// </summary>
		public const string RG_PLAYER = "\\w{2,16}";

		// other regexes
		/// <summary>
		///     Possible space or tab
		/// </summary>
		public const string RG_SPACE = "(\\s{0,}|\\t{0,})";

		/// <summary>
		///     At least one space
		/// </summary>
		public const string RG_FSPACE = "\\s+";

		/// <summary>
		///     Stacktrace, like "at net.minecraft.server ...
		/// </summary>
		public const string RG_STACKTRACE = "(at |java\\.)(\\w+\\.){1,}(\\w|\\d|<){1,}(\\(|:|\\.|<|>)";

        public const string RG_CLASS = "(\\[.*?\\])";

        public MinecraftServerBase()
		{
			Name = "Default server";
			Site = "http://minecraft.net";
			Logo = Resources.vanilla_logo;
			SupportsPlugins = false;
			IsLocal = true;

			HasCustomAssembly = false;
			CustomAssembly = null;
			HasCustomSettingsControl = false;
			CustomSettingsControl = null;
		}

		public string Name { get; protected set; }

		public string Site { get; protected set; }

		public Image Logo { get; protected set; }

		public bool SupportsPlugins { get; protected set; }

		public bool IsLocal { get; protected set; }


		public virtual void PrepareLaunch()
		{
			// nothing to do
		}

		public bool HasCustomAssembly { get; protected set; }

		public string CustomAssembly { get; protected set; }

		public virtual string GetLaunchParameters(string defaultParameters = "")
		{
			return defaultParameters;
		}

		public virtual string GetLaunchFlags(string defaultFlags = "")
		{
			return defaultFlags;
		}

		public bool HasCustomSettingsControl { get; protected set; }

		public UserControl CustomSettingsControl { get; protected set; }

		public virtual OutputParseResult ParseOutput(string text)
		{
			string message = ParseMessage(text);
			if (string.IsNullOrEmpty(message)) return null;

			MessageType type = ParseMessageType(message);

			// In case of a player action, also provide the info 
			switch (type)
			{
				case MessageType.PlayerJoin:
					return new OutputParseResult(text, message, type, ParsePlayerJoin(message));
				case MessageType.PlayerLeave:
					return new OutputParseResult(text, message, type, ParsePlayerLeave(message));
				case MessageType.PlayerKick:
					return new OutputParseResult(text, message, type, ParsePlayerActionKick(message));
				case MessageType.PlayerBan:
					return new OutputParseResult(text, message, type, ParsePlayerActionBan(message));
				default:
					return new OutputParseResult(text, message, type);
			}
		}

		public string ParseMessage(string text)
		{
			if (string.IsNullOrEmpty(text)) return text;

			Logger.Log(LogLevel.Debug, "MinecraftServerBase", "Parsing message for \"" + text + "\"");
			text = RemoveTimeStamp(text); // We need to know the type, so we'll continue without the timestamp
			//Logger.Log(LogLevel.Debug, "MinecraftServerBase", "Removed timestamp: \"" + text + "\"");
			text = FilterText(text);
			Logger.Log(LogLevel.Debug, "MinecraftServerBase", "Filtered text: \"" + text + "\"");
			return text;
		}

		public virtual MessageType ParseMessageType(string text)
		{
			if (string.IsNullOrEmpty(text)) return MessageType.Unknown;

			//[WARNING]...
			if (Regex.IsMatch(text, RG_WARN))
			{
				return MessageType.Warning;
			}
			//[SEVERE] ...
			if (Regex.IsMatch(text, RG_SEV))
			{
				return MessageType.Severe;
			}


			// LOGIN
			//[INFO] Bertware[/127.0.0.1:58189] logged in with entity id 27 at ([world] -1001.0479985318618, 2.0, 1409.300000011921)
			//[INFO] Bertware[/127.0.0.1:58260] logged in with entity id 0 at (-1001.0479985318618, 2.0, 1409.300000011921)
			//[INFO]  UUID of player Bertware is f0b27a3369394b25ab897aa4e4db83c1
			//[INFO]  Bertware[/127.0.0.1:51815] logged in with entity id 184 at ([world] 98.5, 64.0, 230.5)

			if (Regex.IsMatch(
				text,
				RG_INFO + RG_SPACE + RG_CLASS + RG_SPACE + RG_PLAYER + RG_IP_BRACKET + " logged in with entity id",
				RegexOptions.IgnoreCase))
			{
				return MessageType.PlayerJoin;
			}

			// Disconnect / leave
			//[INFO]  Bertware lost connection: Disconnected
			//[INFO]  Bertware left the game.
			if (Regex.IsMatch(
				text,
                RG_INFO + RG_SPACE + RG_CLASS + RG_SPACE + RG_PLAYER + " lost connection:",
				RegexOptions.IgnoreCase))
			{
				return MessageType.PlayerLeave;
			}
			// catch player left the game message
			if (Regex.IsMatch(
				text,
                RG_INFO + RG_SPACE + RG_CLASS + RG_SPACE + RG_PLAYER + " left the game",
				RegexOptions.IgnoreCase))
			{
				return MessageType.PlayerLeave;
			}

			// Disconnect / kick
			//[INFO]  Bertware lost connection: test
			//[INFO]  Bertware left the game.
			//[INFO]  CONSOLE: Kicked player Bertware. With reason:
			//test
			//
			// [command sender]: Kicked player [player]. With reason: [newline] [Reason]
			if (Regex.IsMatch(
				text,
                RG_INFO + RG_SPACE + RG_CLASS + RG_SPACE + RG_PLAYER + " lost connection: Kicked by",
				RegexOptions.IgnoreCase))
			{
				return MessageType.PlayerKick;
			}
			//disconnect / ban
			//[INFO]  Bertware lost connection: Banned by admin.
			//[INFO]  Bertware left the game.
			//[INFO]  CONSOLE: Banned player bertware
			if (Regex.IsMatch(
				text,
                RG_INFO + RG_SPACE + RG_CLASS + RG_SPACE + RG_PLAYER + " lost connection: Banned by",
				RegexOptions.IgnoreCase))
			{
				return MessageType.PlayerBan;
			}

			if (Regex.IsMatch(text, RG_INFO + RG_SPACE + "There are " + "\\d+" + "/" + "\\d+" + " players online:",
				RegexOptions.IgnoreCase))
			{
				return MessageType.PlayerList;
			}

			// stacktraces
			if (Regex.IsMatch(
				text,
				"^" + RG_SPACE + RG_STACKTRACE,
				RegexOptions.IgnoreCase))
			{
				return MessageType.JavaStackTrace;
			}

			//TODO: Stacktraces (other formats) and java errors
			// all other text is info text
			return MessageType.Info;
		}

		public virtual string RemoveTimeStamp(string text)
		{
			if (string.IsNullOrEmpty(text)) return text;
			//2014-01-01 00:00:00,000
			text = Regex.Replace(text, "\\d{4}-\\d{2}-\\d{2} \\d{2}:\\d{2}:\\d{2}(,\\d{3}|)\\s*", "");
			//[11:36:21]
			text = Regex.Replace(text, "\\[\\d+:\\d+:\\d+\\]", "");
			//[11:36:21 INFO]
			text = Regex.Replace(text, "\\[\\d+:\\d+:\\d+ ", "[");
			text = text.TrimStart();
			return text;
		}

		public virtual string FilterText(string text)
		{
			if (string.IsNullOrEmpty(text)) return text;
			// fix harmless warning, users question this warning.
			if (text.Contains("org.fusesource.jansi.WindowsAnsiOutputStream")) return "";

			if (text.Equals(">")) return "";

			// filter color codes
			text = Regex.Replace(text,"\uFFFD[0-9a-f]?", "");

			// remove [minecraft] or [minecraft-server] tags, for better parsing
			text = Regex.Replace(text, "\\[minecraft(-server)?\\]", "", RegexOptions.IgnoreCase);

			// [User Authenticator #1/INFO] to [INFO]
			text = Regex.Replace(text, "User Authenticator #\\d/", "");
			// [Server thread/INFO] to [INFO]
			text = Regex.Replace(text, "Server (Shutdown )?thread/", "", RegexOptions.IgnoreCase);
			text = text.Replace("]:", "]");
			text = text.Trim();
			return text;
		}

		public virtual PlayerActionJoin ParsePlayerJoin(string text)
		{
			//[INFO] Bertware[/127.0.0.1:58189] logged in with entity id 27 at ([world] -1001.0479985318618, 2.0, 1409.300000011921)
			//[INFO] Bertware[/127.0.0.1:58260] logged in with entity id 0 at (-1001.0479985318618, 2.0, 1409.300000011921)
			//[INFO]  UUID of player Bertware is f0b27a3369394b25ab897aa4e4db83c1
			//[INFO]  Bertware[/127.0.0.1:51815] logged in with entity id 184 at ([world] 98.5, 64.0, 230.5)
			PlayerActionJoin join = new PlayerActionJoin();
			text = Regex.Replace(text, RG_INFO + RG_SPACE + RG_CLASS, "", RegexOptions.IgnoreCase);
			join.PlayerName = Regex.Match(text, "^\\s?(" + RG_PLAYER +")").Groups[1].Value;
			join.Ip = Regex.Match(text, RG_IP_NOPORT).Value;

			return join;
		}

		public virtual PlayerActionLeave ParsePlayerLeave(string text)
		{
			//[INFO]  Bertware lost connection: Disconnected
			//[INFO]  Bertware left the game.
			PlayerActionLeave leave = new PlayerActionLeave
			{
				PlayerName = Regex.Match(text, RG_FSPACE + RG_PLAYER).Value.Trim(),
				Details = Regex.Match(text, ":(.*)$").Groups[1].Value
			};
			return leave;
		}

		public virtual PlayerActionKick ParsePlayerActionKick(string text)
		{
			//[INFO]  Bertware lost connection: test
			//[INFO]  Bertware left the game.
			//[INFO]  CONSOLE: Kicked player Bertware. With reason:
			//test
			PlayerActionKick leave = new PlayerActionKick
			{
				PlayerName = Regex.Match(text, RG_FSPACE + RG_PLAYER).Value.Trim(),
				Details = Regex.Match(text, ":(.*)$").Groups[1].Value
			};
			return leave;
		}

		public virtual PlayerActionBan ParsePlayerActionBan(string text)
		{
			//[INFO]  Bertware lost connection: Banned by admin.
			//[INFO]  Bertware left the game.
			//[INFO]  CONSOLE: Banned player bertware
			PlayerActionBan leave = new PlayerActionBan
			{
				PlayerName = Regex.Match(text, RG_FSPACE + RG_PLAYER).Value.Trim(),
				Details = Regex.Match(text, ":(.*)$").Groups[1].Value
			};
			return leave;
		}

		public virtual PlayerActionIpBan ParsePlayerActionIpBan(string text)
		{
			return new PlayerActionIpBan();
		}
	}
}