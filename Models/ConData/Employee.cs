using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesApp.Models.ConData
{
    [Table("Employees", Schema = "dbo")]
    public partial class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EmployeeID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string FirstName { get; set; }

        [ConcurrencyCheck]
        public string MiddleName { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string LastName { get; set; }

        [ConcurrencyCheck]
        public long? ManagerID { get; set; }

        [ConcurrencyCheck]
        public string EmployeePhoto { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int GenderID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int DepartmentID { get; set; }

        public Department Department { get; set; }

        public Gender Gender { get; set; }

        public Employee Employee1 { get; set; }

        public ICollection<Employee> Employees1 { get; set; }

    }
}