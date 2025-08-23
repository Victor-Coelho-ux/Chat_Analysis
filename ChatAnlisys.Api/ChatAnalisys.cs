using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ChatAnalysis.Domain.DTO;
using ChatAnalysis.Domain.Interface;

namespace ChatAnlisys.Api
{
    [ApiController]
    [Route("message")]
    public class MessagesController : ControllerBase
    {
        private readonly IQueueMessagesService _messageService;

        public MessagesController(IQueueMessagesService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [Route("queue")]
        public async Task<IActionResult> PostMessage([FromBody] MessageDto message)
        {
            await _messageService.PublishMessageForAnalysisAsync(message);
            return Accepted();
        }
    }
}
