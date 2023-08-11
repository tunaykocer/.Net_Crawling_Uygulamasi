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
            // Siteden ilan URL'lerini alma islemi
            List<string> ilanUrls = await Class1.GetIlanUrlsAsync();
            Console.WriteLine("ilan bilgileri getiriliyor");
            Console.Write("Yukleniyor");

            Console.WriteLine("******Basarili******");


            // Tüm ilanlar için sırayla bilgi alıp , ilanlar listesine ekleme islemi

            List<Ilan> ilanlar = new List<Ilan>();

            foreach (string url in ilanUrls)
            {
                List<Ilan> ilanList = await Class2.GetIlanAsync("https://www.arabam.com/" + url);
                ilanlar.AddRange(ilanList);
            }

            // İlan listelerinin tamamı listelenirse ortalama  ve toplam fiyatı ekrana yazdırma islemi
            if (ilanlar.Count > 0)
            {
                double toplamfiyat = ilanlar.Sum(ilan => ilan.Fiyat);
                double ortalamafiyat = toplamfiyat / ilanlar.Count;
                Console.WriteLine("Ortalama Fiyat: " + ortalamafiyat.ToString("#.###"));
                Console.WriteLine("Toplam Fiyat: " + toplamfiyat.ToString("#.###"));
                Console.ReadKey();
            }
        }
    }
}