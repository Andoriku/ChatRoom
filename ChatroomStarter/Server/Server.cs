using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace Server
{
    class Server
    {
        public static ServerClient client;
        TcpListener server;
        public static string username;
        public bool serverUtilization;
        public bool checkValidator = false;
        List<TcpClient> activeList = new List<TcpClient>();
        public Queue<string> messageQueue = new Queue<string>();
        public Server()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }
        public void Run()
        {
            AcceptClient() ;
            CheckKeys();
            do
            {
                CheckserverUtilization();
                string message = client.Recieve(messageQueue);
                Respond(message);
                CheckserverUtilization();
            }
            while (serverUtilization == true);
        }        
        private void AcceptClient()
        {
            TcpClient clientSocket = default(TcpClient);
            activeList.Add(clientSocket);
            clientSocket = server.AcceptTcpClient();
            Console.WriteLine("Inital Connect");
            NetworkStream stream = clientSocket.GetStream();
            client = new ServerClient(stream, clientSocket);
        }
        private void CheckKeys()
        {
            username = client.RecieveUserName();
            if (ClientDictionary.ContainsKey(username))
            {
                client.UserId = ClientDictionary.TryGetValue(username, out client.UserId).ToString();
            }
            else
            {
                checkValidator = false;
            }
            if (checkValidator == false)
            {
                AddUserToDictionary();
            }
        }
        private void AddUserToDictionary()
        {
            
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
        public bool CheckserverUtilization()
        {
            if (ClientDictionary.Count == 0)
            {
                return serverUtilization = false;
            }
            else
            {
                return serverUtilization = true;
            }
        }
    }
}
