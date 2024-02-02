using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ENTITYAPP.Dto;
using System.Linq;
using ENTITYAPP.Repository.Data;
using ENTITYAPP.Repository.Models;
using ENTITYAPP.Repository;
using ENTITYAPP.DTO;

namespace ENTITYAPP.Service
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _empRepo;
        private readonly IMapper _mapper;
   

        public EmployeeService(EmployeeRepository empRepo,IMapper mapper)
        {
            _empRepo = empRepo;
            _mapper = mapper;
            
        }
        //Add a Method to get data from controller as DTO
        //Change DTO to modal and retun modal object
        public async Task CreateEmployee(EmployeeDto dto)
        {
            var emp = _mapper.Map<Employee>(dto);

            //emp.FullName = dto.FullName;
            //emp.FirstName = dto.FullName.Split(' ')[0];
            //emp.LastName = dto.FullName.Split(' ')[1];
            //emp.Department = dto.Department;
            //emp.EmpDetails = new EmpDetails();
            //emp.EmpDetails.DOJ = dto.EmpDetailstDto.DOJ;
            //emp.EmpDetails.Salary = dto.EmpDetailstDto.Salary;
            //emp.EmpDetails.Address = dto.EmpDetailstDto.Address;




            await _empRepo.CreateEmployee(emp);

        }



        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var employees = await _empRepo.GetEmployees();

            // var employeeDetails = await _empRepo.GetEmployeeDetails();

            var result = employees.Select(emp => new EmployeeDto
            {
                FullName=emp.FullName,
                Department=emp.Department,

                EmpDetailstDto = new EmpDetailsDto
                {
                    DOJ = emp.EmpDetails.DOJ,
                    Salary = emp.EmpDetails.Salary,
                    Address = emp.EmpDetails.Address
                }
            }).ToList();

            return result;

        }


        public async Task DeleteEmployee(int id)
        {
           await _empRepo.DeleteEmployees(id);
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var emp = await _empRepo.GetEmployeeById(id);

            var empDet = await _empRepo.GetEmployeeDetailById(id);
            if (emp == null || empDet == null)
            {
             
                return null;
            }

            var result = new EmployeeDto
            {
                FullName = emp.FullName,
                Department = emp.Department,
                EmpDetailstDto = new EmpDetailsDto
                {
                    DOJ = empDet.DOJ,
                    Salary = empDet.Salary,
                    Address = empDet.Address
                }
            };

            return result;

        }


        public async Task UpdateEmployee(int id, EmployeeDto dto)
        {
            var emp = await _empRepo.GetEmployeeById(id);
            var empdet = await _empRepo.GetEmployeeDetailById(id);            

            if (emp == null)
            {
                return;
            }

            emp.FullName = dto.FullName;
            emp.Department = dto.Department;
            emp.FirstName = dto.FullName.Split(' ')[0];
            emp.LastName = dto.FullName.Split(' ')[1];
            emp.EmpDetails.Salary = dto.EmpDetailstDto.Salary;
            emp.EmpDetails.Address = dto.EmpDetailstDto.Address;
            emp.EmpDetails.DOJ = dto.EmpDetailstDto.DOJ;


            await _empRepo.UpdateEmployee(emp);
            await _empRepo.UpdateEmployeeDetails(empdet);
        }

        public static implicit operator EmployeeService(UserService v)
        {
            throw new NotImplementedException();
        }
    }
}
