using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using FACTURACION.Modelos.DAO;
using FACTURACION.Modelos.Entidades;
using FACTURACION.Vistas;

namespace FACTURACION.Controladores
{

    public class ClienteController
    {
        ClientesView vista;
        ClienteDAO clienteDAO = new ClienteDAO();
        Cliente cliente = new Cliente();
        string operacion = string.Empty;

        public ClienteController(ClientesView view)
        {
            vista = view;
            vista.NuevoButton.Click += new EventHandler(Nuevo);
            vista.GuardarButton.Click += new EventHandler(Guardar);
            vista.ImagenButton.Click += new EventHandler(CargarImagen);
            vista.Load += new EventHandler(Load);
            vista.ModificarButton.Click += new EventHandler(Modificar);
            vista.EliminarButton.Click += new EventHandler(Eliminar);
        }

        private void Eliminar(object sender, EventArgs e)
        {
            if (vista.ClientesdataGridView.SelectedRows.Count > 0)
            {
                bool elimino = clienteDAO.EliminarCliente(Convert.ToInt32(vista.ClientesdataGridView.CurrentRow.Cells[0].Value));
                if (elimino)
                {
                    MessageBox.Show("Cliente eliminado correctamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarClientes();
                }
                else
                {
                    MessageBox.Show("Cliente no se pudo eliminar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Modificar(object sender, EventArgs e)
        {
            if (vista.ClientesdataGridView.SelectedRows.Count > 0)
            {
                operacion = "Modificar";
                HabilitarControles();

                vista.IdtextBox.Text = vista.ClientesdataGridView.CurrentRow.Cells["ID"].Value.ToString();
                vista.NombreTextBox.Text = vista.ClientesdataGridView.CurrentRow.Cells["NOMBRE"].Value.ToString();
                vista.EmailTextBox.Text = vista.ClientesdataGridView.CurrentRow.Cells["EMAIL"].Value.ToString();
                vista.IdentidadTextBox.Text = vista.ClientesdataGridView.CurrentRow.Cells["IDENTIDAD"].Value.ToString();
                vista.DirecciontextBox.Text = vista.ClientesdataGridView.CurrentRow.Cells["DIRECCION"].Value.ToString();

                byte[] miImagen = clienteDAO.SeleccionarImagenCliente(Convert.ToInt32(vista.ClientesdataGridView.CurrentRow.Cells["ID"].Value));
                if (miImagen.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(miImagen);
                    vista.ImagenPictureBox.Image = Bitmap.FromStream(ms);
                }
                else
                {
                    vista.ImagenPictureBox.Image = null;
                }
            }

            
        }

        private void Load(object sender, EventArgs e)
        {
            ListarClientes();
        }

        private void ListarClientes()
        {
            vista.ClientesdataGridView.DataSource = clienteDAO.GetClientes();
        }

        private void CargarImagen(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                vista.ImagenPictureBox.Image = Image.FromFile(dialog.FileName);
            }

        }

        private void Guardar(object sender, EventArgs e)
        {
            if (vista.IdentidadTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.IdentidadTextBox, "Ingrese una identidad");
                vista.IdentidadTextBox.Focus();
                return;
            }
            if (vista.NombreTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.NombreTextBox, "Ingrese un nombre");
                vista.NombreTextBox.Focus();
                return;
            }
            if (vista.EmailTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.EmailTextBox, "Ingrese un email");
                vista.EmailTextBox.Focus();
                return;
            }
            if (vista.DirecciontextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.DirecciontextBox, "Ingrese un email");
                vista.DirecciontextBox.Focus();
                return;
            }

            cliente.Identidad = vista.IdentidadTextBox.Text;
            cliente.Nombre = vista.NombreTextBox.Text;
            cliente.Email = vista.EmailTextBox.Text;
            cliente.Direccion = vista.DirecciontextBox.Text;
            
            if (vista.ImagenPictureBox.Image != null)
            {
                MemoryStream ms = new MemoryStream();
                vista.ImagenPictureBox.Image.Save(ms, ImageFormat.Jpeg);
                cliente.Foto = ms.GetBuffer();
            }

            if (operacion == "Nuevo")
            {
                bool inserto = clienteDAO.InsertarNuevoCliente(cliente);
                if (inserto)
                {
                    MessageBox.Show("Cliente insertado correctamente");
                    DesabilitarControles();
                    LimpiarControles();
                    ListarClientes();
                }
                else
                {
                    MessageBox.Show("Cliente no se pudo insertar");
                }
            }
            else if(operacion == "Modificar")
            {
                cliente.Id = Convert.ToInt32(vista.IdtextBox.Text);
                bool modifico = clienteDAO.ActualizarCliente(cliente);
                if (modifico)
                {
                    MessageBox.Show("Cliente modificado correctamente", "Atanción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DesabilitarControles();
                    LimpiarControles();
                    ListarClientes();
                }
                else
                {
                    MessageBox.Show("Cliente no se pudo modificar", "Atanción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void Nuevo(object sender, EventArgs e)
        {
            operacion = "Nuevo";
            HabilitarControles();
        }

        private void HabilitarControles()
        {
            vista.IdentidadTextBox.Enabled = true;
            vista.NombreTextBox.Enabled = true;
            vista.EmailTextBox.Enabled = true;
            vista.DirecciontextBox.Enabled = true;
            vista.ImagenPictureBox.Enabled = true;
            vista.ImagenButton.Enabled = true;
            vista.GuardarButton.Enabled = true;
            vista.CancelarButton.Enabled = true;

            vista.ModificarButton.Enabled = false;
            vista.NuevoButton.Enabled = false;
        }

        private void LimpiarControles()
        {
            vista.IdentidadTextBox.Clear();
            vista.NombreTextBox.Clear();
            vista.EmailTextBox.Clear();
            vista.DirecciontextBox.Clear();
            vista.ImagenPictureBox.Image = null;
            vista.IdtextBox.Clear();
        }
        private void DesabilitarControles()
        {
            vista.IdentidadTextBox.Enabled = false;
            vista.NombreTextBox.Enabled = false;
            vista.EmailTextBox.Enabled = false;
            vista.DirecciontextBox.Enabled = false;
            vista.ImagenPictureBox.Enabled = false;
            vista.ImagenButton.Enabled = false;
            vista.GuardarButton.Enabled = false;
            vista.CancelarButton.Enabled = false;

            vista.ModificarButton.Enabled = true;
            vista.NuevoButton.Enabled = true;
        }

    }
}
