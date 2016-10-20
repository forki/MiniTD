﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MiniTD.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : Window
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InfoHyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:menno@codingconnected.eu");
        }

        private void InfoHyperlink2_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.codingconnected.eu");
        }
    }
}
