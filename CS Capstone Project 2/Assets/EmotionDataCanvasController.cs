using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionDataCanvasController : MonoBehaviour
{
    public PMDataTestScript emotionData;
    public Text engagementText;
    public Text excitementText;
    public Text focusText;
    public Text interestText;
    public Text relaxationText;
    public Text stressText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        engagementText.text = "" + emotionData.engagement * 100 + "%";
        excitementText.text = "" + emotionData.excitment * 100 + "%";
        focusText.text = "" + emotionData.focus * 100 + "%";
        interestText.text = "" + emotionData.interest * 100 + "%";
        relaxationText.text = "" + emotionData.relaxation * 100 + "%";
        stressText.text = "" + emotionData.stress * 100 + "%";
    }
}
