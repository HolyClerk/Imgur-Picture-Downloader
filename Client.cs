using System;
using System.Net;
using System.Threading;

namespace ImgurParser
{
    class Client
    {
        public static decimal countOfTries = 1;
        public static decimal countOfSuccesTries = 1;

        public static string currentLink;
        public static string path = "H:\\newImg\\";
        public static string errorMessage;

        public static void DownloadImage() 
        {
            WebClient client = new WebClient();

            for (int i = 0; i < 10000; i++)
            {
                while (true)
                {
                    // Необходимость в try обусловлена большей скоростью загрузки, нежели
                    // парсинга. Иначе выходим за пределы списка.
                    try
                    {
                        currentLink = Parse.sites[i];
                        break;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(100);
                        /* !ИСПРАВИТЬ! Поток каким-то образом взаимодействует с потоком интерфейса
                         * и без "Засыпания" поток, на котором находится интерфейс зависает.
                         * Возможно слишком частые запросы к списку слишком нагружают процессор.*/
                    }
                }

                // Если ссылка битая или не найдено изображение - возвращается null 
                try
                {
                    // client.DownloadFile(link, $"{path}cot{countOfTries}_cost{countOfSuccesTries}_picture.png");
                    client.DownloadFile(currentLink, $"{path}CoST_{countOfSuccesTries}_{i}_picture.png");

                    countOfSuccesTries++;
                }
                catch (Exception exc)
                {
                    errorMessage += exc.Message + "\n";
                }
            }
        }
    }
}
