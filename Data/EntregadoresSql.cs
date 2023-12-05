using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class EntregadoresSql : Database, IEntregadoresData
{
    public void Create(Entregadores entregadores)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Entregadores VALUES (@farmaciaid, @nome, @telefone)";

        cmd.Parameters.AddWithValue("@farmaciaid", entregadores.idFarmacia);
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
        cmd.CommandText = "SELECT E.*, F.Nome AS NomeFarmacia FROM Entregadores E INNER JOIN Farmacias F ON E.IdFarmacia = F.FarmaciaId";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Entregadores> lista = new();

        while(reader.Read())
        {
            Entregadores entregadores = new Entregadores();
            entregadores.EntregadorId = reader.GetInt32(0);
            entregadores.idFarmacia = reader.GetInt32(1);
            entregadores.NomeEntregador = reader.GetString(2);
            entregadores.Telefone = reader.GetString(3);        
            
            entregadores.NomeFarmacia = reader.GetString(4);

            lista.Add(entregadores);
        }
        return lista;
    }

    public List<Entregadores> ReadByFarmaciaId(int idFarmacia)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT E.*, F.Nome AS NomeFarmacia FROM Entregadores E INNER JOIN Farmacias F ON E.IdFarmacia = F.FarmaciaId WHERE E.IdFarmacia = @farmaciaId";

        cmd.Parameters.AddWithValue("@farmaciaId", idFarmacia);

        SqlDataReader reader = cmd.ExecuteReader();

        List<Entregadores> lista = new();

        while(reader.Read())
        {
            Entregadores entregadores = new Entregadores();
            entregadores.EntregadorId = reader.GetInt32(0);
            entregadores.idFarmacia = reader.GetInt32(1);
            entregadores.NomeEntregador = reader.GetString(2);
            entregadores.Telefone = reader.GetString(3);

            entregadores.NomeFarmacia = reader.GetString(4);

            lista.Add(entregadores);
        }
        return lista;
    }

    public List<Entregadores> Read(string search, int farmaciaId)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT E.*, F.Nome AS NomeFarmacia FROM Entregadores E INNER JOIN Farmacias F ON E.IdFarmacia LIKE F.FarmaciaId where E.nomeEntregador LIKE @nome and idFarmacia LIKE @idFarmacia";

        cmd.Parameters.AddWithValue("@nome", "%" + search + "%");
        cmd.Parameters.AddWithValue("@idFarmacia", farmaciaId);

        SqlDataReader reader = cmd.ExecuteReader();

        List<Entregadores> lista = new List<Entregadores>();

        while(reader.Read())
        {
            Entregadores entregadores = new Entregadores();
            entregadores.EntregadorId = reader.GetInt32(0);
            entregadores.idFarmacia = reader.GetInt32(1);
            entregadores.NomeEntregador = reader.GetString(2);
            entregadores.Telefone = reader.GetString(3);     

            entregadores.NomeFarmacia = reader.GetString(4);

            lista.Add(entregadores);
        }
        return lista;
    }

    public Entregadores Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT E.*, F.Nome AS NomeFarmacia FROM Entregadores E INNER JOIN Farmacias F ON E.IdFarmacia LIKE F.FarmaciaId where E.EntregadorId LIKE @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Entregadores entregadores = new Entregadores();
            entregadores.EntregadorId = reader.GetInt32(0);
            entregadores.idFarmacia = reader.GetInt32(1);
            entregadores.NomeEntregador = reader.GetString(2);
            entregadores.Telefone = reader.GetString(3);      

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