using System;
using System.Collections.Generic;
using System.Text;


using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using tunay_crawling;

namespace tunay_crawling
{
    public static class Class2
    {
        public static async Task<List<Ilan>> GetIlanAsync(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = await web.LoadFromWebAsync(url);

            List<Ilan> ilanlar = new List<Ilan>();

            // Web sayfasından gelen HTML dokümanı üzerinde XPath ile belirtilen elemanları seçiyoruz

            var ads = doc.DocumentNode.SelectNodes("//*[@id=\"wrapper\"]/div[2]/div[3]/div/div[1]");

            // StreamWriter nesnesi tanımlanıyor
            using (StreamWriter outputFile = new StreamWriter("ilanlar.txt", true))
            {
                // Seçilen her bir ilan için aşağıdaki işlemler yapılıyor

                foreach (var adss in ads)
                {
                    // İlanın ismi seçiliyor ve yazdırılıyor

                    var name = adss.ParentNode.SelectSingleNode("//*[@id=\"wrapper\"]/div[2]/div[3]/div/div[1]/p");
                    if (name != null)
                    {
                        string ilanName = name.InnerText.TrimEnd('.', ' ', '\n', '\r', '\t', '\0');
                        Console.WriteLine("Name: " + ilanName);
                        outputFile.WriteLine("Name: " + ilanName);
                    }

                    // İlanın fiyatı seçiliyor ve yazdırılıyor

                    var price = adss.ParentNode.SelectSingleNode("//*[@id=\"js-hook-for-observer-detail\"]/div[2]/div[1]/div/div/text()");
                    if (price != null)
                    {
                        string priceText = price.InnerText.Trim().Replace("TL", "").Replace(".", "");
                        priceText = priceText.Replace(".", ""); // Nokta karakterini kaldır

                        // İlan fiyatı sayısal bir değere dönüştürülüyor ve yazdırılıyor

                        double ilanPrice;
                        if (double.TryParse(priceText, NumberStyles.Float, CultureInfo.InvariantCulture, out ilanPrice))
                        {
                            Console.WriteLine("Price: " + ilanPrice.ToString("#.###"));
                            outputFile.WriteLine("Price: " + ilanPrice.ToString("#.###"));
                            string ilanName = Regex.Replace(name.InnerText, @"[\n\r\t]+", "").TrimEnd('.', ' ');
                            ilanlar.Add(new Ilan(ilanName, ilanPrice));

                        }
                        else
                        {
                            Console.WriteLine("Invalid price format for: " + priceText);
                        }
                    }
                    Console.WriteLine("=======================================================================");
                }
                // StreamWriter nesnesi kapatılıyor
                outputFile.Close();
            }

            return ilanlar;
        }

    }
}