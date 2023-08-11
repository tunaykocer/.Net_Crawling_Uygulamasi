using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using crawlingapp_tk.Classes;
using System.Net;
using HtmlAgilityPack;

namespace crawlingapp_tk
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Google Chrome'un User-Agent değerini eklemek
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");

                string url = "https://www.arabam.com/";
                string html = await httpClient.GetStringAsync(url);

                var scrapper = new SahibindenScrapper(httpClient, url);
                var ilanlarList = scrapper.GetIlanlar(html);

                foreach (var ilan in ilanlarList)
                {
                    var ilanHtml = await httpClient.GetStringAsync(ilan.IlanDetayUrl);
                    scrapper.GetIlanDetay(ilanHtml, ilan);
                }

                FileOperations.WriteToFile("ilanlar.txt", ilanlarList);
                scrapper.DisplayIlanlar(ilanlarList);
                scrapper.DisplayOrtalamaFiyat(ilanlarList);
            }
        }
    }
}