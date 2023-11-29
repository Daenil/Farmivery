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

    public ActionResult Delete(int id)
    {
        data.Delete(id);
        return RedirectToAction("Index");
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

        List<Clientes> cliente  = data.Login(Email!, Senha!);

        if(cliente == null)
        {
            ViewBag.Erro = "Usuário ou senha incorretos";
            return View();
        }
        return RedirectToAction("IndexC", "Produtos");
    }
    [HttpGet]
    public ActionResult Update(int id)
    {
        Clientes clientes = data.Read(id);

        if (clientes == null)
            return RedirectToAction("Index");

        return View(clientes);
    }

    [HttpPost]
    public ActionResult Update(int id, Clientes clientes)
    {
        data.Update(id, clientes);
        return RedirectToAction("Index");
    }
}