using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAdjuster : MonoBehaviour
{
    /// <summary>A test script that adjusts lights based on the emotion data.</summary>

    //get the emotion data
    public PMDataTestScript pmDataCatcher;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Change main table intensity based on stress
        //stress value is always between 0 and 1
        //we want intensity between 0 and 30
        //LightChangerScript.mainTableLight_Intensity = 40 - (20 * pmDataCatcher.stress + 10);

        //change the brightness of the wall lights to indicate stress level
        //LightChangerScript.crystalLight3_Intensity = 3 * (1 - pmDataCatcher.stress);
        //LightChangerScript.crystalLight4_Intensity = 3 * (1 - pmDataCatcher.stress);

        //Testing changes in color
        float t = Mathf.PingPong(Time.time, 5) / 5;
        ColorChangerScript.mainTableLight_Color = new Vector4(0f, t, 0f);

    }
}
