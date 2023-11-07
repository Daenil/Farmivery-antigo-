using System.ComponentModel.Design;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class FarmaciasSql : Database, IFarmaciasData
{
    public void Create(Farmacias farmacias)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Farmacias VALUES (@nome, @cnpj)";

        cmd.Parameters.AddWithValue("@nome", farmacias.Nome);
        cmd.Parameters.AddWithValue("@cnpj", farmacias.Cnpj)

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Farmacias WHERE FarmaciaId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Produtos> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Farmacias";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Farmacias> lista = new();

        while(reader.Read())
        {
            Farmacias farmacias = new Farmacias();
            farmacias.FarmaciaId = reader.GetInt32(0);
            farmacias.Nome = reader.GetString(1);
            farmacias.Cnpj = reader.GetString(2);

            lista.Add(farmacias);
        }
        return lista;
    }

    public List<Produtos> Read(string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Farmacias WHERE Nome LIKE @nome";

        cmd.Parameters.AddWithValue("@nome", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

        List<Farmacias> lista = new List<Farmacias>();

        while(reader.Read())
        {
            Farmacias farmacias = new Farmacias();
            farmacias.FarmaciaId = reader.GetInt32(0);
            farmacias.Nome = reader.GetString(1);
            farmacias.Cnpj = reader.GetString(2);

            lista.Add(farmacias);
        }
        return lista;
    }

    public Produtos Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Produtos WHERE FarmaciaId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Farmacias farmacias = new Farmacias();
            farmacias.FarmaciaId = reader.GetInt32(0);
            farmacias.Nome = reader.GetString(1);
            farmacias.Cnpj = reader.GetString(2);

            lista.Add(farmacias);
        }

        return null;
    }

    public void Update(int id, Farmacias farmacias)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"UPDATE Farmacias
                            SET Nome = @nome,
                            Cnpj = @cnpj
                            WHERE farmaciaId = @id";

        cmd.Parameters.AddWithValue("@nome", farmacias.Nome);
        cmd.Parameters.AddWithValue("@cnpj", farmacias.Cnpj);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
}