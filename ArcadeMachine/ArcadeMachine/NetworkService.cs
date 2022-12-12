using ArcadeMachineLibrary;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SharedDataDLL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArcadeMachine
{
  
    internal class NetworkService
    {
        private string hostIPAddress { get; }
        private int port { get; }
        private Thread t;
        private OnGameReceivedCallback startGameCallback;
        private TcpClient tcpClient;
        

        public NetworkService(int port, string ip, OnGameReceivedCallback callbackImplementation)
        {
            this.port = port;
            this.hostIPAddress = ip;
            this.startGameCallback = callbackImplementation;
            StartServer(ip, port);
        }

        /// <summary>
        /// deprecated
        /// </summary>
        public void startServerOLD()
        {

           if(t == null)
            {
                Debug.WriteLine("attempting to start...");
                t = new Thread(init);
                t.Start();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// deprecated
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void stopServerOLD()
        {
            if(t != null)
            {
                t.Interrupt();
            }
        }


      private void StartServer(string ipAdr, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAdr);
            TcpListener server = new TcpListener(ip, port);
            server.Start();
            Task<TcpClient> clientel = server.AcceptTcpClientAsync();
            Task.Run(async () =>
            {
                TcpClient client = clientel.GetAwaiter().GetResult();
                NetworkStream stream =  client.GetStream();

                // we need to remember what the client last sent to check for duplicates
                string tempKey = "";
                while (true)
                {
                    try
                    {

                    #pragma warning disable SYSLIB0011
                    IFormatter formatter = new BinaryFormatter();
                    GameObject obj = formatter.Deserialize(stream) as GameObject;
                    #pragma warning restore SYSLIB0011 https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.getstream?view=net-7.0  
                        Debug.WriteLine(stream.DataAvailable);

                        // returns true / false
                        if (stream.DataAvailable)
                        {
                            //debugging purposes, no functional difference between the two conditionals.
                            if (!tempKey.Equals(obj.GetKey())) {      
                            tempKey = obj.GetKey();
  
                            Debug.WriteLine("Initial message received >> [KEY: " + obj.GetKey() + ", VALUE:" + obj.GetValue() +"]" );
                            stream.FlushAsync().Wait();
                            startGameCallback.StartGame(obj.GetKey(), obj.GetValue());
                        }

                        //debugging purposes, no functional difference between the two conditionals.
                        else if(tempKey.Equals(obj.GetKey()))
                            {
                                Debug.WriteLine("Same as last time >> [KEY: " + obj.GetKey() + ", VALUE: " + obj.GetValue() + "]");
                                startGameCallback.StartGame(obj.GetKey(), obj.GetValue());

                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.StackTrace);
                    }
                }
            });
        }

       
        /// <summary>
        /// DEPRECATED - Method <c>init</c> starts the server and receives a message from client. 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void init()
        {
            try
            {
                IPAddress ip = IPAddress.Parse(hostIPAddress);
                TcpListener listener = new TcpListener(ip, port);
                listener.Start();
                Debug.WriteLine("Server online @" + ip + ":" + port + "...\nAwaiting client connection...");

                while (true)
                {
                    this.tcpClient = listener.AcceptTcpClient();
                    Debug.WriteLine("Client connected!");
                    IFormatter formatter = new BinaryFormatter();
                    NetworkStream stream = this.tcpClient.GetStream();

                    #pragma warning disable SYSLIB0011
                    GameObject obj = formatter.Deserialize(this.tcpClient.GetStream()) as GameObject;
                    #pragma warning restore SYSLIB0011 https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.getstream?view=net-7.0

                    Debug.WriteLine("KEY: " + obj.GetKey());
                    Debug.WriteLine("VALUE: " + obj.GetValue());
                    //startGameCallback.StartGame(obj.GetKey(), obj.GetValue());
                } }
            catch (SerializationException e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }
        }
    }

}

