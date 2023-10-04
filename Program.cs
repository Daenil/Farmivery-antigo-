// WebApplication
var builder = WebApplication.CreateBuilder(args);

// Adição de Middlewares
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ProdutosData>();

var app = builder.Build();

// Configuração de Middlewares
app.MapControllerRoute("default", "/{controller=Produtos}/{action=Index}/{id?}");

app.Run();