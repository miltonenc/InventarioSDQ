using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISO815_Inventario.Models
{
    public class AccountingRequest
    {
        public string auxiliar { get; set; }
        public string moneda { get; set; }
        public string descripcion { get; set; }
        public List<AccountingDetalle> detalle { get; set; }
        
    }
}