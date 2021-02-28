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
    public GameObject dialogueChoice3UI;
    public GameObject dialogueChoice4UI;
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
    private bool romance = false;
    private bool action = false;
    
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
  
                if ((int)currentTextPrintAmount < dialogueArray[currentDialogue].Length) 
                {
                    mainTextObject.text = dialogueArray[currentDialogue].Substring(0, (int)currentTextPrintAmount);
                }

                else
                {
                    mainTextObject.text = dialogueArray[currentDialogue].Substring(0, dialogueArray[currentDialogue].Length);
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

            //Check if a choice needs to be made based on value of currentDialogue (will have to do this for all choices)
            //currentDialogue currently serves as an array index and increments each time the player presses the spacebar.
            if(currentDialogue == 3 && textIsAnimating == false)
            {
                makeChoice = true; //not used yet, but may be useful with future features
                
                //Make the dialogue choices visible to the player
                dialogueChoice1UI.SetActive(true);
                dialogueChoice2UI.SetActive(true);
                dialogueChoice3UI.SetActive(true);

                //Set the text for the dialogue choice
                //if (pmDataCatcher.engagement < 0.5) {}
                dialogueChoice1UI.GetComponentInChildren<Text>().text = "(1) Entertainment";
                dialogueChoice2UI.GetComponentInChildren<Text>().text = "(2) Sports";
                dialogueChoice3UI.GetComponentInChildren<Text>().text = "(3) Personal";
                //else {}

                //Check if the player has pressed a button to indicate which choice to make
                if (Input.GetButtonDown("DialogueChoice1")) //Currently tied to the "1" button
                {
                    print("User chose the first option!");
                    makeChoice = false;
                    textIsAnimating = true;
                    Array.Resize(ref dialogueArray, dialogueArray.Length + 3);
                    currentDialogue++;
                    string[] dialogue = {
                        "Ah, that's a great place to start! What a fantastic idea. Okay, let's see here...",
                        "What is a movie you have watched recently that you really enjoyed?",
                        "Very cool! What genre was it?"};
                    Array.Copy(dialogue, 0, dialogueArray, currentDialogue, 3);
                }

               //else if(Input.GetButtonDown("DialogueChoice2")) //Currently tied to the "2" button {}
            }
            else if(currentDialogue == 6 && textIsAnimating == false) 
            {
                makeChoice = true;
                dialogueChoice1UI.SetActive(true);
                dialogueChoice2UI.SetActive(true);
                dialogueChoice3UI.SetActive(true);
                dialogueChoice4UI.SetActive(true);
                dialogueChoice1UI.GetComponentInChildren<Text>().text = "(1) Romance/Drama";
                dialogueChoice2UI.GetComponentInChildren<Text>().text = "(2) Action/Adventure";
                dialogueChoice3UI.GetComponentInChildren<Text>().text = "(3) Comedy";
                dialogueChoice4UI.GetComponentInChildren<Text>().text = "(4) Horror/Thriller";
                
                if (Input.GetButtonDown("DialogueChoice1")) //Romance/Drama branch - completed
                {
                    print("User chose the first option!");
                    makeChoice = false;
                    textIsAnimating = true;
                    romance = true;
                    currentDialogue++;
                    string[] dialogue = {
                        "Romance and drama are my favorite!",                                               //7
                        "Are you a hopeless romantic like myself or are you in a relationship?",            //8
                        "(1) Hopeless romantic",                                                            //9
                        "Me too! I’m hoping I meet the one soon...",                                        //10
                        "(1) Me too",                                                                       //11
                        "What are you looking for in a partner?",                                           //12
                        "Those are great traits to look for!",                                              //13
                        "(2) I’m content being single for now",                                             //14
                        "That’s great! You have so much freedom to enjoy your single life!",                //15
                        "(2) In a relationship",                                                            //16
                        "Did you watch the movie with your SO?",                                            //17
                        "(1) Yes",                                                                          //18
                        "Did they enjoy it?",                                                               //19
                        "(1) Yes, they loved it!",                                                          //20
                        "That’s awesome! I’m glad you could enjoy it together!",                            //21
                        "(2) Not really, they just agreed to watch it because I wanted to",                 //22
                        "That was nice of them!",                                                           //23
                        "(2) No",                                                                           //24
                        "Let me guess, they aren’t really the romantic type?",                              //25
                        "(1) Not at all",                                                                   //26
                        "Can’t always have the same tastes. You know what they say, opposites attract!",    //27
                        "(2) Sometimes they will watch with me, but not always",                            //28
                        "That’s understandable.",                                                           //29
                        "(3) Actually they love the romance they just weren’t around to watch it with me",  //30
                        "Bummer! You should watch it together sometime!",                                   //31
                        "How long have you been together?",                                                 //32
                        "(1) It’s still pretty new!",                                                       //33
                        "That’s so exciting!",                                                              //34
                        "(2) Several months",                                                               //35
                        "(3) About a year",                                                                 //36
                        "(4) We’ve been together for a while now",                                          //37
                        "That’s awesome!",                                                                  //38
                        "Do you have any advice for single people like me?",                                //39
                        "I will keep that in mind. Thanks!"};                                               //40
                    Array.Resize(ref dialogueArray, dialogueArray.Length + dialogue.Length);
                    Array.Copy(dialogue, 0, dialogueArray, currentDialogue, dialogue.Length);
                }
                else if (Input.GetButtonDown("DialogueChoice2")) //Action/Adventure branch - incomplete
                {
                    print("User chose the second option!");
                    makeChoice = false;
                    textIsAnimating = true;
                    action = true;
                    currentDialogue++;
                    string[] dialogue = {
                        "I love action/adventure movies--especially ones with superheroes!",                //7
                        "I always wanted to have superpowers..."};                                          //8
                    Array.Resize(ref dialogueArray, dialogueArray.Length + dialogue.Length);
                    Array.Copy(dialogue, 0, dialogueArray, currentDialogue, dialogue.Length);
                }
            }
            else if(currentDialogue == 8 && textIsAnimating == false && romance == true)
            {
                int[] arr = {2, 9, 10, 16, 17};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 10 && textIsAnimating == false && romance == true) 
            { 
                int[] arr = {2, 11, 12, 14, 15};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 17 && textIsAnimating == false && romance == true) 
            { 
                int[] arr = {2, 18, 19, 24, 25};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 19 && textIsAnimating == false && romance == true) 
            { 
                int[] arr = {2, 20, 21, 22, 23};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 25 && textIsAnimating == false && romance == true) 
            {
                int[] arr = {3, 26, 27, 28, 29, 30, 31};
                branchingDialogue(arr);
            }
            else if((currentDialogue == 27 || currentDialogue == 29) && textIsAnimating == false && romance == true) 
            {
                currentDialogue = 31;
            }
            else if(currentDialogue == 32 && textIsAnimating == false && romance == true) 
            {
                int[] arr = {4, 33, 34, 35, 38, 36, 38, 37, 38};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 34 && textIsAnimating == false && romance == true)
            {
                currentDialogue = 38;
            }
            else if((currentDialogue == 13 || currentDialogue == 15 || currentDialogue == 40) && textIsAnimating == false && romance == true)
            {
                enabled = false;
            }
            else
            {
                makeChoice = false;

                //Make the dialogue choices invisible to the player
                dialogueChoice1UI.SetActive(false);
                dialogueChoice2UI.SetActive(false);
                dialogueChoice3UI.SetActive(false);
                dialogueChoice4UI.SetActive(false);
            }

            //This controls whether to show the "Press Spacebar" text
            if(makeChoice == false && textIsAnimating == false && enabled == true)
            {
                advanceTextIndicator.SetActive(true);
            }
            else
            {
                advanceTextIndicator.SetActive(false);
            }
        }
    }
    //this is used whenever the user needs to make a decision. so far the game gives the user
    //a max of four choices. index 0 of the input array is the number of choices, and the following
    //are pairs, the first of the pair is the choice and the second is the dialogue that the choice leads to
    void branchingDialogue(int[] arr)
    {
        makeChoice = true;
        dialogueChoice1UI.SetActive(true);
        dialogueChoice2UI.SetActive(true);
        dialogueChoice1UI.GetComponentInChildren<Text>().text = dialogueArray[arr[1]];
        dialogueChoice2UI.GetComponentInChildren<Text>().text = dialogueArray[arr[3]];
        if (Input.GetButtonDown("DialogueChoice1")) 
        {
            print("User chose the first option!");
            makeChoice = false;
            textIsAnimating = true;
            currentDialogue = arr[2];
        }
        if (Input.GetButtonDown("DialogueChoice2")) 
        {
            print("User chose the second option!");
            makeChoice = false;
            textIsAnimating = true;
            currentDialogue = arr[4];
        }
        if(arr[0] == 3 || arr[0] == 4) 
        {
            dialogueChoice3UI.SetActive(true);
            dialogueChoice3UI.GetComponentInChildren<Text>().text = dialogueArray[arr[5]];
            if (Input.GetButtonDown("DialogueChoice3")) 
            {
                print("User chose the third option!");
                makeChoice = false;
                textIsAnimating = true;
                currentDialogue = arr[6];
            }
        }
        if(arr[0] == 4) 
        {
            dialogueChoice4UI.SetActive(true);
            dialogueChoice4UI.GetComponentInChildren<Text>().text = dialogueArray[arr[7]];
            if (Input.GetButtonDown("DialogueChoice4")) 
            {
                print("User chose the fourth option!");
                makeChoice = false;
                textIsAnimating = true;
                currentDialogue = arr[8];
            }
        }
    }
}