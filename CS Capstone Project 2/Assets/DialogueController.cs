using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    /// <summary>
    /// This script animates the text, allows text advancement, and (eventually) allows choices to be made in the dialogue
    /// </summary>

    public bool isDialogueActive = false;
    public Text mainTextObject;
    public GameObject advanceTextIndicator;
    public int textAnimateSpeed = 30;

    private string[] dialogueArray = {
        /* THIS IS THE MOST AMOUNT OF TEXT THAT FITS IN THE CURRENT TEXT BOX SIZE:
        * "This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box.",
        */

        "Once upon a time, there was a small, happy text box that lived in a weird void. The text inside this text box consisted of placeholder text that was created for the purpose of filling space. In fact, this current amount of text will fill a large amount of the space.",

        "However, despite that problem, this text box will always do its best to deliver the best text possible, no matter what happens. That is its sole purpose for existence: to share its text with the world and make this restaurant a better place.",

        "Here is one more thing the text box has to say: Have a nice day! There is no more text, so pressing the spacebar will now throw an error."
    };

    private int currentDialogue = 0;
    private bool textIsAnimating = true; //true when the text is animating into the text box
    private float currentTextPrintAmount = 0;
    private int endOfLineCutoff = 65;
    
    // Start is called before the first frame update
    void Start()
    {
        //Always hide the advanceTextIndicator at first
        advanceTextIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDialogueActive)
        {
            //Slowly print the text into the textbox (like in a visual novel)
            if (textIsAnimating)
            {
                currentTextPrintAmount += textAnimateSpeed * Time.deltaTime;

                //Preventing the text from jumping from one line to the next if it's too long
                if (currentTextPrintAmount < dialogueArray[currentDialogue].Length - 1 && dialogueArray[currentDialogue][(int)currentTextPrintAmount].Equals(' '))
                {
                    int endOfNextWord = dialogueArray[currentDialogue].IndexOf(" ", (int)currentTextPrintAmount + 1);

                    if (endOfNextWord > currentTextPrintAmount)
                    {
                        //print("Next word is: " + dialogueArray[currentDialogue].Substring((int)currentTextPrintAmount, endOfNextWord - (int)currentTextPrintAmount));

                        if((int)currentTextPrintAmount + (endOfNextWord - (int)currentTextPrintAmount) > endOfLineCutoff)
                        {
                            //Push the word onto the next line if the text will run over the endOfLineCutoff value
                            dialogueArray[currentDialogue] = dialogueArray[currentDialogue].Remove((int)currentTextPrintAmount, 1).Insert((int)currentTextPrintAmount, "\n");
                            endOfLineCutoff += 65;
                        }
                    }
                }

                mainTextObject.text = dialogueArray[currentDialogue].Substring(0, (int)currentTextPrintAmount);

                if (currentTextPrintAmount >= dialogueArray[currentDialogue].Length)
                {
                    textIsAnimating = false;
                    advanceTextIndicator.SetActive(true);
                    endOfLineCutoff = 65;
                    currentTextPrintAmount = 0;
                }
            }

            //Check if the "space" key has been pressed
            if (Input.GetButtonDown("Jump") && textIsAnimating == false)
            {
                currentDialogue++;
                advanceTextIndicator.SetActive(false);
                textIsAnimating = true;
            }
        }
    }
}
