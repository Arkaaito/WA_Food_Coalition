using System.Data.Entity;

namespace GiveFoodNow.Models
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
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<API.Models.GiveFoodNowContext>());

        public FoodCoalitionAppContext() : base("name=GiveFoodNowContext")
        {
        }

        public System.Data.Entity.DbSet<Pickup> Pickups { get; set; }
        public System.Data.Entity.DbSet<Member> Members { get; set; }
        public System.Data.Entity.DbSet<Donor> Donors { get; set; }
    }
}