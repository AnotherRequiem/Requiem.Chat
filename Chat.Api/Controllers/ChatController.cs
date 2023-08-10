using Chat.Api.Dtos;
using Chat.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : Controller
{
    private readonly ChatService _chatService;
    
    public ChatController(ChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("register-user")]
    public IActionResult RegisterUser(UserDto model)
    {
        if (_chatService.AddUserToList(model.Name))
        {
            // 204 status code
            return NoContent();
        }

        return BadRequest("This name is taken. Please, choose another one");
    }
}