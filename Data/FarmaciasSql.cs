using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class FarmaciasSql : Database, IFarmaciasData
{
    public void Create(Farmacias farmacias)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Farmacias VALUES (@email, @senha, @nome, @cnpj, @cep, @numero, @cidade, @estado, @telefone)";

        cmd.Parameters.AddWithValue("@email", farmacias.Email);
        cmd.Parameters.AddWithValue("@senha", farmacias.Senha);
        cmd.Parameters.AddWithValue("@nome", farmacias.Nome);
        cmd.Parameters.AddWithValue("@cnpj", farmacias.Cnpj);
        cmd.Parameters.AddWithValue("@cep", farmacias.Cep);
        cmd.Parameters.AddWithValue("@numero", farmacias.NumeroRua);
        cmd.Parameters.AddWithValue("@cidade", farmacias.Cidade);
        cmd.Parameters.AddWithValue("@estado", farmacias.Estado);
        cmd.Parameters.AddWithValue("@telefone", farmacias.Telefone);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Farmacias WHERE FarmaciaId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Farmacias> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Farmacias";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Farmacias> lista = new();

        while(reader.Read())
        {
            Farmacias farmacias = new Farmacias();
            farmacias.FarmaciaId = reader.GetInt32(0);
            farmacias.Email = reader.GetString(1);
            farmacias.Senha = reader.GetString(2);
            farmacias.Nome = reader.GetString(3);
            farmacias.Cnpj = reader.GetString(4);
            farmacias.Cep = reader.GetString(5);
            farmacias.NumeroRua = reader.GetInt32(6);
            farmacias.Cidade = reader.GetString(7);
            farmacias.Estado = reader.GetString(8);
            farmacias.Telefone = reader.GetString(9);

            lista.Add(farmacias);
        }
        return lista;
    }

    public List<Farmacias> Read(string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Farmacias WHERE Nome LIKE @nome";

        cmd.Parameters.AddWithValue("@nome", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

        List<Farmacias> lista = new List<Farmacias>();

        while(reader.Read())
        {
            Farmacias farmacias = new Farmacias();
            farmacias.FarmaciaId = reader.GetInt32(0);
            farmacias.Email = reader.GetString(1);
            farmacias.Senha = reader.GetString(2);
            farmacias.Nome = reader.GetString(3);
            farmacias.Cnpj = reader.GetString(4);
            farmacias.Cep = reader.GetString(5);
            farmacias.NumeroRua = reader.GetInt32(6);
            farmacias.Cidade = reader.GetString(7);
            farmacias.Estado = reader.GetString(8);
            farmacias.Telefone = reader.GetString(9);

            lista.Add(farmacias);
        }
        return lista;
    }

    public Farmacias Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Farmacias WHERE FarmaciaId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Farmacias farmacias = new Farmacias();
            farmacias.FarmaciaId = reader.GetInt32(0);
            farmacias.Email = reader.GetString(1);
            farmacias.Senha = reader.GetString(2);
            farmacias.Nome = reader.GetString(3);
            farmacias.Cnpj = reader.GetString(4);
            farmacias.Cep = reader.GetString(5);
            farmacias.NumeroRua = reader.GetInt32(6);
            farmacias.Cidade = reader.GetString(7);
            farmacias.Estado = reader.GetString(8);
            farmacias.Telefone = reader.GetString(9);

            return farmacias;
        }

        return null;
    }

    public Farmacias Login(string Email, string Senha)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "select farmaciaId, email, senha from Farmacias where email = @email and senha = @senha";

        cmd.Parameters.AddWithValue("@email", Email);
        cmd.Parameters.AddWithValue("@senha", Senha);

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Farmacias farmacias = new Farmacias();
            farmacias.FarmaciaId = reader.GetInt32(0);
            farmacias.Email = reader.GetString(1);
            farmacias.Senha = reader.GetString(2);

            return farmacias;
        }

        return null;
    }

    public void Update(int id, Farmacias farmacias)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"UPDATE Farmacias
                            SET email = @email,
                            senha = @senha,
                            Nome = @nome,
                            Cnpj = @cnpj,
                            cep = @cep,
                            numeroRua = @numero,
                            cidade = @cidade,
                            estado = @estado
                            WHERE FarmaciaId = @id";

        cmd.Parameters.AddWithValue("@email", farmacias.Email);
        cmd.Parameters.AddWithValue("@senha", farmacias.Senha);
        cmd.Parameters.AddWithValue("@nome", farmacias.Nome);
        cmd.Parameters.AddWithValue("@cnpj", farmacias.Cnpj);
        cmd.Parameters.AddWithValue("@cep", farmacias.Cep);
        cmd.Parameters.AddWithValue("@numero", farmacias.NumeroRua);
        cmd.Parameters.AddWithValue("@cidade", farmacias.Cidade);
        cmd.Parameters.AddWithValue("@estado", farmacias.Estado);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
    
}