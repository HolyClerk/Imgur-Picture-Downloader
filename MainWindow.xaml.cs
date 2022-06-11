using System;
using System.Windows;
using System.Net.Http;
using System.Threading;
using System.Windows.Controls;
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
        private CodeParser[] parsers;
        private DispatcherTimer timer;

        private StateOfParser stateOfParser;

        private Action ParseAttemp;

        public MainWindow()
        {
            InitializeComponent();

            parsers = new CodeParser[] 
            {
                new CodeParser(),
                new CodeParser(),
                new CodeParser(),
                new CodeParser(),
            };

            ParseAttemp += () => progress.Value += 100 / GetNumberOfTries();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += UpdateStatistics;
            timer.Start();

            stateOfParser.IsBusy = false;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (threadingsComboBox.SelectedIndex == -1 || stateOfParser.IsBusy == true)
            {
                return;
            }

            for (int i = 0; i < threadingsComboBox.SelectedIndex + 1; i++)
            {
                CreateParserAsync(parsers[i], i + 1);
            }

            stateOfParser.IsBusy = true;
        }

        private async void CreateParserAsync(CodeParser codeParser, int threadNum)
        {
            var rLink = new LinkRandomizer();
            var tries = GetNumberOfTries();

            try
            {
                await Task.Run(() =>
                {
                    int i = 0;

                    do
                    {
                        codeParser.AsyncTryDownload(rLink.GetRandomizedLink(7), @$"img\picture_{threadNum}_{i}.png");

                        stateOfParser.NumberOfTry += 1;

                        if (i >= tries)
                        {
                            stateOfParser.IsBusy = false;
                        }

                        i++;

                    } while (stateOfParser.IsBusy);
                });
            }
            catch (Exception exception)
            {
                errorMessage.Text += exception.Message;
            }

            stateOfParser.IsBusy = false;
        }

        private void UpdateStatistics(object sender, EventArgs e)
        {
            if (threadingsComboBox.SelectedIndex == -1)
            {
                return;
            }

            downloadsFirst.Content = parsers[0].GetSuccessfulAttemps();
            downloadsSecond.Content = parsers[1].GetSuccessfulAttemps();
            downloadsThrid.Content = parsers[2].GetSuccessfulAttemps();
            downloadsFourth.Content = parsers[3].GetSuccessfulAttemps();

            amountFrist.Content = parsers[0].GetNumberOfAttemps();
            amountSecond.Content = parsers[1].GetNumberOfAttemps(); 
            amountThird.Content = parsers[2].GetNumberOfAttemps(); 
            amountFourth.Content = parsers[3].GetNumberOfAttemps();

            // Получаем процент выполненной работы по формуле.
            if (stateOfParser.IsBusy)
            {
                progress.Value = (stateOfParser.NumberOfTry / (GetNumberOfTries() / 100)) / (threadingsComboBox.SelectedIndex + 1);
            }
        }

        private int GetNumberOfTries()
        {
            return int.Parse(numOfTries.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var clientHandler = new HttpClientHandler();

            var httpClient = new HttpClient(clientHandler);

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("Referer", "https://away.vk.com/");

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(errorMessage.Text).Result;

                errorMessage.Text = response.Content.ReadAsStringAsync().Result.Length.ToString();
            }
            catch (Exception ex)
            {
                errorMessage.Text += ex.Message;
            }
            
        }
    }
}