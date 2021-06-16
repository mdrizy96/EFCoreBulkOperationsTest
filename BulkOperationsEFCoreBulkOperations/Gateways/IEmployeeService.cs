using System;
using System.Threading.Tasks;

namespace BulkOperationsEFCoreBulkOperations.Gateways
{
    public interface IEmployeeService
    {
        public Task<TimeSpan> AddBulkDataAsync();

        public Task<TimeSpan> UpdateBulkDataAsync();

        public Task<TimeSpan> DeleteBulkDataAsync();
    }
}