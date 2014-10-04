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
        public List<DonorDistanceResult> GetNearbyDonations(double latitude, double longitude)
        {
            String _connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DonationContext"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.usp_GetNearDonations", connection))
                {
                    List<DonorDistanceResult> result = new List<DonorDistanceResult>();
                    command.CommandType = CommandType.StoredProcedure;
                    #region Add Parameters
                    command.Parameters.Add(new SqlParameter("@latitude", latitude));
                    command.Parameters.Add(new SqlParameter("@longitude", longitude));
                    #endregion

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var donation = new DonorDistanceResult();
                        donation.Name = reader.GetValue(0).ToString();
                        donation.Email = reader.GetValue(1).ToString();
                        donation.Phone = reader.GetValue(2).ToString();
                        donation.Address = reader.GetValue(3).ToString();
                        //donation.Description = reader.GetValue(4).ToString();
                        //donation.Status = reader.GetValue(5).ToString();
                        //donation.ExpirationDate = (DateTime)reader.GetValue(6);

                        // Need some extra logic for when distance == 0, since double.parse returns empty string
                        String distance = reader.GetValue(7).ToString();
                        //if (distance.Equals(""))
                            //donation.Distance = 0.0;
                        //else
                            //donation.Distance = Double.Parse(distance);
                        
                        result.Add(donation);
                    }
                    return result;
                }
            }
        }

    }
}