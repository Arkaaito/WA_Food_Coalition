using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Services {
    public interface ILocationService {
        List<DonorDistanceResult> GetNearbyDonations(double latitude, double longitude);
    }
}