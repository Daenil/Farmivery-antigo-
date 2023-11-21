using Microsoft.Data.SqlClient;
public class PessoasSql : Database, IPessoasData
{
    public void Create (Pessoas pessoa)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "Insert into Pessoas VALUES (@nome, @email, @senha, @telefone, @dataNasc)";

        cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
        cmd.Parameters.AddWithValue("@email", pessoa.Email);
        cmd.Parameters.AddWithValue("@senha", pessoa.Senha);
        cmd.Parameters.AddWithValue("@telefone", pessoa.Telefone);
        cmd.Parameters.AddWithValue("@dataNasc", pessoa.DataNasc);

    }

}