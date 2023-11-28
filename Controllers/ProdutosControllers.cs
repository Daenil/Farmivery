using Microsoft.AspNetCore.Mvc;

public class ProdutosController : Controller
{
    private IProdutosData data;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ProdutosController(IProdutosData data, IWebHostEnvironment hostingEnvironment)
    {
        this.data = data;
        _hostingEnvironment = hostingEnvironment;
    }

    public ActionResult Index()
    {
        List<Produtos> listap = data.Read();
        return View(listap);
    }

    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"];

        List<Produtos> listap = data.Read(search);
        return View("index", listap);
    }

    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Produtos = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Produtos model)
    {
        if (model.Image != null && model.Image.Length > 0)
        {
            model.FileName = Path.GetFileName(model.Image.FileName);
            model.FilePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", model.FileName);

            using (var stream = new FileStream(model.FilePath!, FileMode.Create))
            {
                model.Image.CopyTo(stream);
            }

        }
        data.Create(model);

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

        if (produtos == null)
            return RedirectToAction("Index");

        return View(produtos);
    }

    [HttpPost]
    public ActionResult Update(int id, Produtos model)
    {
        if (model.Image != null && model.Image.Length > 0)
        {
            model.FileName = Path.GetFileName(model.Image.FileName);
            model.FilePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", model.FileName);

            using (var stream = new FileStream(model.FilePath!, FileMode.Create))
            {
                model.Image.CopyTo(stream);
            }
        }

        data.Update(id, model);

        return RedirectToAction("Index");
    }

}