using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesApp.Models.ConData
{
    [Table("Departments", Schema = "dbo")]
    public partial class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string DepartmentName { get; set; }

        public ICollection<Employee> Employees { get; set; }

    }
}