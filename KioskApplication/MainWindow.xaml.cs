using KioskApplication.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace KioskApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            PinTextBox.DataContext = ViewModel;
        }

        private void NumericButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var value = button.Content.ToString();
            ViewModel.Pin += value;
        }

    }
}
