using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ISO815_Inventario.Models
{
    public class ISO815_InventarioContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ISO815_InventarioContext() : base("name=ISO815_INVEntities")
        {
        }

        public System.Data.Entity.DbSet<ISO815_Inventario.Models.almacen> almacens { get; set; }

        public System.Data.Entity.DbSet<ISO815_Inventario.Models.articulo> articuloes { get; set; }

        public System.Data.Entity.DbSet<ISO815_Inventario.Models.asiento_contable> asiento_contable { get; set; }

        public System.Data.Entity.DbSet<ISO815_Inventario.Models.tipo_inventario> tipo_inventario { get; set; }

        public System.Data.Entity.DbSet<ISO815_Inventario.Models.transaccion> transaccions { get; set; }
    }
}
