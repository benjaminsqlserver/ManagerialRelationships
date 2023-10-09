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
    public partial class Genders
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

        protected IEnumerable<EmployeesApp.Models.ConData.Gender> genders;

        protected RadzenDataGrid<EmployeesApp.Models.ConData.Gender> grid0;
        protected bool isEdit = true;

        protected string search = "";

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            genders = await ConDataService.GetGenders(new Query { Filter = $@"i => i.GenderName.Contains(@0)", FilterParameters = new object[] { search } });
        }
        protected override async Task OnInitializedAsync()
        {
            genders = await ConDataService.GetGenders(new Query { Filter = $@"i => i.GenderName.Contains(@0)", FilterParameters = new object[] { search } });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            isEdit = false;
            gender = new EmployeesApp.Models.ConData.Gender();
        }

        protected async Task EditRow(EmployeesApp.Models.ConData.Gender args)
        {
            isEdit = true;
            gender = args;
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EmployeesApp.Models.ConData.Gender gender)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ConDataService.DeleteGender(gender.GenderID);

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
                    Detail = $"Unable to delete Gender" 
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await ConDataService.ExportGendersToCSV(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Genders");
            }

            if (args == null || args.Value == "xlsx")
            {
                await ConDataService.ExportGendersToExcel(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Genders");
            }
        }
        protected bool errorVisible;
        protected EmployeesApp.Models.ConData.Gender gender;

        protected async Task FormSubmit()
        {
            try
            {
                var result = isEdit ? await ConDataService.UpdateGender(gender.GenderID, gender) : await ConDataService.CreateGender(gender);

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