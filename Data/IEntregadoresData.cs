public interface IEntregadoresData
{
    public List<Entregadores> Read();
    public List<Entregadores> ReadByFarmaciaId(int idFarmacia);
    public List<Entregadores> Read(string search, int farmaciaId);
    public Entregadores Read(int id);
    public void Create(Entregadores entregadores);
    public void Update(int id, Entregadores entregadores);
    public void Delete(int id);
}