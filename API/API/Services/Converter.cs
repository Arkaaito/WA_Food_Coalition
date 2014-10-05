using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using API.Models;

namespace API.Services
{
    public static class Converter {
        private const string JsonFieldFormatString = "\n\"{0}\":\"{1}\",";
        private const string JsonFieldFormatNotQuoted = "\n\"{0}\":{1},";
        private const string JsonFieldFormatNotQuotedEnd = "\n\"{0}\":{1}";
        
        private const string donor_header = "donor json: {";
        private const string donor_id = "Id";
        private const string donor_first_name = "FirstName";
        private const string donor_last_name = "LastName";
        private const string donor_email = "Email";
        private const string donor_phone = "Phone";
        private const string donor_opt_in = "Optin";
        private const string donor_footer = "\n}";

        private const string member_header = "member json: {";
        private const string member_id = "Id";
        private const string member_name = "Name";
        private const string member_contact_first_name = "ContactFirstName";
        private const string member_contact_last_name = "ContactLastName";
        private const string member_email = "Email";
        private const string member_address = "Address";
        private const string member_city = "City";
        private const string member_zip = "Zip";
        private const string member_range_in_meters = "RangeInMeters";
        private const string member_phone = "Phone";
        private const string member_latitude = "Latitude";
        private const string member_longitude = "Longitude";
        private const string member_footer = "\n}";

        private const string pickup_header = "pickup json: {";
        private const string pickup_id = "Id";
        private const string pickup_donor_id = "DonorId";
        private const string pickup_donor = "Donor";
        private const string pickup_items = "Items";
        private const string pickup_address = "Address";
        private const string pickup_city = "City";
        private const string pickup_zip = "Zip";
        private const string pickup_latitude = "Latitude";
        private const string pickup_longitude = "Longitude";
        private const string pickup_notes = "Notes";
        private const string pickup_schedule = "Schedule";
        private const string pickup_status = "Status";
        private const string pickup_footer = "\n}";

        public static string DonorToJson(Donor donor) {
            StringBuilder sb = new StringBuilder();
            sb.Append(donor_header);
            sb.Append(String.Format(JsonFieldFormatNotQuoted,donor_id,donor.Id.ToString()));
            sb.Append(String.Format(JsonFieldFormatString,donor_first_name,donor.FirstName));
            sb.Append(String.Format(JsonFieldFormatString,donor_last_name,donor.LastName));
            sb.Append(String.Format(JsonFieldFormatString,donor_phone,donor.Phone));
            sb.Append(String.Format(JsonFieldFormatString,donor_email,donor.Email));
            sb.Append(String.Format(JsonFieldFormatNotQuotedEnd,donor_opt_in,true));
            sb.Append(donor_footer);
            return sb.ToString();
        }
        public static string MemberToJson(Member member) {
            StringBuilder sb = new StringBuilder();
            sb.Append(member_header);
            sb.Append(String.Format(JsonFieldFormatNotQuoted,member_id,member.Id.ToString()));
            sb.Append(String.Format(JsonFieldFormatString,member_name,member.Name));
            sb.Append(String.Format(JsonFieldFormatString,member_contact_first_name,member.ContactFirstName));
            sb.Append(String.Format(JsonFieldFormatString,member_contact_last_name,member.ContactLastName));
            sb.Append(String.Format(JsonFieldFormatString,member_email,(null != member.Email)?member.Email:"null"));
            sb.Append(String.Format(JsonFieldFormatString,member_address, member.Address));
            sb.Append(String.Format(JsonFieldFormatString,member_city,member.City));
            sb.Append(String.Format(JsonFieldFormatString,member_zip,member.Zip));
            sb.Append(String.Format(JsonFieldFormatString,member_range_in_meters,member.RangeInMeters));
            sb.Append(String.Format(JsonFieldFormatString,member_phone, (null != member.Phone)?member.Phone:"null"));
            sb.Append(String.Format(JsonFieldFormatNotQuoted,member_latitude,member.Latitude.ToString()));
            sb.Append(String.Format(JsonFieldFormatNotQuotedEnd,member_longitude,member.Longitude.ToString()));
            sb.Append(member_footer);
            return sb.ToString();
        }
        public static string PickupToJson(Pickup pickup) {
            StringBuilder sb = new StringBuilder();
            sb.Append(pickup_header);
            sb.Append(String.Format(JsonFieldFormatNotQuoted,pickup_id,pickup.Id));
            sb.Append(String.Format(JsonFieldFormatNotQuoted,pickup_donor_id,pickup.DonorId));
            sb.Append(String.Format(JsonFieldFormatNotQuoted,pickup_donor,(null != pickup.Donor)?pickup.Donor.ToString():"null"));
            sb.Append(String.Format(JsonFieldFormatString,pickup_items, pickup.Items));
            sb.Append(String.Format(JsonFieldFormatString,pickup_address, pickup.Address));
            sb.Append(String.Format(JsonFieldFormatString,pickup_city,pickup.City));
            sb.Append(String.Format(JsonFieldFormatString,pickup_zip, pickup.Zip));
            sb.Append(String.Format(JsonFieldFormatNotQuoted,pickup_latitude,pickup.Latitude));
            sb.Append(String.Format(JsonFieldFormatNotQuoted,pickup_longitude,pickup.Longitude));
            sb.Append(String.Format(JsonFieldFormatString,pickup_notes,(null != pickup.Notes)?pickup.Notes:"null"));
            sb.Append(String.Format(JsonFieldFormatString,pickup_schedule,pickup.Schedule.ToString()));
            int status = 0;
            switch (pickup.Status) {
                case StatusTypes.New: status = 1; break;
                case StatusTypes.Scheduled: status = 2; break;
                case StatusTypes.Closed: status = 3; break;
            }
            sb.Append(String.Format(JsonFieldFormatNotQuotedEnd,pickup_status,status.ToString()));
            sb.Append(pickup_footer);
            return sb.ToString();
        }
    }
}