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
    public class ArticuloController : Controller
    {
        private ISO815_InventarioContext db = new ISO815_InventarioContext();

        // GET: Articulo
        public ActionResult Index()
        {
            var articuloes = db.articuloes.Include(a => a.tipo_inventario).Include(a => a.almacen);
            return View(articuloes.ToList());
        }

        // GET: Articulo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articuloes.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // GET: Articulo/Create
        public ActionResult Create()
        {
            ViewBag.tipo_inventario_id = new SelectList(db.tipo_inventario, "id", "nombre");
            ViewBag.almacen_id = new SelectList(db.almacens, "id", "nombre");

            return View();
        }

        // POST: Articulo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,precio,existencia,tipo_inventario_id,estado,almacen_id")] articulo articulo)
        {
            if (ModelState.IsValid)
            {
                articulo.estado = 1;
                db.articuloes.Add(articulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tipo_inventario_id = new SelectList(db.tipo_inventario, "id", "nombre", articulo.tipo_inventario_id);
            ViewBag.almacen_id = new SelectList(db.almacens, "id", "nombre", articulo.almacen_id);
            return View(articulo);
        }

        // GET: Articulo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articuloes.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }

            ViewBag.tipo_inventario_id = new SelectList(db.tipo_inventario, "id", "nombre", articulo.tipo_inventario_id);
            ViewBag.almacen_id = new SelectList(db.almacens, "id", "nombre", articulo.almacen_id);
            return View(articulo);
        }

        // POST: Articulo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,precio,existencia,tipo_inventario_id,estado,almacen_id")] articulo articulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tipo_inventario_id = new SelectList(db.tipo_inventario, "id", "nombre", articulo.tipo_inventario_id);
            ViewBag.almacen_id = new SelectList(db.almacens, "id", "nombre", articulo.almacen_id);
            return View(articulo);
        }

        // GET: Articulo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articuloes.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }

            ViewBag.tipo_inventario_id = new SelectList(db.tipo_inventario, "id", "nombre", articulo.tipo_inventario_id);
            ViewBag.almacen_id = new SelectList(db.almacens, "id", "nombre", articulo.almacen_id);
            return View(articulo);
        }

        // POST: Articulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            articulo articulo = db.articuloes.Find(id);
            articulo.estado = 0;
            db.Entry(articulo).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.tipo_inventario_id = new SelectList(db.tipo_inventario, "id", "nombre", articulo.tipo_inventario_id);
            ViewBag.almacen_id = new SelectList(db.almacens, "id", "nombre", articulo.almacen_id);
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
