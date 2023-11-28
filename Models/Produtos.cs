public class Produtos
{
    public int ProdutoId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int ProdQtd { get; set;}
    public IFormFile Image { get; set; }
    public string? FilePath { get; set; }
    public string? FileName { get; set; }
}