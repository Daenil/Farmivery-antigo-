using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

class ClientesSql : Database, IClientesData
{
    public void Cadastrar(Clientes cliente)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "exec sp_cadCliente @nomeCli, @emailCli, @senhaCli,@celularCli, @dataNascCli";

        cmd.Parameters.AddWithValue("@nomeCli", cliente.Pessoas.Nome);
        cmd.Parameters.AddWithValue("@emailCli", cliente.Pessoas.Email);
        cmd.Parameters.AddWithValue("@senhaCli", cliente.Pessoas.Senha);
        cmd.Parameters.AddWithValue("@celularCli", cliente.Pessoas.Telefone);
        cmd.Parameters.AddWithValue("@dataNascCli",cliente.Pessoas.DataNasc);


        cmd.ExecuteNonQuery();
    }
public Clientes Login(string Email, string Senha)
{
    SqlCommand cmd = new SqlCommand();
    cmd.Connection = connection;
    cmd.CommandText = "SELECT pessoasId, email, senha FROM v_Clientes WHERE email = @Email AND senha = @Senha";

    cmd.Parameters.AddWithValue("@Email", Email);
    cmd.Parameters.AddWithValue("@Senha", Senha);

    SqlDataReader reader = cmd.ExecuteReader();

    if (reader.Read())
    {
        Clientes cliente = new Clientes();
        cliente.Pessoas.PessoasId = reader.GetInt32(0);
        cliente.Pessoas.Email = reader.GetString(1);
        cliente.Pessoas.Senha = reader.GetString(2);

        reader.Close();

        return cliente;
    }

    reader.Close();
    return null;
}

}

