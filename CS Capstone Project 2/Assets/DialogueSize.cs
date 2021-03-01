using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSize : MonoBehaviour
{
    public GameObject dialogueChoice1UI;
    public GameObject dialogueChoice2UI;
    public GameObject dialogueChoice3UI;
    public GameObject dialogueChoice4UI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text text1 = dialogueChoice1UI.GetComponentInChildren<Text>();
        Text text2 = dialogueChoice2UI.GetComponentInChildren<Text>();
        Text text3 = dialogueChoice3UI.GetComponentInChildren<Text>();
        Text text4 = dialogueChoice4UI.GetComponentInChildren<Text>();

        float textWidth1 = LayoutUtility.GetPreferredWidth(text1.rectTransform);                    //width the text would like to be
        float textWidth2 = LayoutUtility.GetPreferredWidth(text2.rectTransform);
        float textWidth3 = LayoutUtility.GetPreferredWidth(text3.rectTransform); 
        float textWidth4 = LayoutUtility.GetPreferredWidth(text4.rectTransform); 

        float parentWidth1 = dialogueChoice1UI.GetComponentInChildren<RectTransform>().rect.width;  //actual width of the text's parent container
        float parentWidth2 = dialogueChoice2UI.GetComponentInChildren<RectTransform>().rect.width;
        float parentWidth3 = dialogueChoice3UI.GetComponentInChildren<RectTransform>().rect.width;
        float parentWidth4 = dialogueChoice4UI.GetComponentInChildren<RectTransform>().rect.width;

        if(textWidth1 > parentWidth1 || textWidth2 > parentWidth2 || textWidth3 > parentWidth3 || textWidth4 > parentWidth4) {
            dialogueChoice1UI.GetComponentInChildren<Text>().fontSize = 3;
            dialogueChoice2UI.GetComponentInChildren<Text>().fontSize = 3;
            dialogueChoice3UI.GetComponentInChildren<Text>().fontSize = 3;
            dialogueChoice4UI.GetComponentInChildren<Text>().fontSize = 3;
        }
    }
}
