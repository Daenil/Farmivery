public interface IFarmaceuticosData
{
    public List<Farmaceuticos> Read();
    public List<Farmaceuticos> ReadByFarmaciaId(int idFarmacia);
    public List<Farmaceuticos> Read(string search, int farmaciaId);
    public Farmaceuticos Read(int id);
    public void Create(Farmaceuticos farmaceuticos);
    public void Update(int id, Farmaceuticos farmaceuticos);
    public void Delete(int id);
}