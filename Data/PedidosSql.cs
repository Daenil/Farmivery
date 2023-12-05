using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using System.Data;


public class PedidosSql : Database, IPedidosData
{
    public void CloseConnection()
    {
        if (connection != null && connection.State == ConnectionState.Open)
        {
            connection.Close();
        }
    }

    public void Pedido(Pedidos pedidos, int tipoPagamento)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Pedidos VALUES (@idcli, @idprod, @qtd, @tipopagamento)";

        cmd.Parameters.AddWithValue("@idcli", pedidos.IdCliente);
        cmd.Parameters.AddWithValue("@idprod", pedidos.IdProduto);
        cmd.Parameters.AddWithValue("@qtd", pedidos.Qtd);
        cmd.Parameters.AddWithValue("@tipopagamento", pedidos.TipoPagamento);

        cmd.ExecuteNonQuery();
    }

    public List<Pedidos> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Pedidos";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Pedidos> lista = new();

        while(reader.Read())
        {
            Pedidos pedidos = new Pedidos();
            pedidos.PedidoId = reader.GetInt32(0);
            pedidos.IdCliente = reader.GetInt32(1);
            pedidos.IdProduto = reader.GetInt32(2);
            pedidos.Qtd = reader.GetInt32(3);
            pedidos.TipoPagamento = reader.GetInt32(4);

            lista.Add(pedidos);
        }
        return lista;
    }

    public Produtos ReadProduto(int idProduto)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Produtos WHERE ProdutoId = @id";

            cmd.Parameters.AddWithValue("@id", idProduto);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Produtos produtos = new Produtos();
                    produtos.ProdutoId = reader.GetInt32(0);
                    produtos.idFarmacia = reader.GetInt32(1);
                    produtos.Nome = reader.GetString(2);
                    produtos.Descricao = reader.GetString(3);
                    produtos.Preco = reader.GetDecimal(4);
                    produtos.ProdQtd = reader.GetInt32(5);
                    produtos.FileName = reader.GetString(6);

                    return produtos;
                }
            }
        }

        return null;
    }

    public Clientes ReadCliente(int IdCliente)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Clientes WHERE clienteId = @id";

            cmd.Parameters.AddWithValue("@id", IdCliente);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Clientes clientes = new Clientes();
                    clientes.ClienteId = reader.GetInt32(0);
                    clientes.NomeCliente = reader.GetString(1);
                    clientes.Email = reader.GetString(2);
                    clientes.Senha = reader.GetString(3);
                    clientes.Telefone = reader.GetString(4);
                    clientes.Cep = reader.GetString(5);
                    clientes.NumeroCasa = reader.GetInt32(6);
                    clientes.Cidade = reader.GetString(7);
                    clientes.Estado = reader.GetString(8);

                    return clientes;
                }
            }
        }

        return null;
    }
}