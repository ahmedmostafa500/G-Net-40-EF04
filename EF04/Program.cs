using EF04.Model;

namespace EF04
{
    internal class Program
    {

        static void Main(string[] args)
        {
            #region Menu
            #region AddNewCoustmer
            static void AddCustomer()
            {
                using var context = new BankContext();

                Console.Write("Full Name: ");
                string name = Console.ReadLine();

                Console.Write("National ID: ");
                string nid = Console.ReadLine();

                Console.Write("DOB: ");
                DateTime dob = DateTime.Parse(Console.ReadLine());

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Phone: ");
                string phone = Console.ReadLine();

                Console.Write("Address: ");
                string address = Console.ReadLine();

                Console.Write("Type (Individual/Business): ");
                string type = Console.ReadLine();

                var customer = new Customer
                {
                    FullName = name,
                    NationalId = nid,
                    DateOfBirth = dob,
                    Email = email,
                    PhoneNumber = phone,
                    Address = address,
                    CustomerType = type
                };

                context.Customers.Add(customer);
                context.SaveChanges();

                Console.WriteLine($"Customer created with ID = {customer.Id}");
            }
            #endregion
            #region OpenNewAcount
            static void OpenAccount()
            {
                using var context = new BankContext();

                Console.Write("Account Number: ");
                string accNumber = Console.ReadLine();

                Console.Write("Account Type: ");
                string accType = Console.ReadLine();

                Console.Write("Branch Code: ");
                if (!int.TryParse(Console.ReadLine(), out int branchCode))
                {
                    Console.WriteLine("Invalid Branch Code!");
                    return;
                }

                Console.Write("Customer Id: ");
                if (!int.TryParse(Console.ReadLine(), out int customerId))
                {
                    Console.WriteLine("Invalid Customer Id!");
                    return;
                }

                Console.Write("Ownership Type (Primary / CoHolder): ");
                string ownership = Console.ReadLine();

                var branch = context.Branches.Find(branchCode);
                var customer = context.Customers.Find(customerId);

                if (branch == null || customer == null)
                {
                    Console.WriteLine("Branch or Customer not found!");
                    return;
                }

                var account = new Account
                {
                    AccountNumber = accNumber,
                    AccountType = accType,
                    BranchCode = branchCode,
                    OpeningDate = DateTime.Now,
                    CurrentBalance = 0,
                    AccountStatus = "Active"
                };

                context.Accounts.Add(account);

                var customerAccount = new CustomerAccount
                {
                    CustomerId = customerId,
                    AccountNumber = accNumber,
                    OwnershipType = ownership,
                    OwnershipStartDate = DateTime.Now
                };

                context.CustomerAccounts.Add(customerAccount);

                context.SaveChanges();

                Console.WriteLine("Account created successfully.");
            }
            #endregion
            #region UpdateAccountStatus
            static void UpdateAccountStatus()
            {
                using var context = new BankContext();

                Console.Write("Account Number: ");
                string accNumber = Console.ReadLine();

                Console.Write("Customer Id: ");
                if (!int.TryParse(Console.ReadLine(), out int customerId))
                {
                    Console.WriteLine("Invalid Customer Id!");
                    return;
                }

                var account = context.Accounts
                    .FirstOrDefault(a => a.AccountNumber == accNumber);

                var relation = context.CustomerAccounts
                    .FirstOrDefault(ca => ca.AccountNumber == accNumber && ca.CustomerId == customerId);

                if (account == null || relation == null)
                {
                    Console.WriteLine("Account or relation not found!");
                    return;
                }

                account.AccountStatus = account.AccountStatus == "Active" ? "Closed" : "Active";

                context.SaveChanges();

                Console.WriteLine($"Account status updated to {account.AccountStatus}");
            }
            #endregion
            #region RemoveAccountfromCustomer
            static void RemoveAccountFromCustomer()
            {
                using var context = new BankContext();

                Console.Write("Account Number: ");
                string accNumber = Console.ReadLine();

                Console.Write("Customer Id: ");
                if (!int.TryParse(Console.ReadLine(), out int customerId))
                {
                    Console.WriteLine("Invalid Customer Id!");
                    return;
                }

                var relation = context.CustomerAccounts
                    .FirstOrDefault(ca => ca.AccountNumber == accNumber && ca.CustomerId == customerId);

                if (relation == null)
                {
                    Console.WriteLine("Relation not found!");
                    return;
                }

                context.CustomerAccounts.Remove(relation);
                context.SaveChanges();

                Console.WriteLine("Account removed from customer.");
            }
            #endregion
            #region ListCustomers
            static void ListCustomers()
            {
                using var context = new BankContext();

                var customers = context.Customers
                    .Select(c => new
                    {
                        c.Id,
                        c.FullName,
                        Accounts = c.CustomerAccounts.Select(ca => new
                        {
                            ca.Account.AccountNumber,
                            ca.Account.AccountType,
                            ca.Account.AccountStatus
                        }).ToList()
                    }).ToList();

                foreach (var c in customers)
                {
                    Console.WriteLine($"Customer: {c.FullName} (ID: {c.Id})");

                    if (c.Accounts.Count == 0)
                    {
                        Console.WriteLine("   No Accounts");
                    }
                    else
                    {
                        foreach (var acc in c.Accounts)
                        {
                            Console.WriteLine($"   Account: {acc.AccountNumber} | {acc.AccountType} | {acc.AccountStatus}");
                        }
                    }

                    Console.WriteLine("--------------------------------");
                }
            }
            #endregion



            #endregion
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== National Bank - Management =====");
                Console.WriteLine("1) Add Customer");
                Console.WriteLine("2) Open Account");
                Console.WriteLine("3) Update Account Status");
                Console.WriteLine("4) Remove Account from Customer");
                Console.WriteLine("5) List Customers");
                Console.WriteLine("0) Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddCustomer();
                            break;
                        case "2":
                            OpenAccount();
                            break;
                        case "3":
                            UpdateAccountStatus();
                            break;
                        case "4":
                            RemoveAccountFromCustomer();
                            break;
                        case "5":
                            ListCustomers();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid choice!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();

            }
        }
    }
}
