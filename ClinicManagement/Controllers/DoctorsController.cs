using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClinicManagement.Core;
using ClinicManagement.Core.Models;
using ClinicManagement.Core.ViewModel;
using Microsoft.AspNet.Identity;

namespace ClinicManagement.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var doctors = _unitOfWork.Doctors.GetDectors();
            return View(doctors);
        }

        //Details for admin
        public ActionResult Details(int id)
        {
            var viewModel = new DoctorDetailViewModel
            {
                Doctor = _unitOfWork.Doctors.GetDoctor(id),
                UpcomingAppointments = _unitOfWork.Appointments.GetTodaysAppointments(id),
                Appointments = _unitOfWork.Appointments.GetAppointmentByDoctor(id),
            };
            return View(viewModel);
        }

        public ActionResult SearchDoctor(string specialization)
        {
            SearchViewModel searchViewModel = new SearchViewModel();
            searchViewModel.RecommendedDoctors = _unitOfWork.Doctors.GetDoctorBySpecialization(specialization);
            searchViewModel.AllDoctors = _unitOfWork.Doctors.GetAvailableDoctors().ToList();
            searchViewModel.RequiredSpecialization = specialization;
            //var viewModel = new DoctorDetailViewModel
            //{
            //    ,
            //    UpcomingAppointments = _unitOfWork.Appointments.GetTodaysAppointments(id),
            //    Appointments = _unitOfWork.Appointments.GetAppointmentByDoctor(id),
            //};
            return View(searchViewModel);
        }

        public ActionResult DoctorProfile()
        {
            var userId = User.Identity.GetUserId();
            var viewModel = new DoctorDetailViewModel
            {
                Doctor = _unitOfWork.Doctors.GetProfile(userId),
                Appointments = _unitOfWork.Appointments.GetUpcommingAppointments(userId),
            };
            return View(viewModel);
        }
        public ActionResult Edit(int id)
        {
            var doctor = _unitOfWork.Doctors.GetDoctor(id);
            if (doctor == null) return HttpNotFound();
            List<string> availableDays = doctor.AvailableDays != null ? doctor.AvailableDays.Split(',').ToList() : new List<string>();
            var viewModel = new DoctorFormViewModel()
            {

                Id = doctor.Id,
                Name = doctor.Name,
                Phone = doctor.Phone,
                Address = doctor.Address,
                IsAvailable = doctor.IsAvailable,
                Specialization = doctor.SpecializationId,
                Specializations = _unitOfWork.Dropdowns.GetSpecializations(),
                Days = _unitOfWork.Dropdowns.GetDays(),
                AvailableDays = availableDays,
                TimeFrom = doctor.TimeFrom,
                TimeTo = doctor.TimeTo
                

            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(DoctorFormViewModel viewModel)
        {   
            if (!ModelState.IsValid)
            {
                viewModel.Specializations = _unitOfWork.Dropdowns.GetSpecializations();
                return View(viewModel);
            }

            var doctorInDb = _unitOfWork.Doctors.GetDoctor(viewModel.Id);
            doctorInDb.Id = viewModel.Id;
            doctorInDb.Name = viewModel.Name;
            doctorInDb.Phone = viewModel.Phone;
            doctorInDb.Address = viewModel.Address;
            doctorInDb.IsAvailable = viewModel.IsAvailable;
            doctorInDb.SpecializationId = viewModel.Specialization;
            doctorInDb.AvailableDays = string.Join(",", viewModel.AvailableDays);
            doctorInDb.TimeFrom = viewModel.TimeFrom;
            doctorInDb.TimeTo = viewModel.TimeTo;

            _unitOfWork.Complete();

            return RedirectToAction("Details", new { id = viewModel.Id });
        }


    }

}

