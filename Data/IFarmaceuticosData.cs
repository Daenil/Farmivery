public interface IFarmaceuticosData
{
    public List<Farmaceuticos> Read();
    public List<Farmaceuticos> Read(string search);
    public Farmaceuticos Read(int id);
    public void Create(Farmaceuticos farmaceuticos);
    public List<Farmaceuticos> Login(string Email, string Senha);

    public void Update(int id, Farmaceuticos farmaceuticos);
    public void Delete(int id);
}