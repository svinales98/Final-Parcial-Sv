using System;
using System.Data;
using Infra.Conexion;
using Infra.Modelos;


namespace Infra.Datos
{
    public class UsuarioDatos
    {
        private ConexionDB conexion;

        public UsuarioDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void insertarUsuario(UsuarioModel usuario)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand("INSERT INTO usuarios(id_usuario, id_per, nombre_usuario, contraseña, nivel, estado)" +
                                                   "VALUES(@id_usuario, @id_per, @nombre_usuario, @contraseña, @nivel, @estado)", conn);
            comando.Parameters.AddWithValue("id_usuario", usuario.idUsuario);
            comando.Parameters.AddWithValue("id_per", usuario.persona.idPersona);
            comando.Parameters.AddWithValue("nombre_usuario", usuario.nombreUsuario);
            comando.Parameters.AddWithValue("contraseña", usuario.contrasena);
            comando.Parameters.AddWithValue("nivel", usuario.nivel);
            comando.Parameters.AddWithValue("estado", "A");
            comando.ExecuteNonQuery();
        }

        public void modificarUsuario(UsuarioModel usuario)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"UPDATE usuarios SET nombre_usuario = '{usuario.nombreUsuario}', contraseña = '{usuario.contrasena}', " +
                                                   $"nivel = '{usuario.nivel}', estado = '{usuario.estado}'" +
                                                   $" WHERE id_usuario = '{usuario.idUsuario}'", conn);

            comando.ExecuteNonQuery();
        }

        public void eliminarUsuario(int id)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"DELETE FROM usuarios" +
                                                $" WHERE id_usuario = '{id}'", conn);
            comando.ExecuteNonQuery();
        }
        public UsuarioModel obtenerUsuarioPorId(int id)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select u.*, p.* from usuarios u " +
                                                   $" inner join persona p on u.id_per = p.id_per" +
                                                   $" where u.id_usuario = '{id}'", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new UsuarioModel
                {
                    idUsuario = reader.GetInt32("id_usuario"),
                    nombreUsuario = reader.GetString("nombre_usuario"),
                    nivel = reader.GetInt32("nivel"),
                    estado = reader.GetString("estado"),
                    persona = new PersonaModel
                    {
                        idPersona = reader.GetInt32("id_per"),
                        nombre = reader.GetString("nombre"),
                        apellido = reader.GetString("apellido"),
                        documento = reader.GetString("nro_doc")
                    }
                };
            }
            return null;
        }

        public UsuarioModel obtenerUsuarioPorNombre(string usuario)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select u.*, p.* from usuarios u " +
                                                   $" inner join persona p on u.id_per = p.id_per" +
                                                   $" where u.nombre_usuario like '{usuario}'", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new UsuarioModel
                {
                    idUsuario = reader.GetInt32("id_usuario"),
                    nombreUsuario = reader.GetString("nombre_usuario"),
                    contrasena = reader.GetString("contraseña"),
                    estado = reader.GetString("estado"),
                    persona = new PersonaModel
                    {
                        nombre = reader.GetString("nombre"),
                        apellido = reader.GetString("apellido"),
                        email = reader.GetString("email")
                    }
                };
            }
            return null;
        }
        public UsuarioModel obtenerUsuario(string usuario)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select nombre_usuario from usuarios" +
                                                   $" where nombre_usuario == '{usuario}'", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new UsuarioModel
                {
                    nombreUsuario = reader.GetString("nombre_usuario"),
                };
            }
            return null;
        }
        public UsuarioModel obtenerContrasena(string usuario)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select contraaseña from usuarios" +
                                                   $" where nombre_usuario == '{usuario}'", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new UsuarioModel
                {
                    contrasena = reader.GetString("contraseña"),
                };
            }
            return null;
        }
    }
}
