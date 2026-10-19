// JavaApi.cs in bukkitgui2/bukkitgui2
// Created 2014/02/22
//
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.
//
// ©Bertware, visit http://bertware.net

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Net.Bertware.Bukkitgui2.Core.Logging;

namespace Net.Bertware.Bukkitgui2.AddOn.Starter
{
	internal static class JavaApi
	{
		private static Boolean _initialized;

		private static List<JavaInstallation> _installations;

		/// <summary>
		///     Registry roots (under SOFTWARE) of Adoptium-family distributions: Eclipse Temurin and its predecessors.
		///     All of them use the layout {root}\{JRE|JDK}\{version}\{jvm}\MSI, with the install folder in the "Path" value.
		/// </summary>
		private static readonly string[] AdoptiumRegistryRoots = {"Eclipse Adoptium", "Eclipse Foundation", "AdoptOpenJDK"};

		/// <summary>
		///     All detected java installations, newest version first, 64 bit before 32 bit
		/// </summary>
		public static List<JavaInstallation> Installations
		{
			get
			{
				if (!_initialized) Initialize();
				return _installations;
			}
		}

		/// <summary>
		///     Initialize the class, check available versions and paths
		/// </summary>
		public static void Initialize()
		{
			_installations = new List<JavaInstallation>();

			foreach (RegistryView view in Environment.Is64BitOperatingSystem
				? new[] {RegistryView.Registry64, RegistryView.Registry32}
				: new[] {RegistryView.Registry32})
			{
				bool is64Bit = view == RegistryView.Registry64;
				try
				{
					using (RegistryKey software = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view).OpenSubKey("SOFTWARE"))
					{
						if (software == null) continue;
						DetectOracleFromRegistry(software, is64Bit);
						DetectAdoptiumFromRegistry(software, is64Bit);
					}
				}
				catch (Exception exception)
				{
					Logger.Log(LogLevel.Warning, "JavaApi", "Failed to detect java from registry (" + view + ")",
						exception.Message);
				}
			}

			// Also look on disk, for installs that didn't register themselves (zip installs, or registry access failed)
			DetectJavaByFilestructure();

			_installations = _installations
				.OrderByDescending(i => i.Version)
				.ThenByDescending(i => i.Is64Bit)
				.ThenBy(i => i.Vendor)
				.ToList();

			_initialized = true;
		}

		/// <summary>
		///     Oracle java: SOFTWARE\JavaSoft\{Java Runtime Environment|Java Development Kit|JRE|JDK}\{version}, value JavaHome
		/// </summary>
		private static void DetectOracleFromRegistry(RegistryKey software, bool is64Bit)
		{
			using (RegistryKey javaSoft = software.OpenSubKey("JavaSoft"))
			{
				if (javaSoft == null) return;

				foreach (string kind in new[] {"Java Runtime Environment", "Java Development Kit", "JRE", "JDK"})
				{
					using (RegistryKey versionRoot = javaSoft.OpenSubKey(kind))
					{
						if (versionRoot == null) continue;

						foreach (string subkey in versionRoot.GetSubKeyNames())
						{
							int version = ParseMajorVersion(subkey);
							if (version == 0) continue;

							using (RegistryKey versionKey = versionRoot.OpenSubKey(subkey))
							{
								object javaHome = versionKey == null ? null : versionKey.GetValue("JavaHome");
								if (javaHome == null || string.IsNullOrEmpty(javaHome.ToString())) continue;
								AddInstallation("Oracle", version, is64Bit, Path.Combine(javaHome.ToString(), "bin\\java.exe"));
							}
						}
					}
				}
			}
		}

