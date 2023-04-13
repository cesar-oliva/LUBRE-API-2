using Lubre.Repository.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;


namespace Lubre.WebAPI.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        
        /// <summary>
        /// receives by parameter the application in Employee and we inject it
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="employee"></param>
        /// <param name="mapper"></param>
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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
                var response =  await _employeeRepository.GetAllAsync();
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
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null) return new JsonResult("Not Found") { StatusCode = 400 };
                return new JsonResult("Employee Found") { StatusCode = 200 };   
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(RegisterEmployeeRequestDTO employeeDto) 
        {
            try
            {
                if(ModelState.IsValid){
                var newEmployee = await _employeeRepository.AddAsync(employeeDto);
                return CreatedAtAction("GetOne", new { id = newEmployee.Id }, newEmployee);
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
        /// <returns>response object</returns>
        /// <response code="200"> OK. returns the requested object </response>
        /// <response code="400"> NotFound. returns the requested object was not found </response>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, RegisterEmployeeRequestDTO dto)
        {
            if(id.Equals(Guid.Empty)) return new JsonResult("Not Found") { StatusCode = 400 };
            if (dto == null) return new JsonResult("Not Found") { StatusCode = 400 };
            try
            {            
                var employee = await _employeeRepository.UpdateAsync(id, dto);
                return new JsonResult(employee) { StatusCode = 200 };      
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
            if (id.Equals(Guid.Empty)) return NotFound();
            try
            {
                _employeeRepository.DeleteAsync(id);
                return new JsonResult("The employee has been removed") { StatusCode = 200 };     
            }
            catch (System.Exception)
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
        }
    }
