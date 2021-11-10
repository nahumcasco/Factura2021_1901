using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FACTURACION.Modelos.Entidades;

namespace FACTURACION.Modelos.DAO
{
    public class ClienteDAO : Conexion
    {
        SqlCommand comando = new SqlCommand();

        public bool InsertarNuevoCliente(Cliente cliente)
        {
            bool inserto = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO CLIENTE ");
                sql.Append(" VALUES (@Identidad, @Nombre, @Email, @Direccion, @Foto); ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Identidad", SqlDbType.NVarChar, 20).Value = cliente.Identidad;
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = cliente.Nombre;
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = cliente.Email;
                comando.Parameters.Add("@Direccion", SqlDbType.NVarChar, 100).Value = cliente.Direccion;
                if (cliente.Foto != null)
                {
                    comando.Parameters.Add("@Foto", SqlDbType.Image).Value = cliente.Foto;
                }
                else
                {
                    comando.Parameters.Add("@Foto", SqlDbType.Image).Value = DBNull.Value;
                }
                
                comando.ExecuteNonQuery();
                MiConexion.Close();
                inserto = true;
            }
            catch (Exception ex)
            {
                inserto = false;
                MiConexion.Close();
            }
            return inserto;
        }

        public DataTable GetClientes()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM CLIENTE ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();

                SqlDataReader dr = comando.ExecuteReader();
                dt.Load(dr);
                MiConexion.Close();
            }
            catch (Exception)
            {
                MiConexion.Close();
            }
            return dt;
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            bool actualizo = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE CLIENTE ");
                sql.Append(" SET IDENTIDAD = @Identidad, NOMBRE = @Nombre, EMAIL = @Email, DIRECCION = @Direccion, FOTO = @Foto ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Id", SqlDbType.Int).Value = cliente.Id;
                comando.Parameters.Add("@Identidad", SqlDbType.NVarChar, 20).Value = cliente.Identidad;
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = cliente.Nombre;
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = cliente.Email;
                comando.Parameters.Add("@Direccion", SqlDbType.NVarChar, 100).Value = cliente.Direccion;
                if (cliente.Foto != null)
                {
                    comando.Parameters.Add("@Foto", SqlDbType.Image).Value = cliente.Foto;
                }
                else
                {
                    comando.Parameters.Add("@Foto", SqlDbType.Image).Value = DBNull.Value;
                }

                comando.ExecuteNonQuery();
                MiConexion.Close();
                actualizo = true;
            }
            catch (Exception ex)
            {
                actualizo = false;
                MiConexion.Close();
            }
            return actualizo;
        }

        public bool EliminarCliente(int id)
        {
            bool elimino = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM cliente ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                comando.ExecuteNonQuery();
                elimino = true;
                MiConexion.Close();

            }
            catch (Exception ex)
            {
                elimino = false;
                MiConexion.Close();
            }
            return elimino;
        }
    
        public byte[] SeleccionarImagenCliente(int id)
        {
            byte[] miImagen = new byte[0];
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT FOTO FROM CLIENTE ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    miImagen = (byte[])dr["FOTO"];
                }
                MiConexion.Close();
            }
            catch (Exception ex)
            {
                MiConexion.Close();
            }
            return miImagen;
        }
    
        
        public List<KeyValuePair<int, string>> GetClientePorIdentidad(string identidad)
        {
            List<KeyValuePair<int, string>> miLista = new List<KeyValuePair<int, string>>();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT ID, NOMBRE FROM CLIENTE ");
                sql.Append(" WHERE IDENTIDAD = @Identidad; ");

                using (MiConexion)
                {
                    MiConexion.Open();
                    using (comando)
                    {
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = sql.ToString();
                        comando.Parameters.Add("@Identidad", SqlDbType.NVarChar, 20).Value = identidad;

                        SqlDataReader dr = comando.ExecuteReader();
                        if (dr.Read())
                        {
                            miLista.Add(new KeyValuePair<int, string>((int)dr["ID"], dr["NOMBRE"].ToString()));
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }
            return miLista;

        }


        public DataTable GetClientesPorNombre(string nombre)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM CLIENTE ");
                sql.Append(" WHERE NOMBRE LIKE ('%" + nombre + "%') ");
                
                using (MiConexion)
                {
                    MiConexion.Open();
                    using (comando)
                    {
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = sql.ToString();

                        SqlDataReader dr = comando.ExecuteReader();
                        if (dr.Read())
                        {
                            dt.Load(dr);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return dt;
        }





    }
}
