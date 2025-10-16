using Microsoft.AspNetCore.Mvc;
using ResturantBooking.DTOs.TableDTOs;
using ResturantBooking.Services.IServices;

namespace ResturantBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        // Injecerat
        public TableController(ITableService tableservice)
        {
            _tableService = tableservice;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var tables = await _tableService.GetTableByIDAsync(id);
            if(tables == null) { return NotFound(); }
            return Ok(tables);
        }

        [HttpPost]
        public async Task<ActionResult> Create(TableDTO dto)
        {
            var id = await _tableService.CreateTableAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(TableDTO dto, int id)
        {
            if(id != dto.Id) { return BadRequest(); }
            var succes = await _tableService.UpdateTableAsync(dto);
            if (!succes) { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var succes = await _tableService.DeleteTableAsync(id);
            if (!succes) { return NotFound(); }
            return NoContent();
        }
    }
}
