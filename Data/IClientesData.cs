public interface IClientesData
{   
    public void Cadastrar(Clientes cliente);

    public Clientes Login(string Email, string Senha);

}