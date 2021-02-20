using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InternetTestScript : MonoBehaviour
{
    public Text textObject;

    IEnumerator checkInternetConnection(Action<bool> action)
    {
        UnityWebRequest www = new UnityWebRequest("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }
    void Start()
    {
        Debug.Log(Application.internetReachability.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(checkInternetConnection((isConnected) => {
            //textObject.text = isConnected ? "Internet connected" : "No internet connection";
        }));
    }
}
