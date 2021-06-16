using BulkOperationsEFCoreBulkOperations.Gateways;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BulkOperationsEFCoreBulkOperations.Controllers
{
    [Route("api/bulk-operations")]
    [ApiController]
    public class BulkOperationsController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public BulkOperationsController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBulkData()
        {
            var response = await _employeeService.AddBulkDataAsync();
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBulkData()
        {
            var response = await _employeeService.UpdateBulkDataAsync();
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBulkData()
        {
            var response = await _employeeService.DeleteBulkDataAsync();
            return Ok(response);
        }
    }
}