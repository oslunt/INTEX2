using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Threading.Tasks;
using INTEX2.Models;

namespace INTEX2.Controllers
{
    public class InferenceController : Controller

    {

        private InferenceSession _session;
        public InferenceController(InferenceSession session)
        {
            _session = session;
        }




        [HttpGet]
        public IActionResult EnterData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Score(Crash_pred data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });

            Tensor<string> score = result.First().AsTensor<string>();
            var prediction = new Prediction { PredictedValue = score.Last() };
            result.Dispose();
            return View("Score", prediction);
        }

    }
}
