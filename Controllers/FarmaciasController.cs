using Microsoft.AspNetCore.Mvc;

public class FarmaciaController : Controller
{
    private IFarmaciasData data;

    public FarmaciaController(IFarmaciasData data, IWebHostEnvironment hostingEnvironment)
    {
        this.data = data;
    } 

        public ActionResult Index()
    {
        List<Farmacias> listaf = data.Read();
        return View(listaf);
    }

    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"];

        List<Farmacias> listaf = data.Read(search);
        return View("index", listaf);
    }

    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Farmacias = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Farmacias farmacias)
    {
        data.Create(farmacias);
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
        Farmacias farmacias = data.Read(id);

        if (farmacias == null)
            return RedirectToAction("Index");

        return View(farmacias);
    }

    [HttpPost]
    public ActionResult Update(int id, Farmacias farmacias)
    {
        data.Update(id, farmacias);
        return RedirectToAction("Index");
    }
}