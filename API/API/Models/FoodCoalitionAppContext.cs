using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class FoodCoalitionAppContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 

        public FoodCoalitionAppContext() : base("name=GiveFoodNowContext")
        {
        }

        public System.Data.Entity.DbSet<API.Models.Pickup> Pickups { get; set; }
        public System.Data.Entity.DbSet<API.Models.Member> Members { get; set; }
        public System.Data.Entity.DbSet<API.Models.Donor> Donors { get; set; }
    }
}