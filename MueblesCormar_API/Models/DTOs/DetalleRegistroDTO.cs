namespace MueblesCormar_API.Models.DTOs
{
    public class DetalleRegistroDTO
    {
        public int IddetalleRegistro { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnidad { get; set; }
        public decimal Subtotal { get; set; }
        public decimal? Impuestos { get; set; }
        public decimal Total { get; set; }
        public int Idregistro { get; set; }
        public int Idproducto { get; set; }
    }
}
