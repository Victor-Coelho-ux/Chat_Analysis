using ChatAnalysis.Application.Interfaces;
using ChatAnalysis.Domain.Enum;
using ChatAnalysis.Domain.Result;
using VaderSharp2;

namespace ChatAnalysis.Application.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly SentimentIntensityAnalyzer _analyzer;
        private readonly HttpClient _httpClient;

        public SentimentAnalysisService()
        {
            _analyzer = new SentimentIntensityAnalyzer();
            _httpClient = new HttpClient();
        }

        public SentimentResult Analyze(string message)
        {
            var results = _analyzer.PolarityScores(message);

            SentimentTypeEnum sentiment = SentimentTypeEnum.Neutral;
            if (results.Compound >= 0.05) sentiment = SentimentTypeEnum.Positive;
            else if (results.Compound <= -0.05) sentiment = SentimentTypeEnum.Negative;

            return new SentimentResult { 
                Sentiment = sentiment
            };
        }
    }
}
