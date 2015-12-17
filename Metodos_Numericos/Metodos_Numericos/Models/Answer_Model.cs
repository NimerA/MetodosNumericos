using Metodos_Numericos.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodos_Numericos.Models
{
    public class Answer_Model : BaseModel
    {
        public string Res { get; set; }
        public int status { get; set; }
    }

    public class AnswerList_Model : BaseModel
    {
        public List<double> Res { get; set; }
        public string message { get; set; }
        public int status { get; set; }
    }

    public class AnswerArr_Model : BaseModel
    {
        public List<List<double>> Res { get; set; }
        public string message { get; set; }
        public int status { get; set; }
    }

    public class AnswerPointd_Model : BaseModel
    {
        public List<Pointd> Res { get; set; }
        public string message { get; set; }
        public int status { get; set; }
    }
}