using Microsoft.AspNetCore.Mvc;

public class EntregadoresController : Controller
{
    private IEntregadoresData data;

    public EntregadoresController(IEntregadoresData data, IWebHostEnvironment hostingEnvironment)
    {
        this.data = data;
    } 

    public ActionResult Index()
    {
        List<Entregadores> lista = data.Read();
        return View(lista);
    }

    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"];

        List<Entregadores> lista = data.Read(search);
        return View("index", lista);
    }

    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Entregadores = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Entregadores entregadores)
    {
        data.Create(entregadores);
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
        Entregadores entregadores = data.Read(id);

        if (entregadores == null)
            return RedirectToAction("Index");

        return View(entregadores);
    }

    [HttpPost]
    public ActionResult Update(int id, Entregadores entregadores)
    {
        data.Update(id, entregadores);
        return RedirectToAction("Index");
    }
}