using System;
using System.Collections.Generic;

namespace MueblesCormar_API.Models
{
    public partial class ProveedorInventario
    {
        public int IdproveedorInventario { get; set; }
        public int Idproveedor { get; set; }
        public int Idproducto { get; set; }

        public virtual Inventario IdproductoNavigation { get; set; } = null!;
        public virtual Proveedor IdproveedorNavigation { get; set; } = null!;
    }
}
