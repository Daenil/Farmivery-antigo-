// WebApplication
var builder = WebApplication.CreateBuilder(args);

// Adição de Middlewares
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IProdutosData, ProdutosSql>();
builder.Services.AddTransient<IFarmaciasData, FarmaciasSql>();


var app = builder.Build();

// Configuração de Middlewares
app.MapControllerRoute("default", "/{controller=Produtos}/{action=Index}/{id?}");

app.UseStaticFiles();
app.Run();