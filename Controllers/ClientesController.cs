using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class ClientesController : Controller
{
    private IClientesData data;

    public ClientesController(IClientesData data, IWebHostEnvironment hostingEnvironment)
    {
        this.data = data;
    } 

    public ActionResult Index()
    {
        List<Clientes> lista = data.Read();
        return View(lista);
    }

    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"];

        List<Clientes> lista = data.Read(search);
        return View("index", lista);
    }

    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Clientes = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Clientes clientes)
    {
        data.Create(clientes);
        return RedirectToAction("Login");
    }

    public ActionResult Delete()
    {
        int id = HttpContext.Session.GetInt32("UserId") ?? 0;

        data.Delete(id);

        return RedirectToAction("Login");
        
        HttpContext.Session.Clear();
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

        Clientes cliente = data.Login(Email!, Senha!);

        if (cliente == null)
        {
            ViewBag.Erro = "Usuário ou senha incorretos";
            return View();
        }

        int clienteId = cliente.ClienteId;

        HttpContext.Session.SetInt32("UserId", clienteId);
        HttpContext.Session.SetString("TipoUser", "Cliente");

        return RedirectToAction("IndexC", "Produtos");
    }

    [HttpPost]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
        Clientes clientes = data.Read(id);

        if (clientes == null)
            return RedirectToAction("Perfil");

        return View(clientes);
    }

    [HttpPost]
    public ActionResult Update(int id, Clientes clientes)
    {
        data.Update(id, clientes);

        return RedirectToAction("Perfil");
    }

    public ActionResult Perfil()
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Login", "Clientes");
        }

        int clienteId = HttpContext.Session.GetInt32("UserId") ?? 0;

        Clientes cliente = data.Read(clienteId);

        List<Produtos> produtos = data.ReadProdutos(clienteId);

        cliente.Produtos = produtos;

        if (cliente == null)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Clientes");
        }

        return View(cliente);
    }

    public string GetTipoPagamento(int tipoPagamento)
    {
        switch (tipoPagamento)
        {
            case 1:
                return "Crédito";
            case 2:
                return "Pix";
            case 3:
                return "Pagar na entrega";
            default:
                return "Desconhecido";
        }
    }
}