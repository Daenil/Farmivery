using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class EntregadoresSql : Database, IEntregadoresData
{
    public void Create(Entregadores entregadores)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Entregadores VALUES (@nome, @telefone)";

        cmd.Parameters.AddWithValue("@nome", entregadores.NomeEntregador);
        cmd.Parameters.AddWithValue("@telefone", entregadores.Telefone);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Entregadores WHERE entregadorId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Entregadores> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Entregadores";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Entregadores> lista = new();

        while(reader.Read())
        {
            Entregadores entregadores = new Entregadores();
            entregadores.EntregadorId = reader.GetInt32(0);
            entregadores.NomeEntregador = reader.GetString(1);
            entregadores.Telefone = reader.GetString(2);            

            lista.Add(entregadores);
        }
        return lista;
    }

    public List<Entregadores> Read(string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Entregadores WHERE nomeEntregador LIKE @nome";

        cmd.Parameters.AddWithValue("@nome", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

        List<Entregadores> lista = new List<Entregadores>();

        while(reader.Read())
        {
            Entregadores entregadores = new Entregadores();
            entregadores.EntregadorId = reader.GetInt32(0);
            entregadores.NomeEntregador = reader.GetString(1);
            entregadores.Telefone = reader.GetString(2);  

            lista.Add(entregadores);
        }
        return lista;
    }

    public Entregadores Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Entregadores WHERE entregadorId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Entregadores entregadores = new Entregadores();
            entregadores.EntregadorId = reader.GetInt32(0);
            entregadores.NomeEntregador = reader.GetString(1);
            entregadores.Telefone = reader.GetString(2);  

            return entregadores;
        }

        return null;
    }

    public void Update(int id, Entregadores entregadores)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"UPDATE Entregadores
                            SET nomeEntregador = @nome,
                            telefone = @telefone
                            WHERE entregadorId = @id";

        cmd.Parameters.AddWithValue("@nome", entregadores.NomeEntregador);
        cmd.Parameters.AddWithValue("@telefone", entregadores.Telefone);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
}