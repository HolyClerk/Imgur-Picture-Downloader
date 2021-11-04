using System;
using System.Threading;
using System.Windows;

namespace ImgurParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int time = 0;

        // Поток для вывода текущего состояния парсера
        private void timerTick(object sender, EventArgs e)
        {
            time++;

            errorMessage.Text += Client.errorMessage;
            linkBlock.Text += Client.currentLink + "\n";

            countOfDlTries.Content = Client.countOfTries;
            countOfSucTries.Content = Client.countOfSuccesTries;
            percentOfSucces.Content = $"{(Client.countOfSuccesTries / Client.countOfTries) * 100}%";

            timeLab.Content = $"{Math.Round(Client.countOfTries / time, 3)} обработок сайтов и {Math.Round(Client.countOfSuccesTries / time, 3)} скачиваний в сек. ";
            timePast.Content = $"Времени прошло: {time}";

            if (time % 600 == 0)
            {
                errorMessage.Text += 
                    $"На данный момент прошло {time / 60} минут: \n" +
                    $"1) Среднее обр. {Math.Round(Client.countOfTries / time, 3)} \n" +
                    $"2) Среднее скач. {Math.Round(Client.countOfSuccesTries / time, 3)} \n" +
                    $"3) Кол-во скач. {Client.countOfSuccesTries} \n" +
                    $"4) Кол-во попыток {Client.countOfTries} \n" +
                    $"5) Сред. успешных скач. {Math.Round(Client.countOfSuccesTries / Client.countOfTries, 3) * 100}% \n\n";
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Start();
        }

        private void StartParsing_Click(object sender, RoutedEventArgs e)
        {
            // Используется 2 потока для увеличения скорости парсинга, не забивая
            // ее поток загрузкой. Из-за асинхрона необходимо каждый раз проверять
            // состояние списка - добавился ли новый.

            Thread downloadThread = new Thread(Client.DownloadImage);
            Thread parseThread = new Thread(Parse.GenerateLink);

            downloadThread.Start();
            parseThread.Start();
        }
    }
}