using GICBank.ConsoleApp.ApplicationBusiness.CustomException;

using GICBank.ConsoleApp.ApplicationBusiness.Interface;
using GICBank.ConsoleApp.ApplicationBusiness.Services;
using GICBank.ConsoleApp.Core.Interface;
using GICBank.ConsoleApp.Infrastructure.Data;
using GICBank.ConsoleApp.UI;
using GICBank.ConsoleApp.UI.CommandList;
using Microsoft.Extensions.DependencyInjection;

namespace GICBank_ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Set up the service collection
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<IAccountRepository, AccountRepository>()
                    .AddSingleton<IInterestRuleRepository, InterestRuleRepository>()
                    .AddSingleton<ITransactionRepository, TransactionRepository>()
                    .AddSingleton<ITransactionService, TransactionService>()               
                    .AddSingleton<IInterestRuleService, InterestRuleService>()                  
                    .BuildServiceProvider();

                // Resolve the service and use it
                var transactionService = serviceProvider.GetService<ITransactionService>();
                var interestRuleService = serviceProvider.GetService<IInterestRuleService>();
                
                string command = string.Empty;
                Dictionary<string, IInputCommand> commandStrategies = new Dictionary<string, IInputCommand> {
                                                               {"T", new TransactionCommand(transactionService)},
                                                               {"I", new InterestRuleCommand(interestRuleService)},
                                                               {"P", new PrintCommand(transactionService)},
                                                               {"Q", new QuitCommand()}
                                                             };


                do
                {
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?");
                    Console.WriteLine("[T] Input transactions");
                    Console.WriteLine("[I] Define interest rules");
                    Console.WriteLine("[P] Print statement");
                    Console.WriteLine("[Q] Quit");
                    Console.Write("> ");
                    command = Console.ReadLine();

                    command = command.ToUpper();
                    if (commandStrategies.TryGetValue(command, out IInputCommand strategy))
                    {
                        strategy.Execute();
                    }
                    else
                    {
                        Console.WriteLine("Command not recognized");
                    }

                } while (command.ToUpper() != "Q");
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepiton occurs.Please contact GIC_Support");
            }
        }
    }
}
