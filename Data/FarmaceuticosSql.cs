using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class FarmaceuticosSql : Database, IFarmaceuticosData
{
    public void Create(Farmaceuticos farmaceuticos)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Farmaceuticos VALUES (@farmaciaid, @nome, @telefone)";

        cmd.Parameters.AddWithValue("@farmaciaId", farmaceuticos.idFarmacia);
        cmd.Parameters.AddWithValue("@nome", farmaceuticos.NomeFarmaceutico);
        cmd.Parameters.AddWithValue("@telefone", farmaceuticos.Telefone);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Farmaceuticos WHERE farmaceuticoId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Farmaceuticos> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT Fa.*, F.nome AS NomeFarmac FROM Farmaceuticos Fa INNER JOIN Farmacias F ON Fa.IdFarmacia = F.FarmaciaId";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Farmaceuticos> lista = new();

        while(reader.Read())
        {
            Farmaceuticos farmaceuticos = new Farmaceuticos();
            farmaceuticos.FarmaceuticosId = reader.GetInt32(0);
            farmaceuticos.idFarmacia = reader.GetInt32(1);
            farmaceuticos.NomeFarmaceutico = reader.GetString(2);
            farmaceuticos.Telefone = reader.GetString(3); 

            farmaceuticos.NomeFarmacia = reader.GetString(4);           

            lista.Add(farmaceuticos);
        }
        return lista;
    }

    public List<Farmaceuticos> ReadByFarmaciaId(int idFarmacia)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT Fa.*, F.nome AS NomeFarmac FROM Farmaceuticos Fa INNER JOIN Farmacias F ON Fa.IdFarmacia = F.FarmaciaId WHERE Fa.IdFarmacia = @farmaciaId";

        cmd.Parameters.AddWithValue("@farmaciaId", idFarmacia);

        SqlDataReader reader = cmd.ExecuteReader();

        List<Farmaceuticos> lista = new();

        while(reader.Read())
        {
            Farmaceuticos farmaceuticos = new Farmaceuticos();
            farmaceuticos.FarmaceuticosId = reader.GetInt32(0);
            farmaceuticos.idFarmacia = reader.GetInt32(1);
            farmaceuticos.NomeFarmaceutico = reader.GetString(2);
            farmaceuticos.Telefone = reader.GetString(3); 

            farmaceuticos.NomeFarmacia = reader.GetString(4);           

            lista.Add(farmaceuticos);
        }
        return lista;
    }

    public List<Farmaceuticos> Read(string search, int farmaciaId)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT Fa.*, F.Nome AS NomeFarmacia FROM Farmaceuticos Fa INNER JOIN Farmacias F ON Fa.IdFarmacia LIKE F.FarmaciaId where Fa.nomeFarmaceutico LIKE @nome and idFarmacia LIKE @idFarmacia";

        cmd.Parameters.AddWithValue("@nome", "%" + search + "%");
        cmd.Parameters.AddWithValue("@idFarmacia", farmaciaId);

        SqlDataReader reader = cmd.ExecuteReader();

        List<Farmaceuticos> lista = new List<Farmaceuticos>();

        while(reader.Read())
        {
            Farmaceuticos farmaceuticos = new Farmaceuticos();
            farmaceuticos.FarmaceuticosId = reader.GetInt32(0);
            farmaceuticos.idFarmacia = reader.GetInt32(1);
            farmaceuticos.NomeFarmaceutico = reader.GetString(2);
            farmaceuticos.Telefone = reader.GetString(3);   

            farmaceuticos.NomeFarmacia = reader.GetString(4);           


            lista.Add(farmaceuticos);
        }
        return lista;
    }

    public Farmaceuticos Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT Fa.*, F.Nome AS NomeFarmacia FROM Farmaceuticos Fa INNER JOIN Farmacias F ON Fa.IdFarmacia LIKE F.FarmaciaId where Fa.FarmaceuticoId LIKE @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Farmaceuticos farmaceuticos = new Farmaceuticos();
            farmaceuticos.FarmaceuticosId = reader.GetInt32(0);
            farmaceuticos.idFarmacia = reader.GetInt32(1);
            farmaceuticos.NomeFarmaceutico = reader.GetString(2);
            farmaceuticos.Telefone = reader.GetString(3);   

            farmaceuticos.NomeFarmacia = reader.GetString(4);           

            return farmaceuticos;
        }

        return null;
    }

    public void Update(int id, Farmaceuticos farmaceuticos)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"UPDATE Farmaceuticos
                            SET nomeFarmaceutico = @nome,
                            telefone = @telefone
                            WHERE farmaceuticoId = @id";

        cmd.Parameters.AddWithValue("@nome", farmaceuticos.NomeFarmaceutico);
        cmd.Parameters.AddWithValue("@telefone", farmaceuticos.Telefone);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
}