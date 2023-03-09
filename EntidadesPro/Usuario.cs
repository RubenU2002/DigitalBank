using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesPro
{
    public class Usuario
    {
        private int _idUsuario;
        private string _Nombre;
        private DateTime _FechaNac;
        private string _Sexo;
        [Required]
        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public DateTime FechaNac { get => _FechaNac; set => _FechaNac = value; }
        public string Sexo { get => _Sexo; set => _Sexo = value; }
        public override string ToString()
        {
            return $"iduser: {IdUsuario}, Nombre: {Nombre}, FechaNac: {FechaNac}, sexo: {Sexo}";
        }
    }
    public enum sexoUs { M,F,O}
}
