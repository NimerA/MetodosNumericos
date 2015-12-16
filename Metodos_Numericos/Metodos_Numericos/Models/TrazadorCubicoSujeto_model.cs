using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class TrazadorCubicoSujeto_model:BaseModel
    {
        public List<Point> values
        {
            get;
            set;
        }
        public decimal FPOd { get; set; }
        public decimal FPNd { get; set; }
        public Answer_Model ans { get; set; }
    }
}