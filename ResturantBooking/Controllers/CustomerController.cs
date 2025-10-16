using Microsoft.AspNetCore.Mvc;
using ResturantBooking.DTOs.CustomerDTOs;
using ResturantBooking.Services.IServices;

namespace ResturantBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        //Injecerat
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var customers = await _customerService.GetCustomerByID(id);
            if(customers == null) { return NotFound(); }
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CustomerDTO dto)
        {
            var id = await _customerService.CreateCustomerAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(CustomerDTO dto, int id)
        {
            if(id != dto.Id) { return BadRequest(); }

            var succes = await _customerService.UpdateCustomerAsync(dto);
            if (!succes) { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var succes = await _customerService.DeleteCustomerAsync(id);
            if (!succes) { return NotFound(); }
            return NoContent();
        }
    }
}
