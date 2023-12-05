using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class ClientesSql : Database, IClientesData
{
    public void Create(Clientes clientes)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Clientes VALUES (@nome, @email, @senha, @telefone, @cep, @numerocasa, @cidade, @estado)";

        cmd.Parameters.AddWithValue("@nome", clientes.NomeCliente);
        cmd.Parameters.AddWithValue("@email", clientes.Email);
        cmd.Parameters.AddWithValue("@senha", clientes.Senha);
        cmd.Parameters.AddWithValue("@telefone", clientes.Telefone);
        cmd.Parameters.AddWithValue("@cep", clientes.Cep);
        cmd.Parameters.AddWithValue("@numerocasa", clientes.NumeroCasa);
        cmd.Parameters.AddWithValue("@cidade", clientes.Cidade);
        cmd.Parameters.AddWithValue("@estado", clientes.Estado);

        cmd.ExecuteNonQuery();
    }
    
    public Clientes Login(string Email, string Senha)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = connection;
            cmd.CommandText = "SELECT clienteId, email, senha FROM Clientes WHERE email = @email AND senha = @senha";

            cmd.Parameters.AddWithValue("@email", Email);
            cmd.Parameters.AddWithValue("@senha", Senha);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Clientes cliente = new Clientes();
                    cliente.ClienteId = reader.GetInt32(0);
                    cliente.Email = reader.GetString(1);
                    cliente.Senha = reader.GetString(2);

                    return cliente;
                }
            }

            return null;
        }
    }

    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Clientes WHERE clienteId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Clientes> Read()
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Clientes";

            List<Clientes> lista = new List<Clientes>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
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

                    lista.Add(clientes);
                }
            }

            return lista;
        }
    }

    public List<Clientes> Read(string search)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Clientes WHERE nomeCliente LIKE @nome";

            cmd.Parameters.AddWithValue("@nome", "%" + search + "%");

            List<Clientes> lista = new List<Clientes>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
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

                    lista.Add(clientes);
                }
            }

            return lista;
        }
    }

    public Clientes Read(int id)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Clientes WHERE clienteId = @id";

            cmd.Parameters.AddWithValue("@id", id);

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

            return null;
        }
    }

    public void Update(int id, Clientes clientes)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Clientes
                                SET nomeCliente = @nome,
                                email = @email,
                                senha = @senha,
                                telefone = @telefone,
                                cep = @cep,
                                numeroCasa = @numerocasa,
                                cidade = @cidade,
                                estado = @estado
                                WHERE clienteId = @id";

            cmd.Parameters.AddWithValue("@nome", clientes.NomeCliente);
            cmd.Parameters.AddWithValue("@email", clientes.Email);
            cmd.Parameters.AddWithValue("@senha", clientes.Senha);
            cmd.Parameters.AddWithValue("@telefone", clientes.Telefone);
            cmd.Parameters.AddWithValue("@cep", clientes.Cep);
            cmd.Parameters.AddWithValue("@numerocasa", clientes.NumeroCasa);
            cmd.Parameters.AddWithValue("@cidade", clientes.Cidade);
            cmd.Parameters.AddWithValue("@estado", clientes.Estado);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }

    // public List<Produtos> ReadProdutos(int clienteId)
    // {
    //     using (SqlCommand cmd = new SqlCommand())
    //     {
    //         cmd.Connection = connection;
    //         cmd.CommandText = @"SELECT
    //                             P.produtoId,
    //                             P.nome AS NomeProduto,
    //                             P.descricao AS DescricaoProduto,
    //                             P.preco,
    //                             SUM(PD.qtd) AS QuantidadeComprada,
    //                             P.imagem AS ImagemProduto,
    //                             F.nome AS NomeFarmacia
    //                             FROM
    //                             Pedidos PD
    //                             INNER JOIN Produtos P ON PD.idProduto = P.produtoId
    //                             INNER JOIN Farmacias F ON P.idFarmacia = F.farmaciaId
    //                             WHERE
    //                             PD.idCliente = @clienteId
    //                             GROUP BY
    //                             P.produtoId,
    //                             P.nome,
    //                             P.descricao,
    //                             P.preco,
    //                             P.imagem,
    //                             F.nome;";

    //         cmd.Parameters.AddWithValue("@clienteId", clienteId);

    //         SqlDataReader reader = cmd.ExecuteReader();

    //         List<Produtos> listap = new List<Produtos>();

    //         while (reader.Read())
    //         {
    //             Produtos produto = new Produtos();
    //             produto.ProdutoId = reader.GetInt32(0);
    //             produto.Nome = reader.GetString(1);
    //             produto.Descricao = reader.GetString(2);
    //             produto.QtdComprada = reader.GetInt32(3);
    //             produto.FileName = reader.GetString(4);

    //             produto.NomeFarmacia = reader.GetString(5);


    //             listap.Add(produto);
    //         }
    //         return listap;
    //     }
    // }

    public List<Produtos> ReadProdutos(int clienteId)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT
                                P.produtoId,
                                P.nome AS NomeProduto,
                                P.descricao AS DescricaoProduto,
                                P.preco,
                                SUM(PD.qtd) AS QuantidadeComprada,
                                P.imagem AS ImagemProduto,
                                F.nome AS NomeFarmacia
                                FROM
                                Pedidos PD
                                INNER JOIN Produtos P ON PD.idProduto = P.produtoId
                                INNER JOIN Farmacias F ON P.idFarmacia = F.farmaciaId
                                WHERE
                                PD.idCliente = @clienteId
                                GROUP BY
                                P.produtoId,
                                P.nome,
                                P.descricao,
                                P.preco,
                                P.imagem,
                                F.nome;";

            cmd.Parameters.AddWithValue("@clienteId", clienteId);

            List<Produtos> listap = new List<Produtos>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Produtos produto = new Produtos();
                    produto.ProdutoId = reader.GetInt32(0);
                    produto.Nome = reader.GetString(1);
                    produto.Descricao = reader.GetString(2);
                    produto.Preco = reader.GetDecimal(3);
                    produto.QtdComprada = reader.GetInt32(4);
                    produto.FileName = reader.GetString(5);

                    produto.NomeFarmacia = reader.GetString(6);

                    listap.Add(produto);
                }
            }

            return listap;
        }
    }

}