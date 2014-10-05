using API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using API.Services;

namespace API.Services
{
    public class LocationService : ILocationService
    {
        public List<Pickup> GetNearbyPickups(double latitude, double longitude, double range)
        {
            var db = new FoodCoalitionAppContext();
            /*
            var nearbyDonors = db.Donors.Where(d => DistanceBetween(d.Latitude, d.Longitude, latitude, longitude) <= range);
            var pickupsForNearbyDonors = from donor in nearbyDonors
                                         join pickup in db.Pickups
                                         on donor.Id equals pickup.DonorId
                                         select pickup;

            var result = pickupsForNearbyDonors.ToList();
             */
            return new List<Pickup>();
        }

        private double DistanceBetween(double lat1, double lon1, double lat2, double lon2)
        {
            return Distance.Meters(lat1, lon1, lat2, lon2);
        }

    }
}