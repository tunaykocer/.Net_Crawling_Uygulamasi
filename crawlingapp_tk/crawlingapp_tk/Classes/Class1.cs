using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using HtmlAgilityPack;
using System.Threading.Tasks;
using crawlingapp_tk.Classes;

namespace crawlingapp_tk.Classes
{
    public class Class1
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public Class1(HttpClient httpClient, string baseUrl)
        {
            // HttpClient ve baseUrl parametreleri sınıfın özelliklerine atanıyor.
            _httpClient = httpClient;
            _baseUrl = baseUrl;        
        }

        public List<Ilan> GetIlanlar(string html)
        {
            // HTML içeriğinden ilanları çekmek için kullanılan metod.
            // İçeriğe göre ilanların detay URL'leri oluşturulup bir liste olarak döndürülüyor.
            var ilanlarList = new List<Ilan>();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var vitrin_ilanlar = doc.DocumentNode.SelectNodes("//*[@id=\"wrapper\"]/div/div/div/div/div/div/div/div/div/div[1]/a");

            foreach (var ilanNode in vitrin_ilanlar)
            {
                var ilanLink = ilanNode.SelectSingleNode("//*[@id=\"wrapper\"]/div[2]/div[3]/div/div[1]").Attributes["href"].Value;
                ilanlarList.Add(new Ilan { IlanDetayUrl = $"{_baseUrl}{ilanLink}" });
            }

            return ilanlarList;
        }

        public void GetIlanDetay(string ilanHtml, Ilan ilan)
        {
            // İlan detaylarını çekmek için kullanılan metod.
            // İlan HTML içeriğindeki ilan başlığı ve fiyatı ilan nesnesine atanıyor.
            var ilanDoc = new HtmlDocument();
            ilanDoc.LoadHtml(ilanHtml);

            ilan.Baslik = ilanDoc.DocumentNode.SelectSingleNode("//*[@id=\"wrapper\"]/div[2]/div[3]/div/div[1]/p").InnerText.Trim();
            ilan.Fiyat = ilanDoc.DocumentNode.SelectSingleNode("//*[@id=\"js-hook-for-observer-detail\"]/div[2]/div[1]/div/div/text()").InnerText.Trim();
        }

        public void DisplayIlanlar(List<Ilan> ilanlarList)
        {
            // İlanları ekrana yazdırmak için kullanılan metod.
            // İlan başlıkları ve fiyatları liste üzerinde dolaşarak ekrana yazdırılıyor.
            Console.WriteLine("Ilanlar:");
            foreach (var ilan in ilanlarList)
            {
                Console.WriteLine($"Ilan Baslik: {ilan.Baslik}");
                Console.WriteLine($"Ilan Fiyat: {ilan.Fiyat}");
                Console.Write("-----------------------------");
                Console.WriteLine(new string('-', 30));
            }
        }

        public void DisplayOrtalamaFiyat(List<Ilan> ilanlarList)
        {
            // İlanların ortalama fiyatını hesaplamak ve ekrana yazdırmak için kullanılan metod.
            // İlan fiyatları toplanıp ortalama hesaplanarak ekrana yazdırılıyor.
            decimal toplamFiyat = 0;
            foreach (var ilan in ilanlarList)
            {
                decimal fiyat = decimal.Parse(ilan.Fiyat.Replace(" TL", "").Replace(".", "").Replace(",", ""));
                toplamFiyat += fiyat;
            }

            decimal ortalamaFiyat = toplamFiyat / ilanlarList.Count;

            Console.WriteLine($"Ortalama Fiyat: {ortalamaFiyat:C}");
        }
    }
}
