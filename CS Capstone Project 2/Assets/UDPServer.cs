using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class UDPServer : MonoBehaviour
{
    private static int localPort;

    public string IP;
    public int port;

    //connection stuff
    IPEndPoint remoteEndPoint;
    UdpClient client;

    private void sendString (string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint);
            print("Data sent!");
        }
        catch
        {
            print("String was not able to be sent!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //TO SEND DATA
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();

        //Start sending data
        StartCoroutine(SendAndWait(1f));
    }

    private IEnumerator SendAndWait(float waitTime)
    {
        int currentMessage = 1;

        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            sendString("This is test message #" + currentMessage);
            currentMessage++;
        }
    }
}
