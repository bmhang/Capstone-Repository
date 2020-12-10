using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChangerScript : MonoBehaviour
{
    public Light ceilingLight1;
    public Light ceilingLight2;
    public Light ceilingLight3;
    public Light ceilingLight4;
    public Light ceilingLight5;
    public Light ceilingLight6;
    public Light mainTableLight;
    public Light crystalLight1;
    public Light crystalLight2;
    public Light crystalLight3;
    public Light crystalLight4;
    public Light spotLight1;
    public Light spotLight2;
    public Light spotLight3;
    public Light ambientLight1;
    public Light ambientLight2;

    public static float ceilingLight1_Intensity;
    public static float ceilingLight2_Intensity;
    public static float ceilingLight3_Intensity;
    public static float ceilingLight4_Intensity;
    public static float ceilingLight5_Intensity;
    public static float ceilingLight6_Intensity;
    public static float mainTableLight_Intensity;
    public static float crystalLight1_Intensity;
    public static float crystalLight2_Intensity;
    public static float crystalLight3_Intensity;
    public static float crystalLight4_Intensity;
    public static float spotLight1_Intensity;
    public static float spotLight2_Intensity;
    public static float spotLight3_Intensity;
    public static float ambientLight1_Intensity;
    public static float ambientLight2_Intensity;

    // Start is called before the first frame update
    void Start()
    {
        ceilingLight1_Intensity = ceilingLight1.intensity;
        ceilingLight2_Intensity = ceilingLight2.intensity;
        ceilingLight3_Intensity = ceilingLight3.intensity;
        ceilingLight4_Intensity = ceilingLight4.intensity;
        ceilingLight5_Intensity = ceilingLight5.intensity;
        ceilingLight6_Intensity = ceilingLight6.intensity;
        mainTableLight_Intensity = mainTableLight.intensity;
        crystalLight1_Intensity = crystalLight1.intensity;
        crystalLight2_Intensity = crystalLight2.intensity;
        crystalLight3_Intensity = crystalLight3.intensity;
        crystalLight4_Intensity = crystalLight4.intensity;
        spotLight1_Intensity = spotLight1.intensity;
        spotLight2_Intensity = spotLight2.intensity;
        spotLight3_Intensity = spotLight3.intensity;
        ambientLight1_Intensity = ambientLight1.intensity;
        ambientLight2_Intensity = ambientLight2.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        ceilingLight1.intensity = ceilingLight1_Intensity;
        ceilingLight2.intensity = ceilingLight2_Intensity;
        ceilingLight3.intensity = ceilingLight3_Intensity;
        ceilingLight4.intensity = ceilingLight4_Intensity;
        ceilingLight5.intensity = ceilingLight5_Intensity;
        ceilingLight6.intensity = ceilingLight6_Intensity;

        //Slowly adjust the brightness of the main table light
        if(mainTableLight_Intensity != mainTableLight.intensity)
        {
            mainTableLight.intensity += mainTableLight_Intensity > mainTableLight.intensity ? 0.01f : -0.01f;
        }

        if (crystalLight3_Intensity != crystalLight3.intensity)
        {
            crystalLight3.intensity += crystalLight3_Intensity > crystalLight3.intensity ? 0.005f : -0.005f;
        }

        if (crystalLight4_Intensity != crystalLight4.intensity)
        {
            crystalLight4.intensity += crystalLight4_Intensity > crystalLight4.intensity ? 0.005f : -0.005f;
        }

        crystalLight1.intensity = crystalLight1_Intensity;
        crystalLight2.intensity = crystalLight2_Intensity;
        spotLight1.intensity = spotLight1_Intensity;
        spotLight2.intensity = spotLight2_Intensity;
        spotLight3.intensity = spotLight3_Intensity;
        ambientLight1.intensity = ambientLight1_Intensity;
        ambientLight2.intensity = ambientLight2_Intensity;
}
}
