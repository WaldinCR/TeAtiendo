using System;

namespace TeAtiendo.Domain.Excepciones
{
    public class TeAtiendoException : Exception
    {
        public TeAtiendoException(string mensaje) : base(mensaje) { }
    }
}