using Microsoft.AspNetCore.Mvc;

public class FarmaceuticosController : Controller
{
    private IFarmaceuticosData data;
    private IFarmaciasData farmaciasData;
    private IPessoasData pessoasData;

    public FarmaceuticosController(IFarmaceuticosData data, IFarmaciasData farmaciasData, IPessoasData pessoasData)
    {
        this.data = data;
        this.farmaciasData = farmaciasData;
        this.pessoasData = pessoasData;
    }

    public ActionResult Index()
    {
        List<Produtos> listap = data.Read();
        return View(listap);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(Farmaceuticos farmaceuticos, Pessoas pessoas, Farmacias farmacias)
    {
        data.Cadastrar(farmaceuticos);
        pessoasData.Create(pessoas);
        farmaciasData.Create(farmacias);
        return RedirectToAction("Login", "Clientes");
    }
}