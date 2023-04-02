using System.ComponentModel.DataAnnotations;
using BusinessLogic.Interfaces;
using Common.DTO;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem(OrderItemInDto orderItem)
        {
            try
            {
                var createdOrderItem = await _orderItemService.CreateOrderItemAsync(orderItem);
                return Created($"{Request.Path}/{createdOrderItem.Id}", createdOrderItem);
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
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet, Route("{orderItemId:int}")]
        public async Task<IActionResult> GetOrderItem(int orderItemId)
        {
            try
            {
                return Ok(await _orderItemService.GetOrderItemAsync(orderItemId));
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
                return Ok(await _orderItemService.GetOrderItemFilterAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems([Required] int orderId, [FromQuery] List<string>? itemName,
            [FromQuery] List<string>? itemUnit)
        {
            return Ok(await _orderItemService.GetAllOrderItemsAsync(orderId, itemName, itemUnit));
        }

        [HttpPut, Route("{orderItemId:int}")]
        public async Task<IActionResult> UpdateOrderItem(int orderItemId, OrderItemInDto orderItem)
        {
            try
            {
                var updatedOrderItem = await _orderItemService.UpdateOrderItemAsync(orderItemId, orderItem);
                return Created($"{Request.Path}", updatedOrderItem);
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
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete, Route("{orderItemId:int}")]
        public async Task<IActionResult> DeleteOrderItem(int orderItemId)
        {
            try
            {
                await _orderItemService.DeleteOrderItemAsync(orderItemId);
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
                return BadRequest();
            }
        }
    }
}