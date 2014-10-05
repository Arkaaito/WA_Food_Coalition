using System;
using API.Models;

namespace API.Services
{
    public static class Distance {
        // Theta = longitude
        // Phi = latitude
        private const double EarthRadius = 6371000.0d; // wikipedia
        public static Double Meters(Double lat1, Double lon1, Double lat2, Double lon2) {
            double theta = lon1;
            double phi = lat1;
            
            double x1 = EarthRadius * Math.Cos(lon1) * Math.Sin(lat1);
            double y1 = EarthRadius * Math.Sin(lon1) * Math.Sin(lat1);
            double z1 = EarthRadius * Math.Cos(lat1);

            double x2 = EarthRadius * Math.Cos(lon2) * Math.Sin(lat2);
            double y2 = EarthRadius * Math.Sin(lon2) * Math.Sin(lat2);
            double z2 = EarthRadius * Math.Cos(lat2);

            double distance = Math.Sqrt( (x1 - x2)*(x1 - x2) + (y1-y2)*(y1 - y2) + (z1-z2)*(z1-z2));

            return distance;
        }
        public static Double Meters(Pickup donor, Member member) {
            return Meters(donor.Latitude, donor.Longitude, member.Latitude, member.Longitude);
        }
        public static Double Meters(Member member, Pickup donor) {
            return Meters(member.Latitude, member.Longitude, donor.Latitude, donor.Longitude);
        }
    }
}