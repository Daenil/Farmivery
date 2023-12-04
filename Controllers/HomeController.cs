using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private IHomeData data;

    public HomeController(IHomeData data, IWebHostEnvironment hostingEnvironment)
    {
        this.data = data;
    }

    public ActionResult Index()
    {
        List<Produtos> listap = data.Read();
        return View("Index", listap);
    }

    public ActionResult SobreNos()
    {
        return View();
    }
}