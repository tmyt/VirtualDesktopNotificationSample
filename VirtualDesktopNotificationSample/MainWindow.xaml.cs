using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using WindowsDesktop;

namespace VirtualDesktopNotificationSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            VirtualDesktop.CurrentChanged += VirtualDesktop_CurrentChanged;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            VirtualDesktop.CurrentChanged -= VirtualDesktop_CurrentChanged;
        }

        private void VirtualDesktop_CurrentChanged(object sender, VirtualDesktopChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                // Update Text by Current Desktop GUID
                EventMessage.Text = e.NewDesktop.Id.ToString();
                // Move my window to current virtual desktop
                this.MoveToDesktop(e.NewDesktop);
            }));
        }
    }
}
