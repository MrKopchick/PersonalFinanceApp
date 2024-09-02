using System.Collections.Generic;

namespace PersonalFinanceApp.Models
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<Budget> Budgets { get; set; } = new List<Budget>();
    }
}
