﻿// EditorTab.cs in bukkitgui2/bukkitgui2
// Created 2014/01/17
// 
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.
// 
// ©Bertware, visit http://bertware.net

namespace Net.Bertware.Bukkitgui2.AddOn.Editor
{
    public partial class EditorTab : IAddonTab
    {
        public EditorTab()
        {
            InitializeComponent();
        }

        public IAddon ParentAddon { get; set; }
    }
}