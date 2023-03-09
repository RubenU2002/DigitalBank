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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioLogicaN
    {
        [OperationContract]
        ModoConexion CrearConexion(string _conexionString);
        [OperationContract]
        void Create(Usuario user);
        [OperationContract]
        Usuario Read(int IdUsuario);
        [OperationContract]
        List<Usuario> ReadAll();
        [OperationContract]
        string UpdateById(int IdUsuario, Usuario user);
        [OperationContract]
        string Update(Usuario user);
        [OperationContract]
        string DeleteById(int IdUsuario);
        [OperationContract]
        string Delete(Usuario user);
        // TODO: agregue aquí sus operaciones de servicio
    }

    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    // Puede agregar archivos XSD al proyecto. Después de compilar el proyecto, puede usar directamente los tipos de datos definidos aquí, con el espacio de nombres "LogNeg.ContractType".
    [DataContract]
    public class ModoConexion
    {
        private string _errorMsj = string.Empty;
        private SqlConnection _conexion;
        public string ErrorMsj1 { get => _errorMsj; set => _errorMsj = value; }
        public SqlConnection Conexion { get => _conexion; set => _conexion = value; }

    }
}
