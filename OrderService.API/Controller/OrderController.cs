using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.API.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IPublishEndpoint _publishEndpoint;

		public OrderController(IPublishEndpoint publishEndpoint)
		{
			_publishEndpoint = publishEndpoint;
		}

		[HttpPost]
		public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrder command)
		{
			await _publishEndpoint.Publish(command);
			return Ok("Order submitted to the saga.");
		}
	}
}
