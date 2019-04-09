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
    public class TransaccionController : Controller
    {
        private ISO815_InventarioContext db = new ISO815_InventarioContext();

        // GET: Transaccion
        public ActionResult Index()
        {
            var transaccions = db.transaccions.Include(t => t.articulo);
            return View(transaccions.ToList());
        }

        // GET: Transaccion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaccion transaccion = db.transaccions.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            return View(transaccion);
        }

        // GET: Transaccion/Create
        public ActionResult Create()
        {
            ViewBag.articulo_id = new SelectList(db.articuloes, "id", "nombre");

            return View();
        }

        // POST: Transaccion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tipo,articulo_id,cantidad,monto,fecha,estado")] transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                transaccion.estado = 1;
                transaccion.fecha = DateTime.Now;
                db.transaccions.Add(transaccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.articulo_id = new SelectList(db.articuloes, "id", "nombre", transaccion.articulo_id);
            return View(transaccion);
        }

        // GET: Transaccion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaccion transaccion = db.transaccions.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }

            ViewBag.articulo_id = new SelectList(db.articuloes, "id", "nombre", transaccion.articulo_id);
            return View(transaccion);
        }

        // POST: Transaccion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tipo,articulo_id,cantidad,monto,fecha,estado")] transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.articulo_id = new SelectList(db.articuloes, "id", "nombre", transaccion.articulo_id);
            return View(transaccion);
        }

        // GET: Transaccion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaccion transaccion = db.transaccions.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }

            ViewBag.articulo_id = new SelectList(db.articuloes, "id", "nombre", transaccion.articulo_id);
            return View(transaccion);
        }

        // POST: Transaccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            transaccion transaccion = db.transaccions.Find(id);
            transaccion.estado = 0;
            db.Entry(transaccion).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.articulo_id = new SelectList(db.articuloes, "id", "nombre", transaccion.articulo_id);
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
