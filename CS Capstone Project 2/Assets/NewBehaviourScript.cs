using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public PMDataTestScript pmDataCatcher;

    private int frame = 0;
    private float[] excitement = new float[3]; //set to larger array if using real time data
    private float[] frames = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if(frame ==0 || excitement[frame-1] != pmDataCatcher.excitment) //deal with delay from simulator
            {
                frames[frame] = frame;
                excitement[frame] = pmDataCatcher.excitment;

                //if full array, calculate regression and change light accordingly
                if (frame == 2)
                {
                    float slope = Regression(frames, excitement);
                    LightChangerScript.mainTableLight_Intensity = LightChangerScript.mainTableLight_Intensity + (slope * 100);//experiment to find good factor
                    print("Slope: " + slope);
                    frame = -1;
                }

                frame++;
                print("Frame: " + frame);
            } 
    }

    float Regression(float[] xdata, float[] ydata)
    {
        int length = Math.Min(xdata.Length, ydata.Length);
        float sumX = 0;
        float sumX2 = 0;
        float sumY = 0;
        float sumXY = 0;
        for(int i=0; i<length; i++)
        {
            sumX += xdata[i];
            sumX2 += (xdata[i] * xdata[i]);
            sumY += ydata[i];
            sumXY += (xdata[i] * ydata[i]);
        }
        float slope = ((length * sumXY) - (sumX * sumY)) / ((length * sumX2) - (sumX *sumX));
        return slope;
    }
}
