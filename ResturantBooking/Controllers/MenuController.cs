using Microsoft.AspNetCore.Mvc;
using ResturantBooking.DTOs.MenuDTOs;
using ResturantBooking.Services.IServices;

namespace ResturantBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var menus = await _menuService.GetAllMenusAsync();
            return Ok(menus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var menus = await _menuService.GetMenyByIDAsync(id);
            if(menus == null) { return NotFound(); }
            return Ok(menus);
        }

        [HttpGet("popular")]
        public async Task<ActionResult> GetPopular()
        {
            var menus = await _menuService.GetPopularMenusAsync(3);
            return Ok(menus);
        }

        [HttpPost]
        public async Task<ActionResult> Create(MenuDTO dto)
        {
            var id = await _menuService.CreateMenyAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] MenuDTO dto)
        {
            if(id != dto.Id) { return BadRequest(); }
            var succes = await _menuService.UpdateMenuAsync(dto);
            if (!succes) { return NotFound(); }
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var succes = await _menuService.DeleteMenuAsync(id);
            if (!succes) { return NotFound(); }
            return NoContent();
        }

    }
}
