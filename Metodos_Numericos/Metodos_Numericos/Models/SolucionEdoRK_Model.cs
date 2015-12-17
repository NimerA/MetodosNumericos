using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class SolucionEdoRK_Model : BaseModel
    {
        public string function{get; set;}
        public double x0{get; set;}
        public double xf {get; set;}
        public double y0 {get; set;}
        public int n { get; set; }
        public Answer_Model ans { get; set; }
    }
}