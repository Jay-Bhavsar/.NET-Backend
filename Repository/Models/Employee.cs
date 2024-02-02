using System.ComponentModel.DataAnnotations;

namespace ENTITYAPP.Repository.Models
{
    public class Employee
    {

        [Key]
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public EmpDetails EmpDetails { get; set; }

    

    }
}
