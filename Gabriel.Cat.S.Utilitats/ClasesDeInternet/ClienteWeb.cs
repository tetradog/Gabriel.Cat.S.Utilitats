using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Gabriel.Cat.S.Utilitats.ClasesDeInternet
{//source https://www.c-sharpcorner.com/article/web-proxyserver-in-C-Sharp-and-vb/
    public class ClienteWeb
    {
        const string HTTP_VERSION = "HTTP/1.0";
        const string CRLF = "\r\n";
        private static readonly Encoding ASCII = Encoding.ASCII;

        Socket client;
        public byte[] Read { get; set; }
        private byte[] Buffer { get; set; }


        public byte[] RecvBytes { get; set; }
        private ClienteWeb(int readBuffer = 1024, int recivedBytesBuffer = 4096)
        {
            Read = new byte[readBuffer];
            RecvBytes = new byte[recivedBytesBuffer];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcplistener">Tiene que estar ecuchando</param>
        /// <param name="readBuffer"></param>
        /// <param name="recivedBytesBuffer"></param>
        public ClienteWeb(TcpListener tcplistener, int readBuffer = 1024, int recivedBytesBuffer = 4096) : this(readBuffer, recivedBytesBuffer)
        {
            client = tcplistener.AcceptSocket();
        }
        public ClienteWeb(Socket socket, int readBuffer = 1024, int recivedBytesBuffer = 4096):this(readBuffer,recivedBytesBuffer)
        {
 
            client = socket;
        }
        public void Run()
        {
            int index1;
            int index2;
            string part1;
            int index3;
            int index4;
            int index5;
            IPHostEntry ipHost;
            string[] aliases;
            IPAddress[] address;
            IPEndPoint sEndpoint;
            Socket ipSocket;
            string getMessage;
            byte[] bytesGetMessage;
            int rBytes;
            string strRetPage;
            string sURL;
            string clientmessage = " ";
            
            int bytes = readmessage(Read, ref client, ref clientmessage);
            if (bytes != 0)
            {
                index1 = clientmessage.IndexOf(' ');
                index2 = clientmessage.IndexOf(' ', index1 + 1);
                if ((index1 == -1) || (index2 == -1))
                {
                    throw new IOException();
                }
                Debug.WriteLine("Connecting to Site: {0}", clientmessage.Substring(index1 + 1, index2 - index1));
                Debug.WriteLine("Connection from {0}", client.RemoteEndPoint);
                part1 = clientmessage.Substring(index1 + 1, index2 - index1);
                index3 = part1.IndexOf('/', index1 + 8);
                index4 = part1.IndexOf(' ', index1 + 8);
                index5 = index4 - index3;
                sURL = part1.Substring(index1 + 4, (part1.Length - index5) - 8);
                try
                {
                    ipHost = Dns.GetHostEntry(sURL);
                    Debug.WriteLine("Request resolved: ", ipHost.HostName);
                    aliases = ipHost.Aliases;
                    address = ipHost.AddressList;
                    Debug.WriteLine(address[0]);
                    sEndpoint = new IPEndPoint(address[0], 80);
                    ipSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ipSocket.Connect(sEndpoint);
                    if (ipSocket.Connected)
                        Debug.WriteLine("Socket connect OK");
                    getMessage = clientmessage;
                    bytesGetMessage = ASCII.GetBytes(getMessage);
                    ipSocket.Send(bytesGetMessage, bytesGetMessage.Length, 0);
                    rBytes = ipSocket.Receive(RecvBytes, RecvBytes.Length, 0);
                    Debug.WriteLine("Recieved {0}", +rBytes);
                    //Buffer = RecvBytes;
                    strRetPage = null;
                    strRetPage = strRetPage + ASCII.GetString(RecvBytes, 0, rBytes);
                    while (rBytes > 0)
                    {
                        rBytes = ipSocket.Receive(RecvBytes, RecvBytes.Length, 0);
                        strRetPage = strRetPage + ASCII.GetString(RecvBytes, 0, rBytes);
                    }
                    ipSocket.Shutdown(SocketShutdown.Both);
                    ipSocket.Close();
                    sendmessage(client, strRetPage);
                }
                catch (Exception exc2)
                {
                    Debug.WriteLine(exc2.ToString());
                }
            }

        }

        public async Task RunAsync()
        {
            await new Task(new Action(() => Run()));
        }
        private int readmessage(byte[] ByteArray, ref Socket s, ref string clientmessage)
        {
            int bytes = s.Receive(ByteArray, 1024, 0);
            string messagefromclient = ASCII.GetString(ByteArray);
            clientmessage = messagefromclient;
            return bytes;
        }
        private void sendmessage(Socket s, string message)
        {
            int length;
            Buffer = new byte[message.Length + 1];
            length = ASCII.GetBytes(message, 0, message.Length, Buffer, 0);
            s.Send(Buffer, length, 0);
        }
    }
}
