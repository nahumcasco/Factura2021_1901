using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FACTURACION.Modelos.DAO;
using FACTURACION.Modelos.Entidades;
using FACTURACION.Vistas;

namespace FACTURACION.Controladores
{
    public class FacturaController
    {
        FacturaView vista;
        ClienteDAO clienteDAO = new ClienteDAO();
        Cliente cliente = new Cliente();
        UsuarioDAO usuarioDAO = new UsuarioDAO(); 
        Producto producto = new Producto();
        Usuario user = new Usuario();

        ProductoDAO productoDAO = new ProductoDAO();
        decimal subTotal = 0;
        decimal isv = 0;
        decimal totalPagar = 0;
        FacturaDAO facturaDAO = new FacturaDAO();
        List<DetalleFactura> listaDetalleFactura = new List<DetalleFactura>();

        public FacturaController(FacturaView view)
        {
            vista = view;
            vista.IdentidadTextBox.KeyPress += IdentidadTextBox_KeyPress;
            vista.BuscarClienteButton.Click += BuscarClienteButton_Click;
            vista.Load += Vista_Load;
            vista.CodigoProductoTextBox.KeyPress += CodigoProductoTextBox_KeyPress;
            vista.BuscarProductoButton.Click += BuscarProductoButton_Click;
            vista.CantidadTextBox.KeyPress += CantidadTextBox_KeyPress;
            vista.GuardarButton.Click += GuardarButton_Click;
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Factura factura = new Factura();
            factura.Fecha = vista.dateTimePicker1.Value;
            factura.IdCliente = cliente.Id;
            factura.IdUsuario = user.Id;
            factura.ISV = isv;
            factura.SubTotal = subTotal;
            factura.Descuento = Convert.ToDecimal(vista.DescuentoTextBox.Text);
            factura.Total = Convert.ToDecimal(vista.TotalTextBox.Text);

            bool inserto = facturaDAO.InsertarNuevaFactura(factura, listaDetalleFactura);
            if (inserto)
            {
                MessageBox.Show("Factura insertada correctamente");
            }
            else
            {
                MessageBox.Show("Error al insertar la factura");
            }
        }

        private void CantidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(vista.CantidadTextBox.Text))
            {
                DetalleFactura detalle = new DetalleFactura();
                detalle.IdProducto = producto.Id;
                detalle.Cantidad = Convert.ToInt32(vista.CantidadTextBox.Text);
                detalle.Precio = producto.Precio;
                detalle.Total = Convert.ToInt32(vista.CantidadTextBox.Text) * producto.Precio;

                subTotal += detalle.Total;
                isv = subTotal * 0.15M;
                totalPagar = subTotal + isv;

                listaDetalleFactura.Add(detalle);
                vista.DetalleDataGridView.DataSource = null;
                vista.DetalleDataGridView.DataSource = listaDetalleFactura;

                vista.SubTotalTextBox.Text = subTotal.ToString("N2");
                vista.ImpuestoTextBox.Text = isv.ToString("N2");
                vista.TotalTextBox.Text = totalPagar.ToString("N2");
            }
        }

        private void BuscarProductoButton_Click(object sender, EventArgs e)
        {
            BuscarProductoView form = new BuscarProductoView();
            form.ShowDialog();
            producto = form._producto;
            vista.CodigoProductoTextBox.Text = producto.Codigo;
            vista.DescripcionProductoText.Text = producto.Descripcion;
        }

        private void CodigoProductoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                producto = productoDAO.GetProductoPorCodigo(vista.CodigoProductoTextBox.Text);
                vista.DescripcionProductoText.Text = producto.Descripcion;
            }
            else
            {
                producto = null;
            }
        }

        private void BuscarClienteButton_Click(object sender, EventArgs e)
        {
            BuscarClienteView form = new BuscarClienteView();
            form.ShowDialog();
            cliente = form._cliente;
            vista.IdentidadTextBox.Text = cliente.Identidad;
            vista.NombreClienteTextBox.Text = cliente.Nombre;        
        }

        private void Vista_Load(object sender, EventArgs e)
        {
            user = usuarioDAO.GetUsuarioPorEmail(System.Threading.Thread.CurrentPrincipal.Identity.Name);

            vista.UsuarioTextBox.Text = user.Nombre;
        }

        private void IdentidadTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar ==  (char)Keys.Enter)
            {
                cliente = clienteDAO.GetClientePorIdentidad(vista.IdentidadTextBox.Text);
                vista.NombreClienteTextBox.Text = cliente.Nombre;
            }
            else
            {
                cliente = null;
                
            }
        }
    }
}
