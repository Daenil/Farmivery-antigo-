using Microsoft.AspNetCore.Mvc;

public class CadastrarController : Controller
{
    public ActionResult Escolha()
    {
        return View();
    }

    public ActionResult Cliente()
    {
        return View();
    }

    public ActionResult Farmaceuticos()
    {
        return View("Cadastrar", "Farmaceuticos");
    }
}