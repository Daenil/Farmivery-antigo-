using Microsoft.AspNetCore.Mvc;

public class ProdutosController : Controller
{
    private IProdutosData data;

    public ProdutosController(IProdutosData data)
    {
        this.data = data;
    }

    public ActionResult Index()
    {
        List<Produtos> lista = data.Read();
        return View(lista);
    }

    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"];

        List<Produtos> lista = data.Read(search);
        return View("index", lista);
    }

    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Produtos = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Produtos produtos)
    {
        data.Create(produtos);
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        data.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
        Produtos produtos = data.Read(id);

        if(produtos == null)
            return RedirectToAction("Index");

        ViewBag.Produtos = data.Read();

        return View(produtos); 
    }

    [HttpPost]
    public ActionResult Update(int id, Produtos produtos)
    {
        data.Update(id, produtos);
        return RedirectToAction("Index");
    }
}