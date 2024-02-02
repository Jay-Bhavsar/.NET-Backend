using ENTITYAPP.DTO;
using ENTITYAPP.Repository.Data;
using ENTITYAPP.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ENTITYAPP.Repository
{
    public class EmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }
        //Create a method which will take Modal as input
        //Save Modal in DB

        //POST request
        public async Task CreateEmployee(Employee emp)
        {
            _context.Employees.Add(emp);
          
            await _context.SaveChangesAsync();
        }


        //POST request to create User

        



        public async Task CreateUser(Users user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        //GET request 
        public async Task<List<Employee>> GetEmployees()
        {
            return await _context.Employees.Include(x=> x.EmpDetails).ToListAsync();
            
        }
        // GET request for a Employee Details
        public async Task<List<EmpDetails>> GetEmployeeDetails()
        {
            return await _context.EmpDetails.ToListAsync();
        }

        //Delete request 

        public async Task DeleteEmployees(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();

        }
        // GET Employee by ID

        public async Task<Employee> GetEmployeeById(int id)
        {
           return await _context.Employees.FindAsync(id);
        }

        public async Task<EmpDetails> GetEmployeeDetailById(int id)
        {
            return await _context.EmpDetails.FindAsync(id);
        }
       

        //Update Employee 

        public async Task UpdateEmployee(Employee emp)
        {
            _context.Entry(emp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeDetails(EmpDetails empDet)
        {
            _context.Entry(empDet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        internal Task CreateUser(UserDto use)
        {
            throw new NotImplementedException();
        }
    }
}
