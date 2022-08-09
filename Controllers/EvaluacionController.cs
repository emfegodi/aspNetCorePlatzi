using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using platzi_asp_net_core.Models;

namespace platzi_asp_net_core.Controllers
{
    public class EvaluacionController : Controller
    {
        public IActionResult Index(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                return View(_context.Evaluaciones.Find(id));
            }
            else
            {
                return View("ListaEvaluaciones",_context.Evaluaciones);
            }
        }

        public IActionResult ListaEvaluaciones()
        {
            ViewBag.Fecha = DateTime.Now;

            return View("ListaEvaluaciones", _context.Evaluaciones);
        }

        public IActionResult Create()
        {

            ViewBag.Fecha = DateTime.Now;

            return View();
        }
        [HttpPost]
        public IActionResult Create(Evaluacion eva)
        {
            ViewBag.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Evaluaciones.Add(eva);
                _context.SaveChanges();
                ViewBag.MensajeExtra = "Evaluación creada";
                return View("Index", eva);
            }
            else
            {
                return View(eva);
            }
        }

        private EscuelaContext _context;
        public EvaluacionController(EscuelaContext context)
        {
            _context = context;
        }
    }
}