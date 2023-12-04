using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
public class HomeSql : Database, IHomeData
{
    public List<Produtos> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT P.*, F.Nome AS NomeFarmacia FROM Produtos P INNER JOIN Farmacias F ON P.IdFarmacia = F.FarmaciaId";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Produtos> listap = new List<Produtos>();

        while (reader.Read())
        {
            Produtos produto = new Produtos();
            produto.ProdutoId = reader.GetInt32(0);
            produto.idFarmacia = reader.GetInt32(1);
            produto.Nome = reader.GetString(2);
            produto.Descricao = reader.GetString(3);
            produto.Preco = reader.GetDecimal(4);
            produto.ProdQtd = reader.GetInt32(5);
            produto.FileName = reader.GetString(6);

            produto.NomeFarmacia = reader.GetString(7);


            listap.Add(produto);
        }
        return listap;
    }

}