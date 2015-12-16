using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class MetodoDeEuler_Model:BaseModel
    {
        public string ecuacion { get; set; }

        public int extremoInferior { get; set; }

        public int extremoSuperior { get; set; }

        public int condicionInicial{ get; set; }

        public int numeroEspacios { get; set; }

        public Answer_Model ans
        { get; set; }

}
}
    


