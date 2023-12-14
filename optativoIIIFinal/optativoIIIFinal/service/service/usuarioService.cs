using System;
using Infra.Datos;
using Infra.Modelos;

namespace Service.Service;

public class UsuarioService
{
    private UsuarioDatos usuarioDatos;

    public UsuarioService(string cadenaConexion)
    {
        usuarioDatos = new UsuarioDatos(cadenaConexion);
    }

    public void insertarUsuario(UsuarioModel usuario)
    {
        validarDatos(usuario);
        usuarioDatos.insertarUsuario(usuario);
    }

    public void modificarUsuario(UsuarioModel usuario)
    {
        validarDatos(usuario);
        usuarioDatos.modificarUsuario(usuario);
    }

    public void eliminarUsuario(int id)
    {
        usuarioDatos.eliminarUsuario(id);
    }
    public UsuarioModel obtenerusuarioPorId(int id)
    {
        return usuarioDatos.obtenerUsuarioPorId(id);
    }

    public UsuarioModel obtenerusuarioPorNombre(string usuario)
    {
        return usuarioDatos.obtenerUsuarioPorNombre(usuario);
    }

    private void validarDatos(UsuarioModel usuario)
    {
        if (usuario.nombreUsuario.Trim().Length < 1)
        {
            throw new Exception("El nombre del usuario no debe estar nulo");
        }
        if (usuario.contrasena.Trim().Length < 1)
        {
            throw new Exception("Es obligatorio registrar una contraseña para el usuario");
        }
    }
}
