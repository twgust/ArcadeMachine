using System.Net.Sockets;
using System.Net;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using ArcadeMachineLibrary;
namespace ArcadeMachine
{
    internal class NetworkService

    {
        private string hostIPAddress { get; }
        private int port { get; }

        private Thread t;

     


        public NetworkService(int port, String ip)
        {
            this.port = port;
            this.hostIPAddress = ip; 
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void startServer()
        {
           if(t == null)
            {
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
                t.Abort();
            }
        }
        public void init()
        {
            try
            {
            
                IPAddress ip = IPAddress.Parse(hostIPAddress);
                TcpListener listener = new TcpListener(ip, port);
                listener.Start();
                Debug.WriteLine("Server online...\nAwaiting client connection...");
                TcpClient client = listener.AcceptTcpClient();
                Debug.WriteLine("Client connected!");
               

                
                
 
                IFormatter formatter = new BinaryFormatter();
                    NetworkStream stream = client.GetStream();
                SharedData obj = (SharedData) formatter.Deserialize(stream);
                
               
                Debug.WriteLine("KEY: " + obj.getKey());
                Debug.WriteLine("VALUE: " + obj.getValue());
                    // notify with callback 
                    
                stream.Close();
                client.Close();
                
            }

            catch (SerializationException e)
            {
                Debug.WriteLine(e.Message);         
            }
        }
    }
}

