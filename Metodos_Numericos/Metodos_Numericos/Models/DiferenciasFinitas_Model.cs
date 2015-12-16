using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class DiferenciasFinitas_Model : BaseModel
    {
        public string px { get; set; }
        public string qx { get; set; }
        public string rx { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public double alpha { get; set; }
        public double beta { get; set; }
        public int n { get; set; }
        public Answer_Model ans { get; set; }
        
        }
}