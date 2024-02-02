using AutoMapper;
using ENTITYAPP.DTO;
using ENTITYAPP.Repository;
using ENTITYAPP.Repository.Models;

namespace ENTITYAPP.Service
{
    public class UserService
    {
        private readonly EmployeeRepository _empRepo;


        public UserService(EmployeeRepository empRepo)
        {
            _empRepo = empRepo;

        }

        public async Task CreateUser(UserDto dto)
        {
            var use = new Users();

            use.email = dto.email;

            use.password = dto.password;

            await _empRepo.CreateUser(use);
        }

    }
}
