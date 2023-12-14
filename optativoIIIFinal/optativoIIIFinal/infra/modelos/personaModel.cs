using System;
namespace Infra.Modelos
{
    public class PersonaModel
    {
        public int idPersona { get; set; }
        public CiudadModel ciudad { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string tipo_doc { get; set; }
        public string documento { get; set; }
        public string direccion { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public string estado { get; set; }

    }
}

