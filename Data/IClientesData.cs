public interface IClientesData
{
    public List<Clientes> Read();
    public List<Clientes> Read(string search);
    public Clientes Read(int id);
    public void Create(Clientes clientes);
    public void Update(int id, Clientes clientes);
    public void Delete(int id);
}