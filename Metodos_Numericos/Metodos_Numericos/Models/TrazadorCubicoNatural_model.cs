using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class TrazadorCubicoNatural_model:BaseModel
    {
        public List<Point> values {get;set;}
        public Answer_Model ans { get; set; }
    }
}