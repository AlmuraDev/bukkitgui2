﻿// TaskerTab.cs in bukkitgui2/bukkitgui2
// Created 2014/01/17
// Last edited at 2014/06/22 12:34
// ©Bertware, visit http://bertware.net

using System.Windows.Forms;

namespace Net.Bertware.Bukkitgui2.AddOn.Tasker
{
    public partial class TaskerTab : UserControl, IAddonTab
    {
        public TaskerTab()
        {
            InitializeComponent();
        }

        public IAddon ParentAddon { get; set; }
    }
}