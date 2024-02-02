using AutoMapper;
using ENTITYAPP.Dto;
using ENTITYAPP.Repository.Models;
namespace AutoMapperDemo
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
           
            var config = new MapperConfiguration(cfg =>
            {                
                cfg.CreateMap<Employee, EmployeeDto>();               
            });
            
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}