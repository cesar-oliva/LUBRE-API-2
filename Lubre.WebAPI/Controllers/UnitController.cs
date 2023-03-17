using Lubre.Abstractions;
using Lubre.Entities;
using Lubre.WebAPI.DataTransferObject.Incoming;
using Microsoft.AspNetCore.Mvc;

namespace Lubre.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UnitController : Controller
{
    IApplication<Unit> _unit;
    public UnitController(IApplication<Unit> unit)
    {
        _unit = unit;
    }

    /*
        SAVE
    */
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unit.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOne(Guid id)
    {
        if (id.Equals("")) return NotFound();
        var unit = await _unit.GetByIdAsync(id);
        return Ok(unit);
    }
    [HttpPost]
    public async Task<IActionResult> Save(RegisterUnitRequestDTO dto) //Mapear
    {
        var u = new Unit()
        {
            Name = dto.Name,
        };
        await _unit.SaveAsync(u);
        return Ok(u);
    }
    /*
    UPDATE
    */
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(Guid id, RegisterUnitRequestDTO dto)
    {
        if (id.Equals("") || dto == null) return NotFound();
        var u = _unit.GetById(id);
        if (u != null)
        {
            u.Name = dto.Name;
        }
        await _unit.SaveAsync(u);
        return Ok(u);
    }
    /*
    DELETE
    */
    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(Guid id)
    {
        if (id.Equals("")) return NotFound();
        _unit.DeleteAsync(id);
        return Ok();
    }
}