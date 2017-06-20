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
using System.Diagnostics;
namespace Server
{
    public class Server
    {
        public static ServerClient client;
        TcpListener server;
        public string message;
        public static string username;
        public bool serverUtilization = false;
        public bool checkValidator = false;
        public static List<TcpClient> activeList = new List<TcpClient>();
        public static Queue<string> messageQueue = new Queue<string>();
        public static Dictionary<string, string> ClientDictionary = new Dictionary<string, string>();
        public Server()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }
        public void Run()
        {

            Task acceptClient = Task.Run(() => AcceptClient());
            Task respond = Task.Run(() => Respond(message));
            respond.Wait();
        }
        public string GetNewMessage()
        {
            try
            {
                message = client.Recieve(messageQueue);
            }
            catch
            {
                AcceptClient();
            }
            return message;
        }
        private void AcceptClient()
        {
            do
            {
                TcpClient clientSocket = default(TcpClient);
                activeList.Add(clientSocket);
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("Inital Connect");
                NetworkStream stream = clientSocket.GetStream();
                client = new ServerClient(stream, clientSocket);
                CheckKeys();
                Task newMessage = Task.Run(() => GetNewMessage());
                CheckserverUtilization();
            } while (true);
        }
        private void CheckKeys()
        {
            try
            {
                username = client.RecieveUserName();
            }
            catch
            {
                AcceptClient();
            }
            if (ClientDictionary.ContainsKey(username))
            {
                client.UserId = ClientDictionary.TryGetValue(username, out client.UserId).ToString();
                checkValidator = true;
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
            try
            {
                do
                {
                    foreach (TcpClient n in activeList)
                    {
                        client.Send(username + body);
                    }
                } while (messageQueue.Last() == body);
            }
            catch
            {
                GetNewMessage();
            }
        }
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
