using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeAtiendo.Domain.Excepciones;

namespace TeAtiendo.Domain.Excepciones
{
    
    public class TeAtiendoExcepcion : Exception
    {
        public TeAtiendoExcepcion(string mensaje) : base(mensaje) { }
    }
}