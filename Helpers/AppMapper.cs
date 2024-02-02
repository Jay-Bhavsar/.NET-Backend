using AutoMapper;
using ENTITYAPP.Dto;
using ENTITYAPP.DTO;
using ENTITYAPP.Repository.Models;

namespace ENTITYAPP.Helpers
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {

            //CreateMap<EmployeeDto, Employee>()
            //   .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FullName.Split(' ')[0]))
            //   .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.FullName.Split(' ')[1]));

            CreateMap<EmpDetailsDto, EmpDetails>();
            CreateMap<EmployeeDto, Employee>()
                          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.SplitFulltoFirst(src.FullName)))
                          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.SplitFulltoLast(src.FullName)))
                          .ForMember(dest => dest.EmpDetails, opt => opt.MapFrom(src => src.EmpDetailstDto));
                           //.AfterMap((src, dest) =>
                           // {
                           //     dest.EmpDetails = new EmpDetails
                           //     {
                           //         Salary = src.EmpDetailstDto.Salary,
                           //         Address = src.EmpDetailstDto.Address,
                           //         DOJ = src.EmpDetailstDto.DOJ
                           //     };
                           // });

        }
    }
}
