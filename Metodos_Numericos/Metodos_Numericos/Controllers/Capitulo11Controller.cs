using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metodos_Numericos.Models;
using Metodos_Numericos.Algorithms;
using System.Text;

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
            model.px = "-2/x";
            model.qx = "2/(x^2)";
            model.rx = "(sin(ln(x)))/(x^2)";
            model.a = 1;
            model.b = 3;
            model.alpha = 1;
            model.beta = 2;
            model.n = 9;
          //  PosicionFalsa Fpos = new PosicionFalsa();
            // model.ans = new Answer_Model();
          //  model.ans.Res = Fpos.Calculate(model.ecuacion, model.aproximacionP0, model.aproximacionP1, model.toleracia, model.iteraciones);
            //  model.ans.status = 0;
            MetodoDiferenciasFinitas resultado = new MetodoDiferenciasFinitas();
            model.ans = new Answer_Model();
            List<Pointd> lista = resultado.Calcular(model.px, model.qx, model.rx, model.a, model.b, model.alpha, model.beta, model.n);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            model.ans.status = 0;
            for (int x = 0; x < lista.Count; x++)
            {
                sb.AppendLine(x.ToString());
            }
            string resu = sb.ToString();
            model.ans.Res = resu;

            return View(model);
        }

        [HttpPost]
        public ActionResult DiferenciasFinitas(DiferenciasFinitas_Model model)
        {

            MetodoDiferenciasFinitas resultado = new MetodoDiferenciasFinitas();
            model.ans = new Answer_Model();
            List<Pointd> lista = resultado.Calcular(model.px, model.qx, model.rx, model.a, model.b, model.alpha, model.beta, model.n);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            if (lista != null){
                model.ans.status = 1;
                for (int x = 0; x < lista.Count; x++)
            {
                sb.AppendLine(x.ToString());
            }
            string resu = sb.ToString();
            model.ans.Res = resu;
            }
            else {
                model.ans.Res = "Metodo Fracaso";
                model.ans.status = 2;
            }
            return View(model);
        }


        [ChildActionOnly]
        public ActionResult _Respuesta(Answer_Model model)
        {
            return PartialView(model);
        }


    }
}