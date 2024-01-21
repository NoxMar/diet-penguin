using DietPenguin.WebApi.Contracts.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DietPenguin.WebApi.User;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("me")]
    public async Task<ActionResult> GetCurrentUser()
    {
        var result = await _mediator.Send(new GetCurrentUserRequest());
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
}