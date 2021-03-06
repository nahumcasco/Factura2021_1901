using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FACTURACION.Modelos.DAO;
using FACTURACION.Modelos.Entidades;
using FACTURACION.Vistas;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace FACTURACION.Controladores
{
    public class LoginController
    {
        LoginView vista;

        public LoginController(LoginView view)
        {
            vista = view;
            vista.AceptarButton.Click += new EventHandler(ValidarUsuario);
        }

        private void ValidarUsuario(object serder, EventArgs e)
        {
            UsuarioDAO userDAO = new UsuarioDAO();
            Usuario user = new Usuario();
            user.Email = vista.EmailTextBox.Text;
            user.Clave = EncriptarClave(vista.ClaveTextBox.Text);

            bool valido = userDAO.ValidarUsuario(user);
            if (valido)
            {
                MenuView menu = new MenuView();
                vista.Hide();
                System.Security.Principal.GenericIdentity identidad = new System.Security.Principal.GenericIdentity(vista.EmailTextBox.Text);
                System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(identidad, null);
                System.Threading.Thread.CurrentPrincipal = principal;

                menu.Show();
            }
            else
            {
                MessageBox.Show("Usuario Incorrecto");
            }
        }

        public static string EncriptarClave(string str)
        {
            string cadena = str + "MiClavePersonal";
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(cadena));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }


    }
}
