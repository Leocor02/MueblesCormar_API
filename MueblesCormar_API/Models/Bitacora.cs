using System;
using System.Collections.Generic;

namespace MueblesCormar_API.Models
{
    public partial class Bitacora
    {
        public int Idbitacora { get; set; }
        public string Accion { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public int Idusuario { get; set; }

        public virtual Usuario IdusuarioNavigation { get; set; } = null!;
    }
}
