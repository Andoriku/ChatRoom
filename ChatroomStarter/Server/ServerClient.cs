using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerClient
    {
        NetworkStream stream;
        TcpClient client;
        public string UserId;
        public ServerClient(NetworkStream Stream, TcpClient Client)
        {
            stream = Stream;
            client = Client;
            UserId = System.Guid.NewGuid().ToString();
        }
        public void Send(string Message)
        {
            byte[] message = Encoding.ASCII.GetBytes(Message);
            try
            {
                foreach (TcpClient i in Server.activeList)
                {
                    stream.Write(message, 0, message.Count());
                }
            }
            catch
            {
            }
        }
        public string RecieveUserName()
        {

            byte[] usernameInput = new byte[30];
            stream.Read(usernameInput, 0, usernameInput.Length);
            string recievedMessageString = Encoding.ASCII.GetString(usernameInput).Replace("\0", string.Empty);
            Console.WriteLine(recievedMessageString);
            return recievedMessageString;

        }
        public string Recieve(Queue<string> messageQueue)
        {

            byte[] recievedMessage = new byte[256];
            try
            {
                stream.Read(recievedMessage, 0, recievedMessage.Length);
            }
            catch
            {
                string recievedAnswer = Server.username + " has logged off";
                Console.WriteLine(recievedAnswer);
                Server.ClientDictionary.Remove(Server.username);
                return recievedAnswer;
            }
            string recievedMessageString = Encoding.ASCII.GetString(recievedMessage).Replace("\0", string.Empty);
            Server.messageQueue.Enqueue(recievedMessageString);
            Console.WriteLine(recievedMessageString);
            messageQueue.Enqueue(recievedMessageString);
            return recievedMessageString;
        }
    }
}
