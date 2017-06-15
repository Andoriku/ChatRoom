using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client
    {
        public TcpClient clientSocket;
        NetworkStream stream;
        public string usernameInput;
        public Client(string IP, int port)
        {
            usernameInput = UI.GetUsername();
            clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse(IP), port);
            stream = clientSocket.GetStream();
             
        }
        public void SendUserName()
        {
            byte[] username = Encoding.ASCII.GetBytes(usernameInput);
            stream.Write(username, 0, username.Count());
        }
        public void Send()
        {
            string messageString = UI.GetInput();
            Console.Clear();
            byte[] message = Encoding.ASCII.GetBytes(messageString);
            stream.Write(message, 0, message.Count());
        }
        public void Recieve()
        {
            byte[] recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            UI.DisplayMessage(Encoding.ASCII.GetString(recievedMessage),usernameInput);
        }
    }
}
