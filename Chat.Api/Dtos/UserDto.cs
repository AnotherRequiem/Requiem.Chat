using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Dtos;

public class UserDto
{
    [Required]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "Name must be at least {2}, and maximum of {1} characters")]
    public string Name { get; set; }
}