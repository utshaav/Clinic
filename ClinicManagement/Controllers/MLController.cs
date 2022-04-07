using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using ClinicManagementML.Model;
using Newtonsoft.Json;
 
namespace ClinicManagement.Controllers
{
    public class MLController : Controller
    {

        public Dictionary<string, List<string>> DiseaseDoctorAssocoation { get; set; }
        public MLController()
        {
            DiseaseDoctorAssocoation = new Dictionary<string, List<string>>{
                { "General Physician", new List<string>{ "Allergy", "Drug Reaction", "AIDS", "Malaria", "Chicken pox", "Dengue", "Typhoid", "Common Cold", "Pneumonia" } },
                { "Gastroenterologist", new List<string>{"GERD" , "Chronic cholestasis", "Peptic ulcer diseae", "Gastroenteritis", "Jaundice"}},
                { "Endocrinologist", new List<string>{ "Diabetes","Hypothyroidism", "Hyperthyroidism", "Hypoglycemia"  }},
                { "Pulmonologist", new List<string>{"Bronchial Asthma", "Tuberculosis" }},
                { "Cardiologist", new List<string>{"Hypertension", "Cardiologist",  }},
                { "Neurologist", new List<string>{"Migraine", "Cervical spondylosis", "Paralysis (brain hemorrhage)","(vertigo) Paroymsal  Positional Vertigo" }},
                { "Hepatologist", new List<string>{ "hepatitis A", "Hepatitis B", "Hepatitis C", "Hepatitis D", "Hepatitis E", "Alcoholic hepatitis", }},
                { "Proctologist", new List<string>{"Dimorphic hemmorhoids(piles)", }},
                { "Rheumatologist", new List<string>{ "Osteoarthristis", "Arthritis"}},
                { "Urologists", new List<string>{ "Urinary tract infection"}},
                { "Dermatologist", new List<string>{ "Fungal infection", "Varicose veins", "Acne", "Psoriasis", "Impetigo" }}

            };
        }
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
            int max = i + 40;
            lstStr = lstStr.Where(x => x.Value >= i && x.Value < max).ToDictionary(t => t.Key, t => t.Value);
            return PartialView(lstStr);
        }

        public ActionResult Predict()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> PredictAsync(String[] symptoms )
        {
            ModelInput modelInput = new ModelInput();
            ModelOutput modelOutput;
            foreach (var prop in modelInput.GetType().GetProperties())
            {
                if (symptoms.Contains(prop.Name))
                {
                    prop.SetValue(modelInput, 1F);
                }
            }

            //var predictionResult = ConsumeModel.Predict(modelInput);

            HttpClient client = new HttpClient();
            var response = await client.PostAsJsonAsync("https://localhost:44350/api/ml", modelInput);
            string specialization = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                var readAsStringAsync = await response.Content.ReadAsStringAsync();
                modelOutput = JsonConvert.DeserializeObject<ModelOutput>(readAsStringAsync);
                foreach (var item in DiseaseDoctorAssocoation)
                {
                    bool flag = false;
                    foreach (var value in item.Value)
                    {
                        if (value.ToLower() == modelOutput.Prediction.Trim().ToLower())
                        {
                            specialization = item.Key;
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }

            }

            return RedirectToActionPermanent("SearchDoctor", "Doctors", new { specialization = specialization });
        }
    }
}