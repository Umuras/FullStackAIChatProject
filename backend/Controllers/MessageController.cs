using backend.Dtos;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            List<Message> messages = await messageService.GetAllMessagesAsync();
            List<MessageResponse> messagesResponse = messageService.ChangeMessagesResponse(messages);
            return Ok(messagesResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageById(int id)
        {
            Message message = await messageService.GetMessageByIdAsync(id);
            MessageResponse messageResponse = messageService.ChangeMessageResponse(message);
            return Ok(messageResponse);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] MessageRequest message)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Message dbMessage = await messageService.AddMessageAsync(message);
            MessageResponse messageResponse = messageService.ChangeMessageResponse(dbMessage);

            return CreatedAtAction(nameof(GetMessageById), new {id = dbMessage.Id}, messageResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] MessageRequest messageRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Message dbMessage = await messageService.UpdateMessageAsync(id, messageRequest);
            MessageResponse messageResponse = messageService.ChangeMessageResponse(dbMessage);

            return Ok(messageResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await messageService.DeleteMessageAsync(id);
            return NoContent();
        }
    }
}
