using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClinicManagementML.Model;


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
    }
}