		/// <summary>
		///     Eclipse Temurin / AdoptOpenJDK: SOFTWARE\{root}\{JRE|JDK}\{version}\{jvm}\MSI, value Path
		/// </summary>
		private static void DetectAdoptiumFromRegistry(RegistryKey software, bool is64Bit)
		{
			foreach (string rootName in AdoptiumRegistryRoots)
			{
				using (RegistryKey root = software.OpenSubKey(rootName))
				{
					if (root == null) continue;
					string vendor = rootName == "AdoptOpenJDK" ? "AdoptOpenJDK" : "Temurin";

					foreach (string kind in new[] {"JRE", "JDK"})
					{
						using (RegistryKey kindKey = root.OpenSubKey(kind))
						{
							if (kindKey == null) continue;

							foreach (string versionName in kindKey.GetSubKeyNames())
							{
								int version = ParseMajorVersion(versionName);
								if (version == 0) continue;

								using (RegistryKey versionKey = kindKey.OpenSubKey(versionName))
								{
									if (versionKey == null) continue;

									foreach (string jvm in versionKey.GetSubKeyNames())
									{
										using (RegistryKey msi = versionKey.OpenSubKey(jvm + "\\MSI"))
										{
											object installPath = msi == null ? null : msi.GetValue("Path");
											if (installPath == null || string.IsNullOrEmpty(installPath.ToString())) continue;
											AddInstallation(vendor, version, is64Bit,
												Path.Combine(installPath.ToString(), "bin\\java.exe"));
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private static void DetectJavaByFilestructure()
		{
			string programFiles = ProgramFiles();
			string programFilesX86 = ProgramFilesx86();
			bool separateX86 = !string.Equals(programFiles, programFilesX86, StringComparison.OrdinalIgnoreCase);

			foreach (string vendorDir in new[] {"Java", "Eclipse Adoptium", "AdoptOpenJDK", "Eclipse Foundation"})
			{
				string vendor = vendorDir == "Java" ? "Oracle" : (vendorDir == "AdoptOpenJDK" ? "AdoptOpenJDK" : "Temurin");
				DetectJavaInFolder(Path.Combine(programFiles, vendorDir), vendor, Environment.Is64BitOperatingSystem);
				if (separateX86) DetectJavaInFolder(Path.Combine(programFilesX86, vendorDir), vendor, false);
			}
		}

		private static void DetectJavaInFolder(string folder, string vendor, bool is64Bit)
		{
			if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder)) return;

			foreach (string dir in Directory.GetDirectories(folder))
			{
				// jre1.8.0_301, jdk1.8.0_301, jre-8.0.392.8-hotspot, jdk-17.0.9.9-hotspot, jdk8u392-b08-jre
				Match match = Regex.Match(Path.GetFileName(dir) ?? "", @"^(?:jre|jdk)-?(\d+(?:\.\d+)?)", RegexOptions.IgnoreCase);
				if (!match.Success) continue;

				int version = ParseMajorVersion(match.Groups[1].Value);
				string exe = Path.Combine(dir, "bin\\java.exe");
				if (version != 0 && File.Exists(exe)) AddInstallation(vendor, version, is64Bit, exe);
			}
		}

		/// <summary>
		///     Get the major java version from a version string: "1.8" -> 8, "1.8.0_301" -> 8, "8.0.392.8" -> 8, "17" -> 17
		/// </summary>
		/// <returns>The major version, or 0 if it couldn't be parsed</returns>
		private static int ParseMajorVersion(string text)
		{
			Match match = Regex.Match(text ?? "", @"^1\.(\d+)|^(\d+)");
			if (!match.Success) return 0;
			string number = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value;
			int version;
			return int.TryParse(number, out version) ? version : 0;
		}

		/// <summary>
		///     Register a java installation
		/// </summary>
		/// <param name="vendor">who made this java (Oracle, Temurin, ...)</param>
		/// <param name="version">the major runtime version (8, 17, ...)</param>
		/// <param name="is64Bit">wether or not this is a 64bit java executable</param>
		/// <param name="path">The path to java.exe. If this file does not exist, the installation is skipped</param>
		private static void AddInstallation(string vendor, int version, bool is64Bit, string path)
		{
			Logger.Log(LogLevel.Info, "JavaApi", "Registering java " + version + " (" + vendor + ") at " + path);
			if (!File.Exists(path))
			{
				Logger.Log(LogLevel.Warning, "JavaApi", "Failed to register java " + version + " at " + path, "File not found");
				return;
			}

			string fullPath = Path.GetFullPath(path);
			if (_installations.Any(i => string.Equals(i.Path, fullPath, StringComparison.OrdinalIgnoreCase))) return;

			JavaInstallation installation = new JavaInstallation(vendor, version, is64Bit, fullPath);
			_installations.Add(installation);
			Logger.Log(LogLevel.Info, "JavaApi", "Registered " + installation);
		}

		/// <summary>
		///     Find the installation for a java.exe path
		/// </summary>
		/// <returns>The installation, or null if no detected installation uses this path</returns>
		public static JavaInstallation GetInstallation(string path)
		{
			if (string.IsNullOrEmpty(path)) return null;
			return Installations.FirstOrDefault(i => string.Equals(i.Path, path, StringComparison.OrdinalIgnoreCase));
		}

		/// <summary>
		/// Get the latest available java version on this system, for running small tools (retrieving versions etc)
		/// </summary>
		/// <returns>A full path to java.exe</returns>
		public static string GetIdealJavaPath()
		{
			JavaInstallation newest = Installations.FirstOrDefault();
			return newest == null ? null : newest.Path;
		}

		private static string ProgramFilesx86()
		{
			if (8 == IntPtr.Size
			    || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
			{
				return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
			}

			return Environment.GetEnvironmentVariable("ProgramFiles");
		}

		private static string ProgramFiles()
		{
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ProgramW6432")))
				return (Environment.GetEnvironmentVariable("ProgramW6432"));
			return Environment.GetEnvironmentVariable("ProgramFiles");
		}
	}

	/// <summary>
	///     One installed java runtime
	/// </summary>
	public class JavaInstallation
	{
		public JavaInstallation(string vendor, int version, bool is64Bit, string path)
		{
			Vendor = vendor;
			Version = version;
			Is64Bit = is64Bit;
			Path = path;
		}

		/// <summary>
		///     Who made this java: Oracle, Temurin, AdoptOpenJDK
		/// </summary>
		public string Vendor { get; private set; }

		/// <summary>
		///     The major runtime version: 8, 11, 17, ...
		/// </summary>
		public int Version { get; private set; }

		public bool Is64Bit { get; private set; }

		/// <summary>
		///     Full path to java.exe
		/// </summary>
		public string Path { get; private set; }

		/// <summary>
		///     Text shown in the java version list, e.g. "Java 8 - 64 bit (Temurin, jre-8.0.392.8-hotspot)"
		/// </summary>
		public override string ToString()
		{
			// java.exe -> bin -> install folder
			string folder = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Path)));
			return "Java " + Version + " - " + (Is64Bit ? "64" : "32") + " bit (" + Vendor + ", " + folder + ")";
		}
	}
}
