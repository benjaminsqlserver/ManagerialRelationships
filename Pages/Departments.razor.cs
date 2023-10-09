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
    public partial class Departments
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

        protected IEnumerable<EmployeesApp.Models.ConData.Department> departments;

        protected RadzenDataGrid<EmployeesApp.Models.ConData.Department> grid0;
        protected bool isEdit = true;

        protected string search = "";

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            departments = await ConDataService.GetDepartments(new Query { Filter = $@"i => i.DepartmentName.Contains(@0)", FilterParameters = new object[] { search } });
        }
        protected override async Task OnInitializedAsync()
        {
            departments = await ConDataService.GetDepartments(new Query { Filter = $@"i => i.DepartmentName.Contains(@0)", FilterParameters = new object[] { search } });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            isEdit = false;
            department = new EmployeesApp.Models.ConData.Department();
        }

        protected async Task EditRow(EmployeesApp.Models.ConData.Department args)
        {
            isEdit = true;
            department = args;
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EmployeesApp.Models.ConData.Department department)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ConDataService.DeleteDepartment(department.DepartmentID);

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
                    Detail = $"Unable to delete Department" 
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await ConDataService.ExportDepartmentsToCSV(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Departments");
            }

            if (args == null || args.Value == "xlsx")
            {
                await ConDataService.ExportDepartmentsToExcel(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Departments");
            }
        }
        protected bool errorVisible;
        protected EmployeesApp.Models.ConData.Department department;

        protected async Task FormSubmit()
        {
            try
            {
                var result = isEdit ? await ConDataService.UpdateDepartment(department.DepartmentID, department) : await ConDataService.CreateDepartment(department);

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