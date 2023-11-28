using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class FarmaceuticosSql : Database, IFarmaceuticosData
{
    public void Cadastrar(Farmaceuticos farmaceuticos)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "exec sp_cadFarmaceuticos @nomeFarmaceutico, @emailFarmaceutico, @senhaFarmaceutico, @dataNascFarmaceutico, @nomeFarmacia, @cnpjFarmacia, @celularFarmaceutico";

        cmd.Parameters.AddWithValue("@nomeFarmaceutico", farmaceuticos.Pessoas.Nome);
        cmd.Parameters.AddWithValue("@emailFarmaceutico", farmaceuticos.Pessoas.Email);
        cmd.Parameters.AddWithValue("@senhaFarmaceutico", farmaceuticos.Pessoas.Senha);
        cmd.Parameters.AddWithValue("@dataNascFarmaceutico", farmaceuticos.Pessoas.DataNasc);
        cmd.Parameters.AddWithValue("@nomeFarmacia", farmaceuticos.Farmacias.Nome);
        cmd.Parameters.AddWithValue("@cnpjFarmacia", farmaceuticos.Farmacias.Cnpj);
        cmd.Parameters.AddWithValue("@celularFarmaceutico", farmaceuticos.Pessoas.Telefone);

        cmd.ExecuteNonQuery();
    }

    public List<Produtos> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Produtos";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Produtos> listap = new();

        while(reader.Read())
        {
            Produtos produto = new Produtos();
            produto.ProdutoId = reader.GetInt32(0);
            produto.Nome = reader.GetString(1);
            produto.Descricao = reader.GetString(2);
            produto.Preco = reader.GetDecimal(3);
            produto.ProdQtd = reader.GetInt32(4);
            produto.FileName = reader.GetString(5);

            listap.Add(produto);
        }
        return listap;
    }
}