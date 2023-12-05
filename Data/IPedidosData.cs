public interface IPedidosData
{
    public void Pedido(Pedidos pedidos, int tipoPagamento);
    public List<Pedidos> Read();
    public Produtos ReadProduto(int idProduto);
    public Clientes ReadCliente(int IdCliente);
    public void CloseConnection();
}