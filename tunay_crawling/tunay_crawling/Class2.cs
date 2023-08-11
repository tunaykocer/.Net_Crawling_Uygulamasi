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
            HtmlWeb web = new HtmlWeb();//web sayfası icin
            HtmlDocument doc = await web.LoadFromWebAsync(url);//urlden web içeriğini çekip , işlemek için HtmlDocument nesnesine yüklüyor.

            List<Ilan> ilanlar = new List<Ilan>();

            // Web sayfasında XPath ile belirtilen elemanları seçme işlemi yapıyorum.

            var vtrn = doc.DocumentNode.SelectNodes("//*[@id=\"wrapper\"]/div[2]/div[3]/div/div[1]");

            // StreamWriter nesnesi tanımlıyorum ve ilanlar.txt adlı dosyaya konsoldaki veriler kaydediliyor.
            using (StreamWriter outputFile = new StreamWriter("ilanlar.txt", true))
            {
                // Seçilen her bir ilan için aşağıdaki işlemler yapılıyor
                
                    foreach (var vitrin in vtrn)
                    {
                        // İlanın ismi seçiliyor ve yazdırılıyor

                        var name = vitrin.ParentNode.SelectSingleNode("//*[@id=\"wrapper\"]/div[2]/div[3]/div/div[1]/p");
                        if (name != null)
                        {
                            string ilanName = name.InnerText.TrimEnd('.', ' ', '\n', '\r', '\t', '\0'); //metnin sağ tarafındaki(sonundaki) gereksiz boşlukları satırbaşı ve null karakterleri temizlemesi icin yapılan islem
                            Console.WriteLine("İlanAdı: " + ilanName);
                            outputFile.WriteLine("İlan Adı: " + ilanName);
                        }

                        // İlanın fiyatı seçiliyor ve yazdırılıyor
                        var fiyatt = vitrin.ParentNode.SelectSingleNode("//*[@id=\"js-hook-for-observer-detail\"]/div[2]/div[1]/div/div/text()");
                        if (fiyatt != null)
                        {
                            string fiyatBilgi = fiyatt.InnerText.Trim().Replace("TL", "").Replace(".", "");
                            fiyatBilgi = fiyatBilgi.Replace(".", ""); // Nokta ve boşluğu kaldırma islemi


                            // İlandaki fiyatı yazdırma islemi
                            double ilanFiyati;
                            if (double.TryParse(fiyatBilgi, NumberStyles.Float, CultureInfo.InvariantCulture, out ilanFiyati))
                            {
                                Console.WriteLine("Fiyat: " + ilanFiyati.ToString("#.###"));
                                outputFile.WriteLine("Fiyat: " + ilanFiyati.ToString("#.###"));
                                string ilanName = Regex.Replace(name.InnerText, @"[\n\r\t]+", "").TrimEnd('.', ' ');// metnin sonundaki noktaları ve boşlukları kaldırıp daha düzenli bir hale getirme islemi
                                ilanlar.Add(new Ilan(ilanName, ilanFiyati));

                            }
                            else
                            {
                                Console.WriteLine("Sayılmayanlar: " + fiyatBilgi);
                            }
                        }
                        Console.WriteLine("---------------------------------------------------");
                    }
                // StreamWriter nesnesi kapatılıyor / ilanlar.txt dosyası icin
                outputFile.Close();
                }

                return ilanlar;
            }

        }
    }
