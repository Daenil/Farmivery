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

    public ActionResult IndexF()
    {
        int farmaciaId = GetFarmaciaIdFromSession();
        
        List<Produtos> listap = data.ReadByFarmaciaId(farmaciaId);

        ViewBag.PesquisaDesc = false;

        return View("IndexF", listap);
    }

    public ActionResult IndexC()
    {
        List<Produtos> listap = data.Read();
        return View("IndexC", listap);
    }

    public ActionResult Search(IFormCollection form)
    {
        if (HttpContext.Session.GetString("TipoUser") == "Farmacia")
        {
            int farmaciaId = GetFarmaciaIdFromSession();

            string search = form["search"];

            List<Produtos> listap = data.Read(search, farmaciaId);

            ViewBag.PesquisaDesc = false;

            return View("indexF", listap);
        }
        else
        {
            string search = form["search"];

            List<Produtos> listap = data.Read(search);

            return View("IndexC", listap);
        }
    }

    public ActionResult SearchDesc(IFormCollection form)
    {
        string search = form["searchdesc"];

        List<Produtos> listap = data.ReadbyDesc(search);

        ViewBag.PesquisaDesc = true;

        return View("indexF", listap);
    }

    private int GetFarmaciaIdFromSession()
    {
        return HttpContext.Session.GetInt32("UserId") ?? 0;
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
        int idFarmacia = GetFarmaciaIdFromSession();
        
        if (idFarmacia == 0)
        {
            return RedirectToAction("Login", "Farmacias");
        }        
        
        model.idFarmacia = idFarmacia;

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

        return RedirectToAction("IndexF", "Produtos");
    }

    public ActionResult Delete(int id)
    {
        data.Delete(id);
        return RedirectToAction("IndexF");
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
        Produtos produtos = data.Read(id);

        if (produtos == null)
            return RedirectToAction("IndexF");

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

        return RedirectToAction("IndexF");
    }

    [HttpGet]
    public ActionResult Comprar(int id)
    {
        Produtos produto = data.Read(id);

        if (produto == null)
            return RedirectToAction("IndexC");

        return View(produto);
    }

    private int GetClienteIdFromSession()
    {
        return HttpContext.Session.GetInt32("UserId") ?? 0;
    }

    [HttpPost]
    public ActionResult Comprar(int id, int quantidade, int IdCliente, int tipoPagamento)
    {
        IdCliente = GetClienteIdFromSession();

        if (IdCliente == 0)
        {
            return RedirectToAction("Login", "Clientes");
        }
   
        Pedidos pedido = new Pedidos
        {
            IdCliente = IdCliente,
            IdProduto = id,
            Qtd = quantidade,
            TipoPagamento = tipoPagamento
        };

        data.Comprar(id, quantidade);

        PedidosSql pedidosSql = new PedidosSql();
        pedidosSql.Pedido(pedido, tipoPagamento);

        return RedirectToAction("CompraSucedida", "Pedidos", new {idProduto = id, IdCliente = IdCliente});
    }
}