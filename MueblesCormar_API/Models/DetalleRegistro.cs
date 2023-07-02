using System;
using System.Collections.Generic;

namespace MueblesCormar_API.Models
{
    public partial class DetalleRegistro
    {
        public int IddetalleRegistro { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnidad { get; set; }
        public decimal Subtotal { get; set; }
        public decimal? Impuestos { get; set; }
        public decimal Total { get; set; }
        public int Idregistro { get; set; }
        public int Idproducto { get; set; }

        public virtual Inventario IdproductoNavigation { get; set; } = null!;
        public virtual Registro IdregistroNavigation { get; set; } = null!;
    }
}
