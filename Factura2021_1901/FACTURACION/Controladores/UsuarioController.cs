using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FACTURACION.Modelos.DAO;
using FACTURACION.Modelos.Entidades;
using FACTURACION.Vistas;

namespace FACTURACION.Controladores
{
    public class UsuarioController
    {
        UsuariosView vista;
        string operacion = string.Empty;

        public UsuarioController(UsuariosView view)
        {
            vista = view;
            vista.NuevoButton.Click += new EventHandler(Nuevo);
            vista.GuardarButton.Click += new EventHandler(Guardar);
        }

        private void Nuevo(object sender, EventArgs e)
        {
            HabilitarControles();
            operacion = "Nuevo";
        }

        private void Guardar(object sender, EventArgs e)
        {
            if (vista.NombretextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.NombretextBox, "Ingrese un nombre");
                vista.NombretextBox.Focus();
                return;
            }
            if (vista.EmailtextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.EmailtextBox, "Ingrese un email");
                vista.EmailtextBox.Focus();
                return;
            }
            if (vista.ClavetextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.ClavetextBox, "Ingrese una contraseña");
                vista.ClavetextBox.Focus();
                return;
            }

            UsuarioDAO usuarioDAO = new UsuarioDAO();
            Usuario user = new Usuario();
            user.Nombre = vista.NombretextBox.Text;
            user.Email = vista.EmailtextBox.Text;
            user.Clave = vista.ClavetextBox.Text;
            user.EsAdministrador = vista.EsAdministradorcheckBox.Checked;

            if (operacion == "Nuevo")
            {
               bool inserto = usuarioDAO.InsertarNuevoUsuario(user);
                LimpiarControles();
                DesabilitarControles();
            }


            
        }

        private void HabilitarControles()
        {
            vista.NombretextBox.Enabled = true;
            vista.EmailtextBox.Enabled = true;
            vista.ClavetextBox.Enabled = true;
            vista.EsAdministradorcheckBox.Enabled = true;
            vista.GuardarButton.Enabled = true;
            vista.CancelarButton.Enabled = true;
            vista.ModificarButton.Enabled = false;
            vista.NuevoButton.Enabled = false;
        }

        private void LimpiarControles()
        {
            vista.NombretextBox.Clear();
            vista.EmailtextBox.Clear();
            vista.ClavetextBox.Clear();
            vista.EsAdministradorcheckBox.Checked = false;
        }

        private void DesabilitarControles()
        {
            vista.NombretextBox.Enabled = false;
            vista.EmailtextBox.Enabled = false;
            vista.ClavetextBox.Enabled = false;
            vista.EsAdministradorcheckBox.Enabled = false;
            vista.GuardarButton.Enabled = false;
            vista.CancelarButton.Enabled = false;
            vista.ModificarButton.Enabled = true;
            vista.NuevoButton.Enabled = true;
        }

    }
}
