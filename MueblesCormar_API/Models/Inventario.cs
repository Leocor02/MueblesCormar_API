using System;
using System.Collections.Generic;

namespace MueblesCormar_API.Models
{
    public partial class Inventario
    {
        public Inventario()
        {
            DetalleRegistros = new HashSet<DetalleRegistro>();
            ProveedorInventarios = new HashSet<ProveedorInventario>();
        }

        public int Idproducto { get; set; }
        public string Nombre { get; set; } = null!;
        public int Cantidad { get; set; }
        public string Descripcion { get; set; } = null!;
        public string ImagenProducto { get; set; } = null!;
        public decimal PrecioUnidad { get; set; }

        public virtual ICollection<DetalleRegistro> DetalleRegistros { get; set; }
        public virtual ICollection<ProveedorInventario> ProveedorInventarios { get; set; }
    }
}
