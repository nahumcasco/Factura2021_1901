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
    public partial class BuscarClienteView : Form
    {
        public BuscarClienteView()
        {
            InitializeComponent();
        }
        ClienteDAO clienteDAO = new ClienteDAO();

       public Cliente _cliente = new Cliente();

        private void BuscarClienteView_Load(object sender, EventArgs e)
        {
            ClientesdataGridView.DataSource = clienteDAO.GetClientes();
        }

        private void NombreClienteTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ClientesdataGridView.DataSource = clienteDAO.GetClientesPorNombre(NombreClienteTextBox.Text);
        }

        private void Aceptarbutton_Click(object sender, EventArgs e)
        {
            if (ClientesdataGridView.RowCount > 0)
            {
                if (ClientesdataGridView.SelectedRows.Count > 0)
                {
                    _cliente.Id =  (int)ClientesdataGridView.CurrentRow.Cells["ID"].Value;
                    _cliente.Identidad = ClientesdataGridView.CurrentRow.Cells["IDENTIDAD"].Value.ToString();
                    _cliente.Nombre = ClientesdataGridView.CurrentRow.Cells["NOMBRE"].Value.ToString();
                    this.Close();
                }
            }
        }

        private void Cancelarbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
