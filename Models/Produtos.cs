public class Produtos
{
    public int ProdutoId { get; set; }
    public int idFarmacia { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int ProdQtd { get; set;}
    public IFormFile Image { get; set; }
    public string? FilePath { get; set; }
    public string? FileName { get; set; }
    public string NomeFarmacia { get; set; }
    public int? QtdComprada { get; set; }
    public int? TipoPagamento { get; set; }
    public string TipoPagamentoDescricao
    {
        get
        {
            switch (TipoPagamento)
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
}