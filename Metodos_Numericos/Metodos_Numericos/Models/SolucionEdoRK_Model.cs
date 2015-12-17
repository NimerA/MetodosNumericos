using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class SolucionEdoRK_Model : BaseModel
    {
        public string ecuacion { get; set; }
        public double extremoInferior { get; set; }
        public double extremoSuperior { get; set; }
        public double condicionInicial { get; set; }
        public int numeroEspacios { get; set; }
        public Answer_Model ans
        { get; set; }
    }
}