using System;
using System.Collections.Generic;
using System.Text;

namespace PaperAgent.Services
{

    public class BillingService
    {
        private readonly DatabaseService _dbService;
        public BillingService(DatabaseService dbService)
        {
            _dbService = dbService;
        }
    }

}
