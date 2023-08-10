using Chat.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Persistence.Interfaces;

namespace Chat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : Controller
{
    private readonly IUserRepository _userRepository;
    
    public ChatController(IUserRepository repository)
    {
        _userRepository = repository;
    }

    [HttpPost("register-user")]
    public IActionResult RegisterUser(UserDto model)
    {
        if (_userRepository.AddUser(model.Name))
        {
            //204 status code
            return NoContent();
        }
        
        return BadRequest("This name is taken. Please, choose another one");
        
        // if (_chatService.AddUserToList(model.Name))
        // {
        //     // 204 status code
        //     return NoContent();
        // }
        //
        // return BadRequest("This name is taken. Please, choose another one");
    }
}