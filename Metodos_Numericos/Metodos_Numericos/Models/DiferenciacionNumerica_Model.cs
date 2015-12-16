using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class DiferenciacionNumerica_Model : BaseModel
    {
        public int n { get; set; }
        public double xd { get; set; }
        public List<Point> values { get; set; }
        public Answer_Model ans { get; set; }
    }
}