using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class Response
    {
        public bool success { set; get; }
        public string message { set; get; }
        public object data { set; get; }
        public string error { set; get; }
        public ErrorCode code { get; set; }
    }
}