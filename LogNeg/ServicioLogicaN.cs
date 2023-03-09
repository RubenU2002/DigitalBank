using AccesoDatosPro;
using EntidadesPro;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LogNeg
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código y en el archivo de configuración a la vez.
    public class ServicioLogicaN : IServicioLogicaN
    {
        private readonly string _stringConnection = "Data Source=LAPTOP-4F1904VM;Initial Catalog=PruebaTec;Integrated Security=True";
        private DataBase _objAccesoDatos = null;
        private ModoConexion _mode;
        private enum _sp { crear, eliminar, actualizar, leer }
        public ServicioLogicaN()
        {
            _mode = new ModoConexion();
            if (_mode.ErrorMsj1.Equals(string.Empty))
            {
                _mode.Conexion = CrearConexion(_stringConnection).Conexion;
                ObjAccesoDatos = new DataBase(_mode.Conexion);
            }
        }

        public DataBase ObjAccesoDatos { get => _objAccesoDatos; set => _objAccesoDatos = value; }

        public ModoConexion CrearConexion(string _conexionString)
        {
            SqlConnection conexionBD = new SqlConnection(_conexionString);
            string msj = string.Empty;
            try
            {
                conexionBD.Open();
            }
            catch (Exception ex)
            {
                msj = ex.Message;
            }
            if (conexionBD.State.Equals(System.Data.ConnectionState.Open))
            {
                conexionBD.Close();
                return new ModoConexion { Conexion = conexionBD };
            }
            else
            {
                return new ModoConexion { ErrorMsj1 = $"Error en la conexión: {msj}" };
            }
        }

        public void Create(Usuario user)
        {
            if (ObjAccesoDatos != null)
            {
                Console.WriteLine("entro al create y if");
                ObjAccesoDatos.CRUD((int)_sp.crear, user);
            }
        }

        public string DeleteById(int IdUsuario)
        {
            if (ObjAccesoDatos != null)
            {
                ObjAccesoDatos.CRUD((int)_sp.leer, new Usuario { IdUsuario = IdUsuario });
                if (ObjAccesoDatos.Consulta() != null)
                {
                    var usuarioEliminado = ObjAccesoDatos.Consulta()[0];
                    ObjAccesoDatos.CRUD((int)_sp.eliminar, new Usuario { IdUsuario = IdUsuario });
                    ObjAccesoDatos.DsResultados = null;
                    return $"Se ha eliminado al usuario con: " + usuarioEliminado.ToString();
                }
                return $"El usuario asignado al id de usuario: {IdUsuario} no existe";
            }
            return "La conexion a BD falló";
        }

        public string Delete(Usuario user)
        {
            if (ObjAccesoDatos != null)
            {
                ObjAccesoDatos.CRUD((int)_sp.leer, user);
                if (ObjAccesoDatos.Consulta() != null)
                {
                    var usuarioEliminado = ObjAccesoDatos.Consulta()[0];
                    ObjAccesoDatos.CRUD((int)_sp.eliminar, user);
                    return $"Se ha eliminado al usuario con: " + usuarioEliminado.ToString();
                }
                return $"El usuario asignado al id de usuario: {user.IdUsuario} no existe";
            }
            return "La conexion a BD falló";
        }

        public Usuario Read(int IdUsuario)
        {
            ObjAccesoDatos.CRUD((int)_sp.leer, new Usuario { IdUsuario = IdUsuario });
            if (ObjAccesoDatos.DsResultados.Tables[0].Rows.Count != 0)
            {
                return ObjAccesoDatos.Consulta()[0];
            }
            return new Usuario { IdUsuario = -10 };
        }

        public List<Usuario> ReadAll()
        {
            ObjAccesoDatos.Index();
            return ObjAccesoDatos.Consulta();
        }

        public string UpdateById(int IdUsuario, Usuario user)
        {
            if (ObjAccesoDatos != null)
            {
                ObjAccesoDatos.CRUD((int)_sp.leer, new Usuario { IdUsuario = IdUsuario });
                if (ObjAccesoDatos.DsResultados.Tables[0].Rows.Count != 0)
                {
                    user.IdUsuario = IdUsuario;
                    ObjAccesoDatos.CRUD((int)_sp.actualizar, user);
                    return $"Ha actualizado correctamente al usuario con: {user}";
                }
                return "No se ha actualizado nada";
            }
            return $"La conexión a la BD falló";
        }

        public string Update(Usuario user)
        {
            if (ObjAccesoDatos != null)
            {
                ObjAccesoDatos.CRUD((int)_sp.leer, user);
                if (ObjAccesoDatos.DsResultados.Tables[0].Rows.Count != 0)
                {
                    ObjAccesoDatos.CRUD((int)_sp.actualizar, user);
                    return $"Ha actualizado correctamente al usuario con: {user}";
                }
                return "No se ha actualizado nada";
            }
            return $"La conexión a la BD falló";
        }
    }
}
