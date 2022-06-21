using BlazorServer.Data;

namespace BlazorServer.Model
{
    public partial class EmployeesBlazor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public string EmployeeCode { get; set; }
    }
}
