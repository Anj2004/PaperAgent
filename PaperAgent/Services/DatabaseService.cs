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
    }
}
