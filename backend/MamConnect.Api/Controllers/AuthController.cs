using System.Threading.Tasks;
using MamConnect.Application.Auth.Commands;
using MamConnect.Application.Auth.Queries;
using MamConnect.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MamConnect.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserCommand _registerUserCommand;
    private readonly LoginUserQuery _loginUserQuery;
    private readonly SetPasswordCommand _setPasswordCommand;

    public AuthController(
        RegisterUserCommand registerUserCommand,
        LoginUserQuery loginUserQuery,
        SetPasswordCommand setPasswordCommand)
    {
        _registerUserCommand = registerUserCommand;
        _loginUserQuery = loginUserQuery;
        _setPasswordCommand = setPasswordCommand;
    }

    [Authorize]
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserRequest request)
    {
        bool created = await _registerUserCommand.ExecuteAsync(request);
        if (!created)
        {
            return Conflict();
        }

        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(UserLoginRequest request)
    {
        AuthResponse? response = await _loginUserQuery.ExecuteAsync(request);
        if (response == null)
        {
            return Unauthorized();
        }

        return response;
    }

    [AllowAnonymous]
    [HttpPost("set-password")]
    public async Task<ActionResult<AuthResponse>> SetPassword(SetPasswordRequest request)
    {
        SetPasswordCommand.Result result = await _setPasswordCommand.ExecuteAsync(request);
        if (result.Status == SetPasswordCommand.ResultStatus.UserNotFound)
        {
            return Unauthorized();
        }

        if (result.Status == SetPasswordCommand.ResultStatus.AlreadyInitialized)
        {
            return Conflict();
        }

        return result.Response!;
    }
}
