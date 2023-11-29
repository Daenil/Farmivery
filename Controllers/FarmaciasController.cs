using Microsoft.AspNetCore.Mvc;

public class FarmaciasController : Controller
{
    private IFarmaciasData data;

    public FarmaciasController(IFarmaciasData data, IWebHostEnvironment hostingEnvironment)
    {
        this.data = data;
    } 

    public ActionResult Index()
    {
        List<Farmacias> lista = data.Read();
        return View(lista);
    }

    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"];

        List<Farmacias> lista = data.Read(search);
        return View("index", lista);
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
        return RedirectToAction("Login");
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

        List<Farmacias> farmacias  = data.Login(Email!, Senha!);

        if(farmacias == null)
        {
            ViewBag.Erro = "Usuário ou senha incorretos";
            return View();
        }
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