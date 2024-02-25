using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace employee_system.Models
{
    public class DepartmentEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }


    }
}
