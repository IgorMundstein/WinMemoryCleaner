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
using System.Windows.Shapes;
using System.Windows.Threading;
using WinMemoryCleaner;
using WinMemoryCleaner3.ViewModel;
using Wpf.Ui.Controls;
namespace WinMemoryCleaner3.View
{
    /// <summary>
    /// NewMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        MainWindowViewModel VM { get; set; }
        DispatcherTimer dispatcherTimer;
        public MainWindow( MainWindowViewModel vm)
        {
            VM = vm;
            this.DataContext = vm;
            vm.OnRemoveProcessFromExclusionListCommandCompleted += SetFocusToProcessExclusionList;
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(5);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            VM.MonitorComputer();
            VM.MonitorApp();
            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }
        private void MenuItemOptimize_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        /// <summary>
        /// Sets the focus to process exclusion list.
        /// </summary>
        private void SetFocusToProcessExclusionList()
        {
            FocusManager.SetFocusedElement(this, SettingPanel.ProcessExclusionList);
            SettingPanel.ProcessExclusionList.Focus();
        }

        private void NotifyIcon_LeftDoubleClick(Wpf.Ui.Tray.Controls.NotifyIcon sender, RoutedEventArgs e)
        {
            if (Visibility == Visibility.Hidden|| Visibility == Visibility.Collapsed)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }

        private void FluentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Settings.CloseToTheNotificationArea)
            {
                e.Cancel = true;
                Visibility = Visibility.Collapsed;
            }
            
        }
    }
}
