using SmartAddress.DTOs;
using SmartAddress.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAddress.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDistricts()
        {
            using (SmartAddressEntities db = new SmartAddressEntities())
            {
                var list = db.Districts.Select(d => new { d.DistID, d.DistrictName }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }  
        }

        [HttpGet]
        public JsonResult GetCitiesByDistrict(int id)
        {
            using (SmartAddressEntities db = new SmartAddressEntities())
            {
                var list = db.Cities.Where(c => c.DistID == id)
                    .Select(c => new { c.CityID, c.CityName }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
                
        }

        [HttpPost]
        public JsonResult SaveAddress(AddressCreateDTO dto)
        {
            using (SmartAddressEntities db = new SmartAddressEntities())
            {
                AddressDetail ad = new AddressDetail
                {
                    DistID = dto.DistID,
                    CityID = dto.CityID,
                    StreetName = dto.StreetName,
                    BuildingName = dto.BuildingName,
                    LandMark = dto.LandMark,
                    FullAddress = dto.FullAddress,
                    PinCode = dto.PinCode
                };
                db.AddressDetails.Add(ad);
                db.SaveChanges();
            }
            return Json(new { success = true });
        }

        //Search ke liye add kiya mamla ye 

        

    }
}