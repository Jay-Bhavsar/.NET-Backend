using ENTITYAPP.Dto;
using Microsoft.AspNetCore.Mvc;
using ENTITYAPP.Service;
using Microsoft.AspNetCore.Authorization;
using Contract;
using LazyCache;
using Microsoft.Extensions.Caching.Distributed;
using ENTITYAPP.Repository.Models;

namespace ENTITYAPP.Controllers
{
    //[Authorize]
    [ApiController]


    public class EmployeeController : Controller
    {
        private const string employeeListCacheKey = "employeeList";
        private readonly EmployeeService _empService;
        private readonly IloggerManager _logger;
        private readonly IDistributedCache _cache;
        private static readonly SemaphoreSlim semaphore = new(1, 1);
        
        public EmployeeController(EmployeeService empService, IloggerManager logger,IDistributedCache cache)
        {
            _empService = empService;
            _logger = logger;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        }
        [HttpPost]
        [TypeFilter(typeof(MyActionFilter))]
        [Route("api/AddEmployees")]
        public async Task AddEmployee(EmployeeDto employee)
        {
           await _empService.CreateEmployee(employee);
        }

       
       
        [HttpGet]
        [Route("api/GetEmployees")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInfo("Trying to fetch from cache");

            if (_cache.TryGetValue(employeeListCacheKey, out List<EmployeeDto>? employees))
            {
                _logger.LogInfo("employee found in cache");
            }
            else
            {
                try
                {
                    await semaphore.WaitAsync();

                    if (_cache.TryGetValue(employeeListCacheKey, out employees))
                    {
                        _logger.LogInfo("employee found in cache");
                    }
                    else
                    {
                        _logger.LogInfo("employee not found in cache");

                        employees = await _empService.GetEmployees();

                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));

                        await _cache.SetAsync(employeeListCacheKey, employees, cacheEntryOptions);
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }

            if (employees != null)
            {
                return Ok(employees);
            }
            else
            {
                _logger.LogError("Employee list is null.");
                return NotFound(); // or another appropriate status code
            }
        }

        [HttpDelete]
        [Route("api/DeleteEmployee/{id}")]

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _empService.DeleteEmployee(id);
            return Ok("employee deleted Successfully");
            
        }
        [HttpGet]
        [Route("api/GetEmployeeById/{id}")]

        public async Task <IActionResult> GetEmployeeById(int id)
        {
            var employee = await _empService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }
        [HttpPut]
        [Route("api/UpdateEmployee/{id}")]

        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto employee)
        {
            await _empService.UpdateEmployee(id, employee);
            return Ok("employee Updated");
        }
    }
}