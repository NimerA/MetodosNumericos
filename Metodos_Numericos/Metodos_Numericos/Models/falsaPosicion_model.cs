using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class falsaPosicion_model: BaseModel
    {
        public string ecuacion { get; set; }
        public double aproximacionP0 { get; set; }
        public double aproximacionP1 { get; set; }
        public double toleracia { get; set; }
        public int iteraciones { get; set; }
        public Answer_Model ans { get; set; }
    }
}