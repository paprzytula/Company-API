using AutoMapper;
using Company_API.Data;
using Company_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Department, DepartmentCreateDTO>().ReverseMap();
            CreateMap<Department, DepartmentUpdateDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();
            CreateMap<Skill, SkillDTO>().ReverseMap();
            CreateMap<Skill, SkillCreateDTO>().ReverseMap();
            CreateMap<Skill, SkillUpdateDTO>().ReverseMap();

        }
    }
}
