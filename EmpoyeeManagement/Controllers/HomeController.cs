using EmpoyeeManagement.Data;
using EmpoyeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpoyeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly DataBaseContext _databasecontext;
        public HomeController(DataBaseContext databasecontext)
        {
            _databasecontext = databasecontext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var Employees = await _databasecontext.employees.ToArrayAsync();
            return Ok(Employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await _databasecontext.employees.AddAsync(employee);
            await _databasecontext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var responce = await _databasecontext.employees.FirstOrDefaultAsync(x => x.Id == id);
            if (responce == null)
            {
                return NotFound();
            }
            return Ok(responce);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _databasecontext.employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Department = updateEmployeeRequest.Department;

            await _databasecontext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var responce = await _databasecontext.employees.FindAsync(id);
            if (responce == null)
            {
                return NotFound();
            }

            _databasecontext.employees.Remove(responce);
            await _databasecontext.SaveChangesAsync();
            return Ok(responce);
        }
    }
}
