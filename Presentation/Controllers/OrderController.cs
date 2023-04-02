using BusinessLogic.Interfaces;
using Common.DTO;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderInDto order)
        {
            try
            {
                var createdOrder = await _orderService.CreateOrderAsync(order);
                return Created($"{Request.Path}/{createdOrder.Id}", createdOrder);
            }
            catch (ForeignKeyException ex)
            {
                var response = new ValidationProblemDetails
                {
                    Status = 400
                };

                response.Errors.Add("Provider", new[] { ex.Message });

                return BadRequest(response);
            }
            catch (OrderNumberEqualsOrderItemNameException ex)
            {
                var response = new ValidationProblemDetails
                {
                    Status = 400
                };

                response.Errors.Add("Number", new[] { ex.Message });

                return BadRequest(response);
            }
            catch (OrderNumberProviderIdIsNotUniqueException ex)
            {
                var response = new ValidationProblemDetails
                {
                    Status = 400

                };

                response.Errors.Add("Number", new[] { ex.Message });
                response.Errors.Add("Provider", new[] { ex.Message });

                return BadRequest(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet, Route("{orderId:int}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            try
            {
                return Ok(await _orderService.GetOrderAsync(orderId));
            }
            catch (PrimaryKeyException ex)
            {
                var response = new ValidationProblemDetails
                {
                    Status = 404
                };

                response.Errors.Add("Id", new[] { ex.Message });

                return NotFound(response);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet, Route("Filter")]
        public async Task<IActionResult> GetFilters()
        {
            try
            {
                return Ok(await _orderService.GetOrderFilterAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] DateTime? startPeriod,
            [FromQuery] DateTime? endPeriod, [FromQuery] List<string>? orderNumber, [FromQuery] List<int>? providerId)
        {
            return Ok(await _orderService.GetAllOrdersAsync(startPeriod, endPeriod, orderNumber, providerId));
        }

        [HttpPut, Route("{orderId:int}")]
        public async Task<IActionResult> UpdateOrder(int orderId, OrderInDto order)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateOrderAsync(orderId, order);
                return Created($"{Request.Path}", updatedOrder);
            }
            catch (ForeignKeyException ex)
            {
                var response = new ValidationProblemDetails
                {
                    Status = 400
                };

                response.Errors.Add("Provider", new[] { ex.Message });

                return BadRequest(response);
            }
            catch (OrderNumberEqualsOrderItemNameException ex)
            {
                var response = new ValidationProblemDetails
                {
                    Status = 400
                };

                response.Errors.Add("Number", new[] { ex.Message });

                return BadRequest(response);
            }
            catch (OrderNumberProviderIdIsNotUniqueException ex)
            {
                var response = new ValidationProblemDetails
                {
                    Status = 400

                };

                response.Errors.Add("Number", new[] { ex.Message });
                response.Errors.Add("Provider", new[] { ex.Message });

                return BadRequest(response);
            }
            catch (PrimaryKeyException ex)
            {
                var response = new ValidationProblemDetails
                {
                    Status = 404
                };

                response.Errors.Add("Id", new[] { ex.Message });

                return NotFound(response);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete, Route("{orderId:int}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                await _orderService.DeleteOrderAsync(orderId);
                return Ok();
            }
            catch (PrimaryKeyException ex)
            {
                var response = new ValidationProblemDetails
                {
                    Status = 404
                };

                response.Errors.Add("Id", new[] { ex.Message });

                return NotFound(response);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}