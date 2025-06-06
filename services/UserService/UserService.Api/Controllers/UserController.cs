﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Users.Commands;
using UserService.Application.Users.Queries;

namespace UserService.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserCmd request)
    {
        var response = await _mediator.Send(request);
        if (response.Errors.Any())
            return UnprocessableEntity(response);

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var response = await _mediator.Send(new GetUserByIdCmd(id));
        if (response.Errors.Any())
            return UnprocessableEntity(response);

        return Ok(response);
    }
}
