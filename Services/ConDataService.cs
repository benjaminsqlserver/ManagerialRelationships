using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using EmployeesApp.Data;

namespace EmployeesApp
{
    public partial class ConDataService
    {
        ConDataContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly ConDataContext context;
        private readonly NavigationManager navigationManager;

        public ConDataService(ConDataContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportDepartmentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/departments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/departments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDepartmentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/departments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/departments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDepartmentsRead(ref IQueryable<EmployeesApp.Models.ConData.Department> items);

        public async Task<IQueryable<EmployeesApp.Models.ConData.Department>> GetDepartments(Query query = null)
        {
            var items = Context.Departments.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDepartmentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDepartmentGet(EmployeesApp.Models.ConData.Department item);
        partial void OnGetDepartmentByDepartmentId(ref IQueryable<EmployeesApp.Models.ConData.Department> items);


        public async Task<EmployeesApp.Models.ConData.Department> GetDepartmentByDepartmentId(int departmentid)
        {
            var items = Context.Departments
                              .AsNoTracking()
                              .Where(i => i.DepartmentID == departmentid);

 
            OnGetDepartmentByDepartmentId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDepartmentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDepartmentCreated(EmployeesApp.Models.ConData.Department item);
        partial void OnAfterDepartmentCreated(EmployeesApp.Models.ConData.Department item);

        public async Task<EmployeesApp.Models.ConData.Department> CreateDepartment(EmployeesApp.Models.ConData.Department department)
        {
            OnDepartmentCreated(department);

            var existingItem = Context.Departments
                              .Where(i => i.DepartmentID == department.DepartmentID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Departments.Add(department);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(department).State = EntityState.Detached;
                throw;
            }

            OnAfterDepartmentCreated(department);

            return department;
        }

        public async Task<EmployeesApp.Models.ConData.Department> CancelDepartmentChanges(EmployeesApp.Models.ConData.Department item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDepartmentUpdated(EmployeesApp.Models.ConData.Department item);
        partial void OnAfterDepartmentUpdated(EmployeesApp.Models.ConData.Department item);

        public async Task<EmployeesApp.Models.ConData.Department> UpdateDepartment(int departmentid, EmployeesApp.Models.ConData.Department department)
        {
            OnDepartmentUpdated(department);

            var itemToUpdate = Context.Departments
                              .Where(i => i.DepartmentID == department.DepartmentID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(department);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDepartmentUpdated(department);

            return department;
        }

        partial void OnDepartmentDeleted(EmployeesApp.Models.ConData.Department item);
        partial void OnAfterDepartmentDeleted(EmployeesApp.Models.ConData.Department item);

        public async Task<EmployeesApp.Models.ConData.Department> DeleteDepartment(int departmentid)
        {
            var itemToDelete = Context.Departments
                              .Where(i => i.DepartmentID == departmentid)
                              .Include(i => i.Employees)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDepartmentDeleted(itemToDelete);


            Context.Departments.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDepartmentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEmployeesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/employees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/employees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmployeesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/employees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/employees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmployeesRead(ref IQueryable<EmployeesApp.Models.ConData.Employee> items);

        public async Task<IQueryable<EmployeesApp.Models.ConData.Employee>> GetEmployees(Query query = null)
        {
            var items = Context.Employees.AsQueryable();

            items = items.Include(i => i.Department);
            items = items.Include(i => i.Gender);
            items = items.Include(i => i.Employee1);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEmployeesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmployeeGet(EmployeesApp.Models.ConData.Employee item);
        partial void OnGetEmployeeByEmployeeId(ref IQueryable<EmployeesApp.Models.ConData.Employee> items);


        public async Task<EmployeesApp.Models.ConData.Employee> GetEmployeeByEmployeeId(long employeeid)
        {
            var items = Context.Employees
                              .AsNoTracking()
                              .Where(i => i.EmployeeID == employeeid);

            items = items.Include(i => i.Department);
            items = items.Include(i => i.Gender);
            items = items.Include(i => i.Employee1);
 
            OnGetEmployeeByEmployeeId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEmployeeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEmployeeCreated(EmployeesApp.Models.ConData.Employee item);
        partial void OnAfterEmployeeCreated(EmployeesApp.Models.ConData.Employee item);

        public async Task<EmployeesApp.Models.ConData.Employee> CreateEmployee(EmployeesApp.Models.ConData.Employee employee)
        {
            OnEmployeeCreated(employee);

            var existingItem = Context.Employees
                              .Where(i => i.EmployeeID == employee.EmployeeID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Employees.Add(employee);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(employee).State = EntityState.Detached;
                throw;
            }

            OnAfterEmployeeCreated(employee);

            return employee;
        }

        public async Task<EmployeesApp.Models.ConData.Employee> CancelEmployeeChanges(EmployeesApp.Models.ConData.Employee item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEmployeeUpdated(EmployeesApp.Models.ConData.Employee item);
        partial void OnAfterEmployeeUpdated(EmployeesApp.Models.ConData.Employee item);

        public async Task<EmployeesApp.Models.ConData.Employee> UpdateEmployee(long employeeid, EmployeesApp.Models.ConData.Employee employee)
        {
            OnEmployeeUpdated(employee);

            var itemToUpdate = Context.Employees
                              .Where(i => i.EmployeeID == employee.EmployeeID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(employee);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEmployeeUpdated(employee);

            return employee;
        }

        partial void OnEmployeeDeleted(EmployeesApp.Models.ConData.Employee item);
        partial void OnAfterEmployeeDeleted(EmployeesApp.Models.ConData.Employee item);

        public async Task<EmployeesApp.Models.ConData.Employee> DeleteEmployee(long employeeid)
        {
            var itemToDelete = Context.Employees
                              .Where(i => i.EmployeeID == employeeid)
                              .Include(i => i.Employees1)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEmployeeDeleted(itemToDelete);


            Context.Employees.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEmployeeDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportGendersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/genders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/genders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportGendersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/genders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/genders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGendersRead(ref IQueryable<EmployeesApp.Models.ConData.Gender> items);

        public async Task<IQueryable<EmployeesApp.Models.ConData.Gender>> GetGenders(Query query = null)
        {
            var items = Context.Genders.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnGendersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGenderGet(EmployeesApp.Models.ConData.Gender item);
        partial void OnGetGenderByGenderId(ref IQueryable<EmployeesApp.Models.ConData.Gender> items);


        public async Task<EmployeesApp.Models.ConData.Gender> GetGenderByGenderId(int genderid)
        {
            var items = Context.Genders
                              .AsNoTracking()
                              .Where(i => i.GenderID == genderid);

 
            OnGetGenderByGenderId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnGenderGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnGenderCreated(EmployeesApp.Models.ConData.Gender item);
        partial void OnAfterGenderCreated(EmployeesApp.Models.ConData.Gender item);

        public async Task<EmployeesApp.Models.ConData.Gender> CreateGender(EmployeesApp.Models.ConData.Gender gender)
        {
            OnGenderCreated(gender);

            var existingItem = Context.Genders
                              .Where(i => i.GenderID == gender.GenderID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Genders.Add(gender);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(gender).State = EntityState.Detached;
                throw;
            }

            OnAfterGenderCreated(gender);

            return gender;
        }

        public async Task<EmployeesApp.Models.ConData.Gender> CancelGenderChanges(EmployeesApp.Models.ConData.Gender item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnGenderUpdated(EmployeesApp.Models.ConData.Gender item);
        partial void OnAfterGenderUpdated(EmployeesApp.Models.ConData.Gender item);

        public async Task<EmployeesApp.Models.ConData.Gender> UpdateGender(int genderid, EmployeesApp.Models.ConData.Gender gender)
        {
            OnGenderUpdated(gender);

            var itemToUpdate = Context.Genders
                              .Where(i => i.GenderID == gender.GenderID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(gender);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterGenderUpdated(gender);

            return gender;
        }

        partial void OnGenderDeleted(EmployeesApp.Models.ConData.Gender item);
        partial void OnAfterGenderDeleted(EmployeesApp.Models.ConData.Gender item);

        public async Task<EmployeesApp.Models.ConData.Gender> DeleteGender(int genderid)
        {
            var itemToDelete = Context.Genders
                              .Where(i => i.GenderID == genderid)
                              .Include(i => i.Employees)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnGenderDeleted(itemToDelete);


            Context.Genders.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterGenderDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}