using System;
using System.Collections.Generic;

namespace MueblesCormar_API.Models
{
    public partial class RolUsuario
    {
        public RolUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdrolUsuario { get; set; }
        public string Rol { get; set; } = null!;

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
