using ENTITYAPP.DTO;
using System.ComponentModel.DataAnnotations;

namespace ENTITYAPP.Dto
{
    public class EmployeeDto
    {
        public string FullName { get; set; }
        public string Department { get; set; }


        public  EmpDetailsDto EmpDetailstDto { get; set; }

        public string SplitFulltoFirst(string FullName)
        {
            return FullName.Split(' ')[0];
        }

        public string SplitFulltoLast(string FullName)
        {
            return FullName.Split(' ')[1];
        }
    }
}
