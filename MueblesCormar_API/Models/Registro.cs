using System;
using System.Collections.Generic;

namespace MueblesCormar_API.Models
{
    public partial class Registro
    {
        public Registro()
        {
            DetalleRegistros = new HashSet<DetalleRegistro>();
        }

        public int Idregistro { get; set; }
        public DateTime Fecha { get; set; }
        public string Nota { get; set; } = null!;
        public int Idusuario { get; set; }

        public virtual Usuario IdusuarioNavigation { get; set; } = null!;
        public virtual ICollection<DetalleRegistro> DetalleRegistros { get; set; }
    }
}
