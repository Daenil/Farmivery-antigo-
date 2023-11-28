//WebApplication
var builder = WebApplication.CreateBuilder(args);

//Adição de Middlewares
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IProdutosData, ProdutosSql>();
builder.Services.AddTransient<IFarmaciasData, FarmaciasSql>();
builder.Services.AddTransient<IClientesData, ClientesSql>();
builder.Services.AddTransient<IPessoasData, PessoasSql>();
builder.Services.AddTransient<IFarmaceuticosData, FarmaceuticosSql>();

builder.Services.AddSession();

// Adicione a configuração do HttpContextAccessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSession();

//Configurações de Middlewares
app.MapControllerRoute("default", "/{controller=Clientes}/{action=Login}/{id?}");

app.UseStaticFiles();
app.Run();
