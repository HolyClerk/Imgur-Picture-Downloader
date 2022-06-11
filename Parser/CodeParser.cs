using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using ImgurParser.Download;

namespace ImgurParser.Parser
{
    internal sealed class CodeParser
    {
        private HttpClient httpClient;
        private DownloadClient loadClient;

        public CodeParser()
        {
            var clientHandler = new HttpClientHandler();

            loadClient = new DownloadClient();
            httpClient = new HttpClient(clientHandler);

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("Referer", "https://away.vk.com/");
        }

        public async void AsyncDownload(string link, string fileName)
        {
            await Task.Run(() =>
            {
                HttpResponseMessage response = httpClient.GetAsync(link).Result;

                if (response.Content.ReadAsStringAsync().Result.Length > 3000)
                {
                    loadClient.AsyncDownloadImage(link, fileName);
                }
            });        
        }
    }
}
