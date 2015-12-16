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
        public JsonResult Lagrange(LaGrange_Model model)
        {
            InterpolacionLaGrange lag = new InterpolacionLaGrange();

            double[] list1 = new double[model.values.Count];
            double[] list2 = new double[ model.values.Count ];


            for ( int i=0; i < model.values.Count; i++ )
            {
                list1[ i ] = ( Double.Parse( model.values[i].y) );
                list2[ i ] = ( Double.Parse( model.values[i].x) );
            }
            model.ans = new Answer_Model();

            model.ans.Res = lag.Calculate(model.inter, list1, list2);

            if (model.ans.Res[0] == 'L')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return Json(model.ans);
        }
        //========================================= InterpolacionLaGrange ==========================================
        [HttpGet]
        public ActionResult InterNewton() 
        {
            InterNewton_Model model = new InterNewton_Model();
            InterpolacionLaGrange internew = new InterpolacionLaGrange();
            
            model.ans = new Answer_Model();
            model.ans.status = 0;
            return View(model);
        }

        [HttpPost]
        public JsonResult InterNewton(InterNewton_Model model)
        {
            InterpolacionLaGrange internew = new InterpolacionLaGrange();
            double[] list1 = new double[ model.values.Count ];
            double[] list2 = new double[ model.values.Count ];


            for ( int i=0; i < model.values.Count; i++ )
            {
                list1[ i ] = ( Double.Parse( model.values[ i ].x ) );
                list2[ i ] = ( Double.Parse( model.values[ i ].y ) );
            }
            model.ans = new Answer_Model();
            model.ans.Res = internew.Calculate(model.inter, list1, list2);
            if (model.ans.Res[0] == 'L')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return Json(model.ans);
        }

        //============================================= Trazadores cúbicos naturales =========================
        [HttpGet]
        public ActionResult TrazadorNatural()
        {
            TrazadorCubicoNatural_model model = new TrazadorCubicoNatural_model();
            TrazadoresCubicos trazNat = new TrazadoresCubicos();
            model.ans = new Answer_Model();
            model.ans.status = 0;
            return View(model);
        }
        [HttpPost]
        public JsonResult TrazadorNatural(TrazadorCubicoNatural_model model, string submitbutton)
        {

            TrazadoresCubicos trazNat = new TrazadoresCubicos();
            model.ans = new Answer_Model();
            for ( int start = 0; start < model.values.Count; start++ )
            {
                trazNat.AddPoint( Decimal.Parse( model.values[ start ].x ), Decimal.Parse( model.values[ start ].y ) );
            }
            model.ans.Res = trazNat.Solve();
            model.ans.status = 0;

            if (model.ans.Res[0] == 'J')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return Json(model.ans);


        }

        [HttpGet]
        public ActionResult TrazadorSujeto()
        {
            TrazadorCubicoSujeto_model model = new TrazadorCubicoSujeto_model();
           
            TrazadoresCubicos trazSuj = new TrazadoresCubicos();
            model.ans = new Answer_Model();
            model.ans.status = 0;
            return View(model);
        }

        [HttpPost]
        public JsonResult TrazadorSujeto(TrazadorCubicoSujeto_model model, string submitbutton)
        {

            TrazadoresCubicos trazSuj = new TrazadoresCubicos();
            model.ans = new Answer_Model();

            try
            {
                for (int start = 0; start < model.values.Count; start++)
                {
                    trazSuj.AddPoint( Decimal.Parse( model.values[ start ].x ), Decimal.Parse( model.values[ start ].y ) );
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
            return Json(model.ans);
        }


        [ChildActionOnly]
        public ActionResult _Respuesta(Answer_Model model)
        {
            return PartialView(model);
        }
    }
}