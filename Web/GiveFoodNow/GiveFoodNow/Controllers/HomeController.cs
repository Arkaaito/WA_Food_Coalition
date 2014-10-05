using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;
using API.Services;

namespace GiveFoodNow.Controllers
{
    public class HomeController : Controller
    {
        private FoodCoalitionAppContext db = new FoodCoalitionAppContext();

        public ActionResult Index()
        {
            int memberId = 2; // TODO: need to get the member id from the login info
            Member loggedInMember = db.Members.Find(memberId);
            ViewBag.m = loggedInMember;
            double latitude = loggedInMember.Latitude;
            double longitude = loggedInMember.Longitude;
            double range = loggedInMember.RangeInMeters;
            // TODO: when Jack adds the member ID to pickup, add it to the 'Scheduled' subclause below
            var pickups = db.Pickups.Where(p => (p.Status == StatusTypes.New) || (p.Status == StatusTypes.Scheduled)).AsEnumerable()
                        .Where(p => Distance.Meters(p.Latitude, p.Longitude, latitude, longitude) <= range);
            ViewBag.NearbyPickups = pickups;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}