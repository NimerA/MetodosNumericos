using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class MetodoNewton_Model :BaseModel
    {
        public string function1 { get; set; }
        public string function2 { get; set; }
        public double Aproximado { get; set; }
        public double Tol { get; set; }
        public double Iteraciones { get; set; }
        public Answer_Model ans { get; set; }
    }
}