using CrudOpration.Entity;
using CrudOpration.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudOpration.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: api/employee
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/employee/{id}
        [HttpPost("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(id);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/employee
        [HttpPost("insert")]
        public async Task<IActionResult> Insert(EmployeeEntity employee)
        {
            try
            {
                var newEmployeeId = await _employeeRepository.Insert(employee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployeeId }, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(EmployeeEntity employee)
        {
            try
            {
                var affectedRows = await _employeeRepository.Update(employee);
                return affectedRows > 0 ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // DELETE: api/employee/{id}
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _employeeRepository.Delete(id) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
