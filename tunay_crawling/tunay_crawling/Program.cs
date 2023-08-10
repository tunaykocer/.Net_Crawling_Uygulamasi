using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using tunay_crawling;

namespace tunay_crawling
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Arabam.com'dan ilan URL'lerini al
            List<string> ilanUrls = await Class1.GetIlanUrlsAsync();
            Console.WriteLine("https://www.arabam.com/ Vitrin Bolumu ilan bilgileri getiriliyor");
            Console.Write("Yukleniyor");

            for (int i = 0; i < 10; i++)
            {
                Console.Write("~");
                Thread.Sleep(200);
            }

            Console.WriteLine("Basarili!");


            // Tüm ilanlar için sırayla bilgi al ve ilanlar listesine ekle

            List<Ilan> ilanlar = new List<Ilan>();

            foreach (string url in ilanUrls)
            {
                List<Ilan> ilanList = await Class2.GetIlanAsync("https://www.arabam.com/" + url);
                ilanlar.AddRange(ilanList);
            }

            // İlan listeslerinin tamamı listelenirse ortalama  ve toplam fiyatı ekrana yazdırma islemi
            if (ilanlar.Count > 0)
            {
                double totalPrice = ilanlar.Sum(ilan => ilan.Fiyat);
                double averagePrice = totalPrice / ilanlar.Count;
                Console.WriteLine("Average Price: " + averagePrice.ToString("#.###"));
                Console.WriteLine("Total Prices: " + totalPrice.ToString("#.###"));
                Console.ReadKey();
            }
        }
    }
}