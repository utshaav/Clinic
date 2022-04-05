using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClinicManagementML.Model;
 
namespace ClinicManagement.Controllers
{
    public class MLController : Controller
    {
        // GET: ML
        public ActionResult ListOfDiseases(int? index)
        {
            int i = index != null ? int.Parse(index.ToString()) : 0;
            ModelInput modelInput = new ModelInput();
            List<SelectListItem> selectLists = new List<SelectListItem>();
            Dictionary<String, int> lstStr = new Dictionary<String, int>();
            int j = 0;
            foreach (var prop in modelInput.GetType().GetProperties())
            {
                lstStr.Add(prop.Name, j);
                j++;
            }
            int max = i + 20;
            lstStr = lstStr.Where(x => x.Value >= i && x.Value < max).ToDictionary(t => t.Key, t => t.Value);
            return PartialView(lstStr);
        }

        public ActionResult Predict()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Predict(String[] symptoms )
        {
            ModelInput modelInput = new ModelInput();
            foreach (var prop in modelInput.GetType().GetProperties())
            {
                if (symptoms.Contains(prop.Name))
                {
                    prop.SetValue(modelInput, 1);
                }
            }
            var predictionResult = ConsumeModel.Predict(modelInput);
            return View();
        }
    }
}