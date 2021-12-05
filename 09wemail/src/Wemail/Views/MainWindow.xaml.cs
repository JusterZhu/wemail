using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Wemail.Common.MVVM;
using Wemail.ViewModels;

namespace Wemail.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = DataContext as MainWindowViewModel;
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _vm.LoadModulesCommand.Execute();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GrdTitle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (System.Exception){}
        }
    }
}
