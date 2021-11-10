namespace FACTURACION.Modelos.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Existencia { get; set; }
        public decimal Precio { get; set; }
        public byte[] Foto { get; set; }
        public int IdTipoProducto { get; set; }
    }
}
