using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClinicManagementML.Model;
using Microsoft.ML;

namespace ClinicManagement.API.Controllers
{
    [ApiController]
    [Route("/api/ML")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public IActionResult MakePrediction(ModelInput modelInput)
        {
            try
            {
                var predictionResult = ConsumeModel.Predict(modelInput);
                var result = predictionResult.Prediction;
                var score = predictionResult.Score;
                //some logic
                return Ok(predictionResult);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Test()
        {
            ModelOutput predictionResult;
            List<ModelInput> lst = Import<ModelInput>();
            foreach (var item in lst)
            {
                predictionResult = ConsumeModel.Predict(item);
                var result = predictionResult.Prediction;
                var score = predictionResult.Score;
            }
                //some logic
                return Ok();
           
        }
        public List<KeyValuePair<string, string>> Mappings;

        public List<T> Import<T>()
        {
            string file = @"C:\Users\utsha.DESKTOP-HM54OFA\Downloads\archive\Testing.csv";
            //Create MLContext
            MLContext mlContext = new MLContext();

            //Load Data
            IDataView data = mlContext.Data.LoadFromTextFile<ModelInput>(file, separatorChar: ',', hasHeader: true);
            //List<T> list = new List<T>();
            //List<string> lines = System.IO.File.ReadAllLines(file).ToList();
            //string headerLine = lines[0];
            //var headerInfo = headerLine.Split(',').ToList().Select((v, i) => new { ColName = v, ColIndex = i });

            //Type type = typeof(T);
            //var properties = type.GetProperties();

            //var dataLines = lines.Skip(1);
            //dataLines.ToList().ForEach(line => {
            //    var values = line.Split(',');
            //    T obj = (T)Activator.CreateInstance(type);

            //    //set values to obj properties from csv columns
            //    foreach (var prop in properties)
            //    {
            //        object[] attrs = prop.GetCustomAttributes(true);
            //        //find mapping for the prop
            //        //var mapping = Mappings.SingleOrDefault(m => m.Value == prop.Name);
            //        var colName = prop.Name;
            //        var colIndex = headerInfo.SingleOrDefault(s => s.ColName == colName).ColIndex;
            //        var value = values[colIndex];
            //        var propType = prop.PropertyType;
            //        prop.SetValue(obj, Convert.ChangeType(value, propType));
            //    }

            //    list.Add(obj);
            //});

            //return list;
            return null;
        }
    }
}
