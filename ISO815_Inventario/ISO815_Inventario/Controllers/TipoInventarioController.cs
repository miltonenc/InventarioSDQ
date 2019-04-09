using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ISO815_Inventario.Models;

namespace ISO815_Inventario.Controllers
{
    public class TipoInventarioController : Controller
    {
        private ISO815_InventarioContext db = new ISO815_InventarioContext();

        // GET: TipoInventario
        public ActionResult Index()
        {
            return View(db.tipo_inventario.ToList());
        }

        // GET: TipoInventario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_inventario tipo_inventario = db.tipo_inventario.Find(id);
            if (tipo_inventario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_inventario);
        }

        // GET: TipoInventario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoInventario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,cuenta_contable,estado")] tipo_inventario tipo_inventario)
        {
            if (ModelState.IsValid)
            {
                tipo_inventario.estado = 1;
                db.tipo_inventario.Add(tipo_inventario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_inventario);
        }

        // GET: TipoInventario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_inventario tipo_inventario = db.tipo_inventario.Find(id);
            if (tipo_inventario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_inventario);
        }

        // POST: TipoInventario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,cuenta_contable,estado")] tipo_inventario tipo_inventario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_inventario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_inventario);
        }

        // GET: TipoInventario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_inventario tipo_inventario = db.tipo_inventario.Find(id);
            if (tipo_inventario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_inventario);
        }

        // POST: TipoInventario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tipo_inventario tipo_inventario = db.tipo_inventario.Find(id);
            tipo_inventario.estado = 0;
            db.Entry(tipo_inventario).State = EntityState.Modified;
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
