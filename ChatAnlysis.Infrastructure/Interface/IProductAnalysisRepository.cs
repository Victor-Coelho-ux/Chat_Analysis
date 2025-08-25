using ChatAnalysis.Domain.Enum;

namespace ChatAnlysis.Infrastructure.Interface
{
    public interface IProductAnalysisRepository
    {
        Task IncrementSentimentAsync(int productId, SentimentTypeEnum sentimentTypeId);
    }
}
