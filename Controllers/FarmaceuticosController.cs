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
        int idFarmacia = GetFarmaciaIdFromSession();

        List<Farmaceuticos> lista = data.ReadByFarmaciaId(idFarmacia);
        return View(lista);
    }

    public ActionResult Search(IFormCollection form)
    {
        int farmaciaId = GetFarmaciaIdFromSession();

        string search = form["search"];

        List<Farmaceuticos> lista = data.Read(search, farmaciaId);

        return View("index", lista);
    }

    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Farmaceuticos = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Farmaceuticos model)
    {
        int idFarmacia = GetFarmaciaIdFromSession();
        
        if (idFarmacia == 0)
        {
            return RedirectToAction("Login", "Farmacias");
        }  
        
        model.idFarmacia = idFarmacia;

        data.Create(model);

        return RedirectToAction("Index", "Farmaceuticos");
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