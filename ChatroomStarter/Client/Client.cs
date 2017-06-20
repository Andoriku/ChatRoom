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
            SendUserName();

        }
        public void SendUserName()
        {
            byte[] username = Encoding.ASCII.GetBytes(usernameInput);
            stream.Write(username, 0, username.Count());
            
        }
        public void Send()
        {
            while (clientSocket.Connected == true)
            {
                string messageString = UI.GetInput();
                byte[] message = Encoding.ASCII.GetBytes(messageString);
                stream.Write(message, 0, message.Count());
            }
        }
        public void Recieve()
        {
            while (true)
            {
                byte[] recievedMessage = new byte[256];
                try
                {
                    stream.Read(recievedMessage, 0, recievedMessage.Length);
                }
                catch
                {
                    
                    string recievedAnswer = "User has logged off";
                    Console.WriteLine(recievedAnswer);
                }
                UI.DisplayMessage(Encoding.ASCII.GetString(recievedMessage).Replace("\0", string.Empty), usernameInput);
            }
        }
    }
}
