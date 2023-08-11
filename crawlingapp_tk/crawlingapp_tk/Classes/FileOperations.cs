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
            using (StreamWriter file = new StreamWriter(fileName))
            {
                foreach (var ilan in ilanlarList)
                {
                    file.WriteLine($"Ilan Baslik: {ilan.Baslik}");
                    file.WriteLine($"Ilan Fiyat: {ilan.Fiyat}");
                    file.WriteLine(new string('-', 30));
                }
            }
        }
    }
}
