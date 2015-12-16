using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class RegresionLineal_Model : BaseModel
    {
        public double inter { get; set; }
        public List<Point> values { get; set; }
        public Answer_Model ans { get; set; }
    }


}