// WebApplication
var builder = WebApplication.CreateBuilder(args);

// Adição de Middlewares
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IProdutosData, ProdutosSql>();
builder.Services.AddTransient<IFarmaciasData, FarmaciasSql>();
builder.Services.AddTransient<IFarmaceuticosData, FarmaceuticosSql>();
builder.Services.AddTransient<IClientesData, ClientesSql>();
builder.Services.AddTransient<IEntregadoresData, EntregadoresSql>();


var app = builder.Build();

// Configuração de Middlewares
app.MapControllerRoute("default", "/{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();
app.Run();