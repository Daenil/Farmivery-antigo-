public interface IFarmaciasData
{
    public List<Produtos> Read();
    public List<Produtos> Read(string search);
    public Produtos Read(int id);
    public void Create(Farmacias farmacias);
    public void Update(int id, Farmacias farmacias);
    public void Delete(int id);
}