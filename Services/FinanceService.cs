using PersonalFinanceApp.Models;
using System;
using System.Linq;

namespace PersonalFinanceApp.Services
{
    public class FinanceService
    {
        private readonly User _user;

        public FinanceService(User user)
        {
            _user = user;
        }

        public void AddIncome()
        {
            Console.Write("Enter income category: ");
            string category = Console.ReadLine();

            Console.Write("Enter income description: ");
            string description = Console.ReadLine();

            Console.Write("Enter income amount: ");
            decimal amount = ConsoleHelper.GetDecimalInput();

            var income = new Transaction
            {
                Date = DateTime.Now,
                Category = category,
                Description = description,
                Amount = amount
            };

            _user.Transactions.Add(income);
            Console.WriteLine("Income added.");
        }

        public void AddExpense()
        {
            Console.Write("Enter expense category: ");
            string category = Console.ReadLine();

            Console.Write("Enter expense description: ");
            string description = Console.ReadLine();

            Console.Write("Enter expense amount: ");
            decimal amount = ConsoleHelper.GetDecimalInput();

            var expense = new Transaction
            {
                Date = DateTime.Now,
                Category = category,
                Description = description,
                Amount = -amount
            };

            _user.Transactions.Add(expense);
            Console.WriteLine("Expense added.");

            UpdateBudget(category, -amount);
        }

        public void ViewBalance()
        {
            decimal balance = _user.Transactions.Sum(t => t.Amount);
            Console.WriteLine($"Current balance: {balance:C}");

            var expensesByCategory = _user.Transactions
                .Where(t => t.Amount < 0)
                .GroupBy(t => t.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(t => t.Amount)
                });

            Console.WriteLine("Expenses by category:");
            foreach (var expense in expensesByCategory)
            {
                Console.WriteLine($"{expense.Category}: {expense.Total:C}");
            }
        }

        public void PlanBudget()
        {
            Console.Write("Enter category to plan budget: ");
            string category = Console.ReadLine();

            Console.Write("Enter budget limit for this category: ");
            decimal limit = ConsoleHelper.GetDecimalInput();

            var budget = _user.Budgets.FirstOrDefault(b => b.Category == category);
            if (budget == null)
            {
                budget = new Budget { Category = category, Limit = limit };
                _user.Budgets.Add(budget);
            }
            else
            {
                budget.Limit = limit;
            }

            Console.WriteLine($"Budget for category {category} set to {limit:C}.");
        }

        private void UpdateBudget(string category, decimal amountSpent)
        {
            var budget = _user.Budgets.FirstOrDefault(b => b.Category == category);
            if (budget != null)
            {
                budget.Spent += amountSpent;
                if (budget.Spent > budget.Limit)
                {
                    Console.WriteLine($"Warning: Budget limit exceeded for category {category}!");
                }
            }
        }
    }
}
