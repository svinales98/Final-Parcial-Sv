using System;
using Infra.Datos;
using Infra.Modelos;

namespace Service.Service
{
    public class CiudadService
    {
        private CiudadDatos ciudadDatos;

        public CiudadService(string cadenaConexion)
        {
            ciudadDatos = new CiudadDatos(cadenaConexion);
        }

        public void insertarCiudad(CiudadModel ciudad)
        {
            validarCiudad(ciudad);
            ciudadDatos.insertarCiudad(ciudad);
        }

        public void modificarCiudad(CiudadModel ciudad)
        {
            validarCiudad(ciudad);
            ciudadDatos.modificarCiudad(ciudad);
        }

        public void eliminarCiudad(int id)
        {
            ciudadDatos.eliminarCiudad(id);
        }
        public CiudadModel obtenerCiudadporId(int id)
        {
            return ciudadDatos.obtenerCiudadPorId(id);
        }

        public IEnumerable<CiudadModel> obtenerCiudades()
        {
            return ciudadDatos.obtenerCiudades();
        }

        private void validarCiudad(CiudadModel ciudad)
        {
            if (ciudad.ciudad.Trim().Length < 1)
            {
                throw new Exception("El nombre de la ciudad no debe estar nulo");
            }
            if (ciudad.depa.Trim().Length < 1)
            {
                throw new Exception("El departamento de la ciudad no debe estar nulo");
            }
        }

    }
}

