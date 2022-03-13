using ClinicManagement.Core.Models;
using ClinicManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicManagement.Persistence.Repositories
{
    public class DropdownRepository : IDropdownRepository
    {
        private readonly ApplicationDbContext _context;

        public DropdownRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.ToList();
        }

        public IEnumerable<Days> GetDays()
        {
            return _context.Days.ToList();
        }

        public IEnumerable<Specialization> GetSpecializations()
        {
            return _context.Specializations.ToList();
        }
    }
}