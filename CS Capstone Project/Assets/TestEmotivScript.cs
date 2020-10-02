using System.Collections;
using System.Collections.Generic;
using EmotivUnityPlugin;
using UnityEngine;

public class TestEmotivScript : MonoBehaviour
{
    List<Headset> headsets = new List<Headset>();
    bool headsetFound = false;
    bool headsetSubscribed = false;
    bool isConnectDone = false;
    ConnectToCortexStates _lastState;

    const float TIME_QUERY_HEADSET = 2.0f;
    float _timerCounter_queryHeadset = 0;

    void Start()
    {
        DataStreamManager.Instance.StartAuthorize("");
        // StartCoroutine(checkForHeadsets());
    }

    // Update is called once per frame
    void Update()
    {
        _timerCounter_queryHeadset += Time.deltaTime;
        if(_timerCounter_queryHeadset > TIME_QUERY_HEADSET) {
            _timerCounter_queryHeadset -= TIME_QUERY_HEADSET;
            List<Headset> detectedHeadset = DataStreamManager.Instance.GetDetectedHeadsets();
            print(detectedHeadset.Count);
        }

        if(isConnectDone)
            return;

        var curState = DataStreamManager.Instance.GetConnectToCortexState();
        if (_lastState == curState)
                return;

        // curState = ConnectToCortexStates.License_HardLimited; // TODO: only for test now
        _lastState = curState;
        switch (curState) {
            case ConnectToCortexStates.Service_connecting: {
                // _stateText.text = "Connecting To service..."; // TODO: check font size
                Debug.Log("=============== Connecting To service");
                break;
            }
            case ConnectToCortexStates.EmotivApp_NotFound: {
                Debug.Log("=============== Connect_failed");
                break;
            }
            case ConnectToCortexStates.Login_waiting: {
                Debug.Log("=============== Login_waiting");
                break;
            }
            case ConnectToCortexStates.Login_notYet: {
                Debug.Log("=============== Login_notYet");
                break;
            }
            case ConnectToCortexStates.Authorizing: {
                Debug.Log("=============== Authorizing");
                break;
            }
            case ConnectToCortexStates.Authorize_failed: {
                Debug.Log("=============== Authorize_failed");
                break;
            }
            case ConnectToCortexStates.Authorized: {
                isConnectDone = true;
                Debug.Log("=============== Authorized");
                break;
            }
            case ConnectToCortexStates.LicenseExpried: {
                isConnectDone = true;
                Debug.Log("=============== Trial expired");
                break;
            }
            case ConnectToCortexStates.License_HardLimited: {
                isConnectDone = true;
                Debug.Log("=============== License_HardLimited");
                break;
            }
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        DataStreamManager.Instance.Stop();
    }

    public void clickToQuery() {
        DataStreamManager.Instance.QueryHeadsets();
    }

    IEnumerator checkForHeadsets() {
        yield return new WaitForSecondsRealtime(10f);
        while(true) {

            if (!headsetFound) {
                headsets = DataStreamManager.Instance.GetDetectedHeadsets();
                if(headsets.Count > 0) {
                    headsetFound = true;
                }
                else {
                    DataStreamManager.Instance.QueryHeadsets();
                }
            }

            if(headsets.Count > 0 && !headsetFound) {
                print(headsets[0].HeadsetID + headsets[0].HeadsetType);
                headsetFound = true;
            }
            

            if(headsetFound && !headsetSubscribed) {
                List<string> dataStreamList = new List<string>(){DataStreamName.DevInfos, DataStreamName.EEG};
                DataStreamManager.Instance.StartDataStream(dataStreamList, headsets[0].HeadsetID);
                headsetSubscribed = true;
            }

            if(headsetSubscribed) {
                // print(_dataStream.GetEEGChannels().Count);
            }

            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
