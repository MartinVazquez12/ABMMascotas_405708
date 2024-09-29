using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ABMMascotas
{
    public class AcessoDatos
    {
        private string lineaConexion = @"Data Source=172.16.10.196;Initial Catalog=Veterinaria;User ID=alumno1w1;Password=alumno1w1";
        private SqlCommand comando;
        private SqlConnection conexion;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
            set { lector = value; }
        }

        public AcessoDatos()
        {
            conexion = new SqlConnection(lineaConexion);
            
        }


        private void Conectar()
        {
            conexion.Open();
            comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;          
        }

        public void Desconectar()
        {
            conexion.Close();
        }

        public DataTable ConsultarBD(string nombreTabla)
        {
            DataTable tabla = new DataTable();
            Conectar();
            comando.CommandText = "SELECT * FROM " + nombreTabla + " ORDER BY 2";
            tabla.Load(comando.ExecuteReader());
            Desconectar();
            return tabla;

        }

        public bool InsertarNuevo(string nombreTabla, List<Parametros> lParametros)
        {
            int filasAfectadas = 0;
            Conectar();
            comando.CommandText = "INSERT INTO " + nombreTabla + " VALUES(@codigo, @nombre, @especie, @sexo, @fechanacimiento)";

            foreach (Parametros prmtro in lParametros)
            {
                comando.Parameters.AddWithValue(prmtro.Nombre, prmtro.Valor);
            }
            filasAfectadas = comando.ExecuteNonQuery();
            Desconectar();
            return filasAfectadas == 1;
        }



    }
}
