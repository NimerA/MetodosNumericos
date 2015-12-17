using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metodos_Numericos.Models;
using Metodos_Numericos.Algorithms;

namespace Metodos_Numericos.Controllers
{
    public class Capitulo2Controller : Controller
    { 
        //=================================================================== METODO BISECION =======================
        [HttpGet]
        public ActionResult MetodoBiseccion()
        {
            MetodoBiseccion_Model model = new MetodoBiseccion_Model();
            model.function = "x^3+4x^2-10";
            model.a=1;
            model.b=2;
            model.Tol=0.0000001;
            model.Iterator=50;
            MetodoDeBiseccion metbis = new MetodoDeBiseccion();
            model.ans = new Answer_Model();
            model.ans.status = 0;
            model.ans.Res = metbis.MetodoBiseccion(model.function, model.a, model.b, model.Tol, model.Iterator);
            return View(model);
        }
        [HttpPost]
        public ActionResult MetodoBiseccion(MetodoBiseccion_Model model, string submitbutton)
        {
            switch (submitbutton) 
            {
                case "Limpiar":
                    return View(model);
                default:
                    MetodoDeBiseccion metbis = new MetodoDeBiseccion();
                    model.ans = new Answer_Model();
                    model.ans.Res = metbis.MetodoBiseccion(model.function, model.a, model.b, model.Tol, model.Iterator);
                    if (model.ans.Res[0] == 'P')
                        model.ans.status = 1;
                    else
                        model.ans.status = 2;
                    return View(model);
            }

        }
        //=========================================================== METODO PUNTO FIJO ========================================
        [HttpGet]
        public ActionResult MetodoPuntoFijo() 
        {
            Metodopuntofijo_model model = new Metodopuntofijo_model();
            model.function1 = "x^3+4x^2-10";
            model.function2 = "(1/2)(10-x^3)^(1/2)";
            model.Aproximado=1.5;
            model.Tol=0.00001;
            model.Iteraciones=35;
            PuntoFijo metpunto = new PuntoFijo();
            model.ans = new Answer_Model();
            model.ans.Res = metpunto.puntofijo(model.function1, model.function2, model.Aproximado, model.Tol, model.Iteraciones);
            model.ans.status = 0;
            return View(model);
        }
        [HttpPost]
        public ActionResult MetodoPuntoFijo(Metodopuntofijo_model model, string submitbutton)
        {
            PuntoFijo metpunto = new PuntoFijo();
            model.ans = new Answer_Model();
            model.ans.Res = metpunto.puntofijo(model.function1, model.function2, model.Aproximado, model.Tol, model.Iteraciones);
            if (model.ans.Res[0] == 'L')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return View(model);
        }
        //=========================================================== METODO NEWTON ========================================
        [HttpGet]
        public ActionResult MetodoNewton() 
        {
            MetodoNewton_Model model = new MetodoNewton_Model();
            model.function1 = "cos(x)-x";
            model.function2 = "cos(x)-x";
            model.Aproximado = 0.74;
            model.Tol= 0.0001;
            model.Iteraciones=20;
            NewtonRaphson newton = new NewtonRaphson();
            model.ans = new Answer_Model();
            model.ans.Res = newton.newtonRaphson(model.function1, model.function2, model.Aproximado, model.Tol, model.Iteraciones);
            return View(model);
        }
        [HttpPost]
        public ActionResult MetodoNewton(MetodoNewton_Model model, string submitbutton) 
        {
            NewtonRaphson newton = new NewtonRaphson();
            model.ans = new Answer_Model();
            model.ans.Res = newton.newtonRaphson(model.function1, model.function2, model.Aproximado, model.Tol, model.Iteraciones);
            if (model.ans.Res[0] == 'L')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return View(model);
        }

