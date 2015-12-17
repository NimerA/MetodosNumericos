using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metodos_Numericos.Models;
using Metodos_Numericos.Algorithms;

namespace Metodos_Numericos.Controllers
{
    public class Capitulo4Controller : Controller
    {
        // GET: Capitulo4
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ReglaTrapecio()
        {
            ReglaTrapecio_Model mE = new ReglaTrapecio_Model();
            mE.ans = new Answer_Model();
            mE.ans.Res = "salida";
            return View(mE);
        }
        [HttpPost]
        public ActionResult ReglaTrapecio(ReglaTrapecio_Model model)
        {

            ReglaDelTrapezio resultado = new ReglaDelTrapezio();
            model.ans = new Answer_Model();
            model.ans.Res = resultado.Calcular(model.function, model.a, model.b, model.n);

            if (model.ans.Res[0] == 'L')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return View(model);
        }

        [HttpGet]
        public ActionResult Simpson()
        {
            Simpson_Model model = new Simpson_Model();
            model.function = "sin(x)";
            model.a=0;
            model.b=3.1416;
            model.n=20;


            Simpson resultado = new Simpson();
            model.ans = new Answer_Model();
            model.ans.Res = resultado.Calcular(model.function, model.a, model.b, model.n);
            return View(model);
        }

        [HttpPost]
        public ActionResult Simpson(Simpson_Model model)
        {

            Simpson resultado = new Simpson();
            model.ans = new Answer_Model();
            model.ans.Res = resultado.Calcular(model.function, model.a, model.b, model.n);

            if (model.ans.Res[0] == 'L')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return View(model);
        }

        [HttpGet]
        public ActionResult CuadraturaGauss()
        {
            CuadraturaGauss_Model mE = new CuadraturaGauss_Model();
            mE.function = "x^3+2x^2";
            mE.a = 1;
            mE.b = 5;
            mE.n = 2;
            CuadraturaGauss resultado = new CuadraturaGauss();
            mE.ans = new Answer_Model();
            mE.ans.Res = resultado.Calcular(mE.function, mE.a, mE.b, mE.n);
            return View(mE);
        }

        [HttpPost]
        public ActionResult CuadraturaGauss(CuadraturaGauss_Model model)
             {
      
                 CuadraturaGauss resultado = new CuadraturaGauss();
                 model.ans = new Answer_Model();
                 model.ans.Res = resultado.Calcular(model.function, model.a, model.b, model.n);

                 if (model.ans.Res[0] == 'L')
                     model.ans.status = 1;
                 else
                     model.ans.status = 2;
                 return View(model);
             }

        [HttpGet]
        public ActionResult DiferenciacionNumerica()
        {
            DiferenciacionNumerica_Model mE = new DiferenciacionNumerica_Model();
            mE.ans = new Answer_Model();
            mE.n = 2;
            mE.xd=1.5;
            mE.ans.Res = "30";
            return View(mE);
        }

        [HttpPost]
        public  JsonResult DiferenciacionNumerica(DiferenciacionNumerica_Model model)
          {
              DiferenciacionNumerica dn = new DiferenciacionNumerica();

              double[] list1 = new double[model.values.Count];
              double[] list2 = new double[model.values.Count];


              for (int i = 0; i < model.values.Count; i++)
              {
                  list1[i] = (Double.Parse(model.values[i].y));
                  list2[i] = (Double.Parse(model.values[i].x));
              }
              model.ans = new Answer_Model();

              model.ans.Res = dn.Calcular(list2, list1,model.xd, model.n);

              if (model.ans.Res[0] == 'L')
                  model.ans.status = 1;
              else
                  model.ans.status = 2;
              return Json(model.ans);

          }

        //------------------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------------------------

        [ChildActionOnly]
        public ActionResult _Respuesta(Answer_Model model)
        {
            return PartialView(model);
        }
    }
}