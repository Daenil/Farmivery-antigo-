using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class ProdutosSql : Database, IProdutosData
{
    public void Create(Produtos produto)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Produtos VALUES (@nome, @descricao, @preco, @prod_qtd)";

        cmd.Parameters.AddWithValue("@nome", produto.Nome);
        cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
        cmd.Parameters.AddWithValue("@preco", produto.Preco);
        cmd.Parameters.AddWithValue("@prod_qtd", produto.ProdQtd);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Produtos WHERE ProdutoId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Produtos> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Produtos";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Produtos> lista = new();

        while(reader.Read())
        {
            Produtos produto = new Produtos();
            produto.ProdutoId = reader.GetInt32(0);
            produto.Nome = reader.GetString(1);
            produto.Descricao = reader.GetString(2);
            produto.Preco = reader.GetDecimal(3);
            produto.ProdQtd = reader.GetInt32(4);

            lista.Add(produto);
        }
        return lista;
    }

    public List<Produtos> Read(string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FORM Produtos WHERE Nome LIKE @nome";

        cmd.Parameters.AddWithValue("@nome", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

        List<Produtos> lista = new List<Produtos>();

        while(reader.Read())
        {
            Produtos produto = new Produtos();
            produto.ProdutoId = reader.GetInt32(0);
            produto.Nome = reader.GetString(1);
            produto.Descricao = reader.GetString(2);
            produto.Preco = reader.GetDecimal(3);
            produto.ProdQtd = reader.GetInt32(4);

            lista.Add(produto);
        }
        return lista;
    }

    public Produtos Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Produtos WHERE ProdutoId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Produtos produto = new Produtos();
            produto.ProdutoId = reader.GetInt32(0);
            produto.Nome = reader.GetString(1);
            produto.Descricao = reader.GetString(2);
            produto.Preco = reader.GetDecimal(3);
            produto.ProdQtd = reader.GetInt32(4);

            return produto;
        }

        return null;
    }

    public void Update(int id, Produtos produtos)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"UPDATE Produtos
                            SET Nome = @nome,
                            Descricao = @descricao,
                            Preco = @preco,
                            ProdQtd = @prod_qtd
                            WHERE ProdutoId = @id";

        cmd.Parameters.AddWithValue("@nome", produtos.Nome);
        cmd.Parameters.AddWithValue("@descricao", produtos.Descricao);
        cmd.Parameters.AddWithValue("@preco", produtos.Preco);
        cmd.Parameters.AddWithValue("@prod_qtd", produtos.ProdQtd);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
}