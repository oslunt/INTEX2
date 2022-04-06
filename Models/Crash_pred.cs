using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace INTEX2.Models
{
    public class Crash_pred
    {
        public float intersection_related { get; set; }
        public float teenage_driver_involved { get; set; }
        public float older_driver_involved { get; set; }
        public float night_dark_condition { get; set; }
        public float single_vehicle { get; set; }
        public float roadway_departure { get; set; }
        public float main_road_name_Other { get; set; }
        public float city_Outside_City_Limits { get; set; }
        public float city_Other { get; set; }
        public float county_name_Other { get; set; }
        public float county_name_Salt_Lake { get; set; }
        public float county_name_UTAH { get; set; }




        public Tensor<float> asTensor()
        {
            float[] data = new float[]
            {
                intersection_related, teenage_driver_involved,
       older_driver_involved, night_dark_condition, single_vehicle,
       roadway_departure, main_road_name_Other, city_Outside_City_Limits,
       city_Other, county_name_Other, county_name_Salt_Lake,
       county_name_UTAH
            };

            //num of features
            int[] dimensions = new int[] { 1, 12 };
            return new DenseTensor<float>(data, dimensions);
        }


    }
}
