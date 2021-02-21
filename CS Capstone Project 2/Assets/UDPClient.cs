using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPClient : MonoBehaviour
{
    // DEBUG
    public Text textObject;

    //the text that is received
    public string receivedText = "0 0 0 0 0 0";

    //receiving Thread
    Thread receiveThread;

    //udpclient object
    UdpClient client;

    //port we are listening at
    public int port;

    // Start is called before the first frame update
    void Start()
    {
        //Create a thread to receive incoming messages
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    void Update()
    {
        //DEBUG TO TEXT SCREEN
        textObject.text = receivedText;
    }

    //receive thread
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                receivedText = Encoding.UTF8.GetString(data);
            }
            catch
            {
                print("Error encountered while receiving data.");
            }
        }
    }
}
