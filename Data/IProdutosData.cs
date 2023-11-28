public interface IProdutosData
{
    public List<Produtos> Read();
    public List<Produtos> Read(string search);
    public Produtos Read(int id);
    public void Create(Produtos produto);
    public void Update(int id, Produtos produto);
    public void Delete(int id);
}