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
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Clientes";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Clientes> lista = new();

        while(reader.Read())
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
        return lista;
    }

    public List<Clientes> Read(string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Clientes WHERE nomeCliente LIKE @nome";

        cmd.Parameters.AddWithValue("@nome", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

        List<Clientes> lista = new List<Clientes>();

        while(reader.Read())
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
        return lista;
    }

    public Clientes Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Clientes WHERE clienteId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

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

        return null;
    }

    public void Update(int id, Clientes clientes)
    {
        SqlCommand cmd = new SqlCommand();
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