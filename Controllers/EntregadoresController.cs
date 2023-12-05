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
        int idFarmacia = GetFarmaciaIdFromSession();

        List<Entregadores> lista = data.ReadByFarmaciaId(idFarmacia);

        return View(lista);
    }

    public ActionResult Search(IFormCollection form)
    {
        int farmaciaId = GetFarmaciaIdFromSession();

        string search = form["search"];

        List<Entregadores> lista = data.Read(search, farmaciaId);
        
        return View("index", lista);
    }

    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Entregadores = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Entregadores model)
    {
        int idFarmacia = GetFarmaciaIdFromSession();

        if (idFarmacia == 0)
        {
            return RedirectToAction("Login", "Farmacias");
        }          

        model.idFarmacia = idFarmacia;

        data.Create(model);

        return RedirectToAction("Index", "Entregadores");
    }

    private int GetFarmaciaIdFromSession()
    {
        return HttpContext.Session.GetInt32("UserId") ?? 0;
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