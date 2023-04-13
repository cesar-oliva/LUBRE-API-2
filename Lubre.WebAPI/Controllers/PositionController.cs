using Lubre.Repository.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;


namespace Lubre.WebAPI.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : Controller
    {
        
        /// <summary>
        /// receives by parameter the application in Position and we inject it
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="position"></param>
        /// <param name="mapper"></param>
        private readonly IPositionRepository _positionRepository;
        public PositionController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }
        /// <summary>
        /// get a list of position objects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response =  await _positionRepository.GetAllAsync();
                if (response == null) return new JsonResult("Not Found") { StatusCode = 400 };
                return new JsonResult(response) { StatusCode = 200 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }  
        }

        /// <summary>
        /// get a position object
        /// </summary>
        /// <remarks>
        /// receives an id from the client and returns an object of type position 
        /// </remarks>
        /// <param name="id">object id</param>
        /// <returns>position  object</returns>
        /// <response code="200"> OK. returns the requested object </response>
        /// <response code="400"> NotFound. returns the requested object was not found </response>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            if (id.Equals(Guid.Empty)) return new JsonResult("Not Found") { StatusCode = 400 };
            try
            {
                var unit = await _positionRepository.GetByIdAsync(id);
                if (unit == null) return new JsonResult("Not Found") { StatusCode = 400 };
                return new JsonResult(unit) { StatusCode = 200 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }  
        }

        [HttpPost]
        public async Task<IActionResult> Save(RegisterPositionRequestDTO positionDto) 
        {
            try
            {
                if(ModelState.IsValid){
                var newPosition = await _positionRepository.AddAsync(positionDto);
                return CreatedAtAction("GetOne", new { id = newPosition.Id }, newPosition);
                }
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }  
        }
        /// <summary>
        /// update a position objetc by id
        /// </summary>
        /// <remarks>
        /// Receive the object to modify, look for the position by id, map the entities request the update
        /// </remarks>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns>response object</returns>
        /// <response code="200"> OK. returns the requested object </response>
        /// <response code="400"> NotFound. returns the requested object was not found </response>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, RegisterPositionRequestDTO dto)
        {
            if(id.Equals(Guid.Empty)) return new JsonResult("Not Found") { StatusCode = 400 };
            if (dto == null) return new JsonResult("Not Found") { StatusCode = 400 };
            try
            {    
                var position = await _positionRepository.UpdateAsync(id,dto);
                return new JsonResult(position) { StatusCode = 200 };      
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            } 
        }

        /// <summary>
        /// Delete a position object by id
        /// </summary>
        /// <remarks>
        ///  Receive the object to deleted, look for the position by id, request the delete
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
                _positionRepository.DeleteAsync(id);
                return new JsonResult("the gender has been removed") { StatusCode = 200 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
        }
    }
