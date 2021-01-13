using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_UI.Shared.Static
{
    public static class Endpoints
    {
        public static readonly string BaseUrl = "https://localhost:44386";
        public static readonly string CategoriesEndpoint = $"{BaseUrl}/api/categories/";
        public static readonly string DepartmentsEndpoint = $"{BaseUrl}/api/departments/";
        public static readonly string EmployeesEndpoint = $"{BaseUrl}/api/employees/";
        public static readonly string ProjectsEndpoint = $"{BaseUrl}/api/projects/";
        public static readonly string SkillsEndpoint = $"{BaseUrl}/api/skills/";
        public static readonly string RegisterEndpoint = $"{BaseUrl}/api/users/register";
        public static readonly string LoginEndpoint = $"{BaseUrl}/api/users/login";

    }
}
