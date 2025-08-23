using ChatAnalysis.Domain.DTO;

namespace ChatAnalysis.Domain.Interface

{
    public interface IQueueMessagesService
    {
        Task PublishMessageForAnalysisAsync(MessageDto message);
    }
}

