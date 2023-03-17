using Lubre.Abstractions;
using Lubre.Entities;
using Lubre.WebAPI.DataTransferObject.Incoming;
using Microsoft.AspNetCore.Mvc;

namespace Lubre.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenderController : Controller
{
    IApplication<Gender> _gender;
    public GenderController(IApplication<Gender> gender)
    {
        _gender = gender;
    }

    /*
        SAVE
    */
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _gender.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOne(Guid id)
    {
        if (id.Equals("")) return NotFound();
        var gender = await _gender.GetByIdAsync(id);
        return Ok(gender);
    }
    [HttpPost]
    public async Task<IActionResult> Save(RegisterGenderRequestDTO dto) //Mapear
    {
        var g = new Gender()
        {
            Name = dto.GenderName,
        };
        await _gender.SaveAsync(g);
        return Ok(g);
    }
    /*
    UPDATE
    */
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(Guid id, RegisterGenderRequestDTO dto)
    {
        if (id.Equals("") || dto == null) return NotFound();
        var g = _gender.GetById(id);
        if (g != null)
        {
            g.Name = dto.GenderName;
        }
        await _gender.SaveAsync(g);
        return Ok(g);
    }
    /*
    DELETE
    */
    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(Guid id)
    {
        if (id.Equals("")) return NotFound();
        _gender.DeleteAsync(id);
        return Ok();
    }
}