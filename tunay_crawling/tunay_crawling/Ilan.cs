using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace tunay_crawling
{
    public class Ilan
    {

        public string Name { get; set; }
        public double Price { get; set; }

        public Ilan(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }


}