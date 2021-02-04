using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerScript : MonoBehaviour
{
    /// <summary>A script that adjusts light color. Set a light's color with a Vector4, e.g. color = new Vector4(0.5f, 1f, 0f) for RGBA.</summary>

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

    public static Vector4 ceilingLight1_Color;
    public static Vector4 ceilingLight2_Color;
    public static Vector4 ceilingLight3_Color;
    public static Vector4 ceilingLight4_Color;
    public static Vector4 ceilingLight5_Color;
    public static Vector4 ceilingLight6_Color;
    public static Vector4 mainTableLight_Color;
    public static Vector4 crystalLight1_Color;
    public static Vector4 crystalLight2_Color;
    public static Vector4 crystalLight3_Color;
    public static Vector4 crystalLight4_Color;
    public static Vector4 spotLight1_Color;
    public static Vector4 spotLight2_Color;
    public static Vector4 spotLight3_Color;
    public static Vector4 ambientLight1_Color;
    public static Vector4 ambientLight2_Color;

    // Start is called before the first frame update
    void Start()
    {
        ceilingLight1_Color = ceilingLight1.color;
        ceilingLight2_Color = ceilingLight2.color;
        ceilingLight3_Color = ceilingLight3.color;
        ceilingLight4_Color = ceilingLight4.color;
        ceilingLight5_Color = ceilingLight5.color;
        ceilingLight6_Color = ceilingLight6.color;
        mainTableLight_Color = mainTableLight.color;
        crystalLight1_Color = crystalLight1.color;
        crystalLight2_Color = crystalLight2.color;
        crystalLight3_Color = crystalLight3.color;
        crystalLight4_Color = crystalLight4.color;
        spotLight1_Color = spotLight1.color;
        spotLight2_Color = spotLight2.color;
        spotLight3_Color = spotLight3.color;
        ambientLight1_Color = ambientLight1.color;
        ambientLight2_Color = ambientLight2.color;
    }

    // Update is called once per frame
    void Update()
    {
        ceilingLight1.color = ceilingLight1_Color;
        ceilingLight2.color = ceilingLight2_Color;
        ceilingLight3.color = ceilingLight3_Color;
        ceilingLight4.color = ceilingLight4_Color;
        ceilingLight5.color = ceilingLight5_Color;
        ceilingLight6.color = ceilingLight6_Color;
        mainTableLight.color = mainTableLight_Color;
        crystalLight3.color = crystalLight3_Color;
        crystalLight4.color = crystalLight4_Color;
        crystalLight1.color = crystalLight1_Color;
        crystalLight2.color = crystalLight2_Color;
        spotLight1.color = spotLight1_Color;
        spotLight2.color = spotLight2_Color;
        spotLight3.color = spotLight3_Color;
        ambientLight1.color = ambientLight1_Color;
        ambientLight2.color = ambientLight2_Color;
    }
}
