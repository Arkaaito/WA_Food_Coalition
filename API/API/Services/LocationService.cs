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
        public List<Pickup> GetNearbyPickups(double latitude, double longitude, double range)
        {
            var db = new FoodCoalitionAppContext();
            var nearbyDonors = db.Donors.Where(d => DistanceBetween(d.Latitude, d.Longitude, latitude, longitude) <= range);
            var pickupsForNearbyDonors = from donor in nearbyDonors
                                         join pickup in db.Pickups
                                         on donor.Id equals pickup.DonorId
                                         select pickup;

            var result = pickupsForNearbyDonors.ToList();
            return result;
        }

        private double DistanceBetween(double p1, double p2, double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

    }
}