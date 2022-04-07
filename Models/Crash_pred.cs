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
        public float commercial_motor_veh_involved { get; set; }
        public float teenage_driver_involved { get; set; }
        public float older_driver_involved { get; set; }
        public float night_dark_condition { get; set; }
        public float single_vehicle { get; set; }
        public float distracted_driving { get; set; }
        public float roadway_departure { get; set; }
        public float route_89 { get; set; }
        public float route_Other { get; set; }
        public float main_road_name_Other { get; set; }
        public float city_Other { get; set; }
        public float city_Salt_Lake_City { get; set; }
        public float city_West_Valley_City { get; set; }
        public float county_name_Other { get; set; }
        public float county_name_Salt_Lake { get; set; }
        public float county_name_UTAH { get; set; }
        public float county_name_Weber { get; set; }
        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                intersection_related, commercial_motor_veh_involved, teenage_driver_involved, older_driver_involved, night_dark_condition,
                single_vehicle, distracted_driving, roadway_departure, route_89, route_Other, main_road_name_Other, city_Other,
                city_Salt_Lake_City, city_West_Valley_City, county_name_Other, county_name_Salt_Lake, county_name_UTAH, county_name_Weber
            };
            int[] dimensions = new int[] { 1, 18 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}
