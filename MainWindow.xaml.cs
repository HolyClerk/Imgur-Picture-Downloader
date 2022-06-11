using System;
using System.Threading;
using System.Windows;
using System.Threading.Tasks;

using ImgurParser.Parser;
using ImgurParser.Link;


namespace ImgurParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string a = "";

        private async void StartParsing_Click(object sender, RoutedEventArgs e)
        {
            var rLink = new LinkRandomizer();

            var firstParser = new CodeParser();
            var secondParser = new CodeParser();

            await Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    firstParser.AsyncDownload(rLink.GetRandomizedLink(7), $"picture_1_{i}");
                }
            });

            await Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    secondParser.AsyncDownload(rLink.GetRandomizedLink(7), $"picture_2_{i}");
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            errorMessage.Text += a;
        }
    }
}