// WebApplication
var builder = WebApplication.CreateBuilder(args);

// Adição de Middlewares
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IProdutosData, ProdutosSql>();
builder.Services.AddTransient<IFarmaciasData, FarmaciasSql>();
builder.Services.AddTransient<IClientesData, ClientesSql>();
builder.Services.AddSession();


var app = builder.Build();

app.UseSession();

// Configuração de Middlewares
app.MapControllerRoute("default", "/{controller=Farmacias}/{action=Index}/{id?}");

app.UseStaticFiles();
app.Run();