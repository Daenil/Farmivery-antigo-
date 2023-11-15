using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

class ClientesSql : Database, IClientesData
{
    public void Cadastrar(Clientes cliente)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "exec sp_cadCliente @nomeCli, @emailCli, @senhaCli, @dataNascCli, @celularCli";

        cmd.Parameters.AddWithValue("@nomeCli", Clientes.Pessoas.Nome);
        cmd.Parameters.AddWithValue("@emailCli", Clientes.Pessoas.Email);
        cmd.Parameters.AddWithValue("@senhaCli", Clientes.Pessoas.Senha);
        cmd.Parameters.AddWithValue("@dataNascCli",Clientes.Pessoas.DataNasc);
        cmd.Parameters.AddWithValue("@celularCli", Clientes.Cli_celular);

        cmd.ExecuteNonQuery();
    }

//     public Pessoas? Login(string email, string senha)
//     {
//         // var pessoa = pessoas.SingleOrDefault(p => p.Email == email && p.Senha == senha);
//         // return pessoa;
        
//     }
// }