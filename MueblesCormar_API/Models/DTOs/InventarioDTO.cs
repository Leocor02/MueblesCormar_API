namespace MueblesCormar_API.Models.DTOs
{
    public class InventarioDTO
    {
        public int Idproducto { get; set; }
        public string Nombre { get; set; } = null!;
        public int Cantidad { get; set; }
        public string Descripcion { get; set; } = null!;
        public string ImagenProducto { get; set; } = null!;
        public decimal PrecioUnidad { get; set; }
    }
}
