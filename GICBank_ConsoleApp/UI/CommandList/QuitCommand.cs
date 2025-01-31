using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBank.ConsoleApp.UI.CommandList
{
    public class QuitCommand : IInputCommand
    {
        public void Execute()
        {
            Console.WriteLine("Thank you for banking with AwesomeGIC Bank.");
            Console.WriteLine("Have a nice day!");

            Environment.Exit(0);
        }
    }
}
