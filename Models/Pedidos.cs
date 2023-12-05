public class Pedidos
{
    public int PedidoId { get; set; }
    public Clientes clientes { get; set; }
    public int IdCliente { get; set; }
    public Produtos produtos { get; set; }
    public int IdProduto { get; set; }
    public int Qtd { get; set; }
    public int TipoPagamento { get; set; }
}