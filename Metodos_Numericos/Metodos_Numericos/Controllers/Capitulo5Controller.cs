using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metodos_Numericos.Models;
using Newtonsoft.Json;
using Metodos_Numericos.Algorithms;

namespace Metodos_Numericos.Controllers
{
    public class Capitulo5Controller : Controller
    {
        // GET: Capitulo5
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MetodoDeEuler()
        {
            MetodoDeEuler_Model mE=new MetodoDeEuler_Model();
            mE.ans=new Answer_Model();
            mE.ans.Res = "salida";
            return View(mE);
        }

        public class MetodoDeEulerModel
        {
            public AnswerPointd_Model ans { get; set; }

            public string f { get; set; }

            public string x { get; set; }

            public string xf { get; set; }

            public string y { get; set; }

            public string n { get; set; }
        }

        [HttpPost]
        public JsonResult MetodoDeEuler(MetodoDeEulerModel model)
        {
            MetodoEuler eg = new MetodoEuler();
            
            try
            {
                List<Pointd> r = eg.Calcular(model.f, Convert.ToDouble(model.x), Convert.ToDouble(model.xf), Convert.ToDouble(model.y), Convert.ToInt32(model.n));
                model.ans = new AnswerPointd_Model();
                model.ans.Res = r;

                if (model.ans.Res.Count > 0)
                    model.ans.status = 1;
                else
                    model.ans.status = 2;
            }
            catch (Exception)
            {
                model.ans.message = "Error en los datos";
                model.ans.status = 2;
            }

            return Json(model.ans);
        }

        [HttpGet]
        public ActionResult SolucionEdoRK()
        {
            SolucionEdoRK_Model mE = new SolucionEdoRK_Model();
            mE.ans = new Answer_Model();
            mE.ans.Res = "salida";
            return View(mE);
        }
        public class SolucionRungeKuttaModel
        {
            public AnswerPointd_Model ans { get; set; }

            public string f { get; set; }

            public string x { get; set; }

            public string xf { get; set; }

            public string y { get; set; }

            public string n { get; set; }
        }

        [HttpPost]
        public JsonResult SolucionEdoRK(SolucionRungeKuttaModel model)
        {
            SolucionRungeKutta eg = new SolucionRungeKutta();

            try
            {
                List<Pointd> r = eg.Calcular(model.f, Convert.ToDouble(model.x), Convert.ToDouble(model.xf), Convert.ToDouble(model.y), Convert.ToInt32(model.n));
                model.ans = new AnswerPointd_Model();
                model.ans.Res = r;

                if (model.ans.Res.Count > 0)
                    model.ans.status = 1;
                else
                    model.ans.status = 2;
            }
            catch (Exception)
            {
                model.ans.message = "Error en los datos";
                model.ans.status = 2;
            }

            return Json(model.ans);
        }



        [HttpGet]
        public ActionResult SistemaEdoRK()
        {
            SistemaEdoRK_Model mE = new SistemaEdoRK_Model();
            mE.ans = new Answer_Model();
            mE.ans.Res = "salida";
            return View(mE);
        }
        public class SistemaRungeKuttaModel
        {
            public AnswerPointd_Model ans { get; set; }

            public string x0 { get; set; }

            public string y0 { get; set; }

            public string xf { get; set; }

            public string n { get; set; }
        }
        [HttpPost]
        public JsonResult SistemaEdoRK(SistemaRungeKuttaModel model)
        {
            SistemasRungeKutta eg = new SistemasRungeKutta();

            try
            {
                List<Pointd> r = eg.Calcular(Convert.ToDouble(model.x0), Convert.ToDouble(model.y0), Convert.ToDouble(model.xf), Convert.ToInt32(model.n));
                model.ans = new AnswerPointd_Model();
                model.ans.Res = r;

                if (model.ans.Res.Count > 0)
                    model.ans.status = 1;
                else
                    model.ans.status = 2;
            }
            catch (Exception)
            {
                model.ans.message = "Error en los datos";
                model.ans.status = 2;
            }

            return Json(model.ans);
        }

        [ChildActionOnly]
        public ActionResult _Respuesta( Answer_Model model )
        {
            return PartialView( model );
        }
    }
}