// WebApplication
var builder = WebApplication.CreateBuilder(args);

// Adição de Middlewares
builder.Services.AddControllersWithViews();

// Configuração do suporte à sessão
builder.Services.AddSession();

builder.Services.AddTransient<IProdutosData, ProdutosSql>();
builder.Services.AddTransient<IFarmaciasData, FarmaciasSql>();
builder.Services.AddTransient<IFarmaceuticosData, FarmaceuticosSql>();
builder.Services.AddTransient<IClientesData, ClientesSql>();
builder.Services.AddTransient<IEntregadoresData, EntregadoresSql>();

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

// Habilita o suporte à sessão
app.UseSession();

app.MapControllerRoute("default", "/{controller=Home}/{action=Index}/{id?}");

app.Run();
