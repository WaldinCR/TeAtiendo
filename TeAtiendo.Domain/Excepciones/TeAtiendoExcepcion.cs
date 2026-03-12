namespace TeAtiendo.Domain.Excepciones
{
    public sealed class TeAtiendoExcepcion : Exception
    {
        public TeAtiendoExcepcion(string message) : base(message) { }
    }
}