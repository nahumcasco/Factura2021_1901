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
    public class ProductoDAO : Conexion
    {
        SqlCommand comando = new SqlCommand();


        public Producto GetProductoPorCodigo(string codigo)
        {
            Producto _producto = new Producto();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT ID, CODIGO, DESCRIPCION, EXISTENCIA, PRECIO FROM PRODUCTO ");
                sql.Append(" WHERE CODIGO = @Codigo ");
                MiConexion.Close();
                using (MiConexion)
                {
                    MiConexion.Open();
                    using (comando)
                    {
                        comando.Connection = MiConexion;
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = sql.ToString();
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 50).Value = codigo;
                        SqlDataReader dr = comando.ExecuteReader();

                        if (dr.Read())
                        {
                            _producto.Id = (int)dr["ID"];
                            _producto.Codigo = (string)dr["CODIGO"];
                            _producto.Descripcion = (string)dr["DESCRIPCION"];
                            _producto.Existencia = (int)dr["EXISTENCIA"];
                            _producto.Precio = (decimal)dr["PRECIO"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return _producto;
        }

        public DataTable GetProductos()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM PRODUCTO ");
                MiConexion.Close();
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

        public DataTable GetProductosPorDescripcion(string descripcion)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM PRODUCTO WHERE DESCRIPCION LIKE ('%" + descripcion + "%') ");
                MiConexion.Close();
                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
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


    }
}
