using BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorServer.Pages
{
    public partial class Employees
    {
        private bool isVisible;
        private string searchString = "";
        private string editordelete = "save";
        private EmployeeDTO Employee = new EmployeeDTO();
        private List<EmployeeDTO> EmployeesList = new List<EmployeeDTO>();
    //    private string[] Designation =
    //{
    //    "PM", "Architect", " SSE", "SE","ASE",
        
    //};
        Color Color = Color.Success;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                isVisible = true;

                await GetEmployees();
                isVisible = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                isVisible = false;
                navigationManager.NavigateTo("/");
                SnackBar.Add("Something went wrong", Severity.Error);
            }
        }
        private async Task<List<EmployeeDTO>> GetEmployees()
        {
            EmployeesList = await EmployeeService.GetEmployees();
            EmployeesList.Reverse();
            return EmployeesList;
        }

        private void OnScroll(ScrollEventArgs e)
        {
            Color = (e.FirstChildBoundingClientRect.Top * -1) switch
            {
                var x when x < 500 => Color.Primary,
                var x when x < 1500 => Color.Secondary,
                var x when x < 2500 => Color.Tertiary,
                _ => Color.Error
            };
        }

        private bool Search(EmployeeDTO Employee)
        {
            if (string.IsNullOrWhiteSpace(searchString)) return true;
            if (Employee.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
            )
            {
                return true;
            }
            return false;
        }
        private async Task Save()
        {
            if (editordelete == "edit")
            {
                var res = await EmployeeService.UpdateEmployee(Employee);
                if (res)
                {
                    editordelete = "save";
                    SnackBar.Add("Employee Saved.", Severity.Success);
                    Employee = new EmployeeDTO();
                    await GetEmployees();
                }
            }
            else if (editordelete == "save")
            {
                var res = await EmployeeService.AddEmployee(Employee);
                Employee = new EmployeeDTO();
                if (res == true)
                {
                    SnackBar.Add("Employee Saved.", Severity.Success);
                    await GetEmployees();
                }
            }


        }
        private void Edit(int id)
        {
            Employee = EmployeesList.FirstOrDefault(c => c.Id == id);
            editordelete = "edit";
        }
        private async Task Delete(int id)
        {
            var res = await EmployeeService.DeleteEmployee(id);
            if (res)
            {
                SnackBar.Add("Employee Deleted.", Severity.Error);
                await GetEmployees();
            }

        }
    }
}
