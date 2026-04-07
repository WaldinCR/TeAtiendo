namespace TeAtiendo.Web.Services
{
    public class FavoritosService
    {
        public event Action? OnChange;
        private HashSet<Guid> _favoritos = new();

        public IReadOnlyCollection<Guid> Favoritos => _favoritos;
        public int Count => _favoritos.Count;

        public bool IsFavorito(Guid restauranteId) => _favoritos.Contains(restauranteId);

        public void Toggle(Guid restauranteId)
        {
            if (_favoritos.Contains(restauranteId))
                _favoritos.Remove(restauranteId);
            else
                _favoritos.Add(restauranteId);
            OnChange?.Invoke();
        }

        public void Add(Guid restauranteId)
        {
            _favoritos.Add(restauranteId);
            OnChange?.Invoke();
        }

        public void Remove(Guid restauranteId)
        {
            _favoritos.Remove(restauranteId);
            OnChange?.Invoke();
        }
    }
}