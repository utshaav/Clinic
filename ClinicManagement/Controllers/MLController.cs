using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagementML.Model;
 
namespace ClinicManagement.Controllers
{
    public class MLController : Controller
    {
        // GET: ML
        public ActionResult GetListOfDiseases()
        {
            ModelInput modelInput = new ModelInput;


            foreach (var prop in modelInput.GetType().GetProperties())
            {
                
            }
            return View();
        }
    }
}