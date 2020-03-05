using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RokoAwards.EFDataAccessLibrary.DataAccess;
using RokoAwards.EFDataAccessLibrary.Models;

namespace RokoAwards.BusinessLogicLibrary
{
    public class CityLogic
    {
        private readonly ApplicationContext _db;

        public CityLogic(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<City> AddCityAsync(string cityName)
        {
            City city = new City { CityName = cityName };
            _db.Cities.Add(city);
            await _db.SaveChangesAsync();
            return await _db.Cities.FirstOrDefaultAsync(c => c.CityName == cityName);
        }

        public async Task<City> GetCityByNameAsync(string cityName)
        {
            return await _db.Cities.FirstOrDefaultAsync(c => c.CityName == cityName);
        }
    }
}
