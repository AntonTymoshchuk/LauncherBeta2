﻿using RamGecTools;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LauncherBeta2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RequestTextBox.Focus();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Height = 40;
            RequestTextBox.Text = string.Empty;
            WindowState = WindowState.Minimized;
            Hide();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.Space)
                e.Handled = true;
            else
                base.OnKeyDown(e);
        }

        private void RequestTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RequestTextBox.Text.Equals(string.Empty))
                RequestTextBox.Tag = false;
            else
                RequestTextBox.Tag = true;
        }
    }
}
