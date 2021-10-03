using ExampleWithMediatR.Domain.Commands;
using ExampleWithMediatR.Domain.Dtos;
using ExampleWithMediatR.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleWithMediatR.Controllers
{
    public class CustomerController : ApiControllerBase
    {
        public CustomerController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CustomerDto>> GetCustomerAsync(Guid id)
        {
            return Single(await QueryAsync(new GetCustomerQuery(id)));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateCustomerAsync([FromBody] CreateCustomerCommand command)
        {
            return Ok(await CommandAsync(command));
        }
    }
}
