using Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi_ClinicaTerapia.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("GetMessageForReceptorId/{toId}/{fromId}/{typeUserTo}")]
        public ActionResult<IEnumerable<Message>> GetMessageForReceptorId(int toId, int fromId, string typeUserTo)
        {
            var messages = _messageService.GetMessageForReceptorId(toId, fromId, typeUserTo);
            return Ok(messages);
        } 
        [HttpGet("GetMessageDetailUsersMessage/{fromId}/{fromTyperUser}")]
        public ActionResult<IEnumerable<Message>> GetMessageDetailUsersMessage(int fromId, string fromTyperUser)
        {
            var messages = _messageService.GetMessageDetailUsersMessage(fromId, fromTyperUser);
            return Ok(messages);
        }
        [HttpGet("GetMessagesForUserId/{fromId}/{typeFromUser}")]
        public ActionResult<IEnumerable<Message>> GetMessagesForUserId(int fromId, string typeFromUser)
        {
            var messages = _messageService.GetMessagesForUserId(fromId, typeFromUser);
            return Ok(messages);
        }
        [HttpPost("PostInsertaMessage")]
        public void InsertaMessage(Message message)
        {
            _messageService.InsertaMessage(message);
        }
    }
}
