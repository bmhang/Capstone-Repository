using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public PMDataTestScript pmDataCatcher;

    private int numData = 0; //how many data points are currently stored
    private static int n = 4; //number of points to collect before calculating slope
    private float[] relaxation = new float[n];
    private float[] frames = new float[n];
    private int previousChange = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if(pmDataCatcher.relaxation != -1 && (numData == 0 || relaxation[numData-1] != pmDataCatcher.relaxation)) //deal with delay from simulator
            {
                //frames[numData] = Time.deltaTime; Doesn't give right time
                frames[numData] = numData;
                relaxation[numData] = pmDataCatcher.relaxation;

                //if full array, calculate regression and change light accordingly
                if (numData == n-1)
                {
                    float slope = Regression(frames, relaxation);
                    
                    if (slope > 0.01)
                    {
                        LightChangerScript.mainTableLight_Intensity = LightChangerScript.mainTableLight_Intensity + (previousChange * slope * 100);//experiment to find good factor
                        LightChangerScript.crystalLight1_Intensity = LightChangerScript.crystalLight1_Intensity + (previousChange * slope * 100);
                        previousChange = 1;
                    }
                    else if(slope < -.01)
                    {
                        LightChangerScript.mainTableLight_Intensity = LightChangerScript.mainTableLight_Intensity + (previousChange * slope * 100);//experiment to find good factor
                        LightChangerScript.crystalLight1_Intensity = LightChangerScript.crystalLight1_Intensity + (previousChange * slope * 100);
                        previousChange = -1;
                    }
                    print("Slope: " + slope);
                    numData = -1;
                }

                numData++;
                print("Frame: " + numData);
                for (int i = 0; i < n; i++)
                {
                    print("Relaxation" + i + ": " + relaxation[i]);
                    print("Time" + i + frames[i]);
                }
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
