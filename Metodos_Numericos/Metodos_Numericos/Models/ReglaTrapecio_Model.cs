using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class ReglaTrapecio_Model : BaseModel
    {
        public string function{get; set;}
        public double a{get; set;}
        public double b{get; set;}
        public int n { get; set; }
        public Answer_Model ans { get; set; }
    }
}