using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class TwitterTimeLineItem
    {
        public string created_at { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public TwitterUser user { get; set; }
    }
}
