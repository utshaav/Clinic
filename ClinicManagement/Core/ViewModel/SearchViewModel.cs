using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicManagement.Core.Models;

namespace ClinicManagement.Core.ViewModel
{
    public class SearchViewModel
    {
        public List<Doctor> RecommendedDoctors { get; set; }
        public List<Doctor> AllDoctors { get; set; }
        public List<Doctor> FeaturedDoctors { get; set; }
        public string RequiredSpecialization { get; set; }
    }
}