using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotivUnityPlugin;

public class PMDataTestScript : MonoBehaviour
{
    public dirox.emotiv.controller.DataSubscriber dm;
    public Light directionalLight;

    private float engagement = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DataStreamManager.Instance.GetPMLists().Count > 0) {
            foreach (var ele in DataStreamManager.Instance.GetPMLists()) {
                string chanStr  = ele;
                double data     = DataStreamManager.Instance.GetPMData(ele);

                if(chanStr == "eng" && data > -1) {
                    engagement = (float)data;
                }

                print("Stream: " + chanStr + " " + "Value: " + data);
            }
        }

        directionalLight.transform.eulerAngles = new Vector3(360 * engagement, 0, 0);
        print("ENGAGEMENT: " + engagement);
    }
}
