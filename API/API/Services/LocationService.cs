using API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API.Services
{
    public class LocationService : ILocationService
    {
        public List<Pickup> GetNearbyPickups(double latitude, double longitude)
        {
            var db = new FoodCoalitionAppContext();
            var nearbyDonors = db.Donors;
            var result = new List<Pickup>();

            // TODO: get pickups whose donors are near the member's lat/lng


            return result;
        }

    }
}