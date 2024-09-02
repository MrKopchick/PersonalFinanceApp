using System;

namespace PersonalFinanceApp.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
