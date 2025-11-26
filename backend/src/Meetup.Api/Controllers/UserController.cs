using Microsoft.AspNetCore.Mvc;

namespace Meetup.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("validate")]
    public IActionResult ValidateUser([FromBody] ValidateUserRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.UserName))
        {
            return Ok(new ValidateUserResponse
            {
                IsValid = false,
                Message = "Username cannot be empty"
            });
        }

        // Validate that username contains only letters (A-Z, a-z)
        bool isValid = request.UserName.All(c => char.IsLetter(c));

        return Ok(new ValidateUserResponse
        {
            IsValid = isValid,
            Message = isValid 
                ? $"Username '{request.UserName}' is valid" 
                : $"Username '{request.UserName}' is invalid. Only letters (A-Z, a-z) are allowed",
            UserName = request.UserName
        });
    }
}

public class ValidateUserRequest
{
    public string UserName { get; set; } = string.Empty;
}

public class ValidateUserResponse
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? UserName { get; set; }
}
