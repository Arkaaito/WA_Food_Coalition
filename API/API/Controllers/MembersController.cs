using API.Models;
using API.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers {
    public class MembersController : ApiController {
        private FoodCoalitionAppContext _db = new FoodCoalitionAppContext();
        private ILocationService _locationService = new ConcreteLocationService();
        private int _defaultNearbyFoodBanks = Convert.ToInt32(WebConfigurationManager.AppSettings["DefaultAmountOfNearbyFoodBanksToReturn"]);

        public IQueryable<Member> Get(double latitude, double longitude, int? amountToReturn) {
            if (amountToReturn == null)
                amountToReturn = _defaultNearbyFoodBanks;

            return _locationService.GetNearbyFoodBanks(latitude, longitude, (int)amountToReturn).AsQueryable();
        }

        // Get all foodbanks - detailed.
        public IQueryable<Member> Get() {
            return _db.FoodBanks.OrderBy(fb => fb.Name);
        }

        public Member Get(int id) {
            Member foodBank = _db.FoodBanks.Where(fb => fb.ID == id).FirstOrDefault();
            if (foodBank == null) {
                throw new HttpException();
            }
            return foodBank;
        }

        // Insert
        [ResponseType(typeof (Member))]
        public IHttpActionResult Post(Member foodBank) {
            //if (WebConfigurationManager.AppSettings["AdminToken"] != token) {
            //    return BadRequest("Unauthorized.");
            //}
            //if (!ModelState.IsValid) {
            //    return BadRequest(ModelState);
            //}

            _db.FoodBanks.Add(foodBank);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = foodBank.Id }, foodBank);
        }

        // Update
        public IHttpActionResult Put(int id, Member foodBank) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (id != foodBank.Id) {
                return BadRequest();
            }
            _db.Entry(foodBank).State = EntityState.Modified;

            try {
                _db.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                if (!FoodBankExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return Ok();
        }

        // Delete api/FoodBank/5
        [ResponseType(typeof (Member))]
        public IHttpActionResult Delete(int id) {
            Member foodBank = _db.FoodBanks.Find(id);

            if (foodBank == null) {
                return NotFound();
            }

            _db.FoodBanks.Remove(foodBank);
            _db.SaveChanges();

            return Ok();
        }

        private bool FoodBankExists(int id) {
            return _db.Donations.Count(e => e.ID == id) > 0;
        }
    }
}