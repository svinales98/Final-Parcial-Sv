using Infra.Datos;
using Infra.Modelos;
using System;
namespace Service.Service
{
    public class PersonaService
    {
        private PersonaDatos personaDatos;

        public PersonaService(string cadenaConexion)
        {
            personaDatos = new PersonaDatos(cadenaConexion);
        }

        public void insertarPersona(PersonaModel persona)
        {
            validarDatos(persona);
            personaDatos.insertarPersona(persona);
        }

        public void modificarPersona(PersonaModel persona)
        {
            validarDatos(persona);
            personaDatos.modificarPersona(persona);
        }

        public void eliminarPersona(string cedula)
        {
            personaDatos.eliminarPersona(cedula);
        }
        public PersonaModel obtenerPersonaPorCedula(string cedula)
        {
            return personaDatos.obtenerPersonaPorcedula(cedula);
        }

        public IEnumerable<PersonaModel> obtenerPersonas()
        {
            return personaDatos.obtenerPersonas();
        }

        private void validarDatos(PersonaModel persona)
        {
            if (persona.nombre.Trim().Length < 1)
            {
                throw new Exception("El nombre de la persona no debe estar nulo");
            }
            if (persona.ciudad.idCiudad < 1)
            {
                throw new Exception("Debe informar la ciudad de la persona");
            }
            if (persona.documento.Trim().Length < 1)
            {
                throw new Exception("Debe informar la cedula de la persona");
            }
            if (persona.apellido.Trim().Length < 1)
            {
                throw new Exception("Debe informar el apellido de la persona");
            }
            if (persona.tipo_doc.Trim().Length < 1)
            {
                throw new Exception("Debe informar el tipo de documnento");
            }
        }

    }
}

