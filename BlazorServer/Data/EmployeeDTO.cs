using System.ComponentModel.DataAnnotations;

namespace BlazorServer.Data
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public Designation? Designation { get; set; }

        
        public Department? Department { get; set; }

        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }

    }
}
