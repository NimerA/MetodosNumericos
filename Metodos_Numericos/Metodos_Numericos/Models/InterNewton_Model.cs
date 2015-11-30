using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class InterNewton_Model: BaseModel
    {
        public double inter { get; set; }
        public string xs { get; set; }
        public string fs { get; set; }
        public Answer_Model ans { get; set; }

    }
}