using System.Collections.Generic;
using ClinicManagement.Core.Models;

namespace ClinicManagement.Core.Repositories
{
    public interface ICityRepository
    {
        IEnumerable<City> GetCities();
    }
    public interface IDropdownRepository
    {
        IEnumerable<City> GetCities();
        IEnumerable<Specialization> GetSpecializations();
        IEnumerable<Days> GetDays();

    }
}