using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MurderParty.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}