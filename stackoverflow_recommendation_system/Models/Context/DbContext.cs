using Microsoft.Data.SqlClient;
using System.Data;

namespace stackoverflow_recommendation_system.Models.Context
{
    public class DbContext
    {
        private readonly string? _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}
