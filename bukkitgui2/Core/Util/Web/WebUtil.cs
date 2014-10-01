﻿// WebUtil.cs in bukkitgui2/bukkitgui2
// Created 2014/02/02
// 
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.
// 
// ©Bertware, visit http://bertware.net

using System;
using System.Net;
using Net.Bertware.Bukkitgui2.Core.Logging;

namespace Net.Bertware.Bukkitgui2.Core.Util.Web
{
    internal static class WebUtil
    {
        public const string Mail = "contact@bertware.net";

        public static readonly string UserAgent = "Bertware 1.3/" + Share.AssemblyName + " " + Share.AssemblyVersion +
                                                  "/" +
                                                  Mail;

        public static string RetrieveString(string url)
        {
            using (WebClient webc = new WebClient())
            {
                try
                {
                    Logger.Log(LogLevel.Info, "WebUtil", "Retrieving data from " + url);
                    webc.Headers.Set(HttpRequestHeader.UserAgent, UserAgent);
                    return webc.DownloadString(url);
                }
                catch (WebException webException)
                {
                    Logger.Log(LogLevel.Warning, "WebUtil", "Couldn't retrieve data from " + url, webException.Message);
                    return "";
                }
            }
        }

        public static string GetGeoLocation(string ip)
        {
            return RetrieveString("http://geoip.bertware.net/?key=qXCsPUpvCvaQnWRv&ip=" + ip);
        }

        public static void DownloadFile(string url, string targetlocation, Boolean showUi, Boolean sync = false)
        {
            FileDownloader fileDownloadDialog = new FileDownloader();
            fileDownloadDialog.AddFile(url, targetlocation);
            if (!sync)
            {
                fileDownloadDialog.StartDownload();
            }
            else
            {
                fileDownloadDialog.ShowDialog();
            }
        }
    }
}