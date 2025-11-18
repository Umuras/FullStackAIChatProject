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
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageById(int id)
        {
            Message message = await messageService.GetMessageByIdAsync(id);
            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] MessageRequest message)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            await messageService.AddMessageAsync(message);

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] MessageRequest messageRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await messageService.UpdateMessageAsync(id, messageRequest);

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
