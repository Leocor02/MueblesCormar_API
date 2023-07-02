using System;
using System.Collections.Generic;

namespace MueblesCormar_API.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            ProveedorInventarios = new HashSet<ProveedorInventario>();
        }

        public int Idproveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;

        public virtual ICollection<ProveedorInventario> ProveedorInventarios { get; set; }
    }
}
