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
                var list = db.Districts
                    .Select(d => new { d.DistID, d.DistrictName })
                    .ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetCitiesByDistrict(int id)
        {
            using (SmartAddressEntities db = new SmartAddressEntities())
            {
                var list = db.Cities
                    .Where(c => c.DistID == id)
                    .Select(c => new { c.CityID, c.CityName })
                    .ToList();
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

        public ActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public JsonResult SearchAddresses(int? distID = null, int? cityID = null)
        {
            using (SmartAddressEntities db = new SmartAddressEntities())
            {
                var query = db.AddressDetails.AsQueryable();

                if (distID.HasValue)
                    query = query.Where(a => a.DistID == distID.Value);

                if (cityID.HasValue)
                    query = query.Where(a => a.CityID == cityID.Value);

                var results = query
                    .Include(a => a.District)
                    .Include(a => a.City)
                    .Select(a => new AddressDetailDTO
                    {
                        AddressID = a.AddressID,
                        //DistID = a.DistID,
                        //CityID = a.CityID,
                        DistrictName = a.District.DistrictName,
                        CityName = a.City.CityName,
                        StreetName = a.StreetName,
                        BuildingName = a.BuildingName,
                        LandMark = a.LandMark,
                        FullAddress = a.FullAddress,
                        PinCode = a.PinCode
                    })
                    .ToList();

                return Json(results, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteAddress(int id)
        {
            using (SmartAddressEntities db = new SmartAddressEntities())
            {
                var address = db.AddressDetails.FirstOrDefault(a => a.AddressID == id);
                if (address != null)
                {
                    db.AddressDetails.Remove(address);
                    db.SaveChanges();
                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
        }

    }
}