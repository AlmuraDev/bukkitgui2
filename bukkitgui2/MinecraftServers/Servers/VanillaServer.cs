// VanillaServer.cs in bukkitgui2/bukkitgui2
// Created 2014/01/17
// 
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.
// 
// ©Bertware, visit http://bertware.net

using Net.Bertware.Bukkitgui2.Core.Util.Web;
using Net.Bertware.Bukkitgui2.MinecraftServers.Tools.Vanilla;
using Net.Bertware.Bukkitgui2.Properties;

namespace Net.Bertware.Bukkitgui2.MinecraftServers.Servers
{
	/// <summary>
	///     Default vanilla server. All parsing code is already in the server base
	/// </summary>
	internal class VanillaServer : MinecraftServerBase
	{
		public VanillaServer()
		{
			Name = "Vanilla";
			Site = "http://minecraft.net";
			Logo = Resources.vanilla_logo;
		}

		public override string GetLaunchFlags(string defaultFlags = "")
		{
			return " nogui " + defaultFlags;
		}
	}
}