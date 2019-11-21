using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practica5_prog3.Models;

namespace Practica5_prog3.Controllers
{
    public class materiasController : Controller
    {
        private consultaEntities db = new consultaEntities();


        class Resultado { 
            public string id { get; set; }
            public string materias { get; set; }

    }



        // GET: materias
       public ActionResult Index(string busqueda)
        {
            var lista = from x in db.materias select x;

            if (string.IsNullOrEmpty(busqueda))
            {
                return View(db.materias.ToList());

            }
            else
            {
                lista = lista.Where(a => a.materia1.Contains(busqueda));
                return View(lista);
            }
        }
        [HttpPost]
        //public ActionResult Index(String buscar)
        //{
        //    var alfo = from n in db.materia where n.materia1.Contains(buscar) select n;

        //    return View(alfo);


        //}


        // GET: materias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            materia materia = db.materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
        }

        // GET: materias/Create
        public ActionResult Create()
        {
            ViewBag.id_estudiante = new SelectList(db.estudiantes, "id", "nombre");
            return View();
        }

        // POST: materias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,materia1,id_estudiante")] materia materia)
        {
            if (ModelState.IsValid)
            {
                db.materias.Add(materia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_estudiante = new SelectList(db.estudiantes, "id", "nombre", materia.id_estudiante);
            return View(materia);
        }

        // GET: materias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            materia materia = db.materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_estudiante = new SelectList(db.estudiantes, "id", "nombre", materia.id_estudiante);
            return View(materia);
        }

        // POST: materias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,materia1,id_estudiante")] materia materia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_estudiante = new SelectList(db.estudiantes, "id", "nombre", materia.id_estudiante);
            return View(materia);
        }

        // GET: materias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            materia materia = db.materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
        }

        // POST: materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            materia materia = db.materias.Find(id);
            db.materias.Remove(materia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
