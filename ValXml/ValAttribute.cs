using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValToXml
{
    public class ValAttribute
    {
        public ValAttribute()
        {}

        public string name{get;set;}
        public string access { get; set; }
        public string xsi { get; set; }
        public string type { get; set; }
        public string size { get; set; }

    }
}
