using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

class ClientesSql : Database, IClientesData
{
    public void Cadastrar(Clientes cliente)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "exec sp_cadCliente @nomeCli, @emailCli, @senhaCli,@celularLi, @dataNascCli";

        cmd.Parameters.AddWithValue("@nomeCli", cliente.Pessoas.Nome);
        cmd.Parameters.AddWithValue("@emailCli", cliente.Pessoas.Email);
        cmd.Parameters.AddWithValue("@senhaCli", cliente.Pessoas.Senha);
        cmd.Parameters.AddWithValue("@celularCli", cliente.Pessoas.Telefone);
        cmd.Parameters.AddWithValue("@dataNascCli",cliente.Pessoas.DataNasc);


        cmd.ExecuteNonQuery();
    }
    public List<Clientes> Login()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "select v_Clientes.email, v_Clientes.senha from v_Clientes";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Clientes> listaC = new();

        




    }
}

