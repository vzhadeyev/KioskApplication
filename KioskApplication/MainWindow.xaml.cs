using KioskApplication.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


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
            PinTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0087E1"));
            PinTextBox.FontSize = 50;
            var button = sender as Button;
            var value = button.Content.ToString();
            ViewModel.Pin += value;
        }

        string path;

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            //4425
            var name = ViewModel.Pin + ".pdf";
            var saveFileDirectory = Path.GetTempPath();
            var saveFileName = Path.Combine(saveFileDirectory, name);
            path = saveFileName;
            using (var client = new WebClient())
            {
                Uri uri = new Uri("http://arman.39ma.ru/files/" + name);
                try
                {
                    client.DownloadFile(uri, saveFileDirectory + name); // synchronusly download file cause we don't want to allow user interract with UI 
                    ResetViewModel();
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                        {
                            ResetViewModel();
                            PinTextBox.Foreground = new SolidColorBrush(Colors.Red);
                            PinTextBox.FontSize = 36;
                            PinTextBox.Text = "Неверный код!";
                            return;
                        }
                    }
                }
            }

            // TO DO: 
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = saveFileName;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;

            p.Start();
            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);
            if (false == p.CloseMainWindow())
                p.Kill();
        }

        private void ResetViewModel()
        {
            ViewModel.Pin = string.Empty;
        }

        private void WebClientDownloadCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            MessageBox.Show(path);
        }

        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BackSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Pin = ViewModel.Pin != string.Empty ? ViewModel.Pin.Remove(ViewModel.Pin.Length - 1) : string.Empty;
        }
    }
}
