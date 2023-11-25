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
    public List<Clientes> Login(string Email, string Senha)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "select v_Clientes.pessoasId,v_Clientes.email, v_Clientes.senha from v_Clientes";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Clientes> listaC = new();

        while(reader.Read())
        {
            Clientes cliente  = new Clientes();
            cliente.Pessoas.PessoasId = reader.GetInt32(0);
            cliente.Pessoas.Email = reader.GetString(1);
            cliente.Pessoas.Senha = reader.GetString(2);

            listaC.Add(cliente);

            if(email == cliente.Pessoas.Email && senha == cliente.Pessoas.Senha)
            {
                return listaC;
            }

            else
            {
                return null;
            }

        }
        return null;

        




    }
}

