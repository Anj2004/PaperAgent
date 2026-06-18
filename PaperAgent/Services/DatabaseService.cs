using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PaperAgent.Models;
using PaperAgent.ViewModels;

namespace PaperAgent.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _db;

        public async Task InitAsync()
        {
            if (_db != null)
            {
                return;
            }
            var db_path = Path.Combine(FileSystem.AppDataDirectory, "paperagent.db3");

            _db = new SQLiteAsyncConnection(db_path);

            await _db.CreateTableAsync<Bill>();
            await _db.CreateTableAsync<BillLineItem>();
            await _db.CreateTableAsync<DeliveryLog>();
            await _db.CreateTableAsync<Household>();
            await _db.CreateTableAsync<PauseRequest>();
            await _db.CreateTableAsync<PaymentRecord>();
            await _db.CreateTableAsync<Publication>();
            await _db.CreateTableAsync<PublicationCalendar>();
            await _db.CreateTableAsync<Subscription>();
        }

        public async Task<List<Household>> GetAllHouseholdsAsync()
        {
            var houses = await _db.Table<Household>().ToListAsync();
            return houses;
        }

        public async Task SaveHouseholdAsync(Household household)
        {
            if (_db == null)
            {
                System.Diagnostics.Debug.WriteLine("SaveHouseholdAsync: _db is null");
                return;
            }
            await _db.InsertAsync(household);
        }

        public async Task DeleteHouseholdAsync(int id)
        {
            await _db.DeleteAsync<Household>(id);
        }

        public async Task<Household> GetHouseholdAsync(int id)
        {
            return await _db.Table<Household>().Where(h => h.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Publication>> GetAllPublicationsAsync()
        {
            return await _db.Table<Publication>().ToListAsync();
        }
        public async Task<List<Subscription>> GetSubscriptionsAsync(int householdId)
        {
            return await _db.Table<Subscription>()
                            .Where(s => s.HouseholdId == householdId && s.IsActive)
                            .ToListAsync();
        }

        //Add a save method for publications
        public async Task SavePublicationAsync(Publication publication)
        {
            await _db.InsertAsync(publication);
        }

        public async Task SaveSubscriptionAsync(Subscription subscription)
        {
            await _db.InsertAsync(subscription);
        }

        public async Task SavePauseRequestAsync(PauseRequest pauseRequest)
        {
            await _db.InsertAsync(pauseRequest);
        }

        public async Task<List<PauseRequest>> GetPauseRequestsAsync(int subscriptionId, DateTime From, DateTime To)
        {
            return await _db.Table<PauseRequest>()
                            .Where(p => p.SubscriptionId == subscriptionId && p.FromDate >= From && p.ToDate <= To) //did on conscience
                            .ToListAsync();
        }

        //look from here
        public async Task<Bill> GetBillAsync(int householdId, int month, int year)
        {
            return await _db.Table<Bill>()
                            .Where(b => b.HouseholdId == householdId
                                     && b.BillingMonth == month
                                     && b.BillingYear == year)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> SaveBillAsync(Bill bill)
        {
            await _db.InsertAsync(bill);
            return bill.Id;
        }

        public async Task SaveBillLineItemAsync(BillLineItem item)
        {
            await _db.InsertAsync(item);
        }

        public async Task UpdateBillAsync(Bill bill)
        {
            await _db.UpdateAsync(bill);
        }

        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await _db.Table<Bill>().Where(b => b.Id == id).FirstOrDefaultAsync();    
        }
    }
}
