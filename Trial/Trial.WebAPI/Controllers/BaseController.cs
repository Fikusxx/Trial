using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Trial.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public abstract class BaseController : ControllerBase
{
    private IMediator? mediator;
    protected IMediator Mediator
    {
        get
        {
            return mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
        }
    }
}
