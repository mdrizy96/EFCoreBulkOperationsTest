using BulkOperationsEFCoreBulkOperations.Infrastructure;
using BulkOperationsEFCoreBulkOperations.Infrastructure.Entities;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkOperationsEFCoreBulkOperations.Gateways
{
    public class EmployeeService: IEmployeeService
    {
        private readonly AppDbContext _appDbContext;
        private DateTime Start;
        private TimeSpan TimeSpan;
        //The "duration" variable contains Execution time when we doing the operations (Insert,Update,Delete)

        public EmployeeService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Add Bulk Data

        public async Task<TimeSpan> AddBulkDataAsync()
        {
            List<Employee> employees = new List<Employee>();
            Start = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                employees.Add(new Employee()
                {
                    Name = "Employee_" + i,
                    Designation = "Designation_" + i,
                    City = "City_" + i
                });
            }
            await _appDbContext.BulkInsertAsync(employees);
            TimeSpan = DateTime.Now - Start;
            return TimeSpan;
        }

        #endregion Add Bulk Data

        #region Update Bulk Data

        public async Task<TimeSpan> UpdateBulkDataAsync()
        {
            List<Employee> employees = new List<Employee>();
            Start = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                employees.Add(new Employee()
                {
                    Id = (i + 1),
                    Name = "Update Employee_" + i,
                    Designation = "Update Designation_" + i,
                    City = "Update City_" + i
                });
            }
            await _appDbContext.BulkUpdateAsync(employees);
            TimeSpan = DateTime.Now - Start;
            return TimeSpan;
        }

        #endregion Update Bulk Data

        #region Delete Bulk Data

        public async Task<TimeSpan> DeleteBulkDataAsync()
        {
            List<Employee> employees = new List<Employee>();
            Start = DateTime.Now;
            employees = _appDbContext.Employees.ToList();
            await _appDbContext.BulkDeleteAsync(employees);
            TimeSpan = DateTime.Now - Start;
            return TimeSpan;
        }

        #endregion Delete Bulk Data
    }
}