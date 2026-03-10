using System;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Excepciones;

namespace TeAtiendo.Domain.ServiciosDomain
{
    public class UsuarioDomainService
    {
        public void ValidarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new TeAtiendoExcepcion("El usuario no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Correo))
            {
                throw new TeAtiendoExcepcion("El usuario debe tener un correo.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                throw new TeAtiendoExcepcion("El usuario debe tener un nombre.");
            }
        }
    }
}