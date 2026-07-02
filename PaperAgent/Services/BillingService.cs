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
            try
            {
                var existingBill = await _dbService.GetBillAsync(householdId, month, year);
                if (existingBill != null) return existingBill; // already billed household ozhivakan

                var monthStart = new DateTime(year, month, 1); //keeping it as ex: 01/06/2026
                var monthEnd = monthStart.AddMonths(1).AddDays(-1); //Add 1 month with monthStart and reduce one day from it.
                int daysInMonths = monthEnd.Day;

                var subscriptions = await _dbService.GetSubscriptionsAsync(householdId);
                var publications = await _dbService.GetAllPublicationsAsync();

                Bill bill = new Bill
                {
                    HouseholdId = householdId,
                    BillingMonth = month,
                    BillingYear = year,
                    GeneratedAt = DateTime.Now,
                    IsPaid = false // we also have a total amount attribute which will be filled later with the update bill async call
                };

                await _dbService.SaveBillAsync(bill);

                decimal totalamount = 0;

                foreach (var sub in subscriptions)
                {
                    var pub = publications.FirstOrDefault(p => p.Id == sub.PublicationId);
                    if (pub == null) continue;

                    var pauses = await _dbService.GetPauseRequestsAsync(sub.Id, monthStart, monthEnd);

                    int pausedDays = 0;
                    foreach (var pause in pauses)
                    {
                        var from = pause.FromDate < monthStart ? monthStart : pause.FromDate;
                        var to = pause.ToDate > monthEnd ? monthEnd : pause.ToDate;
                        pausedDays += (to - from).Days + 1; // one household could have multiple pauses in a month. 
                    }

                    int scheduledIssues = daysInMonths;
                    int deliveredIssues = daysInMonths - pausedDays;
                    if (deliveredIssues < 0) deliveredIssues = 0;

                    decimal lineTotal = deliveredIssues * sub.Quantity * pub.PricePerIssue;
                    totalamount += lineTotal;

                    if(totalamount > pub.PricePerMonth)
                    {
                        totalamount = pub.PricePerMonth;
                    }

                    await _dbService.SaveBillLineItemAsync(new BillLineItem
                    {
                        BillId = bill.Id,
                        SubscriptionId = sub.Id,
                        PublicationName = pub.Name,
                        PricePerIssue = pub.PricePerIssue,
                        IssuesDelivered = deliveredIssues,
                        IssuesPaused = pausedDays,
                        LineTotal = lineTotal,
                    });

                    bill.TotalAmount = totalamount;
                    await _dbService.UpdateBillAsync(bill);
                }
                return bill;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GenerateBillAsync failed: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack: {ex.StackTrace}");
                await Shell.Current.DisplayAlertAsync("Error", "Could not generate bill. Please try again.", "OK");
                return null;
            }
            
        }
    }

}
