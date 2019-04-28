using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using campeonato.Models;

namespace campeonato.Controllers
{
    public class CampeonatoController : Controller
    {
        private Campeonato _times = new Campeonato();

        public ActionResult Index()
        {
            var q = _times.lst_camp.AsQueryable();
            q = q.OrderBy(c => c.chave);
            return View(q.ToList());
        }

        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(CampeonatoModelo _time)
        {
            _times.InserirTime(_time);
            return RedirectToAction("index");
        }
        public ActionResult DeletarTime(int id)
        {
            return View(_times.lst_camp.Where(ps => ps.id == id).First());
        }

        [HttpPost]
        public RedirectToRouteResult DeletarTime(int id, FormCollection collection)
        {
            _times.DeletarTime(id);
            return RedirectToAction("index");
        }

        public ActionResult EditarTime(int id)
        {
            return View(_times.lst_camp.Where(ps => ps.id == id).First());
        }

        [HttpPost]
        public RedirectToRouteResult EditarTime(FormCollection collection, CampeonatoModelo _time)
        {
            _times.UpdateTime(_time);
            return RedirectToAction("index");
        }
    }
}