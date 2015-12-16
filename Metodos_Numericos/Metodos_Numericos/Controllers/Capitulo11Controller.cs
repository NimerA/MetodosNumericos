using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metodos_Numericos.Models;
using Metodos_Numericos.Algorithms;

namespace Metodos_Numericos.Controllers
{
    public class Capitulo11Controller : Controller
    {
        // GET: Capitulo11
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DiferenciasFinitas()
        {
            DiferenciasFinitas_Model model = new DiferenciasFinitas_Model();
            model.px = "cos(x)-x";
            model.qx = "cos(x)-x";
            model.rx = "cos(x)-x";
            model.a = 0.01;
            model.b = 7;
            model.alpha = 0.4;
            model.beta = 0.3;
            model.n = 3;
          //  PosicionFalsa Fpos = new PosicionFalsa();
            // model.ans = new Answer_Model();
          //  model.ans.Res = Fpos.Calculate(model.ecuacion, model.aproximacionP0, model.aproximacionP1, model.toleracia, model.iteraciones);
            //  model.ans.status = 0;
            model.ans = new Answer_Model();
            model.ans.Res = "FALTA LINKEAR EL AGORITMO";

            return View(model);
        }

        [HttpPost]
        public ActionResult DiferenciasFinitas(DiferenciasFinitas_Model model, string submitbutton)
        {

            //PosicionFalsa Fpos = new PosicionFalsa();
            //model.ans = new Answer_Model();
            //model.ans.Res = Fpos.Calculate(model.ecuacion, model.aproximacionP0, model.aproximacionP1, model.toleracia, model.iteraciones);
            //if (model.ans.Res[0] == 'L')
            //    model.ans.status = 1;
            //else
            //    model.ans.status = 2;
            return View(model);
        }


        [ChildActionOnly]
        public ActionResult _Respuesta(Answer_Model model)
        {
            return PartialView(model);
        }


    }
}