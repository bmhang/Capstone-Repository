using System.Collections;
using System.Collections.Generic;
using EmotivUnityPlugin;
using UnityEngine;

public class TestEmotivScript : MonoBehaviour
{
    DataStreamManager _dataStream = DataStreamManager.Instance;

    void Start()
    {
        _dataStream.StartAuthorize("");
        StartCoroutine(checkForHeadsets());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator checkForHeadsets() {
        while(true) {
            List<Headset> detectedHeadset = _dataStream.GetDetectedHeadsets();
            print(detectedHeadset.Count);
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
