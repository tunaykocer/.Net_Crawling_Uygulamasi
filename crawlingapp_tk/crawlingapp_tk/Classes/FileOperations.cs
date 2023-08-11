using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crawlingapp_tk.Classes
{
    public class FileOperations
    {
        public static void WriteToFile(string fileName, List<Ilan> ilanlarList)
        {
            using (StreamWriter file = new StreamWriter(fileName)) // Belirtilen "fileName" adlı dosyayı yazmak için bir StreamWriter nesnesi oluşturulur.
            {
                foreach (var ilan in ilanlarList)
                {
                    file.WriteLine($"Ilan Baslik: {ilan.Baslik}");// İlanın başlığı dosyaya yazılır. String interpolation kullanarak ilan başlığı yazdırılır.
                    file.WriteLine($"Ilan Fiyat: {ilan.Fiyat}");// İlanın fiyatı dosyaya yazılır. String interpolation kullanarak ilan fiyatı yazdırılır.
                    file.WriteLine(new string('-', 30));// 30 karakter uzunluğunda "-" karakteri yazılır. Bu, ilan başlıkları ve fiyatları arasına bir ayırıcı olarak eklenir.
                }
            }
        }
    }
}
