using Lubre.Repository.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;


namespace Lubre.WebAPI.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : Controller
    {
        
        /// <summary>
        /// receives by parameter the application in Employee and we inject it
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="gender"></param>
        /// <param name="mapper"></param>
        private readonly IGenderRepository _genderRepository;
        public GenderController(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        /// <summary>
        /// get a list of employee objects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response =  await _genderRepository.GetAllAsync();
                if (response == null) return new JsonResult("Not Found") { StatusCode = 400 };
                return new JsonResult(response) { StatusCode = 200 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }  
        }

        /// <summary>
        /// get a employee object
        /// </summary>
        /// <remarks>
        /// receives an id from the client and returns an object of type employee
        /// </remarks>
        /// <param name="id">object id</param>
        /// <returns>employee object</returns>
        /// <response code="200"> OK. returns the requested object </response>
        /// <response code="400"> NotFound. returns the requested object was not found </response>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            if (id.Equals(Guid.Empty)) return new JsonResult("Not Found") { StatusCode = 400 };
            try
            {
                var gender = await _genderRepository.GetByIdAsync(id);
                if (gender == null) return new JsonResult("Not Found") { StatusCode = 400 };
                return new JsonResult(gender) { StatusCode = 200 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }  
        }

        [HttpPost]
        public async Task<IActionResult> Save(RegisterGenderRequestDTO genderDto) 
        {
            try
            {
                if(ModelState.IsValid){
                var newGender = await _genderRepository.AddAsync(genderDto);
                return CreatedAtAction("GetOne", new { id = newGender.Id }, newGender);
                }
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }  
        }
        /// <summary>
        /// update a employee objetc by id
        /// </summary>
        /// <remarks>
        /// Receive the object to modify, look for the employee by id, map the entities request the update
        /// </remarks>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns>response object</returns>
        /// <response code="200"> OK. returns the requested object </response>
        /// <response code="400"> NotFound. returns the requested object was not found </response>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, RegisterGenderRequestDTO dto)
        {
            if(id.Equals(Guid.Empty)) return new JsonResult("Not Found") { StatusCode = 400 };
            if (dto == null) return new JsonResult("Not Found") { StatusCode = 400 };
            try
            {
                var updateToGender = new ResponseGenderRequestDTO(){
                    Id = id,
                    GenderName = dto.GenderName
                };              
                var gender = await _genderRepository.UpdateAsync(updateToGender);
                return new JsonResult(gender) { StatusCode = 200 };      
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            } 
        }

        /// <summary>
        /// Delete a employe object by id
        /// </summary>
        /// <remarks>
        ///  Receive the object to deleted, look for the employee by id, request the delete
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
                _genderRepository.DeleteAsync(id);
                return new JsonResult("the gender has been removed") { StatusCode = 200 };
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
        }
    }
