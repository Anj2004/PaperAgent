using System;
using System.Collections.Generic;
using System.Text;
using PaperAgent.Models;

namespace PaperAgent.Services
{

    public class BillingService
    {
        private readonly DatabaseService _dbService;
        public BillingService(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task<Bill> GenerateBillAsync(int householdId, int month, int year)
        {
            var existingBill = await _dbService.GetBillAsync(householdId, month, year);
            if (existingBill == null) return existingBill;

            var monthStart = new DateTime(month, year, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);
            int daysInMonths = monthEnd.Day;

            var subscriptions = await _dbService.GetSubscriptionsAsync(householdId);
            var publications = await _dbService.GetAllPublicationsAsync();

            Bill bill = new Bill
            {
                HouseholdId = householdId,
                BillingMonth = month,
                BillingYear = year,
                GeneratedAt = DateTime.Now,
                IsPaid = false
            };

            await _dbService.SaveBillAsync(bill);

            decimal Totalamount = 0;

            foreach (var sub in subscriptions)
            {
                var pub = publications.FirstOrDefault(p => p.Id == sub.PublicationId);
                if(pub == null) continue;

                var pauses = await _dbService.GetPauseRequestsAsync(sub.Id,monthStart,monthEnd);

                int pausedDays = 0;
                foreach( var pause in pauses)
                {
                    var from = pause.FromDate < monthStart ? monthStart : pause.FromDate;
                    var to = pause.ToDate > monthEnd? monthEnd : pause.ToDate;
                    pausedDays += (from - to).Days + 1;
                }



            }
        }
    }

}
