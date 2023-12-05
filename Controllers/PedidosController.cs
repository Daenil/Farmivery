using Microsoft.AspNetCore.Mvc;
using System.Dynamic;


public class PedidosController : Controller
{
    private IPedidosData data;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public PedidosController(IPedidosData data, IWebHostEnvironment hostingEnvironment)
    {
        this.data = data;
        _hostingEnvironment = hostingEnvironment;
    }

    private int GetClienteIdFromSession()
    {
        return HttpContext.Session.GetInt32("UserId") ?? 0;
    }

    public ActionResult CompraSucedida(int idProduto, int IdCliente, Pedidos pedidos)
    {
        Produtos produtos = data.ReadProduto(idProduto);
        Clientes clientes = data.ReadCliente(IdCliente);

        if(clientes != null)
        {
            dynamic viewModel = new ExpandoObject();
            viewModel.NomeCliente = clientes.NomeCliente;
            viewModel.NomeProduto = produtos.Nome;
            viewModel.PrecoProduto = produtos.Preco;
            viewModel.Imagem = produtos.FileName;
            viewModel.Endereco = $"{clientes.Cep}, {clientes.NumeroCasa}, {clientes.Cidade}, {clientes.Estado}";

            return View("CompraSucedida", viewModel);            
        }
        else
        {
            return RedirectToAction("IndexC", "Produtos");
        }
    }
}