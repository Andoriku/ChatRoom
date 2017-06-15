using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        public static ServerClient client;
        TcpListener server;
        public static string username;
        public Server()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }
        public void Run()
        {
            do
            {
                AcceptClient();
                AddUserToDictionary();
                string message = client.Recieve();
                Respond(message);
            }
            while (true); 
        }
        private void AcceptClient()
        {
            TcpClient clientSocket = default(TcpClient);
            clientSocket = server.AcceptTcpClient();
            Console.WriteLine("Inital Connect");
            NetworkStream stream = clientSocket.GetStream();
            client = new ServerClient(stream, clientSocket);
        }
        private void AddUserToDictionary()
        {
            username = client.Recieve();
            Console.WriteLine(username + " is now Connected");
            ClientDictionary.Add(username, client.UserId);
        }
        private void Respond(string body)
        {
             client.Send(body);
        }
        public Dictionary<string, string> ClientDictionary = new Dictionary<string, string>
        {
           
        };
    }
}
