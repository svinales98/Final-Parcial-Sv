using System;
namespace Infra.Modelos
{
    public class UsuarioModel
    {
        public int idUsuario { get; set; }
        public PersonaModel persona { get; set; }
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public int nivel { get; set; }
        public string estado { get; set; }
    }
}
