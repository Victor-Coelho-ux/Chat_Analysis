using ChatAnalysis.Domain.Enum;
using ChatAnalysis.Domain.Result;

namespace ChatAnalysis.Application.Interfaces
{
    public interface ISentimentAnalysisService
    {
        SentimentResult Analyze(string message);
    }
}
