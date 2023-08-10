using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace tunay_crawling
{
    public class Ilan
    {

        public string IlanAdii { get; set; }
        public double Fiyat { get; set; }

        public Ilan(string ilanadii, double fiyat)
        {
            IlanAdii = ilanadii;
            Fiyat = fiyat;
        }
    }


}