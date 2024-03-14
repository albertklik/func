using System.Data;
using System.Data.SqlClient;
using Funcionarios.Database;
using Funcionarios.Interface;
using Funcionarios.Model;

namespace Funcionarios.Service;

public class CargoService: ICargoService
{
   private readonly DatabaseConnection _db;

    public CargoService(DatabaseConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Cargo>> GetAll()
    {
        var list = new List<Cargo>();
        var db = _db.Get();
        db.Open();

        SqlCommand cmd = new SqlCommand(
            "SELECT TOP (1000) " +
            " [codigo]" +
            ",[nome]" +
            ",[descricaoCargo]" +
            " FROM [func].[dbo].[cargo] " +
            " WHERE [ativo] = 'TRUE'"
            , db);
        cmd.CommandType = CommandType.Text;
        var result = await cmd.ExecuteReaderAsync();

        while (result.Read())
        {
            var c = new Cargo
            {
                Codigo = (int)result["codigo"],
                Nome = result["nome"].ToString() ?? "",
                DescricaoCargo = result["descricaoCargo"].ToString() ?? ""
            };
            list.Add(c);
        }
        db.Close();
        return list;
    }

    public async Task<Cargo> Get(int codigo)
    {
        var list = new List<Funcionario>();
        var db = _db.Get();
        db.Open();

        SqlCommand cmd = new SqlCommand(
            "SELECT TOP 1 " +
            " [codigo]" +
            ",[nome]" +
            ",[descricaoCargo]" +
            " FROM [func].[dbo].[cargo] " +
            " WHERE [ativo] = 'TRUE' AND [codigo] = @codigo"
            , db);
        cmd.Parameters.AddWithValue("@codigo", codigo);
        cmd.CommandType = CommandType.Text;
        var result = await cmd.ExecuteReaderAsync();

        var c = new Cargo();
        if (result.HasRows)
        {
            result.Read();
            c.Codigo = (int)result["codigo"];
            c.Nome = result["nome"].ToString() ?? "";
            c.DescricaoCargo = result["descricaoCargo"].ToString() ?? "";
        }
        db.Close();
        return c;
    }

    public async Task Save(Cargo cargo)
    {
        using (SqlConnection db = _db.Get())
        {
            var comandoSql = "INSERT INTO [func].[dbo].[cargo] " +
                                "([nome],[descricaoCargo],[ativo]) " +
                                "Values(@Nome, @DescricaoCargo, @Ativo) ";
            SqlCommand cmd = new SqlCommand(comandoSql, db);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Nome", cargo.Nome);
            cmd.Parameters.AddWithValue("@DescricaoCargo", cargo.DescricaoCargo);
            cmd.Parameters.AddWithValue("@Ativo", 1);
            db.Open();
            await cmd.ExecuteNonQueryAsync();
            db.Close();
        }
    }

    public async Task Update(Cargo cargo)
    {
        using (SqlConnection con = _db.Get())
        {
            string comandoSql = "UPDATE [dbo].[cargo] " +
                                "SET [nome] = @Nome " +
                                ",[descricaoCargo] = @DescricaoCargo " +
                                "WHERE [codigo] = @Codigo";
            SqlCommand cmd = new SqlCommand(comandoSql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Codigo", cargo.Codigo);
            cmd.Parameters.AddWithValue("@Nome", cargo.Nome);
            cmd.Parameters.AddWithValue("@DescricaoCargo", cargo.DescricaoCargo);
            con.Open();
            await cmd.ExecuteNonQueryAsync();
            con.Close();
        }
    }

    public async Task Delete(int codigo)
    {
        using (SqlConnection con = _db.Get())
        {
            string comandoSql = "UPDATE [dbo].[cargo] SET [ativo] = 0 WHERE [codigo] = @Codigo";
            SqlCommand cmd = new SqlCommand(comandoSql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Codigo", codigo);
            con.Open();
            await cmd.ExecuteNonQueryAsync();
            con.Close();
        }
    }
}