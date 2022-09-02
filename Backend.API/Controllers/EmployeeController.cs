using Backend.Bll.DTOs;
using Backend.Bll.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        /// <summary>
        /// Get employee with a given ID
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>A EmployeeDTO model</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _employeeService.GetAsync(id);
            return Ok(result);
        }
        /// <summary>
        /// Get All employees from the DB
        /// </summary>
        /// <returns>A List&lt;EmployeeDTO&gt;</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.GetListAsync();
            return Ok(result);
        }
        /// <summary>
        /// Add a new employee
        /// </summary>
        /// <param name="model">A EmployeeDTO model with employee data</param>
        /// <returns>A EmployeeDTO model</returns>
        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.AddAsync(model);
            return Ok(result);
        }
        /// <summary>
        /// Update an existing employee
        /// </summary>
        /// <param name="model">A EmployeeDTO model with employee data</param>
        /// <returns>A EmployeeDTO model</returns>
        [HttpPut]
        public async Task<IActionResult> Put(EmployeeDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.EditAsync(model);
            return Ok(result);
        }
        /// <summary>
        ///  Delete employee with a given ID
        /// </summary>
        /// <param name="id">Employee ID</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteAsync(id);
            return Ok();
        }
    }
}
