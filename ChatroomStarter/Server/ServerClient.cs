﻿using System;
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
            UserId =  System.Guid.NewGuid().ToString(); 
        }
        public void Send(string Message)
        {
            byte[] message = Encoding.ASCII.GetBytes(Message);
            stream.Write(message, 0, message.Count());
        }
        public string RecieveUserName()
        {

            byte[] usernameInput = new byte[30];
            stream.Read(usernameInput, 0, usernameInput.Length);
            string recievedMessageString = Encoding.ASCII.GetString(usernameInput);
            Console.WriteLine(recievedMessageString);
            return recievedMessageString;

        }
        public string Recieve(Queue<string> messageQueue)
        {

            byte[] recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            string recievedMessageString = Encoding.ASCII.GetString(recievedMessage);
            Console.WriteLine(recievedMessageString);
            messageQueue.Enqueue(recievedMessageString);
            return recievedMessageString;

        }
    }
}
