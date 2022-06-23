using BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using BlazorServer.Components;

namespace BlazorServer.Pages
{
    public partial class Employees
    {
        private bool isVisible;
        private string searchString = "";
        private EmployeeDTO Employee = new EmployeeDTO();
        private List<EmployeeDTO> EmployeesList = new List<EmployeeDTO>();
       
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
       

        private async void OpenDailog()
        {
            var dialog = DialogService.Show<Add_Employee_Dialog>("add Employee", new DialogOptions { DisableBackdropClick = true, MaxWidth = MaxWidth.Small, FullWidth = true });
            var res = await dialog.GetReturnValueAsync<bool?>();
            if (Convert.ToBoolean(res))
            {
                await GetEmployees();
                StateHasChanged();
                SnackBar.Add("Employee Saved.", Severity.Success);
            }
        }

        private async void Edit(int id)
        {
            Employee = EmployeesList.FirstOrDefault(c => c.Id == id);
            DialogParameters keyValuePairs = new DialogParameters();
            keyValuePairs.Add("Employee", Employee);
            var dialog = DialogService.Show<Add_Employee_Dialog>("add Employee",keyValuePairs, new DialogOptions { DisableBackdropClick = true, MaxWidth = MaxWidth.Small, FullWidth = true });
            var res = await dialog.GetReturnValueAsync<bool?>();
            if (Convert.ToBoolean(res))
            {
                StateHasChanged();
                SnackBar.Add("Employee Saved.", Severity.Success);
            }
        }
        private async Task Delete(int id)
        {

            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Are you sure you want to Delete?");
            parameters.Add("ButtonText", "Yes");
            parameters.Add("Color", Color.Error);

            var result = DialogService.Show<Confirm_Dialog>("Confirm", parameters, new DialogOptions { DisableBackdropClick = true, MaxWidth = MaxWidth.Small, FullWidth = true });
            var res = await result.GetReturnValueAsync<bool?>();
            if (Convert.ToBoolean(res))
            {
                if (await EmployeeService.DeleteEmployee(id))
                {
                    SnackBar.Add("Employee Deleted.", Severity.Error);
                    await GetEmployees();
                    StateHasChanged();
                }

            }


        }
    }
}
