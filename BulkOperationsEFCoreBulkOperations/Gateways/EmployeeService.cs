using BulkOperationsEFCoreBulkOperations.Infrastructure;
using BulkOperationsEFCoreBulkOperations.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql.Bulk;
using Z.EntityFramework.Plus;

namespace BulkOperationsEFCoreBulkOperations.Gateways
{
    public class EmployeeService : IEmployeeService
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

            var uploader = new NpgsqlBulkUploader(_appDbContext);
            await uploader.InsertAsync(employees);
            // await _appDbContext.BulkInsertAsync(employees);
            _timeSpan = DateTime.Now - _start;
            return _timeSpan;
        }

        #endregion Add Bulk Data

        #region Update Bulk Data

        public async Task<TimeSpan> UpdateBulkDataAsync()
        {
            _start = DateTime.Now;
            var employees = _appDbContext.Employees.ToList();
            foreach (var employee in employees)
            {
                employee.Name = "Update Employee_" + employee.Id;
                employee.Designation = "Update Designation_" + employee.Id;
                employee.City = "Update City_" + employee.Id;
            }
            var uploader = new NpgsqlBulkUploader(_appDbContext);
            await uploader.UpdateAsync(employees);
            _timeSpan = DateTime.Now - _start;
            return _timeSpan;
        }

        #endregion Update Bulk Data

        #region Delete Bulk Data

        public async Task<TimeSpan> DeleteBulkDataAsync()
        {
            _start = DateTime.Now;
            /*var employees = _appDbContext.Employees.ToList();
            await _appDbContext.BulkDeleteAsync(employees);*/
            await _appDbContext.Employees.Where(x => x.Id < 1000000).DeleteAsync();

            // Delete all employees
            await _appDbContext.Employees.DeleteAsync();
            _timeSpan = DateTime.Now - _start;
            return _timeSpan;
        }

        #endregion Delete Bulk Data
    }
}