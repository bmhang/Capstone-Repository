using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotivUnityPlugin;

public class PMDataTestScript : MonoBehaviour
{
    /// <summary>Collects Performance Metric data from the Emotiv Data Subscriber and hides the UICamera once PM data has been subscribed to.</summary>
 
    public dirox.emotiv.controller.DataSubscriber dm;
    public Light directionalLight;
    public Camera UICamera; //This is the camera that is attached to the Emotiv Canvas
    public DialogueController dialogueController;

    public float engagement = -1;
    public float excitment = -1;
    public float focus = -1;
    public float interest = -1;
    public float relaxation = -1;
    public float stress = -1;

    private bool dataStreamWorking = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Collect the Performance Metric Data
        if(DataStreamManager.Instance.GetPMLists().Count > 0) {
            foreach (var ele in DataStreamManager.Instance.GetPMLists()) {
                string chanStr  = ele;
                double data     = DataStreamManager.Instance.GetPMData(ele);

                if(chanStr == "eng" && data > -1) {
                    engagement = (float)data;
                    dataStreamWorking = true;
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
            }
        }

        //Hide the UICamera and start the dialogue system once data has started streaming from the headset
        if(dataStreamWorking)
        {
            UICamera.gameObject.SetActive(false);
            dialogueController.isDialogueActive = true;
        }

        directionalLight.transform.eulerAngles = new Vector3(360 * engagement, 0, 0); //TEMP
        print("ENGAGEMENT: " + engagement);
        print("EXCITMENT: " + excitment);
        print("FOCUS: " + focus);
        print("INTEREST: " + interest);
        print("RELAXATION: " + relaxation);
        print("STRESS: " + stress);
    }
}