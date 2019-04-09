using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ISO815_Inventario.Models;

namespace ISO815_Inventario.Controllers
{
    public class AsientoContableController : Controller
    {
        private ISO815_InventarioContext db = new ISO815_InventarioContext();
        private HttpClient client;
        private AccountingRequest accountingRequest;

        // GET: AsientoContable
        public ActionResult Index()
        {
            return View(db.asiento_contable.ToList());
        }

        // GET: AsientoContable/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asiento_contable asiento_contable = db.asiento_contable.Find(id);
            if (asiento_contable == null)
            {
                return HttpNotFound();
            }
            return View(asiento_contable);
        }

        // GET: AsientoContable/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AsientoContable/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,tipo_inventario_id,cuenta_contable,tipo_movimiento,monto,fecha,estado")] asiento_contable asiento_contable)
        {
            if (ModelState.IsValid)
            {
                asiento_contable.estado = 1;
                db.asiento_contable.Add(asiento_contable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asiento_contable);
        }

        // GET: AsientoContable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asiento_contable asiento_contable = db.asiento_contable.Find(id);
            if (asiento_contable == null)
            {
                return HttpNotFound();
            }
            return View(asiento_contable);
        }

        // POST: AsientoContable/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,tipo_inventario_id,cuenta_contable,tipo_movimiento,monto,fecha,estado")] asiento_contable asiento_contable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asiento_contable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asiento_contable);
        }

        // GET: AsientoContable/Sincronizar/5
        public Object Sincronizar(int? id)
        {
            asiento_contable asiento_contable = db.asiento_contable.Find(id);
            var resultado = sincronizacionContabilidadCompletada(asiento_contable);
            return this.Json(resultado, JsonRequestBehavior.AllowGet);
            
        }

        private string sincronizacionContabilidadCompletada(asiento_contable asiento_contable)
        {
            client = new HttpClient();
            accountingRequest = new AccountingRequest();

            accountingRequest.auxiliar = "4";
            accountingRequest.moneda = "DOP";
            accountingRequest.descripcion = "Integracion InventarioSDQ-Contabilidad.";

            List<AccountingDetalle> detalle = new List<AccountingDetalle>();
            AccountingDetalle accountingDetalle = new AccountingDetalle();
            accountingDetalle.numero_cuenta = "6";
            accountingDetalle.tipo_transaccion = "DB";
            accountingDetalle.monto = asiento_contable.monto;
            detalle.Add(accountingDetalle);

            accountingDetalle = new AccountingDetalle();
            accountingDetalle.numero_cuenta = "82";
            accountingDetalle.tipo_transaccion = "CR";
            accountingDetalle.monto = asiento_contable.monto;
            detalle.Add(accountingDetalle);

            accountingRequest.detalle = detalle;

            string URL = " https://prjaccounting20190327125427.azurewebsites.net/api/accounting/post";
            string requestBody = new JavaScriptSerializer().Serialize(accountingRequest);
            Console.Out.WriteLine(">>>>> RESUEST BODY: ");
            Console.Out.WriteLine(requestBody);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = requestBody.Length;
            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(requestBody);
            }

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    Console.Out.WriteLine(response);
                    return response;
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }

            return null;
        }

        // GET: AsientoContable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asiento_contable asiento_contable = db.asiento_contable.Find(id);
            if (asiento_contable == null)
            {
                return HttpNotFound();
            }
            return View(asiento_contable);
        }

        // POST: AsientoContable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            asiento_contable asiento_contable = db.asiento_contable.Find(id);
            asiento_contable.estado = 0;
            db.Entry(asiento_contable).State = EntityState.Modified;
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
