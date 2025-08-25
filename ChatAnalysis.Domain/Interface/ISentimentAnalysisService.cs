using ChatAnalysis.Domain.Enum;
using ChatAnalysis.Domain.Result;

namespace ChatAnalysis.Application.Interfaces
{
    public interface ISentimentAnalysisService
    {
        Task<SentimentResult> Analyze(string message, int productId);
    }
}
