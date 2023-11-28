using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class FarmaceuticosSql : Database, IFarmaceuticosData
{
    public void Create(Farmaceuticos farmaceuticos)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Farmaceuticos VALUES (@nome, @email, @senha, @telefone, @cep, @numerocasa, @cidade, @estado)";

        cmd.Parameters.AddWithValue("@nome", farmaceuticos.NomeFarmaceutico);
        cmd.Parameters.AddWithValue("@email", farmaceuticos.Email);
        cmd.Parameters.AddWithValue("@senha", farmaceuticos.Senha);
        cmd.Parameters.AddWithValue("@telefone", farmaceuticos.Telefone);
        cmd.Parameters.AddWithValue("@cep", farmaceuticos.Cep);
        cmd.Parameters.AddWithValue("@numerocasa", farmaceuticos.NumeroCasa);
        cmd.Parameters.AddWithValue("@cidade", farmaceuticos.Cidade);
        cmd.Parameters.AddWithValue("@estado", farmaceuticos.Estado);

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
            farmaceuticos.Email = reader.GetString(2);
            farmaceuticos.Senha = reader.GetString(3);
            farmaceuticos.Telefone = reader.GetString(4);
            farmaceuticos.Cep = reader.GetString(5);
            farmaceuticos.NumeroCasa = reader.GetInt32(6);
            farmaceuticos.Cidade = reader.GetString(7);
            farmaceuticos.Estado = reader.GetString(8);
            

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
            farmaceuticos.Email = reader.GetString(2);
            farmaceuticos.Senha = reader.GetString(3);
            farmaceuticos.Telefone = reader.GetString(4);
            farmaceuticos.Cep = reader.GetString(5);
            farmaceuticos.NumeroCasa = reader.GetInt32(6);
            farmaceuticos.Cidade = reader.GetString(7);
            farmaceuticos.Estado = reader.GetString(8);

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
            farmaceuticos.Email = reader.GetString(2);
            farmaceuticos.Senha = reader.GetString(3);
            farmaceuticos.Telefone = reader.GetString(4);
            farmaceuticos.Cep = reader.GetString(5);
            farmaceuticos.NumeroCasa = reader.GetInt32(6);
            farmaceuticos.Cidade = reader.GetString(7);
            farmaceuticos.Estado = reader.GetString(8);

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
                            email = @email,
                            senha = @senha,
                            telefone = @telefone,
                            cep = @cep,
                            numeroCasa = @numerocasa,
                            cidade = @cidade,
                            estado = @estado
                            WHERE farmaceuticoId = @id";

        cmd.Parameters.AddWithValue("@nome", farmaceuticos.NomeFarmaceutico);
        cmd.Parameters.AddWithValue("@email", farmaceuticos.Email);
        cmd.Parameters.AddWithValue("@senha", farmaceuticos.Senha);
        cmd.Parameters.AddWithValue("@telefone", farmaceuticos.Telefone);
        cmd.Parameters.AddWithValue("@cep", farmaceuticos.Cep);
        cmd.Parameters.AddWithValue("@numerocasa", farmaceuticos.NumeroCasa);
        cmd.Parameters.AddWithValue("@cidade", farmaceuticos.Cidade);
        cmd.Parameters.AddWithValue("@estado", farmaceuticos.Estado);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
}