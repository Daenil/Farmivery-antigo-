using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class ClientesController : Controller
{
    private IClientesData data;
    private IPessoasData pessoasData;

    public ClientesController(IClientesData data, IPessoasData pessoasData)
    {
        this.data = data;
        this.pessoasData = pessoasData;
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    public ActionResult Cadastrar(Clientes cliente, Pessoas pessoa )
    {
        data.Cadastrar(cliente);
        pessoasData.Create(pessoa);
        return RedirectToAction("Login", "Pessoas");


    }


    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(IFormCollection form)
    {
        string? Email = form["Email"];
        string? Senha = form["Senha"];

        List<Clientes> cliente  = data.Login(Email!, Senha!);

        if(cliente == null)
        {
            ViewBag.Erro = "Usuário ou senha incorretos";
            return View();
        }

        HttpContext.Session.SetString("pessoa", JsonSerializer.Serialize(cliente));
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Pessoas");
    }

    
}
