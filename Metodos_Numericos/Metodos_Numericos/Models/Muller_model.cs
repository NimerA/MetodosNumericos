using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class Muller_model:BaseModel
    {
        public string function { get; set; }
        public decimal X0 { get; set; }
        public decimal X1 { get; set; }
        public decimal X2 { get; set; }
        public string tolerancia { get; set; }
        public int iteraciones { get; set; }
        public Answer_Model ans { get; set; }
    }
}