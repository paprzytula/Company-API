using Company_UI.Contracts;
using Company_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Company_UI.Pages.Service
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly IHttpClientFactory _client;
        public EmployeeRepository(IHttpClientFactory client) : base(client)
        {
            _client = client;
        }
    }
}
