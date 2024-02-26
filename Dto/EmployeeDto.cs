using System.ComponentModel.DataAnnotations;

namespace employee_system.Dto
{
    public class EmployeeDto
    {
        public string FirstName { get; set; } 
        public String LastName { get; set; } 
        public String Address { get; set; }    
        public String MobileNumb { get; set; }
        public String Email { get; set; }
        public String BirthDay { get; set; }
        public List<int> DepartmentsId { get; set; }
       

    }
}
