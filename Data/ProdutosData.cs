using System.Net;

public class ProdutosData : IProdutosData
{
    private List<Produtos> lista = new List<Produtos>();
    
    public List<Produtos> Read()
    {
        return lista;
    }

    public List<Produtos> Read(string search)
    {
        var result =    from l in lista
                        where l.Nome.ToLower().Contains(search.ToLower())
                        select l;

        return result.ToList();
    }

    public void Create(Produtos produto)
    {
        lista.Add(produto);
    }

    public void Delete(int id)
    {
        foreach(var produto in lista)
        {
            if(produto.ProdutoId == id)
            {
                lista.Remove(produto);
                break;
            }
        }
    }

    public Produtos Read(int id)
    {
        return lista.FirstOrDefault(produto => produto.ProdutoId == id)
    }

    public void Update(int id, Produtos produto)
    {
        Produtos statusToUpdate = lista.First(produto => produto.ProdutoId == id);
        statusToUpdate.Nome = produto.Nome;
    }
}