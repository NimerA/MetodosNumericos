using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metodos_Numericos.Models;
using Metodos_Numericos.Algorithms;

namespace Metodos_Numericos.Controllers
{
    public class Capitulo3Controller : Controller
    {
        //========================================= InterpolacionLaGrange ==========================================
        [HttpGet]
        public ActionResult Lagrange() 
        {
            LaGrange_Model model = new LaGrange_Model();
            model.ans = new Answer_Model();

            return View(model);
        }

        [HttpPost]
        public ActionResult Lagrange(LaGrange_Model model)
        {
            InterpolacionLaGrange lag = new InterpolacionLaGrange();
            string[] xs = model.xs.Split(',');
            string[] fs = model.fs.Split(',');

            double[] list1 = new double[xs.Length];
            double[] list2 = new double[fs.Length];


            for(int i=0;i<xs.Length;i++)
            {
                list1[i] = (Double.Parse(xs[i]));
                list2[i] = (Double.Parse(fs[i]));
            }
            model.ans = new Answer_Model();

            model.ans.Res = lag.Calculate(model.inter, list1, list2);

            if (model.ans.Res[0] == 'L')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return View(model);
        }
        //========================================= InterpolacionLaGrange ==========================================
        [HttpGet]
        public ActionResult InterNewton() 
        {
            InterNewton_Model model = new InterNewton_Model();
            InterpolacionLaGrange internew = new InterpolacionLaGrange();
            model.inter = 5;
            model.xs = "1,1.3,1.6,1.9,2.2";
            model.fs = "0.7651977,0.6200860,0.4554022,0.2818186,0.1103623";
            string[] xs = model.xs.Split(',');
            string[] fs = model.fs.Split(',');

            double[] list1 = new double[xs.Length];
            double[] list2 = new double[fs.Length];


            for (int i = 0; i < xs.Length; i++)
            {
                list1[i] = (Double.Parse(xs[i]));
                list2[i] = (Double.Parse(fs[i]));
            }
            model.ans = new Answer_Model();
            model.ans.Res = internew.Calculate(model.inter, list1, list2);
            model.ans.status = 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult InterNewton(InterNewton_Model model)
        {
            InterpolacionLaGrange internew = new InterpolacionLaGrange();
            string[] xs = model.xs.Split(',');
            string[] fs = model.fs.Split(',');

            double[] list1 = new double[xs.Length];
            double[] list2 = new double[fs.Length];


            for(int i=0;i<xs.Length;i++)
            {
                list1[i] = (Double.Parse(xs[i]));
                list2[i] = (Double.Parse(fs[i]));
            }
            model.ans = new Answer_Model();
            model.ans.Res = internew.Calculate(model.inter, list1, list2);
            if (model.ans.Res[0] == 'L')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return View(model);
        }

        //============================================= Trazadores cúbicos naturales =========================
        [HttpGet]
        public ActionResult TrazadorNatural()
        {
            TrazadorCubicoNatural_model model = new TrazadorCubicoNatural_model();
            model.x = "0.9,1.3,1.9,2.1,2.6,3.0,3.9,4.4,4.7,5.0,6.0,7.0,8.0,9.2,10.5,11.3,11.6,12.0,12.6,13.0,13.3";
            model.y = "1.3,1.5,1.85,2.1,2.6,2.7,2.4,2.15,2.05,2.1,2.25,2.3,2.25,1.95,1.4,0.9,0.7,0.6,0.5,0.4,0.25";
            model.FPOd = 4;
            model.FPNd = 13;

            TrazadoresCubicos trazNat = new TrazadoresCubicos();
            model.ans = new Answer_Model();

            string[] Xvalues = model.x.Split(',');
            string[] Yvalues = model.y.Split(',');
            for (int start = 0; start < Xvalues.Length; start++)
            {
                trazNat.AddPoint(Decimal.Parse(Xvalues[start]), Decimal.Parse(Yvalues[start]));
            }
            trazNat.AddFPO(4m);
            trazNat.AddFPN(13m);
            model.ans.Res = trazNat.Solve();
            model.ans.status = 0;
            return View(model);
        }
        [HttpPost]
        public ActionResult TrazadorNatural(TrazadorCubicoNatural_model model, string submitbutton)
        {

            TrazadoresCubicos trazNat = new TrazadoresCubicos();
            model.ans = new Answer_Model();

            string[] Xvalues = model.x.Split(',');
            string[] Yvalues = model.y.Split(',');
            for (int start = 0; start < Xvalues.Length; start++)
            {
                trazNat.AddPoint(Decimal.Parse(Xvalues[start]), Decimal.Parse(Yvalues[start]));
            }
            trazNat.AddFPO(model.FPOd);
            trazNat.AddFPN(model.FPNd);
            model.ans.Res = trazNat.Solve();
            model.ans.status = 0;

            if (model.ans.Res[0] == 'J')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return View(model);


        }

        [HttpGet]
        public ActionResult TrazadorSujeto()
        {
            TrazadorCubicoSujeto_model model = new TrazadorCubicoSujeto_model();
            model.x = "2,4,5,8,10";
            model.y = "5,6,9,5,4";
            model.FPOd = 1m;
            model.FPNd = 0m;
            TrazadoresCubicos trazSuj = new TrazadoresCubicos();
            model.ans = new Answer_Model();
            trazSuj.SetExample();
            trazSuj.AddFPO(1m);
            trazSuj.AddFPN(0m);
            model.ans.Res = trazSuj.SolveS();
            model.ans.status = 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult TrazadorSujeto(TrazadorCubicoSujeto_model model, string submitbutton)
        {

            TrazadoresCubicos trazSuj = new TrazadoresCubicos();
            model.ans = new Answer_Model();

            try
            {
                string[] Xvalues = model.x.Split(',');
                string[] Yvalues = model.y.Split(',');
                for (int start = 0; start < Xvalues.Length; start++)
                {
                    trazSuj.AddPoint(Decimal.Parse(Xvalues[start]), Decimal.Parse(Yvalues[start]));
                }
                trazSuj.AddFPO(model.FPOd);
                trazSuj.AddFPN(model.FPNd);
                model.ans.Res = trazSuj.SolveS();

            }
            catch (Exception e) 
            {
                model.ans.Res = "Intruduzca Correctamente los Valores.";
            }

            model.ans.status = 0;

            if (model.ans.Res[0] == 'J')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return View(model);
        }


        [ChildActionOnly]
        public ActionResult _Respuesta(Answer_Model model)
        {
            return PartialView(model);
        }
    }
}