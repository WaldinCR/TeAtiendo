namespace TeAtiendo.Web.Services
{
    public class NotificationService
    {
        public event Action? OnChange;
        private List<AppNotification> _notifications = new();

        public IReadOnlyList<AppNotification> Notifications => _notifications.AsReadOnly();
        public int UnreadCount => _notifications.Count(n => !n.Leida);

        public void Push(string tipo, string mensaje, string? link = null)
        {
            _notifications.Insert(0, new AppNotification
            {
                Id = _notifications.Count + 1,
                Tipo = tipo,
                Mensaje = mensaje,
                Link = link,
                Leida = false,
                Fecha = DateTime.Now
            });
            OnChange?.Invoke();
        }

        // Shortcuts
        public void ReservaConfirmada(string restaurante, string fecha)
            => Push("reserva_confirmada", $"Tu reserva en <strong>{restaurante}</strong> para el {fecha} fue confirmada", "/reservas");

        public void ReservaCancelada(string restaurante)
            => Push("reserva_rechazada", $"Tu reserva en <strong>{restaurante}</strong> fue cancelada", "/reservas");

        public void OrdenCreada(string restaurante, int platos)
            => Push("orden_preparacion", $"Tu orden de {platos} platos en <strong>{restaurante}</strong> esta siendo preparada", "/ordenes");

        public void PagoAprobado(decimal monto, string transaccionId)
            => Push("pago_aprobado", $"Pago de <strong>${monto:N0}</strong> aprobado. Transaccion #{transaccionId}", "/pagos");

        public void PagoPendiente(string restaurante)
            => Push("pago_pendiente", $"Recuerda pagar al llegar a <strong>{restaurante}</strong>", "/pagos");

        public void ResenaRecordatorio(string restaurante)
            => Push("resena", $"Como fue tu experiencia en <strong>{restaurante}</strong>? Dejanos tu resena", "/resenas");

        public void MesaActualizada(int numeroMesa, string accion)
            => Push("mesa", $"Mesa {numeroMesa} fue <strong>{accion}</strong> exitosamente", "/restaurante/mesas");

        public void MenuActualizado(string platoNombre, string accion)
            => Push("menu", $"Plato <strong>{platoNombre}</strong> fue {accion} al menu", "/restaurante/menu");

        public void MarkAsRead(int id)
        {
            var n = _notifications.FirstOrDefault(x => x.Id == id);
            if (n != null) { n.Leida = true; OnChange?.Invoke(); }
        }

        public void MarkAllRead()
        {
            foreach (var n in _notifications) n.Leida = true;
            OnChange?.Invoke();
        }

        public void Clear()
        {
            _notifications.Clear();
            OnChange?.Invoke();
        }
    }

    public class AppNotification
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = "";
        public string Mensaje { get; set; } = "";
        public string? Link { get; set; }
        public bool Leida { get; set; }
        public DateTime Fecha { get; set; }
    }
}