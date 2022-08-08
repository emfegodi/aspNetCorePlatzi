using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using platzi_asp_net_core.Models;

namespace platzi_asp_net_core.Controllers
{
    public class EvaluacionController : Controller
    {
        public IActionResult Index()
        {
            return View(_context.Evaluaciones.ToList()[5] );
        }

        public IActionResult ListaEvaluaciones()
        {
            ViewBag.Fecha = DateTime.Now;

            return View("ListaEvaluaciones", _context.Evaluaciones);
        }
        
        private EscuelaContext _context;
        public EvaluacionController(EscuelaContext context)
        {
           _context = context; 
        }
    }
}