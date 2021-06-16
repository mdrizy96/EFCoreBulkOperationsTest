using BulkOperationsEFCoreBulkOperations.Infrastructure;
using BulkOperationsEFCoreBulkOperations.Infrastructure.Entities;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BulkOperationsEFCoreBulkOperations.Gateways
{
    public class EmployeeService: IEmployeeService
    {
        private readonly AppDbContext _appDbContext;
        private DateTime _start;
        private TimeSpan _timeSpan;
        //The "duration" variable contains Execution time when we doing the operations (Insert,Update,Delete)

        public EmployeeService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Add Bulk Data

        public async Task<TimeSpan> AddBulkDataAsync()
        {
            var employees = new List<Employee>();
            _start = DateTime.Now;
            for (var i = 0; i < 100000; i++)
            {
                employees.Add(new Employee()
                {
                    Name = "Employee_" + i,
                    Designation = "Designation_" + i,
                    City = "City_" + i
                });
            }
            await _appDbContext.Employees.AddRangeAsync(employees);
            await _appDbContext.SaveChangesAsync();
            _timeSpan = DateTime.Now - _start;
            return _timeSpan;
        }

        #endregion Add Bulk Data

        #region Update Bulk Data

        public async Task<TimeSpan> UpdateBulkDataAsync()
        {
            _start = DateTime.Now;
            var employees = await _appDbContext.Employees.ToListAsync();
            foreach (var employee in employees)
            {
                employee.Name = "Update Employee_" + employee.Id;
                employee.Designation = "Update Designation_" + employee.Id;
                employee.City = "Update City_" + employee.Id;
            }

            await _appDbContext.SaveChangesAsync();
            _timeSpan = DateTime.Now - _start;
            return _timeSpan;
        }

        #endregion Update Bulk Data

        #region Delete Bulk Data

        public async Task<TimeSpan> DeleteBulkDataAsync()
        {
            _start = DateTime.Now;
            var employees = await _appDbContext.Employees.ToListAsync();
            _appDbContext.Employees.RemoveRange(employees);
            await _appDbContext.SaveChangesAsync();
            _timeSpan = DateTime.Now - _start;
            return _timeSpan;
        }

        #endregion Delete Bulk Data
    }
}