// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ArcadeMachineLibrary;

ExecuteClient();

static void ExecuteClient()
{
    try
    {
        TcpClient client = new TcpClient("127.0.0.1", 11111);
        SharedData data = new SharedData("nameofthegame", "C:\\Users\\sweet\\Desktop\\gamesdir\\game\\My project.exe");
        NetworkStream stream = client.GetStream();
        IFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Flush();
        stream.Close();
      
    }
    catch (Exception e)
    {
        Debug.WriteLine(e.ToString());
        
    }
}
