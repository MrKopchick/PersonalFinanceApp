namespace PersonalFinanceApp.Models
{
    public class Budget
    {
        public string Category { get; set; }
        public decimal Limit { get; set; }
        public decimal Spent { get; set; }
    }
}
