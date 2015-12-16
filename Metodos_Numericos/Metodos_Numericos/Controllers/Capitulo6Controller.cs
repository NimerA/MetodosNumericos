using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
            var values = JsonConvert.DeserializeObject<List<List<string>>>( model.values );
            return Json("");
        }

        public ActionResult EliminacionDeGaussJordan()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EliminacionDeGaussJordan( eliminacionGaussModel model )
        {
            var values = JsonConvert.DeserializeObject<List<List<string>>>( model.values );
            return Json( "" );
        }

        public ActionResult SistemaEcuacionesInversa()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SistemaEcuacionesInversa( eliminacionGaussModel model )
        {
            var values = JsonConvert.DeserializeObject<List<List<string>>>( model.values );
            return Json( "" );
        }
        //eliminacionporgauss
        //eliminacion por gauss jordan
        //sistema de ecuaciones mediante la inversa
    }

    public class eliminacionGaussModel
    {
        public string values  { get;set;}
        public Answer_Model ans { get;set;}
    }
}