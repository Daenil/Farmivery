var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddTransient<IProdutosData, ProdutosSql>();
builder.Services.AddTransient<IFarmaciasData, FarmaciasSql>();
builder.Services.AddTransient<IFarmaceuticosData, FarmaceuticosSql>();
builder.Services.AddTransient<IClientesData, ClientesSql>();
builder.Services.AddTransient<IEntregadoresData, EntregadoresSql>();
builder.Services.AddTransient<IPedidosData, PedidosSql>();
builder.Services.AddTransient<IHomeData, HomeSql>();

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseSession();

app.MapControllerRoute("default", "/{controller=Home}/{action=Index}/{id?}");

app.Run();
