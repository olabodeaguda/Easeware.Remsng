using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class PageModel
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalSize { get; set; }
        public object[] Data { get; set; }
    }
}
