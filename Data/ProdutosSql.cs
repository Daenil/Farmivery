using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class ProdutosSql : Database, IProdutosData
{
    public void Create(Produtos produto)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Produtos VALUES (@nome, @descricao, @preco, @prod_qtd, @imagem)";

        cmd.Parameters.AddWithValue("@nome", produto.Nome);
        cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
        cmd.Parameters.AddWithValue("@preco", produto.Preco);
        cmd.Parameters.AddWithValue("@prod_qtd", produto.ProdQtd);
        cmd.Parameters.AddWithValue("@imagem", produto.FileName??"download.jpg");

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Produtos WHERE ProdutoId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Produtos> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Produtos";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Produtos> listap = new();

        while(reader.Read())
        {
            Produtos produto = new Produtos();
            produto.ProdutoId = reader.GetInt32(0);
            produto.Nome = reader.GetString(1);
            produto.Descricao = reader.GetString(2);
            produto.Preco = reader.GetDecimal(3);
            produto.ProdQtd = reader.GetInt32(4);
            produto.FileName = reader.GetString(5);

            listap.Add(produto);
        }
        return listap;
    }

    public List<Produtos> Read(string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Produtos WHERE Nome LIKE @nome";

        cmd.Parameters.AddWithValue("@nome", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

        List<Produtos> listap = new List<Produtos>();

        while(reader.Read())
        {
            Produtos produto = new Produtos();
            produto.ProdutoId = reader.GetInt32(0);
            produto.Nome = reader.GetString(1);
            produto.Descricao = reader.GetString(2);
            produto.Preco = reader.GetDecimal(3);
            produto.ProdQtd = reader.GetInt32(4);
            produto.FileName = reader.GetString(5);

            listap.Add(produto);
        }
        return listap;
    }

    public Produtos Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Produtos WHERE ProdutoId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Produtos produto = new Produtos();
            produto.ProdutoId = reader.GetInt32(0);
            produto.Nome = reader.GetString(1);
            produto.Descricao = reader.GetString(2);
            produto.Preco = reader.GetDecimal(3);
            produto.ProdQtd = reader.GetInt32(4);
            produto.FileName = reader.GetString(5);

            return produto;
        }

        return null;
    }

    public void Update(int id, Produtos produtos)
    {
        SqlCommand selectCmd = new SqlCommand();
        selectCmd.Connection = connection;
        selectCmd.CommandText = "SELECT Imagem FROM Produtos WHERE ProdutoId = @id";
        selectCmd.Parameters.AddWithValue("@id", id);

        string imagemAtual = null;

        using (SqlDataReader reader = selectCmd.ExecuteReader())
        {
            if (reader.Read())
            {
                imagemAtual = reader.GetString(0);
            }
        }

        SqlCommand updateCmd = new SqlCommand();
        updateCmd.Connection = connection;
        updateCmd.CommandText = @"UPDATE Produtos
                                SET Nome = @nome,
                                Descricao = @descricao,
                                Preco = @preco,
                                Prod_qtd = @prod_qtd,
                                Imagem = @imagem
                                WHERE ProdutoId = @id";

        updateCmd.Parameters.AddWithValue("@nome", produtos.Nome);
        updateCmd.Parameters.AddWithValue("@descricao", produtos.Descricao);
        updateCmd.Parameters.AddWithValue("@preco", produtos.Preco);
        updateCmd.Parameters.AddWithValue("@prod_qtd", produtos.ProdQtd);
        updateCmd.Parameters.AddWithValue("@imagem", produtos.FileName ?? imagemAtual);
        updateCmd.Parameters.AddWithValue("@id", id);

        updateCmd.ExecuteNonQuery();
    }


}