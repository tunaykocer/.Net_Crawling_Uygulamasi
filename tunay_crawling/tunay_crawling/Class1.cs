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
            int deneme = 2; //  en fazla 2 kez tekrar deneyecek
            int denemesayisi = 0;// Şu ana kadar yapılan deneme sayısı
            while (denemesayisi < deneme)
            {
                try
                {
                    HtmlDocument doc = await web.LoadFromWebAsync("https://www.arabam.com");
                    
                    var ilanUrls = new List<string>(); // ilanUrls adında bir List<string> nesnesi tanımlanıyor

                    var divs = doc.DocumentNode.SelectNodes("//*[@id=\"wrapper\"]/div/div/div/div/div/div/div/div/div/div[1]/a");// XPath ifadesiyle belirtilen ilanlar div elementlerinin linkleri toplanıyor

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
                catch (Exception except)
                {
                    // Hata mesajı yazdırılıyor

                    Console.WriteLine($"Hata oluştu: {except.Message}");  //except ifadesini kullanabilmek için '$' bu işaret kullanılmalı

                    // Yeniden deneme yapılacağına dair mesaj yazdırılıyor

                    Console.WriteLine($"Tekrar ({denemesayisi + 1}/{deneme})");

                    // Deneme sayısı arttırma

                    denemesayisi++;

                    // 3 saniye bekleme islemi

                    Thread.Sleep(3000);
                }
            }
            // İstenilen sayıda tekrar denemeden sonra hata olursa exception atar..

            throw new Exception($"Web isteği {deneme} defa başarılamadı");
        }
    }
}