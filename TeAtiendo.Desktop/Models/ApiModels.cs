using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models
{

    public class Usuario
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = "";

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = "";

        [JsonPropertyName("telefono")]
        public string? Telefono { get; set; }

        [JsonPropertyName("rol")]
        public int Rol { get; set; }

        [JsonPropertyName("fechaRegistro")]
        public DateTime? FechaRegistro { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }

    public class Restaurante
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = "";

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("direccion")]
        public string? Direccion { get; set; }

        [JsonPropertyName("telefono")]
        public string? Telefono { get; set; }

        [JsonPropertyName("correo")]
        public string? Correo { get; set; }

        [JsonPropertyName("imagen")]
        public string? Imagen { get; set; }

        [JsonPropertyName("propietarioId")]
        public int? PropietarioId { get; set; }

        [JsonPropertyName("calificacionPromedio")]
        public decimal? CalificacionPromedio { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }
    }

    public class Mesa
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("restauranteId")]
        public int RestauranteId { get; set; }

        [JsonPropertyName("numero")]
        public int Numero { get; set; }

        [JsonPropertyName("capacidad")]
        public int Capacidad { get; set; }

        [JsonPropertyName("ubicacion")]
        public string? Ubicacion { get; set; }

        [JsonPropertyName("estado")]
        public string? Estado { get; set; }
    }

    public class Menu
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("restauranteId")]
        public int RestauranteId { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = "";

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("activo")]
        public bool Activo { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }
    }

    public class CategoriaPlato
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = "";

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }
    }
    public class Plato
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("menuId")]
        public int MenuId { get; set; }

        [JsonPropertyName("categoriaId")]
        public int? CategoriaId { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = "";

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("precio")]
        public decimal Precio { get; set; }

        [JsonPropertyName("imagen")]
        public string? Imagen { get; set; }

        [JsonPropertyName("disponible")]
        public bool Disponible { get; set; }

        [JsonPropertyName("tiempoPreparacion")]
        public int? TiempoPreparacion { get; set; }
    }

    public class Disponibilidad
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("restauranteId")]
        public int RestauranteId { get; set; }

        [JsonPropertyName("diaSemana")]
        public int DiaSemana { get; set; }

        [JsonPropertyName("horaInicio")]
        public string? HoraInicio { get; set; }

        [JsonPropertyName("horaFin")]
        public string? HoraFin { get; set; }

        [JsonPropertyName("activo")]
        public bool Activo { get; set; }
    }

    public class Reserva
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonPropertyName("restauranteId")]
        public int RestauranteId { get; set; }

        [JsonPropertyName("mesaId")]
        public int? MesaId { get; set; }

        [JsonPropertyName("fechaReserva")]
        public DateTime FechaReserva { get; set; }

        [JsonPropertyName("horaReserva")]
        public string? HoraReserva { get; set; }

        [JsonPropertyName("numPersonas")]
        public int NumPersonas { get; set; }

        [JsonPropertyName("estado")]
        public string? Estado { get; set; }

        [JsonPropertyName("notas")]
        public string? Notas { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        
        [JsonPropertyName("usuario")]
        public Usuario? Usuario { get; set; }

        [JsonPropertyName("restaurante")]
        public Restaurante? Restaurante { get; set; }
    }

    public class Orden
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonPropertyName("restauranteId")]
        public int RestauranteId { get; set; }

        [JsonPropertyName("mesaId")]
        public int? MesaId { get; set; }

        [JsonPropertyName("estado")]
        public int Estado { get; set; }

        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("notas")]
        public string? Notas { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaActualizacion")]
        public DateTime? FechaActualizacion { get; set; }

        // Navegación
        [JsonPropertyName("usuario")]
        public Usuario? Usuario { get; set; }

        [JsonPropertyName("restaurante")]
        public Restaurante? Restaurante { get; set; }

        [JsonPropertyName("detalles")]
        public List<DetalleOrden>? Detalles { get; set; }
    }

    public class DetalleOrden
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("ordenId")]
        public int OrdenId { get; set; }

        [JsonPropertyName("platoId")]
        public int PlatoId { get; set; }

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }

        [JsonPropertyName("precioUnitario")]
        public decimal PrecioUnitario { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }

        [JsonPropertyName("plato")]
        public Plato? Plato { get; set; }
    }

    public class Pago
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("ordenId")]
        public int OrdenId { get; set; }

        [JsonPropertyName("monto")]
        public decimal Monto { get; set; }

        [JsonPropertyName("metodoPago")]
        public string? MetodoPago { get; set; }

        [JsonPropertyName("estado")]
        public string? Estado { get; set; }

        [JsonPropertyName("referencia")]
        public string? Referencia { get; set; }

        [JsonPropertyName("fechaPago")]
        public DateTime? FechaPago { get; set; }
    }

    public class Resena
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonPropertyName("restauranteId")]
        public int RestauranteId { get; set; }

        [JsonPropertyName("calificacion")]
        public int Calificacion { get; set; }

        [JsonPropertyName("comentario")]
        public string? Comentario { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("usuario")]
        public Usuario? Usuario { get; set; }
    }
    public class Notificacion
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonPropertyName("titulo")]
        public string? Titulo { get; set; }

        [JsonPropertyName("mensaje")]
        public string Mensaje { get; set; } = "";

        [JsonPropertyName("tipo")]
        public string? Tipo { get; set; }

        [JsonPropertyName("leida")]
        public bool Leida { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }
    }

    public class Auditoria
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("adminId")]
        public int AdminId { get; set; }

        [JsonPropertyName("accion")]
        public string Accion { get; set; } = "";

        [JsonPropertyName("modulo")]
        public string? Modulo { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("admin")]
        public Usuario? Admin { get; set; }
    }

    public class Moderacion
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("adminId")]
        public int? AdminId { get; set; }

        [JsonPropertyName("tipoContenido")]
        public string? TipoContenido { get; set; }

        [JsonPropertyName("contenidoId")]
        public int ContenidoId { get; set; }

        [JsonPropertyName("motivo")]
        public string? Motivo { get; set; }

        [JsonPropertyName("estado")]
        public string? Estado { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaResolucion")]
        public DateTime? FechaResolucion { get; set; }
    }
    
    public class LoginRequest
    {
        [JsonPropertyName("correo")]
        public string Correo { get; set; } = "";

        [JsonPropertyName("password")]
        public string Password { get; set; } = "";
    }

    public class RegisterRequest
    {
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = "";

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = "";

        [JsonPropertyName("password")]
        public string Password { get; set; } = "";

        [JsonPropertyName("telefono")]
        public string? Telefono { get; set; }

        [JsonPropertyName("rol")]
        public int Rol { get; set; } = 1;
    }

    public class CambiarEstadoOrdenRequest
    {
        [JsonPropertyName("nuevoEstado")]
        public int NuevoEstado { get; set; }
    }

    public class CambiarEstadoModeracionRequest
    {
        [JsonPropertyName("nuevoEstado")]
        public string NuevoEstado { get; set; } = "";
    }
}