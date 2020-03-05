using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RokoAwards.EFDataAccessLibrary.DataAccess;
using RokoAwards.EFDataAccessLibrary.Models;

namespace RokoAwards.BusinessLogicLibrary
{
    public class RoleLogic
    {
        private const string DefaultRole = "User";
        private readonly ApplicationContext _db;

        public RoleLogic(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<Role> GetDefaultRoleAsync()
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.RoleName == DefaultRole);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _db.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleAsync(string roleName)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }
    }
}
