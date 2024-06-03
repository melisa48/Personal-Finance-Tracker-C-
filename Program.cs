using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalFinanceManager
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Transaction> transactions = new List<Transaction>();
            Budget budget = new Budget();
            string? command;

            do
            {
                Console.WriteLine("Personal Finance Manager");
                Console.WriteLine("1. Add Income");
                Console.WriteLine("2. Add Expense");
                Console.WriteLine("3. View Incomes");
                Console.WriteLine("4. View Expenses");
                Console.WriteLine("5. Set Budget");
                Console.WriteLine("6. View Budget");
                Console.WriteLine("7. Generate Report");
                Console.WriteLine("8. Exit");
                Console.Write("Enter command: ");
                command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        AddTransaction(transactions, TransactionType.Income);
                        break;
                    case "2":
                        AddTransaction(transactions, TransactionType.Expense);
                        break;
                    case "3":
                        ViewTransactions(transactions, TransactionType.Income);
                        break;
                    case "4":
                        ViewTransactions(transactions, TransactionType.Expense);
                        break;
                    case "5":
                        SetBudget(budget);
                        break;
                    case "6":
                        ViewBudget(budget, transactions);
                        break;
                    case "7":
                        GenerateReport(transactions);
                        break;
                }

            } while (command != "8");
        }

        static void AddTransaction(List<Transaction> transactions, TransactionType type)
        {
            Console.Write("Enter amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter description: ");
            string? description = Console.ReadLine();
            transactions.Add(new Transaction { Amount = amount, Description = description ?? string.Empty, Type = type });
        }

        static void ViewTransactions(List<Transaction> transactions, TransactionType type)
        {
            var filteredTransactions = transactions.Where(t => t.Type == type);
            foreach (var transaction in filteredTransactions)
            {
                Console.WriteLine($"{transaction.Type}: {transaction.Description} - {transaction.Amount:C}");
            }
        }

        static void SetBudget(Budget budget)
        {
            Console.Write("Enter budget amount: ");
            budget.Amount = Convert.ToDecimal(Console.ReadLine());
        }

        static void ViewBudget(Budget budget, List<Transaction> transactions)
        {
            var totalExpenses = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
            var remainingBudget = budget.Amount - totalExpenses;

            Console.WriteLine($"Budget: {budget.Amount:C}");
            Console.WriteLine($"Total Expenses: {totalExpenses:C}");
            Console.WriteLine($"Remaining Budget: {remainingBudget:C}");
        }

        static void GenerateReport(List<Transaction> transactions)
        {
            var income = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var expenses = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
            var balance = income - expenses;

            Console.WriteLine($"Total Income: {income:C}");
            Console.WriteLine($"Total Expenses: {expenses:C}");
            Console.WriteLine($"Balance: {balance:C}");
        }
    }

    enum TransactionType
    {
        Income,
        Expense
    }

    class Transaction
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty; // Initialize with empty string
        public TransactionType Type { get; set; }
    }

    class Budget
    {
        public decimal Amount { get; set; }
    }
}
