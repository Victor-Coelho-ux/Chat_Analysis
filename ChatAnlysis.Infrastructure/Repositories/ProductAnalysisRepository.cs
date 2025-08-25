using ChatAnalysis.Domain.Enum;
using ChatAnlysis.Infrastructure.Interface;
using MySql.Data.MySqlClient;

namespace ChatAnlysis.Infrastructure.Repositories
{
    public class ProductAnalysisRepository : IProductAnalysisRepository
    {
        private readonly string _connectionString;

        public ProductAnalysisRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task IncrementSentimentAsync(int productId, SentimentTypeEnum sentimentTypeId)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();

                var sql = @"
                INSERT INTO ProductAnalysis (ProductId, SentimentTypeId, Count)
                VALUES (@ProductId, @SentimentTypeId, 1)
                ON DUPLICATE KEY UPDATE Count = Count + 1;";

                using var cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.Parameters.AddWithValue("@SentimentTypeId", sentimentTypeId);

                await cmd.ExecuteNonQueryAsync();

            } catch (Exception ex)
            {
                Console.WriteLine($"Error incrementing sentiment: {ex.Message}");
                throw;
            }
        }
    }
}
