//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ISO815_Inventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class transaccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public transaccion()
        {
            this.estado = 1;
        }

        public int id { get; set; }

        [Display(Name = "Tipo")]
        public string tipo { get; set; }

        [Display(Name = "Articulo")]
        public int articulo_id { get; set; }

        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

        [Display(Name = "Monto")]
        public decimal monto { get; set; }

        [Display(Name = "Fecha")]
        public System.DateTime fecha { get; set; }

        [Display(Name = "Estado")]
        public int estado { get; set; }

        [Display(Name = "Articulo")]
        public virtual articulo articulo { get; set; }
    }
}
