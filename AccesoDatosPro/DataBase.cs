using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesPro;

namespace AccesoDatosPro
{
    public class DataBase
    {
        #region Variables Publicas
        public SqlConnection ObjConexionBD { get => _objConexionBD; set => _objConexionBD = value; }
        public DataSet DsResultados { get => _dsResultados; set => _dsResultados = value; }
        public SqlDataAdapter AdaptadorTabla { get => _adaptadorTabla; set => _adaptadorTabla = value; }
        public SqlCommand SqlCommand { get => _sqlCommand; set => _sqlCommand = value; }
        public bool Scalar { get => _scalar; set => _scalar = value; }
        #endregion
        #region Variables Privadas
        private SqlConnection _objConexionBD;
        private bool _scalar;
        private DataSet _dsResultados;
        private SqlDataAdapter _adaptadorTabla;
        private SqlCommand _sqlCommand;

        #endregion
        #region Variables Constructor
        public DataBase(SqlConnection conexion)
        {
            this.ObjConexionBD = conexion;
            Scalar = false;
            DsResultados = null;
        }
        #endregion
        #region Metodos Privados
        private void EjecutarConsulta()
        {
            DsResultados= null;
            if (Scalar)
            {
                if (ObjConexionBD != null)
                {
                    ObjConexionBD.Open();
                    AdaptadorTabla = new SqlDataAdapter(SqlCommand);
                    DsResultados = new DataSet();
                    AdaptadorTabla.Fill(DsResultados);
                    _objConexionBD.Close();
                }
            }
            else
            {
                if (ObjConexionBD != null)
                {
                    ObjConexionBD.Open();
                    SqlCommand.ExecuteNonQuery();
                    ObjConexionBD.Close();
                }
            }
        }
        public void Index()
        {
            SqlCommand = new SqlCommand("sp_leer_todos", ObjConexionBD);
            Scalar = true;
            EjecutarConsulta();
        }
        private void CrearConsulta(int consulta, Usuario persona)
        {
            switch (consulta)
            {
                case 0:
                    Scalar = false;
                    SqlCommand = new SqlCommand("sp_crear_usuario @Nombre, @FechaNac,@Sexo", ObjConexionBD);
                    SqlCommand.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    SqlCommand.Parameters.AddWithValue("@FechaNac", persona.FechaNac);
                    SqlCommand.Parameters.AddWithValue("@Sexo", persona.Sexo);
                    break;
                case 1:
                    Scalar = false;
                    SqlCommand = new SqlCommand("sp_eliminar_usuario @IdUsuario", ObjConexionBD);
                    SqlCommand.Parameters.AddWithValue("@IdUsuario", persona.IdUsuario);
                    break;
                case 2:
                    Scalar = false;
                    SqlCommand = new SqlCommand("sp_actualizar_usuario @IdUsuario, @Nombre, @FechaNac, @Sexo", ObjConexionBD);
                    SqlCommand.Parameters.AddWithValue("@IdUsuario", persona.IdUsuario);
                    SqlCommand.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    SqlCommand.Parameters.AddWithValue("@FechaNac", persona.FechaNac);
                    SqlCommand.Parameters.AddWithValue("@Sexo", persona.Sexo);
                    break;
                case 3:
                    SqlCommand = new SqlCommand("sp_leer_usuario @IdUsuario", ObjConexionBD);
                    SqlCommand.Parameters.AddWithValue("@IdUsuario", persona.IdUsuario);
                    break;
                default:
                    break;
            }

        }
        #endregion
        #region Metodos Publicos
        public void CRUD(int tipoConsulta, Usuario user)
        {
            CrearConsulta(tipoConsulta, user);
            if (tipoConsulta == 3)
                Scalar = true;
            EjecutarConsulta();

        }
        public List<Usuario> Consulta()
        {
            if (DsResultados.Tables[0].Rows.Count != 0)
            {
                List<Usuario> usuariosEnLista = new List<Usuario>();
                DataTable tabla = DsResultados.Tables[0];
                foreach (DataRow row in tabla.Rows)
                {
                    usuariosEnLista.Add(new Usuario { IdUsuario = (int)row["IdUsuario"], FechaNac = (DateTime)row["FechaNac"], Nombre = (string)row["Nombre"], Sexo = (string)row["Sexo"] });
                }
                return usuariosEnLista;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
