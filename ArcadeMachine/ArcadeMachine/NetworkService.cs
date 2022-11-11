using ArcadeMachineLibrary;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace ArcadeMachine
{
  
    internal class NetworkService
    {
        private string hostIPAddress { get; }
        private int port { get; }
        private Thread t;
        private OnGameReceivedCallback startGameCallback;

        public NetworkService(int port, String ip, OnGameReceivedCallback callbackImplementation)
        {
            this.port = port;
            this.hostIPAddress = ip;
            this.startGameCallback = callbackImplementation;
        }

        
        public void startServer()
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void stop()
        {
            if(t != null)
            {
                t.Interrupt();
            }
        }



        /// <summary>
        /// Method <c>init</c> starts the server and receives a message from client. 
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
                TcpClient client = listener.AcceptTcpClient();

                Debug.WriteLine("Client connected!");

                // deserialization of shared class <c>ArcadeMachineLibrary.SharedData</c>
                IFormatter formatter = new BinaryFormatter();
                NetworkStream stream = client.GetStream();
                SharedData obj = (SharedData) formatter.Deserialize(stream);
              
                Debug.WriteLine("KEY: " + obj.getKey());
                Debug.WriteLine("VALUE: " + obj.getValue());

                // upon successfull deserialization, notify controller with callback function
                // passing the values from SharedData into the callback func. 
                startGameCallback.StartGame(obj.getKey(), obj.getValue());


                // notify with callback 

            }
            catch (SerializationException e)
            {                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.ToString());
            }
        }
    }
}

