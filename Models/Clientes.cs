public class Clientes
{
    public int ClienteId { get; set; }
    public string NomeCliente { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public string Cep { get; set; }
    public int NumeroCasa { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public List<Produtos> Produtos { get; set; }
}