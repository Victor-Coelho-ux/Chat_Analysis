using ChatAnalysis.Application.Interfaces;
using ChatAnalysis.Domain.Enum;
using ChatAnalysis.Domain.Result;
using ChatAnlysis.Infrastructure.Interface;
using VaderSharp2;

namespace ChatAnalysis.Application.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly SentimentIntensityAnalyzer _analyzer;
        private readonly HttpClient _httpClient;
        private readonly IProductAnalysisRepository _repository;

        public SentimentAnalysisService(IProductAnalysisRepository repository)
        {
            _analyzer = new SentimentIntensityAnalyzer();
            _httpClient = new HttpClient();
            _repository = repository;
        }

        public async Task<SentimentResult> Analyze(string message, int productId)
        {
            var results = _analyzer.PolarityScores(message);

            SentimentTypeEnum sentiment = SentimentTypeEnum.Neutral;
            if (results.Compound >= 0.05)   
                sentiment = SentimentTypeEnum.Positive;
            else if (results.Compound <= -0.05) 
                sentiment = SentimentTypeEnum.Negative;

            await _repository.IncrementSentimentAsync(productId, sentiment);

            return new SentimentResult { 
                Sentiment = sentiment
            };
        }
    }
}
