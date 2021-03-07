using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHighlighter : MonoBehaviour
{
    public GameObject dialogueChoice1;
    public GameObject dialogueChoice2;
    public GameObject dialogueChoice3;
    public GameObject dialogueChoice4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("OculusATouch"))
        {
            dialogueChoice4.GetComponentInChildren<Image>().color = new Vector4(0.3f, 0.3f, 0.3f, 1);
        }
        if(Input.GetButtonUp("OculusATouch"))
        {
            dialogueChoice4.GetComponentInChildren<Image>().color = new Vector4(1, 1, 1, 1);
        }

        if (Input.GetButtonDown("OculusBTouch"))
        {
            dialogueChoice2.GetComponentInChildren<Image>().color = new Vector4(0.3f, 0.3f, 0.3f, 1);
        }
        if (Input.GetButtonUp("OculusBTouch"))
        {
            dialogueChoice2.GetComponentInChildren<Image>().color = new Vector4(1, 1, 1, 1);
        }

        if (Input.GetButtonDown("OculusXTouch"))
        {
            dialogueChoice3.GetComponentInChildren<Image>().color = new Vector4(0.3f, 0.3f, 0.3f, 1);
        }
        if (Input.GetButtonUp("OculusXTouch"))
        {
            dialogueChoice3.GetComponentInChildren<Image>().color = new Vector4(1, 1, 1, 1);
        }

        if (Input.GetButtonDown("OculusYTouch"))
        {
            dialogueChoice1.GetComponentInChildren<Image>().color = new Vector4(0.3f, 0.3f, 0.3f, 1);
        }
        if (Input.GetButtonUp("OculusYTouch"))
        {
            dialogueChoice1.GetComponentInChildren<Image>().color = new Vector4(1, 1, 1, 1);
        }
    }
}
