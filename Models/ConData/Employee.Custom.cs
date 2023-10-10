using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesApp.Models.ConData
{
    // Partial class added by Benjamin Fadina to add FullName derived property to Employee Class
    // A derived property is a property that does not map to a database column
    //it is a calculated property
    //it is a read only property that will have only getter methods
    public partial class Employee
    {
        [NotMapped]
        public string FullName
        {
            get
            {
                return this.EmployeeID.ToString()+" "+this.FirstName+" "+this.LastName;
            }
        }
    }
}
