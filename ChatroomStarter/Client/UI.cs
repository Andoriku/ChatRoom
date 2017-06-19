using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class UI

    {
        public static bool exitCondition=false;
        public static void DisplayMessage(string message,string username)
        {
            Console.WriteLine(username + ": " + message);
        }
        public static string GetInput()
        {
            string input =  Console.ReadLine();
            CheckInput(input);
            return input;
        }
        public static void CheckInput(string input)
        {
            if (input.Equals("/leave"))
            {
                exitCondition = true;
            }
        }
        public static string GetUsername()
        {
            Console.WriteLine("Enter UserName: ");
            string usernameInput = Console.ReadLine();
            return usernameInput;
        }
    }
}
