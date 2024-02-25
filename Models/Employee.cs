using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace employee_system.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(MobileNumb), IsUnique = true)]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public String LastName { get; set; }
        [Required]
        [StringLength(300)]
        public String Address { get; set; }
       
        [Required]
        [StringLength(20)]
         public String MobileNumb { get; set; }
        
        [Required]
        [StringLength(100)] 
        public String Email { get; set; }
        
        [Required]
        [StringLength(100)]
        public String BirthDay { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<DepartmentEmployee> DepartmentEmployees { get; set; } = new List<DepartmentEmployee>();
    }
}
