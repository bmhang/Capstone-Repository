using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueController : MonoBehaviour
{
    /// <summary>
    /// This script animates the text, allows text advancement, and (eventually) allows choices to be made in the dialogue
    /// </summary>

    public bool isDialogueActive = false;
    public Text mainTextObject;
    public GameObject advanceTextIndicator;
    public GameObject dialogueChoice1UI;
    public GameObject dialogueChoice2UI;
    public int textAnimateSpeed = 30;

    private string[] dialogueArray = {
        /* THIS IS THE MOST AMOUNT OF TEXT THAT FITS IN THE CURRENT TEXT BOX SIZE:
        * "This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box.",
        */
        //0
        "Once upon a time, there was a small, happy text box that lived in a weird void. The text inside this text box consisted of placeholder text that was created for the purpose of filling space. In fact, this current amount of text will fill a large amount of the space.",
        //1
        "However, despite that problem, this text box will always do its best to deliver the best text possible, no matter what happens. That is its sole purpose for existence: to share its text with the world and make this restaurant a better place.",
        //2
        "Here is one more thing the text box has to say: Have a nice day!",
        //3
        "Once this is finished, a choice will have been made and the text will continue on a different branch! There is no more text, so pressing the spacebar will now throw an error."
    };

    private int currentDialogue = 0;
    private bool textIsAnimating = true; //true when the text is animating into the text box
    private bool makeChoice = false; //true when a choice needs to be made by the player
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
                    endOfLineCutoff = 65;
                    currentTextPrintAmount = 0;
                }
            }

            //Check if the "space" key has been pressed
            if (Input.GetButtonDown("Jump") && textIsAnimating == false && makeChoice == false)
            {
                currentDialogue++;
                advanceTextIndicator.SetActive(false);
                textIsAnimating = true;
            }

            //--------------------FOR POOJA-----------------------
            //Check if a choice needs to be made based on value of currentDialogue (will have to do this for all choices)
            //currentDialogue currently serves as an array index and increments each time the player presses the spacebar.
            if(currentDialogue == 2 && textIsAnimating == false)
            {
                makeChoice = true; //not used yet, but may be useful with future features
                
                //Make the dialogue choices visible to the player
                dialogueChoice1UI.SetActive(true);
                dialogueChoice2UI.SetActive(true);

                //Set the text for the dialogue choice
                dialogueChoice1UI.GetComponentInChildren<Text>().text = "(1) This is the first choice.";
                dialogueChoice2UI.GetComponentInChildren<Text>().text = "(2) This is the second choice.";

                //Check if the player has pressed a button to indicate which choice to make
                if (Input.GetButtonDown("DialogueChoice1")) //Currently tied to the "1" button
                {
                    print("User chose the first option!");
                    currentDialogue++;
                    textIsAnimating = true;
                    //Here, we need to figure out how to branch the dialogue while keeping the currentDialogue
                    //variable incrementing up. You may want to try making another dialogue array and offsetting
                    //the value of currentDialogue so we can keep using it as an array index?
                    Array.Resize(ref dialogueArray, dialogueArray.Length + 4);
                    dialogueArray[currentDialogue+1] = "apple";
                    currentDialogue++;
                    dialogueArray[currentDialogue+1] = "banana";
                }
                else if(Input.GetButtonDown("DialogueChoice2")) //Currently tied to the "2" button
                {
                    print("User chose the second option!");
                    currentDialogue++;
                    textIsAnimating = true;
                    //Same thing as above. We need to continue the dialogue in a separate dialogue array.
                    Array.Resize(ref dialogueArray, dialogueArray.Length + 4);
                    dialogueArray[currentDialogue+1] = "sza";
                    currentDialogue++;
                    dialogueArray[currentDialogue+1] = "saba";
                }
            }
            else
            {
                makeChoice = false;

                //Make the dialogue choices invisible to the player
                dialogueChoice1UI.SetActive(false);
                dialogueChoice2UI.SetActive(false);
            }

            //This controls whether to show the "Press Spacebar" text
            if(makeChoice == false && textIsAnimating == false)
            {
                advanceTextIndicator.SetActive(true);
            }
            else
            {
                advanceTextIndicator.SetActive(false);
            }

        }
    }
}
