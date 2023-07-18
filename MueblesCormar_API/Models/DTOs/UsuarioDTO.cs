namespace MueblesCormar_API.Models.DTOs
{
    public class UsuarioDTO
    {
        public int Idusuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contrasennia { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int IdrolUsuario { get; set; }
    }
}
