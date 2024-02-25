using employee_system.Dto;
using employee_system.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_employee.Data;
using System.Linq;

namespace employee_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto dto)
        {
            var employee = new Employee();
            employee.BirthDay = dto.BirthDay;
            employee.CreateDate = DateTime.Now;
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.Address = dto.Address;
            employee.MobileNumb = dto.MobileNumb;

            foreach (var id in dto.DepartmentsId)
            {
                var department = _context.Departments.FirstOrDefault(x => x.Id == id);
                if (department != null)
                {
                    employee.DepartmentEmployees.Add(new DepartmentEmployee()
                    {
                        Employee = employee,
                        DepartmentId = id,
                        Department = department
                    });
                }
            }


            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);


        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id,[FromBody] EmployeeDto dto)

        {

            var employee = await _context.Employees.
                Include(x => x.DepartmentEmployees).
                ThenInclude(y => y.Department).
                FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {

                employee.BirthDay = dto.BirthDay;
                employee.CreateDate = DateTime.Now;
                employee.FirstName = dto.FirstName;
                employee.LastName = dto.LastName;
                employee.Email = dto.Email;
                employee.Address = dto.Address;
                employee.MobileNumb = dto.MobileNumb;

                var oldId = employee.DepartmentEmployees.Select(x => x.DepartmentId);
                var newId = dto.DepartmentsId.Select(id => (int?)id);
                var add = newId.Except(oldId);
                var removeId = oldId.Except(newId);





                employee.DepartmentEmployees = employee.DepartmentEmployees.Where(x => !removeId.Contains(x.DepartmentId)).ToList();




                foreach (var ids in add)
                {
                    var department = _context.Departments.FirstOrDefault(x => x.Id == ids);
                    if (department != null)
                    {
                        employee.DepartmentEmployees.Add(new DepartmentEmployee()
                        {
                            Employee = employee,
                            DepartmentId = ids,
                            Department = department
                        });
                    }
                }


                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return Ok(employee);
            }
            else
            {
                throw new Exception("The value is null.");
            }


        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);

        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<IActionResult> GetIdByDepartments(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(employee);

        }

        [HttpGet]
        [Route("get-all-order-name")]
        public async Task<IActionResult> GetAllByOrderName()
        {
            var employees = await _context.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToListAsync();
            return Ok(employees);
        }
        [HttpGet]
        [Route("get-all-order-department")]
        public async Task<IActionResult> GetAllByOrderDepartment()
        {
            var employees = await _context.Employees
                .OrderBy(e => e.DepartmentEmployees.FirstOrDefault().Department.Name)
                .ToListAsync();

            return Ok(employees);
        }



    }
}
