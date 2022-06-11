using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ImgurParser.Download
{
    internal sealed class DownloadClient
    {
        public DownloadClient(string fileName)
        {
            FileName = fileName;
        }

        public DownloadClient()
        {
            // Default path
            FileName = @"C:\Images";
        }

        public string FileName { get; set; }

        public async void AsyncDownloadImage(string link)
        {
            await Task.Run(() =>
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(link, FileName);
                }
            });
        }

        public async void DownloadImageAsync(string link, string fileName)
        {
            await Task.Run(() =>
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(link, fileName);
                }
            });
        }
    }
}