using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.WebServcie
{
    public class PageInfo<T>
    {
        public List<T> ItemList { get; set; }
        public int Total { get; set; }
    }
}