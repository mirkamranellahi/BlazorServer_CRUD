using System.ComponentModel.DataAnnotations;

namespace BlazorServer.Data
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
    public class EmployeeDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public Designation? Designation { get; set; }

        [Required]
        public Department? Department { get; set; }

        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }

    }

    public enum Designation
    {
        Intern = 1,
        ASE,
        SE,
        SSE,
        Architect,
        PM
    }
    public enum Department
    {
        BD = 1,
        MED,
        LAMP,
        SI,
        QA,
        Accounts,
        Management
    }
}
