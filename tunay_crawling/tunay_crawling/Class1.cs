using System;
using System.Collections.Generic;
using System.Text;

using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Threading;

namespace tunay_crawling
{
    public static class Class1
    {
        public static async Task<List<string>> GetIlanUrlsAsync()
        {
            HtmlWeb web = new HtmlWeb();
            int maxRetries = 3; // İsteği en fazla 3 kez tekrar deneyecek
            int retryCount = 0;// Şu ana kadar yapılan deneme sayısı
            while (retryCount < maxRetries)
            {
                try
                {
                    HtmlDocument doc = await web.LoadFromWebAsync("https://www.arabam.com");
                    // ilanUrls adında bir List<string> nesnesi tanımlanıyor
                    var ilanUrls = new List<string>();
                    // XPath ifadesiyle belirtilen ilanlar div elementlerinin linkleri toplanıyor

                    var divs = doc.DocumentNode.SelectNodes("//*[@id=\"wrapper\"]/div/div/div/div/div/div/div/div/div/div[1]/a");
                    // Toplanan her bir link ilanUrls listesine ekleniyor
                    foreach (var div in divs)
                    {
                        var ilanUrl = div.GetAttributeValue("href", string.Empty);
                        if (!string.IsNullOrEmpty(ilanUrl))
                        {
                            ilanUrls.Add(ilanUrl);
                        }
                    }
                    // elde edilen ilan url'leri geri döndürülüyor

                    return ilanUrls;
                }
                catch (Exception ex)
                {
                    // Hata mesajı yazdırılıyor

                    Console.WriteLine($"Web isteği sırasında hata oluştu: {ex.Message}");

                    // Yeniden deneme yapılacağına dair mesaj yazdırılıyor

                    Console.WriteLine($"Yeniden deneme yapılıyor ({retryCount + 1}/{maxRetries})...");

                    // Deneme sayısı arttırılıyor

                    retryCount++;

                    // 3 saniye bekletiliyor

                    Thread.Sleep(3000);
                }
            }
            // İstenilen sayıda tekrar denemeden sonra bile başarısızlık yaşanırsa bir Exception fırlatılıyor.

            throw new Exception($"Web isteği {maxRetries} kez denendi fakat başarısız oldu.");
        }
    }
}