using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotivUnityPlugin;

public class PMDataTestScript : MonoBehaviour
{
    public dirox.emotiv.controller.DataSubscriber dm;
    public Light directionalLight;

    private float engagement = -1;
    private float excitment = -1;
    private float focus = -1;
    private float interest = -1;
    private float relaxation = -1;
    private float stress = -1;

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

                if(chanStr ==  "exc" && data > -1) {
                    excitment = (float)data;
                }

                if(chanStr == "foc" && data > -1) {
                    focus = (float)data;
                }

                if(chanStr == "int" && data > -1) {
                    interest = (float)data;
                }

                if(chanStr ==  "rel" && data > -1) {
                    relaxation = (float)data;
                }

                if(chanStr == "str" && data > -1) {
                    stress = (float)data;
                }

                print("Stream: " + chanStr + " " + "Value: " + data);
            }
        }

        directionalLight.transform.eulerAngles = new Vector3(360 * engagement, 0, 0);
        print("ENGAGEMENT: " + engagement);
        print("EXCITMENT: " + excitment);
        print("FOCUS: " + focus);
        print("INTEREST: " + interest);
        print("RELAXATION: " + relaxation);
        print("STRESS: " + stress);
    }
}