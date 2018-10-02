using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class ResponseModel
    {
        public string code { get; set; }
        public string description { get; set; }
        public string[] errors { get; set; }
        public object data { get; set; }
    }
}
