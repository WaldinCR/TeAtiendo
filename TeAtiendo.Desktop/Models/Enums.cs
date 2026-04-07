namespace TeAtiendo.Desktop.Models
{
    public enum RolUsuario
    {
        Cliente = 1,
        Restaurante = 2,
        Admin = 3
    }

    public enum EstadoOrden
    {
        Pendiente = 1,
        EnPreparacion = 2,
        Lista = 3,
        Entregada = 4,
        Cancelada = 5
    }

    public enum EstadoModeracion
    {
        Pendiente,
        Aprobado,
        Rechazado,
        Eliminado
    }
}

