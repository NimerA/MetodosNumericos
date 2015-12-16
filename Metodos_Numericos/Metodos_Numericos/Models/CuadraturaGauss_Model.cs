using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metodos_Numericos.Models;

namespace Metodos_Numericos.Models
{
    public class CuadraturaGauss_Model : BaseModel
    {
        public string function { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public int n { get; set; }
        public Answer_Model ans { get; set; }
    }
}