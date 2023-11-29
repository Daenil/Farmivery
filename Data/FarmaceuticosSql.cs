using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class FarmaceuticosSql : Database, IFarmaceuticosData
{
    public void Create(Farmaceuticos farmaceuticos)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Farmaceuticos VALUES (@nome, @telefone)";

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
        cmd.CommandText = "SELECT * FROM Farmaceuticos";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Farmaceuticos> lista = new();

        while(reader.Read())
        {
            Farmaceuticos farmaceuticos = new Farmaceuticos();
            farmaceuticos.FarmaceuticosId = reader.GetInt32(0);
            farmaceuticos.NomeFarmaceutico = reader.GetString(1);
            farmaceuticos.Telefone = reader.GetString(2);            

            lista.Add(farmaceuticos);
        }
        return lista;
    }

    public List<Farmaceuticos> Read(string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Farmaceuticos WHERE nomeFarmaceutico LIKE @nome";

        cmd.Parameters.AddWithValue("@nome", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

        List<Farmaceuticos> lista = new List<Farmaceuticos>();

        while(reader.Read())
        {
            Farmaceuticos farmaceuticos = new Farmaceuticos();
            farmaceuticos.FarmaceuticosId = reader.GetInt32(0);
            farmaceuticos.NomeFarmaceutico = reader.GetString(1);
            farmaceuticos.Telefone = reader.GetString(2);

            lista.Add(farmaceuticos);
        }
        return lista;
    }

    public Farmaceuticos Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Farmaceuticos WHERE farmaceuticoId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Farmaceuticos farmaceuticos = new Farmaceuticos();
            farmaceuticos.FarmaceuticosId = reader.GetInt32(0);
            farmaceuticos.NomeFarmaceutico = reader.GetString(1);
            farmaceuticos.Telefone = reader.GetString(2);

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