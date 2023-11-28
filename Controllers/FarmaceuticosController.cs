using Microsoft.AspNetCore.Mvc;

public class FarmaceuticosController : Controller
{
    private IFarmaceuticosData data;

    public FarmaceuticosController(IFarmaceuticosData data, IWebHostEnvironment hostingEnvironment)
    {
        this.data = data;
    } 

    public ActionResult Index()
    {
        List<Farmaceuticos> lista = data.Read();
        return View(lista);
    }

    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"];

        List<Farmaceuticos> lista = data.Read(search);
        return View("index", lista);
    }

    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Farmaceuticos = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Farmaceuticos farmaceuticos)
    {
        data.Create(farmaceuticos);
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
        Farmaceuticos farmaceuticos = data.Read(id);

        if (farmaceuticos == null)
            return RedirectToAction("Index");

        return View(farmaceuticos);
    }

    [HttpPost]
    public ActionResult Update(int id, Farmaceuticos farmaceuticos)
    {
        data.Update(id, farmaceuticos);
        return RedirectToAction("Index");
    }
}