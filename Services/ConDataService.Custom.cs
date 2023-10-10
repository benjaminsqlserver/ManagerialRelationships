using Radzen;

namespace EmployeesApp
{
    public partial class ConDataService
    {
        public async Task<IQueryable<EmployeesApp.Models.ConData.Employee>> GetEmployeesInDescendingOrder(Query query = null)
        {
            IQueryable<Models.ConData.Employee> employees = await GetEmployees(query);//call get employees employee method and assign it to employees

            return await Task.FromResult(employees.OrderByDescending(p => p.EmployeeID));
        }
    }
}
