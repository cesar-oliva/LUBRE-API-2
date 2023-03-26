
using AutoMapper;
using Lubre.Abstractions;
using Lubre.Entities;
using Lubre.WebAPI.DataTransferObject.Incoming;
using Lubre.WebAPI.DataTransferObject.Outgoing;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;


namespace Lubre.WebAPI.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        IApplication<Employee> _employee;
        IApplication<Gender> _gender;
        IApplication<Unit> _unit;
        IApplication<Address> _address;
        IApplication<Position> _position;
        private readonly IMapper _mapper;
        /// <summary>
        /// receives by parameter the application in Employee and we inject it
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="employee"></param>
        /// <param name="mapper"></param>
        public EmployeeController(IApplication<Employee> employee,IApplication<Gender> gender,IApplication<Address> address,IApplication<Unit> unit,IApplication<Position> position,IMapper mapper)
        {
            _employee = employee;
            _gender = gender;
            _address = address;
            _unit = unit;
            _position = position;
            _mapper = mapper;
        }
        /// <summary>
        /// get a list of employee objects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ResponseEmployeeRequestDTO))]
        public async Task<IActionResult> Get()
        {
            List<ResponseEmployeeRequestDTO> employeeDTO = new();
            var employee = await _employee.GetAllAsync();     
            foreach (var item in employee)
            {
                var newEmployeeDto = _mapper.Map<ResponseEmployeeRequestDTO>(item);
                //newEmployeeDto.GenderName = _gender.GetById(item.GenderId).Name;
                //newEmployeeDto.UnitName = _unit.GetById(item.UnitId).Name;
                //newEmployeeDto.PositionName = _position.GetById(item.PositionId).Name;
                employeeDTO.Add(newEmployeeDto);
            }
            return Ok(employeeDTO);
            
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
            if (id.Equals(Guid.Empty)) return NotFound();
            var employee = await _employee.GetByIdAsync(id);
            return Ok(_mapper.Map<RegisterEmployeeRequestDTO>(employee));
        }
        [HttpPost]
        public async Task<IActionResult> Save(RegisterEmployeeRequestDTO employeeDto) 
        {
            if(ModelState.IsValid){
                Employee employee = _mapper.Map<Employee>(employeeDto);
                await _employee.SaveAsync(employee);
                var newEmployee = _mapper.Map<ResponseEmployeeRequestDTO>(employee);
                return CreatedAtAction("GetOne", new { id = newEmployee.Id }, newEmployee);
                //return Ok(newEmployee);
            }
                return new JsonResult("Something went wrong") { StatusCode = 500 };
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
        public async Task<IActionResult> Update(RegisterEmployeeRequestDTO dto)
        {
            //if (dto.Id.Equals(Guid.Empty)|| dto == null) return NotFound();
            var employee = _mapper.Map<Employee>(dto);
            await _employee.UpdateAsync(employee);
            return Ok(employee);
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
            _employee.DeleteAsync(id);
            return Ok();
        }
    }
