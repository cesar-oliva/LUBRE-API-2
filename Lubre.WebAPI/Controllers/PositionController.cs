using Lubre.Abstractions;
using Lubre.Entities;
using Lubre.WebAPI.DataTransferObject.Incoming;
using Microsoft.AspNetCore.Mvc;

namespace Lubre.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PositionController : Controller
{
    IApplication<Position> _position;
    public PositionController(IApplication<Position> position)
    {
        _position = position;
    }

    /*
        SAVE
    */
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _position.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOne(Guid id)
    {
        if (id.Equals("")) return NotFound();
        var position = await _position.GetByIdAsync(id);
        return Ok(position);
    }
    [HttpPost]
    public async Task<IActionResult> Save(RegisterPositionRequestDTO dto) //Mapear
    {
        var p = new Position()
        {
            Name = dto.Name,
        };
        await _position.SaveAsync(p);
        return Ok(p);
    }
    /*
    UPDATE
    */
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(Guid id, RegisterPositionRequestDTO dto)
    {
        if (id.Equals("") || dto == null) return NotFound();
        var p = _position.GetById(id);
        if (p != null)
        {
            p.Name = dto.Name;
        }
        await _position.SaveAsync(p);
        return Ok(p);
    }
    /*
    DELETE
    */
    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(Guid id)
    {
        if (id.Equals("")) return NotFound();
        _position.DeleteAsync(id);
        return Ok();
    }
}