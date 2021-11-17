using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FACTURACION.Vistas
{
    public partial class MenuView : Syncfusion.Windows.Forms.Office2010Form
    {
        public MenuView()
        {
            InitializeComponent();
        }
        UsuariosView users;
        ClientesView clientes;
        FacturaView facturas;
        private void UsuariosToolStripButton_Click(object sender, EventArgs e)
        {
            if (users == null)
            {
                users = new UsuariosView();
                users.MdiParent = this;
                users.FormClosed += Users_FormClosed;
                users.Show();
            }
            else
            {
                users.Activate();
            }
           
        }

        private void Users_FormClosed(object sender, FormClosedEventArgs e)
        {
            users = null;
        }

        private void ClientesToolStripButton_Click(object sender, EventArgs e)
        {
            if (clientes == null)
            {
                clientes = new ClientesView();
                clientes.MdiParent = this;
                clientes.FormClosed += Clientes_FormClosed;
                clientes.Show();
            }
            else
            {
                clientes.Activate();
            }
        }

        private void Clientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientes = null;
        }

        private void FacturaToolStripButton_Click(object sender, EventArgs e)
        {
            if (facturas == null)
            {
                facturas = new FacturaView();
                facturas.MdiParent = this;
                facturas.FormClosed += Facturas_FormClosed;
                facturas.Show();
            }
            else
            {
                facturas.Activate();
            }
        }

        private void Facturas_FormClosed(object sender, FormClosedEventArgs e)
        {
            facturas = null;
        }
    }
}
