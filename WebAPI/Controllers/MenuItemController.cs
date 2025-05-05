using Microsoft.AspNetCore.Mvc;
using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.WebAPI.DTOs;

namespace TastyByThreeMusketeers.WebAPI.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    public class MenuItemController : ControllerBase

    {

        private readonly IMenuItemRepository _repository;

        public MenuItemController(IMenuItemRepository repository)

        {

            _repository = repository;

        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<MenuItemDTO>>> GetAll()

        {

            var items = await _repository.GetAllAsync();

            var dtoItems = items.Select(i => new MenuItemDTO

            {

                Id = i.Id,

                Name = i.Name,

                Price = i.Price,

                IsAvailable = i.IsAvailable,

                Description = i.Description

            });

            return Ok(dtoItems);

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<MenuItemDTO>> GetById(int id)

        {

            var item = await _repository.GetByIdAsync(id);

            if (item == null)

                return NotFound();

            return Ok(new MenuItemDTO

            {

                Id = item.Id,

                Name = item.Name,

                Price = item.Price,

                IsAvailable = item.IsAvailable,

                Description = item.Description

            });

        }

        [HttpPost]

        public async Task<ActionResult<MenuItemDTO>> Create(MenuItemDTO dto)

        {

            var menuItem = new MenuItem

            {

                Name = dto.Name,

                Price = dto.Price,

                IsAvailable = dto.IsAvailable,

                Description = dto.Description

            };

            var created = await _repository.AddAsync(menuItem);

            dto.Id = created.Id;

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, MenuItemDTO dto)

        {

            if (id != dto.Id)

                return BadRequest();

            var menuItem = await _repository.GetByIdAsync(id);

            if (menuItem == null)

                return NotFound();

            menuItem.Name = dto.Name;

            menuItem.Price = dto.Price;

            menuItem.IsAvailable = dto.IsAvailable;

            menuItem.Description = dto.Description;

            await _repository.UpdateAsync(menuItem);

            return NoContent();

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)

        {

             await _repository.DeleteAsync(id);

            return NoContent();

        }

    }

}
