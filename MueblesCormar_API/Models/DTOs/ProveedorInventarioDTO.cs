namespace MueblesCormar_API.Models.DTOs
{
    public class ProveedorInventarioDTO
    {
        public int IdproveedorInventario { get; set; }
        public int Idproveedor { get; set; }
        public int Idproducto { get; set; }
        public string NombreProveedor { get; set; } = null!;
        public string NombreProducto { get; set; } = null!;
    }
}
