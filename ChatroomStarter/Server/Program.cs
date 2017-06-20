using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
           Server runningServer = new Server();
            do
            {
                runningServer.Run();
            } while (Server.ClientDictionary.Count > 0);
            Console.ReadLine();

        }
    }
}
