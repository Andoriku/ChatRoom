using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("127.0.0.1", 9999);

            Task sendMessage = Task.Run(() => client.Send());
            Task reciveMessage = Task.Run(() => client.Recieve());
            while (client.clientSocket.Connected == true)
            {
                if (UI.EndCondition == true)
                {
                    client.clientSocket.Close();
                }
                else
                {
                    continue;
                }
            }
           
        }
    }
}
