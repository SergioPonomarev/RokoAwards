using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RokoAwards.EFDataAccessLibrary.DataAccess;
using RokoAwards.EFDataAccessLibrary.Models;

namespace RokoAwards.BusinessLogicLibrary
{
    public class DepartmentLogic
    {
        private readonly ApplicationContext _db;

        public DepartmentLogic(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _db.Departments
                .ToListAsync();
        }

        public async Task<Department> GetDepartmentByNameAsync(string departmentName)
        {
            return await _db.Departments.FirstOrDefaultAsync(d => d.DepartmentName == departmentName);
        }
    }
}
