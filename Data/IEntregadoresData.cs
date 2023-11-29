public interface IEntregadoresData
{
    public List<Entregadores> Read();
    public List<Entregadores> Read(string search);
    public Entregadores Read(int id);
    public void Create(Entregadores entregadores);
    public void Update(int id, Entregadores entregadores);
    public void Delete(int id);
}