public interface IClientesData
{
    public Pessoas? Login(string email, string senha);

    public void Cadastrar(Clientes cliente);
}