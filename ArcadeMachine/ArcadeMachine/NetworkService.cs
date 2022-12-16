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
        private OnGameReceivedCallback startGameCallback;
        

        public NetworkService(int port, string ip, OnGameReceivedCallback callbackImplementation)
        {
            this.port = port;
            this.hostIPAddress = ip;
            this.startGameCallback = callbackImplementation;
            StartServer(ip, port);
        }

        /// <summary>
        /// Starts async server and accepts one incoming client connection (ArcadeMenu) which is terminated on Application Exit.
        /// Recieves game path and title from client (arcade menu) and invokes callbackfunction(title, path) which starts the game.
        /// </summary>
        /// <param name="ipAdr"></param>
        /// <param name="port"></param>
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

                // var for remembering what game was most recently launched
                string tempKey = "";
                while (true)
                {
                    try
                    {
                    #pragma warning disable SYSLIB0011
                    IFormatter formatter = new BinaryFormatter();
                    GameObject obj = formatter.Deserialize(stream) as GameObject;
                    #pragma warning restore SYSLIB0011https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.getstream?view=net-7.0  
                    Debug.WriteLine("[NETWORKSERVICE] >> isDataAvailable: " + stream.DataAvailable);   
                        if (stream.DataAvailable)
                        {
                          //  tempKey = obj.GetKey();
                            var key = obj.GetKey();
                            var value = obj.GetValue();
                            // flush stream and wait so we don't start the same game multiple times.
                            await stream.FlushAsync();
                            Debug.WriteLine("[NETWORKSERVICE] >> New Game [KEY: " + key + ", VALUE:" + value + "]");
                            startGameCallback.StartGame(key, value);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            });
        }
    }
}

