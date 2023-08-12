using System;
using System.Collections.Generic;

namespace MueblesCormar_API.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Bitacoras = new HashSet<Bitacora>();
            Registros = new HashSet<Registro>();
        }

        public int Idusuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int IdrolUsuario { get; set; }

        public virtual RolUsuario? IdrolUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Bitacora>? Bitacoras { get; set; }
        public virtual ICollection<Registro>? Registros { get; set; }
    }
}
