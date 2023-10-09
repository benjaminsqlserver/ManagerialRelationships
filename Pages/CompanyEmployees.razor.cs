using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace EmployeesApp.Pages
{
    public partial class Employees
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public ConDataService ConDataService { get; set; }

        protected IEnumerable<EmployeesApp.Models.ConData.Employee> employees;

        protected RadzenDataGrid<EmployeesApp.Models.ConData.Employee> grid0;
        protected bool isEdit = true;

        protected string search = "";

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            employees = await ConDataService.GetEmployees(new Query { Filter = $@"i => i.FirstName.Contains(@0) || i.MiddleName.Contains(@0) || i.LastName.Contains(@0) || i.EmployeePhoto.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Department,Gender,Employee1" });
        }
        protected override async Task OnInitializedAsync()
        {
            employees = await ConDataService.GetEmployees(new Query { Filter = $@"i => i.FirstName.Contains(@0) || i.MiddleName.Contains(@0) || i.LastName.Contains(@0) || i.EmployeePhoto.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Department,Gender,Employee1" });

            departmentsForDepartmentID = await ConDataService.GetDepartments();

            gendersForGenderID = await ConDataService.GetGenders();

            employeesForManagerID = await ConDataService.GetEmployees();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            isEdit = false;
            employee = new EmployeesApp.Models.ConData.Employee();
        }

        protected async Task EditRow(EmployeesApp.Models.ConData.Employee args)
        {
            isEdit = true;
            employee = args;
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EmployeesApp.Models.ConData.Employee employee)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ConDataService.DeleteEmployee(employee.EmployeeID);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                { 
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error", 
                    Detail = $"Unable to delete Employee" 
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await ConDataService.ExportEmployeesToCSV(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "Department,Gender,Employee1", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Employees");
            }

            if (args == null || args.Value == "xlsx")
            {
                await ConDataService.ExportEmployeesToExcel(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "Department,Gender,Employee1", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Employees");
            }
        }
        protected bool errorVisible;
        protected EmployeesApp.Models.ConData.Employee employee;

        protected IEnumerable<EmployeesApp.Models.ConData.Department> departmentsForDepartmentID;

        protected IEnumerable<EmployeesApp.Models.ConData.Gender> gendersForGenderID;

        protected IEnumerable<EmployeesApp.Models.ConData.Employee> employeesForManagerID;

        protected async Task FormSubmit()
        {
            try
            {
                var result = isEdit ? await ConDataService.UpdateEmployee(employee.EmployeeID, employee) : await ConDataService.CreateEmployee(employee);

            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {

        }
    }
}