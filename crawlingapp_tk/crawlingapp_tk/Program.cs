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

                string url = "https://www.arabam.com/";// Çekilecek web sitesinin URL'si belirlenir.
                string html = await httpClient.GetStringAsync(url);// Belirtilen URL'den web sayfasının içeriği indirilir ve "html" değişkenine atanır.

                var scrapper = new Class1(httpClient, url); //Class1 sınıfının bir örneği oluşturulur, httpClient ve url parametreleri ile.
                var ilanlarList = scrapper.GetIlanlar(html);// Oluşturulan "scrapper" nesnesi aracılığıyla ilanlar çekilir ve ilanlarList'e atanır.

                foreach (var ilan in ilanlarList)
                {
                    var ilanHtml = await httpClient.GetStringAsync(ilan.IlanDetayUrl);// Her bir ilan için ilanın detay sayfasının içeriği  indirilir.
                    scrapper.GetIlanDetay(ilanHtml, ilan);// Oluşturulan "scrapper" nesnesi aracılığıyla ilanın detayları alınır ve ilan nesnesine eklenir.
                }

                FileOperations.WriteToFile("ilanlar.txt", ilanlarList); // ilanlarList içeriği "ilanlar.txt" adlı bir metin dosyasına yazılır.
                scrapper.DisplayIlanlar(ilanlarList);// Oluşturulan "scrapper" nesnesi aracılığıyla ilanlar ekrana yazdırılır.
                scrapper.DisplayOrtalamaFiyat(ilanlarList); // Oluşturulan "scrapper" nesnesi aracılığıyla ilanların ortalama fiyatı ekrana yazdırılır.
            }
        }
    }
}