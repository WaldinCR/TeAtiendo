using System.Net;
using TeAtiendo.Desktop.Services;

namespace TeAtiendo.Desktop.Core.Ui
{
    public static class UIErrorHandler
    {
        public static string ToFriendlyMessage(ApiServiceException ex)
        {
            return ex.StatusCode switch
            {
                HttpStatusCode.Unauthorized => "401: No autorizado. Revisa credenciales.\n" + ex.Message,
                HttpStatusCode.Forbidden => "403: Acceso denegado.\n" + ex.Message,
                HttpStatusCode.Conflict => "409: Conflicto de negocio.\n" + ex.Message,
                HttpStatusCode.BadRequest => "400: Validación/solicitud inválida.\n" + ex.Message,
                _ when (int)ex.StatusCode >= 500 => "500: Error interno.\n" + ex.Message,
                _ => ex.Message
            };
        }
    }
}