using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Metodos_Numericos.Algorithms;
using Metodos_Numericos.Models;
using Newtonsoft.Json;

namespace Metodos_Numericos.Controllers
{
    public class Capitulo6Controller : Controller
    {
        // GET: Capitulo6
        public ActionResult EliminacionDeGauss()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EliminacionDeGauss(eliminacionGaussModel model)
        {
            var values = JsonConvert.DeserializeObject<List<string[]>>( model.values );
            EliminacionGauss eg=new EliminacionGauss();
            double[,] arr = new double[values.Count, values[0].Length];
            try
            {
                for ( int i = 0; i < values.Count; i++ )
                {
                    for ( int j = 0; j < values[ 0 ].Length; j++ )
                    {
                        arr[ i, j ] = Convert.ToDouble( values[ i ][ j ] );
                    }
                }

                model.ans = new AnswerList_Model();
                model.ans.Res = eg.Calcular( arr );

                if ( model.ans.Res.Count>0 )
                    model.ans.status = 1;
                else
                    model.ans.status = 2;
            }
            catch (Exception)
            {
                model.ans.message = "Error en los datos";
            }
            
            return Json(model.ans);
        }

        public ActionResult EliminacionDeGaussJordan()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EliminacionDeGaussJordan( eliminacionGaussModel model )
        {
            var values = JsonConvert.DeserializeObject<List<string[]>>( model.values );
            GaussJordan eg=new GaussJordan();
            double[,] arr = new double[ values.Count, values[ 0 ].Length ];
            try
            {
                for ( int i = 0; i < values.Count; i++ )
                {
                    for ( int j = 0; j < values[ 0 ].Length; j++ )
                    {
                        arr[ i, j ] = Convert.ToDouble( values[ i ][ j ] );
                    }
                }

                model.ans = new AnswerList_Model();
                model.ans.Res = eg.Calcular( arr );

                if ( model.ans.Res.Count > 0 )
                    model.ans.status = 1;
                else
                    model.ans.status = 2;
            }
            catch ( Exception )
            {
                model.ans.message = "Error en los datos";
            }

            return Json( model.ans );
        }

        public ActionResult SistemaEcuacionesInversa()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SistemaEcuacionesInversa( eliminacionGaussModel model )
        {
            var values = JsonConvert.DeserializeObject<List<string[]>>( model.values );
            return Json( "" );
        }
        //eliminacionporgauss
        //eliminacion por gauss jordan
        //sistema de ecuaciones mediante la inversa
    }

    public class eliminacionGaussModel
    {
        public string values  { get;set;}
        public AnswerList_Model ans { get; set; }
    }
}