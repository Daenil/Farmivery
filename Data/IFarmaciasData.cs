public interface IFarmaciasData
{
    public List<Farmacias> Read();
    public List<Farmacias> Read(string search);
    public Farmacias Read(int id);
    public void Create(Farmacias farmacias);
    public void Update(int id, Farmacias farmacias);
    public void Delete(int id);
}