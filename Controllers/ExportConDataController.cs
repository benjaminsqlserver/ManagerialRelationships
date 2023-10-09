using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using EmployeesApp.Data;

namespace EmployeesApp.Controllers
{
    public partial class ExportConDataController : ExportController
    {
        private readonly ConDataContext context;
        private readonly ConDataService service;

        public ExportConDataController(ConDataContext context, ConDataService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/ConData/departments/csv")]
        [HttpGet("/export/ConData/departments/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepartmentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDepartments(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/departments/excel")]
        [HttpGet("/export/ConData/departments/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepartmentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDepartments(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/employees/csv")]
        [HttpGet("/export/ConData/employees/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmployeesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEmployees(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/employees/excel")]
        [HttpGet("/export/ConData/employees/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmployeesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEmployees(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/genders/csv")]
        [HttpGet("/export/ConData/genders/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGendersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetGenders(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/genders/excel")]
        [HttpGet("/export/ConData/genders/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGendersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetGenders(), Request.Query), fileName);
        }
    }
}
