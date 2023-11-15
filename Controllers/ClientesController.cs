using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class ClientesController : Controller
{
    private IClientesData data;

    public ClientesController(IClientesData data)
    {
        this.data = data;
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    public ActionResult Cadastrar(Clientes clientes )
    {
        data.Cadastrar(clientes);
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
        string? Email = form["email"];
        string? Senha = form["senha"];

        var pessoa = data.Login(Email!, Senha!);

        if(pessoa == null)
        {
            ViewBag.Erro = "Usuário ou senha incorretos";
            return View();
        }

        HttpContext.Session.SetString("pessoa", JsonSerializer.Serialize(pessoa));
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Pessoas");
    }

    
}
