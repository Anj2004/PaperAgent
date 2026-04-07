using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using static Java.Util.Concurrent.Flow;

namespace PaperAgent
{
    internal class DatabaseService
    {
        private SQLiteAsyncConnection _db;
        private async Task InitAsync()
        {
            if (_db != null) { return; }
            var dbPath = Path.Combine(FileSystem.AppDataDirectory,"paperagent.db3");
            _db = new SQLiteAsyncConnection(dbPath);

            await _db.CreateTableAsync<Household>();
            await _db.CreateTableAsync<Publication>();
            await _db.CreateTableAsync<Subscription>();
            await _db.CreateTableAsync<PauseRequest>();
            await _db.CreateTableAsync<PublicationCalendar>();
            await _db.CreateTableAsync<DeliveryLog>();
            await _db.CreateTableAsync<Bill>();
            await _db.CreateTableAsync<BillLineItem>();
            await _db.CreateTableAsync<PaymentRecord>();
        }
    }
}
