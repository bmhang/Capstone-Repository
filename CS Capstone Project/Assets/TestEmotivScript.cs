using System.Collections;
using System.Collections.Generic;
using EmotivUnityPlugin;
using UnityEngine;

public class TestEmotivScript : MonoBehaviour
{
    List<Headset> headsets = new List<Headset>();
    bool headsetFound = false;
    bool headsetSubscribed = false;

    void Start()
    {
        DataStreamManager.Instance.StartAuthorize("");
        // StartCoroutine(checkForHeadsets());
    }

    // Update is called once per frame
    void Update()
    {

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
