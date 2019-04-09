using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISO815_Inventario.Models
{
    public class AccountingDetalle
    {
        public string numero_cuenta { get; set; }
        public string tipo_transaccion { get; set; }
        public decimal monto { get; set; }
    }
}