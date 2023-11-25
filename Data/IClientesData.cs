public interface IClientesData
{   
    public void Cadastrar(Clientes cliente);

    public List<Clientes> Login(string email, string senha);

}