        //==========================================================Metodo Secante=========================================
        [HttpGet]
        public ActionResult MetodoSecante()
        {
            metodoSecante_model model = new metodoSecante_model();
            model.function = "x^3 + 2x^2 + 10x - 20";
            model.XO = 0;
            model.X1 = 1;
            model.EPS = 0;
            model.EPSI = "0.001";
            model.MAXIT = 4;
            MetodoDeLaSecante metsec = new MetodoDeLaSecante();
            model.ans = new Answer_Model();
            metsec.function = "x^3 + 2x^2 + 10x - 20";
            metsec.XO = 0;
            metsec.Xl = 1;
            metsec.EPS = 0;
            metsec.EPSI = 0.001m;
            metsec.MAXIT = 4;
            model.ans.Res = metsec.solve();
            model.ans.status = 0;
            return View(model);
        }


        [HttpPost]
        public ActionResult MetodoSecante(metodoSecante_model model, string submitbutton)
        {
            MetodoDeLaSecante metsec = new MetodoDeLaSecante();
            model.ans = new Answer_Model();
            metsec.function = model.function;
            metsec.XO = model.XO;
            metsec.Xl = model.X1;
            metsec.EPS = model.EPS;
            metsec.EPSI = Convert.ToDecimal(model.EPSI);
            metsec.MAXIT = model.MAXIT;
            model.ans.Res = metsec.solve();
            if (model.ans.Res[0] == 'L')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return View(model);
        }

        //===========================================Falsa Posicion=====================================================
        [HttpGet]
        public ActionResult MetodoFalsaPosicion()
        {
            falsaPosicion_model model = new falsaPosicion_model();
            model.ecuacion = "cos(x)-x";
            model.aproximacionP0 = 0.5;
            model.aproximacionP1 = 0.785398;
            model.toleracia = 0.01;
            model.iteraciones = 7;
            PosicionFalsa Fpos = new PosicionFalsa();
            model.ans = new Answer_Model();
            model.ans.Res = Fpos.Calculate(model.ecuacion, model.aproximacionP0, model.aproximacionP1, model.toleracia, model.iteraciones);
            model.ans.status = 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult MetodoFalsaPosicion(falsaPosicion_model model, string submitbutton)
        {

            PosicionFalsa Fpos = new PosicionFalsa();
            model.ans = new Answer_Model();
            model.ans.Res = Fpos.Calculate(model.ecuacion, model.aproximacionP0, model.aproximacionP1, model.toleracia, model.iteraciones);
            if (model.ans.Res[0] == 'L')
                model.ans.status = 1;
            else
                model.ans.status = 2;
            return View(model);
        }

        //=============================================== Muller===============================================
        [HttpGet]
        public ActionResult Muller()
        {
            Muller_model model = new Muller_model();
            model.function = "16x^4-40x^3+5x^2+20x+6";
            model.X0 = 0.5m;
            model.X1 = 1;
            model.X2 = 1.5m;
            model.tolerancia = "0.001";
            model.iteraciones = 25;
            Muller mullerMet = new Muller();
            model.ans = new Answer_Model();
            mullerMet.Function = "x^3 + 2x^2 + 10x - 20";
            mullerMet.X0 = 0.5m;
            mullerMet.X1 = 1;
            mullerMet.X2 = 1.5m;
            mullerMet.Tol = 0.001m;
            mullerMet.N = (decimal)25;
            model.ans.Res = mullerMet.Solve();
            model.ans.status = 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult Muller(Muller_model model, string submitbutton)
        {
            Muller mullerMet = new Muller();
            model.ans = new Answer_Model();
            mullerMet.Function = model.function;
            mullerMet.X0 = model.X0;
            mullerMet.X1 = model.X1;
            mullerMet.X2 = model.X2;
            mullerMet.Tol = Convert.ToDecimal(model.tolerancia);
            mullerMet.N = (decimal)model.iteraciones;
            model.ans.Res = mullerMet.Solve();
            model.ans.status = 0;

            if (model.ans.Res[0] == 'L')
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