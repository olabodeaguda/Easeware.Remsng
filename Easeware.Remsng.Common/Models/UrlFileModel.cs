using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class UrlFileModel
    {
        public MemoryStream fileStream { get; set; }
        public string contenType { get; set; }
        public string fileName { get; set; }
    }
}
