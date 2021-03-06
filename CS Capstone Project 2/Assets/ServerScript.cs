using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class ServerScript : MonoBehaviour
{
    private static int localPort;

    public string IP;
    public int port;

    public bool isClient;

    //connection stuff
    IPEndPoint remoteEndPoint;
    UdpClient client;

    //Emotion Data
    public PMDataTestScript pmDataCatcher;

    private void sendString(string message)
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
        if (!isClient)
        {
            //TO SEND DATA
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
            client = new UdpClient();

            //Start sending data
            StartCoroutine(SendAndWait(1f));
        }
    }

    private IEnumerator SendAndWait(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            string stringToSend = pmDataCatcher.engagement + " " + pmDataCatcher.excitment + " " + pmDataCatcher.focus + " " +
                pmDataCatcher.interest + " " + pmDataCatcher.relaxation + " " + pmDataCatcher.stress;
            sendString(stringToSend);
            print("SENT " + stringToSend);
        }
    }
}