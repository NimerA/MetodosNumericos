using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class metodoSecante_model : BaseModel
    {
        public string function { get; set; }
        public decimal XO{get; set;}
        public decimal X1{get; set;}
        public decimal EPS{get; set;}
        public string EPSI{get; set;}
        public decimal MAXIT{get; set;}
        public Answer_Model ans { get; set; }
    }
}