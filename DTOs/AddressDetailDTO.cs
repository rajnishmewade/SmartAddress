using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAddress.DTOs
{
    public class AddressDetailDTO
    {
        // public int AddressID { get; set; }
        // public int DistID { get; set; }
        // public int CityID { get; set; }
        public string DistrictName { get; set; }
        public string CityName { get; set; } 
        public string StreetName { get; set; }
        public string BuildingName { get; set; }
        public string LandMark { get; set; }
        public string FullAddress { get; set; }
        public string PinCode { get; set; }
    }
}