using PersonalFinanceApp.Helpers;
using PersonalFinanceApp.Models;
using PersonalFinanceApp.Services;
using System;

namespace PersonalFinanceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Personal Finance App!");

            User user = FileService.LoadUser();
            if (user == null)
            {
                Console.WriteLine("Create a new user.");
                user = CreateUser();
                FileService.SaveUser(user);
            }
            else
            {
                if (!LoginUser(user))
                {
                    Console.WriteLine("Incorrect password. Exiting...");
                    return;
                }
            }

            FinanceService financeService = new FinanceService(user);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Add Income");
                Console.WriteLine("2. Add Expense");
                Console.WriteLine("3. View Balance");
                Console.WriteLine("4. Plan Budget");
                Console.WriteLine("5. Save and Exit");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        financeService.AddIncome();
                        break;
                    case "2":
                        financeService.AddExpense();
                        break;
                    case "3":
                        financeService.ViewBalance();
                        break;
                    case "4":
                        financeService.PlanBudget();
                        break;
                    case "5":
                        FileService.SaveUser(user);
                        Console.WriteLine("Data saved. Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static User CreateUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = PasswordHelper.GetPassword();

            string passwordHash = PasswordHelper.HashPassword(password);
            return new User { Username = username, PasswordHash = passwordHash };
        }

        static bool LoginUser(User user)
        {
            Console.Write("Enter password: ");
            string password = PasswordHelper.GetPassword();

            return PasswordHelper.VerifyPassword(password, user.PasswordHash);
        }
    }
}
