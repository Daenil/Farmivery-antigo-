using System.Data;
using Microsoft.Data.SqlClient;

class ClientesSql : Database, IClientesData
{
    public void Cadastrar(Clientes clientes)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "exec sp_cadCliente @nomeCli, @emailCli, @senhaCli,@celularLi, @dataNascCli";

        cmd.Parameters.AddWithValue("@nomeCli", clientes.Pessoas.Nome);
        cmd.Parameters.AddWithValue("@emailCli", clientes.Pessoas.Email);
        cmd.Parameters.AddWithValue("@senhaCli", clientes.Pessoas.Senha);
        cmd.Parameters.AddWithValue("@celularCli", clientes.Pessoas.Telefone);
        cmd.Parameters.AddWithValue("@dataNascCli",clientes.Pessoas.DataNasc);


        cmd.ExecuteNonQuery();
    }
}

