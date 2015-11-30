using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class TrazadorCubicoNatural_model:BaseModel
    {
        public string x { get; set; }
        public string y { get; set; }
        public decimal FPOd { get; set; }
        public decimal FPNd { get; set; }
        public Answer_Model ans { get; set; }
    }
}