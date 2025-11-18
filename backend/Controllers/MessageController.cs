using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageById(int id)
        {
            Message message = await messageService.GetMessageByIdAsync(id);
            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] Message message)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await messageService.AddMessageAsync(message);

            return CreatedAtAction(nameof(GetMessageById), new { id = message.Id }, message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await messageService.UpdateMessageAsync(id, message);

            return Ok("Message updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await messageService.DeleteMessageAsync(id);
            return NoContent();
        }
    }
}
