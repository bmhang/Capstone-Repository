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
    public PMDataTestScript pmDataCatcher;
    public GameObject dialogueChoice1UI;
    public GameObject dialogueChoice2UI;
    public int textAnimateSpeed = 30;

    private string[] dialogueArray = {
        /* THIS IS THE MOST AMOUNT OF TEXT THAT FITS IN THE CURRENT TEXT BOX SIZE:
        * "This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box.",
        */
        //0
        "Good evening, and thank you for joining me here today. It's a pleasure to meet you! I look forward to getting to know you better, especially in this quaint restaurant.",
        //1
        "There sure are a great many things going on in the world right now, aren't there? I'm grateful we're able to meet here without having to worry about anything.",
        //2
        "In fact, this is a very unique restaurant in that it customizes itself to your emotions and mood. There's nothing you need to do. Simply relax and let's have a nice conversation.",
        //3
        "So, now that we're here and waiting for our meal to arrive, what would you like to talk about?"
    };

    private int currentDialogue = 0;
    private bool textIsAnimating = true; //true when the text is animating into the text box
    private bool makeChoice = false; //true when a choice needs to be made by the player
    private float currentTextPrintAmount = 0;
    
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
                if (currentTextPrintAmount < dialogueArray[currentDialogue].Length)
                {
                    currentTextPrintAmount += textAnimateSpeed * Time.deltaTime;
                }

                mainTextObject.text = dialogueArray[currentDialogue].Substring(0, (int)currentTextPrintAmount);

                if (currentTextPrintAmount >= dialogueArray[currentDialogue].Length)
                {
                    textIsAnimating = false;
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
            if(currentDialogue == 3 && textIsAnimating == false)
            {
                makeChoice = true; //not used yet, but may be useful with future features
                
                //Make the dialogue choices visible to the player
                dialogueChoice1UI.SetActive(true);
                dialogueChoice2UI.SetActive(true);

                //Set the text for the dialogue choice
                if (pmDataCatcher.stress > 0.7)
                {
                    dialogueChoice1UI.GetComponentInChildren<Text>().text = "(1) Favorite places to go on vacation.";
                    dialogueChoice2UI.GetComponentInChildren<Text>().text = "(2) Food!";
                }
                else
                {
                    dialogueChoice1UI.GetComponentInChildren<Text>().text = "(1) Work environments.";
                    dialogueChoice2UI.GetComponentInChildren<Text>().text = "(2) Family.";
                }

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
                    string[] dialogue = {
                        "Ah, that's a great place to start! What a fantastic idea. Okay, let's see here...",
                        "One of my favorite places to go to is the Oregon coast. There's a few reasons why. First of all, there are far fewer people there than there are on California beaches!",
                        "Secondly, the sunsets there are absolutely beautiful. I highly recommend traveling there if you have the opportunity.",
                        "dates" };
                    Array.Copy(dialogue, 0, dialogueArray, currentDialogue, 4);
                }
                else if(Input.GetButtonDown("DialogueChoice2")) //Currently tied to the "2" button
                {
                    print("User chose the second option!");
                    currentDialogue++;
                    textIsAnimating = true;
                    //Same thing as above. We need to continue the dialogue in a separate dialogue array.
                    Array.Resize(ref dialogueArray, dialogueArray.Length + 4);
                    string[] dialogue = {"sza", "saba", "denzel", "freddie"};
                    Array.Copy(dialogue, 0, dialogueArray, currentDialogue + 1, 4);
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
