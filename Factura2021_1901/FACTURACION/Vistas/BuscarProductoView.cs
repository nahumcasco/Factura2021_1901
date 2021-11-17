using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FACTURACION.Modelos.DAO;
using FACTURACION.Modelos.Entidades;

namespace FACTURACION.Vistas
{
    public partial class BuscarProductoView : Form
    {
        public BuscarProductoView()
        {
            InitializeComponent();
        }
        ProductoDAO productoDAO = new ProductoDAO();
        public Producto _producto = new Producto();

        private void Aceptarbutton_Click(object sender, EventArgs e)
        {
            if (ProductosdataGridView.RowCount > 0)
            {
                if (ProductosdataGridView.SelectedRows.Count > 0)
                {
                    _producto.Id = (int)ProductosdataGridView.CurrentRow.Cells["ID"].Value;
                    _producto.Codigo = (string)ProductosdataGridView.CurrentRow.Cells["CODIGO"].Value;
                    _producto.Descripcion = (string)ProductosdataGridView.CurrentRow.Cells["DESCRIPCION"].Value;
                    _producto.Existencia = (int)ProductosdataGridView.CurrentRow.Cells["EXISTENCIA"].Value;
                    _producto.Precio = (decimal)ProductosdataGridView.CurrentRow.Cells["PRECIO"].Value;
                    this.Close();
                }
            }
        }

        private void Cancelarbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BuscarProductoView_Load(object sender, EventArgs e)
        {
            ProductosdataGridView.DataSource = productoDAO.GetProductos();
        }

        private void NombreProductoTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ProductosdataGridView.DataSource = productoDAO.GetProductosPorDescripcion(NombreProductoTextBox.Text);
        }
    }
}
