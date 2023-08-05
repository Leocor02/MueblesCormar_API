namespace MueblesCormar_API.Models.DTOs
{
    public class RegistroDTO
    {
        public int Idregistro { get; set; }
        public DateTime Fecha { get; set; }
        public string Nota { get; set; } = null!;
        public int Idusuario { get; set; }
    }
}
