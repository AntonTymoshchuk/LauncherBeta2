using RamGecTools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LauncherBeta2
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private KeyboardHook keyboardHook;
        private bool spacePressed = false;
        private bool lmenuPressed = false;
        private bool rmenuPressed = false;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = new MainWindow();
            MainWindow.Show();
            MainWindow.Height = 40;
            MainWindow.WindowState = WindowState.Minimized;
            MainWindow.Hide();

            keyboardHook = new KeyboardHook();
            keyboardHook.Install();
            keyboardHook.KeyDown += KeyboardHook_KeyDown;
            keyboardHook.KeyUp += KeyboardHook_KeyUp;

            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.MouseClick += NotifyIcon_MouseClick;
            notifyIcon.Text = "Launcher";
            notifyIcon.Icon = LauncherBeta2.Resources.NotificationArea;
            notifyIcon.Visible = true;
        }

        private void KeyboardHook_KeyDown(KeyboardHook.VKeys key)
        {
            switch (key)
            {
                case KeyboardHook.VKeys.SPACE:
                    spacePressed = true;
                    break;
                case KeyboardHook.VKeys.LMENU:
                    lmenuPressed = true;
                    break;
                case KeyboardHook.VKeys.RMENU:
                    rmenuPressed = true;
                    break;
            }

            if (spacePressed == true && (lmenuPressed == true || rmenuPressed == true))
                ShowMainWindow();
        }

        private void KeyboardHook_KeyUp(KeyboardHook.VKeys key)
        {
            switch (key)
            {
                case KeyboardHook.VKeys.SPACE:
                    spacePressed = false;
                    break;
                case KeyboardHook.VKeys.LMENU:
                    lmenuPressed = false;
                    break;
                case KeyboardHook.VKeys.RMENU:
                    rmenuPressed = false;
                    break;
                case KeyboardHook.VKeys.ESCAPE:
                    HideMainWindow();
                    break;
            }
        }

        private void NotifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            MainWindow.Show();
            MainWindow.WindowState = WindowState.Normal;
            MainWindow.Activate();
            SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
            MainWindow.Focus();
        }

        private void HideMainWindow()
        {
            MainWindow.WindowState = WindowState.Minimized;
            MainWindow.Hide();
        }
    }
}
