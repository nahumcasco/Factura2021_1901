using System;

namespace FACTURACION.Modelos.Entidades
{
    public class Factura
    {
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ISV { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public int IdUsuario { get; set; }
    }
}
