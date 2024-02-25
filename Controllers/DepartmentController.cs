using employee_system.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_employee.Data;

namespace employee_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> AddEmployee(Department department)
        {
            department.CreateDate = DateTime.Now;
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return Ok(department);
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            return Ok(departments);

        }
        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<IActionResult> GetIdByDepartments(int id)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x=>x.Id==id);
            return Ok(department);

        }
    }
}
