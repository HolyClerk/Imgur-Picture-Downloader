using System;
using System.Threading;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Threading;

using ImgurParser.Parser;
using ImgurParser.Link;

namespace ImgurParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        private CodeParser firstParser;
        private CodeParser secondParser;

        public MainWindow()
        {
            InitializeComponent();

            firstParser = new CodeParser();
            secondParser = new CodeParser();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += UpdateStatistics;
            timer.Start();
        }

        private async void OnFirstThreadButtonClick(object sender, RoutedEventArgs e)
        {
            var rLink = new LinkRandomizer();

            try
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < 5000; i++)
                    {
                        firstParser.AsyncDownload(rLink.GetRandomizedLink(7), @$"img\picture_{1}_{i}.png");
                    }
                });
            }
            catch (Exception exception)
            {
                errorMessage.Text += exception.Message;
            }
        }

        private async void OnSecondThreadButtonClick(object sender, RoutedEventArgs e)
        {
            var rLink = new LinkRandomizer();

            try
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < 5000; i++)
                    {
                        secondParser.AsyncDownload(rLink.GetRandomizedLink(7), @$"img\picture_{2}_{i}.png");
                    }
                });
            }
            catch (Exception exception)
            {
                errorMessage.Text += exception.Message;
            }
        }

        private void UpdateStatistics(object sender, EventArgs e)
        {
            if (firstParser != null)
            {
                amountFrist.Content = firstParser.GetNumberOfAttemps().ToString();
                downloadsFirst.Content = firstParser.GetSuccessfulAttemps().ToString();
            }
            
            if (secondParser != null)
            {
                amountSecond.Content = secondParser.GetNumberOfAttemps().ToString();
                downloadsSecond.Content = secondParser.GetSuccessfulAttemps().ToString();
            }
        }
    }
}