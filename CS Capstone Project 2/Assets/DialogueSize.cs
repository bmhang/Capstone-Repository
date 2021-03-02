using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueSize : MonoBehaviour
{
    public GameObject dialogueChoice1;
    public GameObject dialogueChoice2;
    public GameObject dialogueChoice3;
    public GameObject dialogueChoice4;
    
    float parentWidth;
    GameObject one;
    GameObject two;
    GameObject three;
    GameObject four;
    List<GameObject> objects = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        one = GetChildWithName(dialogueChoice1, "ChoiceText1");
        GetChildWithName(dialogueChoice1, "ChoiceText1.1");
        two = GetChildWithName(dialogueChoice2, "ChoiceText2");
        GetChildWithName(dialogueChoice2, "ChoiceText2.1");
        three = GetChildWithName(dialogueChoice3, "ChoiceText3");
        GetChildWithName(dialogueChoice3, "ChoiceText3.1");
        four = GetChildWithName(dialogueChoice4, "ChoiceText4");
        GetChildWithName(dialogueChoice4, "ChoiceText4.1");
        
        //actual width of the text's container
        parentWidth = one.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        //reset
        foreach(var obj in objects)
        {
            obj.GetComponent<Text>().fontSize = 5;
        }
        one.transform.localPosition = new Vector3(7.07f, -2.83f, 0f);
        two.transform.localPosition = new Vector3(7.07f, -2.83f, 0f);
        three.transform.localPosition = new Vector3(7.07f, -2.83f, 0f);
        four.transform.localPosition = new Vector3(7.07f, -2.83f, 0f);
        
        Text text1 = one.GetComponent<Text>();
        Text text2 = two.GetComponent<Text>();
        Text text3 = three.GetComponent<Text>();
        Text text4 = four.GetComponent<Text>();
        
        //used to see if the font should be shrunk down to 4 or 3
        for(int size = 4; size >= 3; size--) 
        {   
            float textWidth1 = LayoutUtility.GetPreferredWidth(text1.rectTransform);    //width the text would like to be
            float textWidth2 = LayoutUtility.GetPreferredWidth(text2.rectTransform);
            float textWidth3 = LayoutUtility.GetPreferredWidth(text3.rectTransform); 
            float textWidth4 = LayoutUtility.GetPreferredWidth(text4.rectTransform); 
            
            if(textWidth1 > parentWidth || textWidth2 > parentWidth || textWidth3 > parentWidth || textWidth4 > parentWidth) 
            {
                foreach(var obj in objects)
                {
                    obj.GetComponent<Text>().fontSize = size;
                }
                if(size == 3) 
                {
                    one.transform.localPosition = new Vector3(6.90f, -2.83f, 0f);
                    two.transform.localPosition = new Vector3(6.90f, -2.83f, 0f);
                    three.transform.localPosition = new Vector3(6.90f, -2.83f, 0f);
                    four.transform.localPosition = new Vector3(6.90f, -2.83f, 0f);
                }
            }
        }
    }
    private GameObject GetChildWithName(GameObject obj, string name) 
    {
        GameObject child = obj.transform.Find(name).gameObject;
        objects.Add(child);
        return child;
    }
}
