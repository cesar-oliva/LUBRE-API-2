using Lubre.Repository.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;


namespace Lubre.WebAPI.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : Controller
    {
        
        /// <summary>
        /// receives by parameter the application in Unit and we inject it
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="unit"></param>
        /// <param name="mapper"></param>
        private readonly IUnitRepository _unitRepository;
        public UnitController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }
        /// <summary>
        /// get a list of unit objects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response =  await _unitRepository.GetAllAsync();
                if (response == null) return new JsonResult("Not Found") { StatusCode = 400 };
                return new JsonResult(response) { StatusCode = 200 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }  
        }

        /// <summary>
        /// get a unit object
        /// </summary>
        /// <remarks>
        /// receives an id from the client and returns an object of type unit
        /// </remarks>
        /// <param name="id">object id</param>
        /// <returns>unit object</returns>
        /// <response code="200"> OK. returns the requested object </response>
        /// <response code="400"> NotFound. returns the requested object was not found </response>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            if (id.Equals(Guid.Empty)) return new JsonResult("Not Found") { StatusCode = 400 };
            try
            {
                var unit = await _unitRepository.GetByIdAsync(id);
                if (unit == null) return new JsonResult("Not Found") { StatusCode = 400 };
                return new JsonResult(unit) { StatusCode = 200 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }  
        }

        [HttpPost]
        public async Task<IActionResult> Save(RegisterUnitRequestDTO unitDto) 
        {
            try
            {
                if(ModelState.IsValid){
                var newUnit = await _unitRepository.AddAsync(unitDto);
                return CreatedAtAction("GetOne", new { id = newUnit.Id }, newUnit);
                }
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }  
        }
        /// <summary>
        /// update a unit objetc by id
        /// </summary>
        /// <remarks>
        /// Receive the object to modify, look for the unit by id, map the entities request the update
        /// </remarks>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns>response object</returns>
        /// <response code="200"> OK. returns the requested object </response>
        /// <response code="400"> NotFound. returns the requested object was not found </response>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, RegisterUnitRequestDTO dto)
        {
            if(id.Equals(Guid.Empty)) return new JsonResult("Not Found") { StatusCode = 400 };
            if (dto == null) return new JsonResult("Not Found") { StatusCode = 400 };
            try
            {    
                var unit = await _unitRepository.UpdateAsync(id,dto);
                return new JsonResult(unit) { StatusCode = 200 };      
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            } 
        }

        /// <summary>
        /// Delete a unit object by id
        /// </summary>
        /// <remarks>
        ///  Receive the object to deleted, look for the unit by id, request the delete
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>response object</returns>
        /// <response code="200"> OK. returns the requested object </response>
        /// <response code="400"> NotFound. returns the requested object was not found </response>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id.Equals(Guid.Empty)) return new JsonResult("Not Found") { StatusCode = 400 };
            try
            {
                _unitRepository.DeleteAsync(id);
                return new JsonResult("the gender has been removed") { StatusCode = 200 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
        }
    }
