using System;
using System.Data;
using Infra.Conexion;
using Infra.Modelos;

namespace Infra.Datos
{
    public class PersonaDatos
    {
        private ConexionDB conexion;

        public PersonaDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void insertarPersona(PersonaModel persona)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand("INSERT INTO persona(id_persona, idCiudad, nombre, apellido, tipo, documento, direccion" +
                                                   ", celular, email, estado)" +
                                                   "VALUES(@id_persona, @idCiudad, @nombre, @apellido, @tipo, @documento, " +
                                                   "@direccion, @celular, @email, @estado)", conn);
            comando.Parameters.AddWithValue("id_persona", persona.idPersona);
            comando.Parameters.AddWithValue("idCiudad", persona.ciudad.idCiudad);
            comando.Parameters.AddWithValue("nombre", persona.nombre);
            comando.Parameters.AddWithValue("apellido", persona.apellido);
            comando.Parameters.AddWithValue("tipo", persona.tipo_doc);
            comando.Parameters.AddWithValue("documento", persona.documento);
            comando.Parameters.AddWithValue("direccion", persona.direccion);
            comando.Parameters.AddWithValue("celular", persona.celular);
            comando.Parameters.AddWithValue("email", persona.email);
            comando.Parameters.AddWithValue("estado", "A");
            comando.ExecuteNonQuery();
        }

        public void modificarPersona(PersonaModel persona)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"UPDATE persona SET idCiudad = {persona.ciudad.idCiudad}, nombre = '{persona.nombre}', " +
                                                   $"apellido = '{persona.apellido}'" +
                                                   $", tipo = '{persona.tipo_doc}', " +
                                                   $"direccion = '{persona.direccion}'" +
                                                   $", celular = '{persona.celular}', email = '{persona.email}', estado = '{persona.estado}'" +
                                                   $" WHERE documento = '{persona.documento}'", conn);

            comando.ExecuteNonQuery();
        }

        public void eliminarPersona(string documento)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"DELETE FROM persona" +
                                                $" WHERE nro_doc = '{documento}'", conn);
            comando.ExecuteNonQuery();
        }
        public PersonaModel obtenerPersonaPorcedula(string documento)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select c.*, p.* from ciudad c " +
                                                   $" inner join persona p on p.idCiudad = c.idCiudad" +
                                                   $" where p.nro_doc = '{documento}'", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new PersonaModel
                {
                    idPersona = reader.GetInt32("id_persona"),
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    tipo_doc = reader.GetString("tipo"),
                    documento = reader.GetString("documento"),
                    direccion = reader.GetString("direccion"),
                    celular = reader.GetString("celular"),
                    email = reader.GetString("email"),
                    estado = reader.GetString("estado"),
                    ciudad = new CiudadModel
                    {
                        idCiudad = reader.GetInt32("idCiudad"),
                        ciudad = reader.GetString("ciudad"),
                        depa = reader.GetString("depa"),
                        pc = reader.GetInt32("pc")
                    }
                };
            }
            return null;
        }

        public List<PersonaModel> obtenerPersonas()
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select * from persona", conn);
            using var reader = comando.ExecuteReader();
            List<PersonaModel> personas = new List<PersonaModel>();

            while (reader.Read())
            {
                var persona = new PersonaModel
                {
                    idPersona = reader.GetInt32("id_persona"),
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    tipo_doc = reader.GetString("tipo"),
                    documento = reader.GetString("documento"),
                    direccion = reader.GetString("direccion"),
                    celular = reader.GetString("celular"),
                    email = reader.GetString("email"),
                    estado = reader.GetString("estado"),
                    ciudad = new CiudadModel
                    {
                        idCiudad = reader.GetInt32("idCiudad"),
                        ciudad = reader.GetString("ciudad"),
                        depa = reader.GetString("depa"),
                        pc = reader.GetInt32("pc")
                    }
                };
                personas.Add(persona);
            }
            return personas;
        }
    }
}

