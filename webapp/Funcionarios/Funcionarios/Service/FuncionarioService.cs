using System.Data;
using System.Data.SqlClient;
using Funcionarios.Database;
using Funcionarios.Interface;
using Funcionarios.Model;

namespace Funcionarios.Service;

public class FuncionarioService: IFuncionarioService
{
    private readonly DatabaseConnection _db;

    public FuncionarioService(DatabaseConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Funcionario>> GetAll()
    {
        var list = new List<Funcionario>();
        var db = _db.Get();
        db.Open();

        SqlCommand cmd = new SqlCommand(
            "SELECT TOP (1000) " +
            " [FUN].[codigo]" +
            ",[FUN].[nome]" +
            ",[FUN].[cdcargo] " +
            ",[CAR].[nome] as nomeCargo" +
            ",[FUN].[valorSalario]" +
            ",[FUN].[ativo]" +
            " FROM [func].[dbo].[funcionario] AS FUN" +
            " JOIN [func].[dbo].[cargo] AS [CAR] ON [CAR].[codigo] = [cdcargo] " +
            " WHERE [FUN].[ativo] = 'TRUE'"
            , db);
        cmd.CommandType = CommandType.Text;
        var result = await cmd.ExecuteReaderAsync();

        while (result.Read())
        {
            Funcionario f = new Funcionario
            {
                Codigo = (int)result["codigo"],
                Nome = result["nome"].ToString() ?? "",
                Cargo = result["nomeCargo"].ToString() ?? "",
                CodigoCargo = (int)result["cdcargo"],
                ValorSalario = (decimal)result["valorSalario"]
            };
            list.Add(f);
        }
        db.Close();
        return list;
    }

    public async Task<Funcionario> Get(int codigo)
    {
        var list = new List<Funcionario>();
        var db = _db.Get();
        db.Open();

        SqlCommand cmd = new SqlCommand(
            "SELECT TOP (1000) " +
            " [FUN].[codigo]" +
            ",[FUN].[nome]" +
            ",[FUN].[cdcargo] " +
            ",[CAR].[nome] as nomeCargo" +
            ",[FUN].[valorSalario]" +
            ",[FUN].[ativo]" +
            " FROM [func].[dbo].[funcionario] AS FUN" +
            " JOIN [func].[dbo].[cargo] AS [CAR] ON [CAR].[codigo] = [cdcargo] " +
            " WHERE [FUN].[ativo] = 'TRUE' AND [FUN].[codigo] = @codigo"
            , db);
        cmd.Parameters.AddWithValue("@codigo", codigo);
        cmd.CommandType = CommandType.Text;
        var result = await cmd.ExecuteReaderAsync();

        result.Read();
        var func = new Funcionario
        {
            Codigo = (int)result["codigo"],
            Nome = result["nome"].ToString() ?? "",
            Cargo = result["nomeCargo"].ToString() ?? "",
            CodigoCargo = (int)result["cdcargo"],
            ValorSalario = (decimal)result["valorSalario"]
        };
        db.Close();
        return func;
    }

    public async Task Save(Funcionario funcionario)
    {
        using (SqlConnection db = _db.Get())
        {
            var comandoSql = "INSERT INTO [func].[dbo].[funcionario] " +
                                "([nome],[cdcargo],[valorSalario],[ativo]) " +
                                "Values(@Nome, @CdCargo, @ValorSalario, @Ativo)";
            SqlCommand cmd = new SqlCommand(comandoSql, db);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@CdCargo", funcionario.CodigoCargo);
            cmd.Parameters.AddWithValue("@ValorSalario", funcionario.ValorSalario);
            cmd.Parameters.AddWithValue("@Ativo", 1);
            db.Open();
            await cmd.ExecuteNonQueryAsync();
            db.Close();
        }
    }

    public async Task Update(Funcionario funcionario)
    {
        using (SqlConnection con = _db.Get())
        {
            string comandoSql = "UPDATE [dbo].[funcionario] " +
                                "SET [nome] = @Nome " +
                                ",[cdcargo] = @CdCargo " +
                                ",[valorSalario] = @ValorSalario " +
                                "WHERE [codigo] = @Codigo";
            SqlCommand cmd = new SqlCommand(comandoSql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Codigo", funcionario.Codigo);
            cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@CdCargo", funcionario.CodigoCargo);
            cmd.Parameters.AddWithValue("@ValorSalario", funcionario.ValorSalario);
            con.Open();
            await cmd.ExecuteNonQueryAsync();
            con.Close();
        }
    }

    public async Task Delete(int codigo)
    {
        using (SqlConnection con = _db.Get())
        {
            string comandoSql = "UPDATE [dbo].[funcionario] SET [ativo] = 0 WHERE [codigo] = @Codigo";
            SqlCommand cmd = new SqlCommand(comandoSql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Codigo", codigo);
            con.Open();
            await cmd.ExecuteNonQueryAsync();
            con.Close();
        }
    }
}