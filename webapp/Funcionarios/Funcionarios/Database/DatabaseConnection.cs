using System.Data;
using System.Data.SqlClient;

namespace Funcionarios.Database;

public class DatabaseConnection
{
    public DatabaseConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private readonly IConfiguration _configuration;
    
    public SqlConnection Get()
    {
        string connString = _configuration.GetConnectionString("DefaultConnection") ?? "";
        return new SqlConnection(connString);
    }
}