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

        [HttpPost(nameof(AddBulkData))]
        public async Task<IActionResult> AddBulkData()
        {
            var response = await _employeeService.AddBulkDataAsync();
            return Ok(response);
        }

        [HttpPut(nameof(UpdateBulkData))]
        public async Task<IActionResult> UpdateBulkData()
        {
            var response = await _employeeService.UpdateBulkDataAsync();
            return Ok(response);
        }

        [HttpDelete(nameof(DeleteBulkData))]
        public async Task<IActionResult> DeleteBulkData()
        {
            var response = await _employeeService.DeleteBulkDataAsync();
            return Ok(response);
        }
    }
}