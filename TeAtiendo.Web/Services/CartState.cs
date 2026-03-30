namespace TeAtiendo.Web.Services;


public class CartState
{
    private readonly List<CartItem> _items = new();

    public event Action? OnChange;

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public int? RestauranteId { get; private set; }
    public string? RestauranteNombre { get; private set; }

    public int TotalItems => _items.Sum(i => i.Cantidad);
    public decimal Subtotal => _items.Sum(i => i.PrecioUnitario * i.Cantidad);
    public decimal Tax => Math.Round(Subtotal * 0.18m);
    public decimal Tip => Math.Round(Subtotal * 0.10m);
    public decimal Total => Subtotal + Tax + Tip;

    public void SetRestaurante(int id, string nombre)
    {
        if (RestauranteId != id)
        {
            // Si cambia de restaurante, limpiar carrito
            _items.Clear();
        }
        RestauranteId = id;
        RestauranteNombre = nombre;
        NotifyStateChanged();
    }

    public void AddItem(int platoId, string nombre, decimal precio, string? imageUrl = null)
    {
        var existing = _items.FirstOrDefault(i => i.PlatoId == platoId);
        if (existing != null)
        {
            existing.Cantidad++;
        }
        else
        {
            _items.Add(new CartItem
            {
                PlatoId = platoId,
                Nombre = nombre,
                PrecioUnitario = precio,
                Cantidad = 1,
                ImageUrl = imageUrl
            });
        }
        NotifyStateChanged();
    }

    public void UpdateQuantity(int platoId, int newQuantity)
    {
        var item = _items.FirstOrDefault(i => i.PlatoId == platoId);
        if (item != null)
        {
            if (newQuantity <= 0)
                _items.Remove(item);
            else
                item.Cantidad = newQuantity;
            NotifyStateChanged();
        }
    }

    public void RemoveItem(int platoId)
    {
        var item = _items.FirstOrDefault(i => i.PlatoId == platoId);
        if (item != null)
        {
            _items.Remove(item);
            NotifyStateChanged();
        }
    }

    public void Clear()
    {
        _items.Clear();
        RestauranteId = null;
        RestauranteNombre = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

public class CartItem
{
    public int PlatoId { get; set; }
    public string Nombre { get; set; } = "";
    public decimal PrecioUnitario { get; set; }
    public int Cantidad { get; set; } = 1;
    public string? ImageUrl { get; set; }
}
