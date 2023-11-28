// WebApplication
var builder = WebApplication.CreateBuilder(args);

// Adição de Middlewares
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IProdutosData, ProdutosSql>();
builder.Services.AddTransient<IFarmaciasData, FarmaciasSql>();
builder.Services.AddTransient<IFarmaceuticosData, FarmaceuticosSql>();
builder.Services.AddTransient<IClientesData, ClientesSql>();


var app = builder.Build();

// Configuração de Middlewares
app.MapControllerRoute("default", "/{controller=Farmacias}/{action=Index}/{id?}");

app.UseStaticFiles();
app.Run();