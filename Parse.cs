using System;
using System.Net.Http;
using System.Collections.Generic;

namespace ImgurParser
{
    class Parse
    {
        // Список для сохраненных сайтов, с которым взаимодействую разные потоки
        public static List<string> sites = 
            new List<string>()
            { 
                "https://i.imgur.com/tC10z.png",    // 
                "https://i.imgur.com/tC10z.png",    // 
                "https://i.imgur.com/tC10z.png",    // 
                "https://i.imgur.com/tC10z.png",    // 
                "https://i.imgur.com/tC10z.png",    // 
                "https://i.imgur.com/tC10z.png",    // 
                "https://i.imgur.com/tC10z.png",    // 
                "https://i.imgur.com/tC10z.png"     // 
            };

        private static string currentLink;
        public static string URLcur;            // !ИСПРАВИТЬ! Неправильно отображает ссылки

        /// <summary>
        /// С помощью запросов на сервер получает HTML-код
        /// и проверяет его.
        /// </summary>
        /// <param name="link">Ссылка на сайт<param>
        /// <returns>Находится ли внутри этой ссылки нужное изображение</returns>
        public static bool CheckCode(string link)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);

            // Подключаем хедеры для видимости "человеческих" действий сайтом https://i.imgur.com/Lmvf6.png
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36");
            client.DefaultRequestHeaders.Add("Referer", "https://away.vk.com/");

            // Создаем запрос через URL и сохраняем его в виде строки и исключаем ненужное
            HttpResponseMessage response = client.GetAsync(link).Result;
            if (response.Content.ReadAsStringAsync().Result.Length < 2000)
                return false;

            return true;
        }

        public static void GenerateLink()
        {
            for (int itt = 0; itt < 30000; itt++)
            {
                Random r = new Random();

                char[] lettersAndDigits = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

                while (true)
                {
                    Client.countOfTries++;

                    string URL = "";

                    for (int i = 0; i < 5; i++)
                        URL += lettersAndDigits[r.Next(0, 61)];

                    currentLink = "https://i.imgur.com/" + URL + ".png";
                    URLcur = URL;

                    // Если проходит по условиям поиска изображения добавляет сайт в список,
                    // иначе продолжает попытки найти рабочую ссылку
                    if (CheckCode(currentLink) == true)
                    {
                        sites.Add(currentLink);
                        break;
                    }
                }
            }
        }
    }
}