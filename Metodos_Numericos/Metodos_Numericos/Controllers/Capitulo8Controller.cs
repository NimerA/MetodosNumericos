using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metodos_Numericos.Models;
using Newtonsoft.Json;

namespace Metodos_Numericos.Controllers
{
    public class Capitulo8Controller : Controller
    {
        // GET: Capitulo8
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegresionLineal()
        {
            
            return View();
        }

        [HttpPost]
        public JsonResult RegresionLineal(RegresionLineal_Model model)
        {
            //RegresionLineal RL = new RegresionLineal();

            double[] list1 = new double[model.values.Count];
            double[] list2 = new double[model.values.Count];


            for (int i = 0; i < model.values.Count; i++)
            {
                list1[i] = (Double.Parse(model.values[i].y));
                list2[i] = (Double.Parse(model.values[i].x));
            }
            model.ans = new Answer_Model();
            model.ans.Res = "No algorithm Found";

            ////model.ans.Res = RL.Calculate(model.inter, list1, list2);

            //if (model.ans.Res[0] == 'L')
            //    model.ans.status = 1;
            //else
            //    model.ans.status = 2;
            return Json(model.ans);
        }

        [ChildActionOnly]
        public ActionResult _Respuesta( Answer_Model model )
        {
            return PartialView( model );
        }


    }
}