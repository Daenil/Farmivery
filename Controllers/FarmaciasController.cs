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

        Farmacias farmacias = data.Login(Email!, Senha!);

        if (farmacias == null)
        {
            ViewBag.Erro = "Usuário ou senha incorretos";
            return View();
        }

        int farmaciaId = farmacias.FarmaciaId;

        HttpContext.Session.SetInt32("UserId", farmaciaId);
        HttpContext.Session.SetString("TipoUser", "Farmacia");        

        return RedirectToAction("IndexF", "Produtos");
    }

    [HttpPost]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    public ActionResult Delete()
    {
        int id = HttpContext.Session.GetInt32("UserId") ?? 0;

        data.Delete(id);

        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
        Farmacias farmacias = data.Read(id);

        if (farmacias == null)
            return RedirectToAction("Perfil");

        return View(farmacias);
    }

    [HttpPost]
    public ActionResult Update(int id, Farmacias farmacias)
    {
        data.Update(id, farmacias);
        return RedirectToAction("Perfil");
    }

    public ActionResult Perfil()
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Login", "Farmacias");
        }

        int farmaciaId = HttpContext.Session.GetInt32("UserId") ?? 0;

        Farmacias farmacia = data.Read(farmaciaId);

        if (farmacia == null)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Farmacias");
        }

        return View(farmacia);
    }
}