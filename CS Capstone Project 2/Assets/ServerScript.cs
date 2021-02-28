using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class ServerScript : MonoBehaviour
{
    private static int localport;
    public string ip;
    public int port;
    IPEndPoint remote_endpoint; //oculus endpoint
    UdpClient client; //used to send data
    public PMDataTestScript data_catcher; 
    
    // Start is called before the first frame update
    void Start()
    {
        //setting up a new endpoint
        remote_endpoint = new IPEndPoint(IPAddress.Parse(ip), port);
        client = new UdpClient();
        StartCoroutine(sendAndWait(1f));
    }

    //co-routine (useful for timing)
    private IEnumerator sendAndWait(float waitTime)
    {
        while(true) 
        {
            yield return new WaitForSeconds(waitTime);  //run forever
            string data_to_send = data_catcher.engagement + " " + data_catcher.excitment + " " + data_catcher.focus + " " + data_catcher.interest + " " + data_catcher.relaxation + " " + data_catcher.stress;
            sendString(data_to_send);
        }
    }
    private void sendString(string message) 
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message); //converting string to UTF8
            client.Send(data, data.Length, remote_endpoint);
            print("data sent!");
        }
        catch
        {
            print("data not sent!");
        }
    }
}
