using TeAtiendo.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// AgregA services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// HttpClient pa consumir la API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5067")
});

var app = builder.Build();

// Configura la HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();