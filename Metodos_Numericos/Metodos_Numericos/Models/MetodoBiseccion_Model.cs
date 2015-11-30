using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class MetodoBiseccion_Model : BaseModel
    {
        public string function { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public double Tol { get; set; }
        public int Iterator { get; set; }
        public Answer_Model ans { get; set; }
    }
}