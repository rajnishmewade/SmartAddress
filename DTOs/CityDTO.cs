using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAddress.DTOs
{
    public class CityDTO
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int DistID { get; set; }
        public string DistrictName { get; set; }
    }
}