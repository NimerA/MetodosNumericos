using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metodos_Numericos.Models;

namespace Metodos_Numericos.Controllers
{
    public class Capitulo5Controller : Controller
    {
        // GET: Capitulo5
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MetodoDeEuler()
        {
            MetodoDeEuler_Model mE=new MetodoDeEuler_Model();
            mE.ans=new Answer_Model();
            mE.ans.Res = "salida";
            return View(mE);
        }

        [HttpGet]
        public ActionResult SolucionEdoRK()
        {
            SolucionEdoRK_Model mE = new SolucionEdoRK_Model();
            mE.ans = new Answer_Model();
            mE.ans.Res = "salida";
            return View(mE);
        }

        [HttpGet]
        public ActionResult SistemaEdoRK()
        {
            SistemaEdoRK_Model mE = new SistemaEdoRK_Model();
            mE.ans = new Answer_Model();
            mE.ans.Res = "salida";
            return View(mE);
        }


        [ChildActionOnly]
        public ActionResult _Respuesta( Answer_Model model )
        {
            return PartialView( model );
        }
    }
}