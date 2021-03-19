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
    public GameObject sphere;
    public GameObject girl;

    private string[] dialogueArray = {
        //THIS IS THE MOST AMOUNT OF TEXT THAT FITS IN THE CURRENT TEXT BOX SIZE:
        //"This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box. This is the first text in the text box.",
        
        //0
        "Good evening, and thank you for joining me here today. It's a pleasure to meet you! I look forward to getting to know you better, especially in this quaint restaurant.",
        //1
        "There sure are a great many things going on in the world right now, aren't there? I'm grateful we're able to meet here without having to worry about anything.",
        //2
        "In fact, this is a very unique restaurant in that it customizes itself to your emotions and mood. There's nothing you need to do. Simply relax and let's have a nice conversation.",
        //3
        "So, now that we're here and waiting for our meal to arrive, what would you like to talk about?"};

    private int currentDialogue = 0;
    private bool textIsAnimating = true;    //true when the text is animating into the text box
    private bool makeChoice = false;        //true when a choice needs to be made by the player
    private float currentTextPrintAmount = 0;
    private bool affirmation = false;
    private bool a1, a2, a3, a4 = false;
    private bool comfort = false;
    private bool entertainment = false;
    private bool romance, action, comedy = false;
    
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
            if(textIsAnimating)
            {
                currentTextPrintAmount += textAnimateSpeed * Time.deltaTime;
  
                if((int)currentTextPrintAmount < dialogueArray[currentDialogue].Length) 
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

            //Check if the "space" key has been pressed to finish text scroll early for convenience
            if(Input.GetButtonDown("Jump") && textIsAnimating == true)
            {
                mainTextObject.text = dialogueArray[currentDialogue];
                textIsAnimating = false;
                currentTextPrintAmount = 0;
            }

            //Check if the "space" key has been pressed to advance to next text
            else if(Input.GetButtonDown("Jump") && textIsAnimating == false && makeChoice == false)
            {
                currentDialogue++;
                advanceTextIndicator.SetActive(false);
                textIsAnimating = true;
            }

            //Check if a choice needs to be made based on value of currentDialogue (will have to do this for all choices)
            //currentDialogue currently serves as an array index and increments each time the player presses the spacebar.
            if(currentDialogue == 3 && textIsAnimating == false)
            {
                makeChoice = true;

                Vector3 p = sphere.transform.localPosition;
                //print(p);
                
                //if stressed or relaxed
                if((p.x < 0 && p.y >= 0 && p.z < 0) || (p.x >= 0 && p.y < 0 && p.z >= 0))              
                {
                    dialogueChoice1UI.SetActive(true);
                    dialogueChoice2UI.SetActive(true);
                    dialogueChoice1UI.GetComponentInChildren<Text>().text = "Affirmations";
                    dialogueChoice2UI.GetComponentInChildren<Text>().text = "Comfort topics";
                    
                    if(Input.GetButtonDown("DialogueChoice1")) //Affirmation branch - complete
                    {
                        print("User chose the first option!");
                        affirmation = true;
                        string[] dialogue = {
                            "Ah, that's a great place to start! What a fantastic idea. Okay, let's see here...",    //4
                            "What affirmation resonates with you the most?"};                                       //5
                        setBranch(dialogue);
                    }
                    else if(Input.GetButtonDown("DialogueChoice2")) 
                    {
                        print("User chose the second option!");
                        comfort = true;
                    }
                }
                else
                {
                    //Make the dialogue choices visible to the player
                    dialogueChoice1UI.SetActive(true);
                    dialogueChoice2UI.SetActive(true);
                    dialogueChoice3UI.SetActive(true);
                    dialogueChoice1UI.GetComponentInChildren<Text>().text = "Entertainment";
                    dialogueChoice2UI.GetComponentInChildren<Text>().text = "Sports";
                    dialogueChoice3UI.GetComponentInChildren<Text>().text = "Personal";
                    
                    //Check if the player has pressed a button to indicate which choice to make
                    if(Input.GetButtonDown("DialogueChoice1"))     //Currently tied to the "1" button
                    {
                        print("User chose the first option!");
                        entertainment = true;
                        string[] dialogue = {
                            "Ah, that's a great place to start! What a fantastic idea. Okay, let's see here...",    //4
                            "What is a movie you have watched recently that you really enjoyed?",                   //5
                            "Very cool! What genre was it?"};                                                       //6
                        setBranch(dialogue);    
                    }
                    else if(Input.GetButtonDown("DialogueChoice2")) //Currently tied to the "2" button 
                    {
                        print("User chose the second option!");
                    }
                    else if(Input.GetButtonDown("DialogueChoice3")) //Currently tied to the "3" button 
                    {
                        print("User chose the third option!");
                    }
                }
            }
            else if(currentDialogue == 5 && textIsAnimating == false && affirmation == true) 
            {
                makeChoice = true;
                
                dialogueChoice1UI.SetActive(true);
                dialogueChoice2UI.SetActive(true);
                dialogueChoice3UI.SetActive(true);
                dialogueChoice4UI.SetActive(true);
                
                dialogueChoice1UI.GetComponentInChildren<Text>().text = "Don't sweat the small stuff";
                dialogueChoice2UI.GetComponentInChildren<Text>().text = "I am in charge of how I feel and today I am choosing happiness";
                dialogueChoice3UI.GetComponentInChildren<Text>().text = "I am enough";
                dialogueChoice4UI.GetComponentInChildren<Text>().text = "I have the power to create change";
                
                if(Input.GetButtonDown("DialogueChoice1")) //'Don't sweat the small stuff' branch - complete
                {
                    print("User chose the first option!");
                    a1 = true;
                    string[] dialogue = {
                        "That’s amazing~ It’s so important to decrease the amount of stress in your life. Even the smallest slights alter our bodies.",                     //6
                        "You're doing great because you recognize this affirmation as something important to you.",                                                         //7
                        "What are some ways that help you decrease your stress?",                                                                                           //8                                                         
                        "Practice mindfulness",                                                                                                                             //9
                        "Wonderful~ Can you tell me more about how you practice mindfulness?",                                                                              //10
                        "I love that! You're focusing on yourself and deflecting any negative thoughts.",                                                                   //11
                        "Give yourself a time limit",                                                                                                                       //12
                        "Change your perspective",                                                                                                                          //13
                        "Wonderful~ I'd love to hear you talk about it!",                                                                                                   //14
                        "Is there any advice you would like to give to me?",                                                                                                //15
                        "Yes",                                                                                                                                              //16
                        "Please do tell!",                                                                                                                                  //17
                        "Thank you~ I'll keep that in mind.",                                                                                                               //18
                        "No",                                                                                                                                               //19
                        "That’s alright~ We can move on to something else.",                                                                                                //20
                        "None of the above",                                                                                                                                //21
                        "That’s okay~ It’s never too late! Let's talk about ways to help you decrease stress.",                                                             //22
                        "Which one are you interested in learning about?",                                                                                                  //23
                        "Practice mindfulness",                                                                                                                             //24
                        "Mindfulness is a simple activity that allows you to deflect any negative thoughts you may be having.",                                             //25
                        "By being aware of your thoughts -- and noticing them in a non judgemental way --  you can condition yourself to examine them before reacting to them.",    //26
                        "Give yourself a time limit",                                                                                                                               //27
                        "It’s important to allow yourself time to assess the thing that is causing you stress.",                                                                    //28 
                        "Set a base time of 5 minutes before moving on to the problem and examine the emotions that come with your reflection.",                                    //29
                        "Change your perspective",                                                                                                                                  //30
                        "Remind yourself that you are already remarkably resilient. Also, in order to build and improve your mental stamina, it's important to view things objectively for what they are.", //31
                        "Remember that stress, like situations, come and go -- so whatever you're stressing about now will eventually become a distant memory.",                                            //32
                        "Now then, tell me something you appreciate about yourself?"                                                                                                                        //33
                    };
                    setBranch(dialogue);
                }
                else if(Input.GetButtonDown("DialogueChoice2")) //'I am in charge' branch - complete
                {
                    print("User chose the second option!");
                    a2 = true;
                    string[] dialogue = {
                        "That’s amazing~ It’s so important to remember that while you may not be able to control what happens, you can control how you react and feel.",
                        "You're doing great because you recognize this affirmation as something important to you.",                                   
                        "What are some ways that you practice choosing happiness?",                                                                                                                       
                        "Choose to focus on what you have, not on what you don’t have",                                                                 
                        "Wonderful~ Can you tell me more about how you do that?",                                                                       
                        "I love that! You're focusing on yourself and deflecting any negative thoughts.",                                              
                        "Choose to take care of your body",                                                                                             
                        "Choose to let go when you know you should",                                                                                    
                        "Wonderful~ I would love to hear you talk about it!",                                                                           
                        "Is there any advice you would like to give to me?",                                                                            
                        "Yes",                                                                                                                          
                        "Please do tell!",                                                                                                              
                        "Thank you~ I will keep that in mind.",                                                                                          
                        "No",                                                                                                                           
                        "That’s alright~ We can move on to something else.",                                                                            
                        "None of the above",                                                                                                            
                        "That’s okay~ It’s never too late! We can talk about some ways to help you on your journey of choosing happiness.",              
                        "Which one are you interested in learning about?",                                                                              
                        "Choose to focus on what you have, not on what you don’t have",                                                                 
                        "Appreciate what you have -- focusing on what and who you currently have in your life will bring you closer toward contentment. Stressing about what you don’t have will distract you and lead you down a path filled with negative energy.",  
                        "Take in your accomplishments, you have done amazing things!",                                                                 
                        "Choose to take care of your body",                                                                                            
                        "Taking care of yourself both mentally and physically is crucial to your journey of finding your own happiness. If you lack physical energy, then your mental, emotional, and spiritual energy will be negatively affected.",  
                        "Work for yourself so that you can gain a higher sense of self-accomplishment and self-worth. You are worth it!",                           
                        "Choose to let go when you know you should",                                                                                   
                        "Ultimately you have to be strong for yourself. Love is worth fighting for, but you can’t be the only one fighting. People need to fight for you too.", 
                        "Holding on is being brave, but letting go and moving on is often what makes you stronger and happier.",                      
                        "Now then, tell me something you appreciate about yourself?" 
                    };
                    setBranch(dialogue);
                }
                else if(Input.GetButtonDown("DialogueChoice3")) //'I am enough' branch - complete
                {
                    a3 = true;
                    print("User chose the third option!");
                    string[] dialogue = {
                        "That’s amazing~ It’s so important to acknowledge that you're worthy of self-love and of achieving the things you want in life. You are enough just the way you are now.",
                        "You're doing great because you recognize this affirmation as something important to you.",                                 
                        "What are some things that you do to help remind yourself that you are enough?",                                                                                                   
                        "Think of self-development as a journey, not a race",                                                                           
                        "Wonderful~ Can you tell me more about how you do that?",                                                                       
                        "I love that! You're focusing on yourself and deflecting any negative thoughts.",                                              
                        "Ground yourself by turning to your 'source' (music/books, family/friends, independence, etc)",                              
                        "Remind yourself that you are lovely the way you are",                                                                          
                        "Wonderful~ I would love to hear you talk about it!",                                                                          
                        "Is there any advice you would like to give to me?",                                                                            
                        "Yes",                                                                                                                         
                        "Please do tell!",                                                                                                              
                        "Thank you~ I will keep that in mind.",                                                                                         
                        "No",                                                                                                                          
                        "That’s alright~ We can move on to something else.",                                                                            
                        "None of the above",                                                                                                            
                        "That’s okay~ It’s never too late! Here are some ways of reminding yourself that you are enough.",                              
                        "Which one are you interested in learning about?",                                                                              
                        "Think of self-development as a journey, not a race",                                                                          
                        "Self-development is not a to-do list. Although setting specific goals and due dates help keep you accountable, it can also make you feel that you are incomplete until you reach the end of the list.", 
                        "Everyone's list of goals and dreams is ever-changing and endless. That's why it's important to see your development as a journey rather than a destination. Remember to focus on the process of your progress that you make during the journey.", 
                        "Ground yourself by turning to your 'source'",                                                                                   
                        "You can feel a sense of confusion about who you truly are throughout the process of changing yourself and stepping out of your comfort zone. It’s important to turn to sources that have always inspired and sustained you throughout your life.",    
                        "Music, family, love, independence, nature, and even change itself -- these could be the sources that help maintain the continuation of your past, present, and future. You needed these experiences to be where you are today, so don't forget about them.", 
                        "Remind yourself that you are lovely the way you are",                                                                        
                        "You are just as deserving of love from yourself and others, without changing yourself. Oftentimes people become fixated on their “ideal” selves, but this can be distracting and even detrimental.", 
                        "The “real you” is the present you, and is the one who needs your love. You have always been doing the best you can, and that’s why you are where you need to be today.",   
                        "Now then, tell me something you appreciate about yourself?" 
                    };
                    setBranch(dialogue);
                }
                else if(Input.GetButtonDown("DialogueChoice4")) //'I have the power to create change' branch - complete
                {
                    a4 = true;
                    print("User chose the third option!");
                    string[] dialogue = {
                        "That’s amazing~ It’s so important to remind yourself that you have the power to ignite and create change.",
                        "There are times when you find yourself surrounded by change, and most of the time you may feel powerless to do anything about any of it. You are not powerless, you have the power to make a difference!",                                   
                        "How do you use your personal power to affect change every day?",                                                                                                                      
                        "Avoid becoming overwhelmed and take care of yourself",                                                  
                        "Wonderful~ Can you tell me more about how you manage to do all that?",                                                        
                        "I love that! You're focusing on yourself and deflecting any negative thoughts.",                                            
                        "Practice self-compassion, patience, and/or positive self-reflection",                                                          
                        "Be resilient and avoid discouragement",                                                                                        
                        "Wonderful~ I would love to hear you talk about it!",                                                                           
                        "Is there any advice you would like to give to me?",                                                                            
                        "Yes",                                                                                                                          
                        "Please do tell!",                                                                                                              
                        "Thank you~ I will keep that in mind.",                                                                                          
                        "No",                                                                                                                           
                        "That’s alright~ We can move on to something else.",                                                                            
                        "None of the above",                                                                                                            
                        "That’s okay~ It’s never too late! Here are some ways to help remind yourself that you have the power to create change.",
                        "Which one are you interested in learning about?",
                        "Avoid becoming overwhelmed, prioritize your time, and take care of yourself",
                        "Focus on what you can affect; take time to look at the positive outcomes from challenging situations. Make sure you are leaving time for true self-nourishment", 
                        "Having the power to make change also includes remaining committed to the practices that keep you grounded and connected to your ture, unchanging self.", 
                        "Practice self-compassion, patience, and/or positive self-reflection",
                        "We often see only what we “could have done” or “should have done better” which can be discouraging. By reflecting on what you did and why it helped will create a positive mental release.", 
                        "Consistent self-reflection will increase your chances of being proactive in the future and can improve your efficacy.",  
                        "Be resilient and avoid discouragement",
                        "Change won’t happen overnight. Remember that adversity does not always denote a lack of progress.",    
                        "You are planting seeds that will ultimately bloom into positive change!",
                        "Now then, tell me something you appreciate about yourself?" 
                    };
                    setBranch(dialogue);
                }
            }
            else if(currentDialogue == 8 && textIsAnimating == false  && affirmation == true) 
            {
                int[] arr = {4, 9, 10, 12, 14, 13, 14, 21, 22}; 
                branchingDialogue(arr);
            }
            else if(currentDialogue == 11 && textIsAnimating == false  && affirmation == true) 
            {
                currentDialogue = 14;
            }
            else if(currentDialogue == 15 && textIsAnimating == false  && affirmation == true) 
            {
                int[] arr = {2, 16, 17, 19, 20}; 
                branchingDialogue(arr);
            }
            else if((currentDialogue == 18 || currentDialogue == 20|| currentDialogue == 26 || currentDialogue == 29) && textIsAnimating == false  && affirmation == true) 
            {
                currentDialogue = 32;
            }
            else if(currentDialogue == 23 && textIsAnimating == false  && affirmation == true) 
            {
                int[] arr = {3, 24, 25, 27, 28, 30, 31}; 
                branchingDialogue(arr);
            }
            else if(currentDialogue == 33 && textIsAnimating == false  && affirmation == true) 
            {
                string[] dialogue = {
                    "That’s great to hear~ You have a deep understanding of yourself and what makes you happy.",                                                //34
                    "It takes a lot of courage to express yourself out loud. You’re amazing!",                                                                  //35
                    "If you don't mind, I'd like for us to take a nice deep breath together before we continue our discussion. Ready? Inhale: 1… 2… 3… 4… 5…",  //36                        
                    "Now exhale: 5… 4… 3… 2… 1…",                                                                                                               //37            
                    "Would you like for us to go over positive affirmations together?",                                                                         //38
                    "Yes please",                                                                                                                               //39
                    "I'll be going over four affirmations -- feel free to repeat after me if you’d like.",                                                      //40
                };
                addDialogue(dialogue);
                
                if(a1 == true) {
                    string[] dialogue2 = {
                        "'I am deserving of love.'",                                                                                                //41
                        "'I am powerful.'",                                                                                                         //42
                        "'I can achieve anything.'",                                                                                                //43
                        "'I can overcome any obstacle, big or small.'",                                                                             //44
                        "No, thank you",                                                                                                            //45
                        "Alright then, I'd like to take a moment now to thank you for going on this journey with me~ Keep doing what you're doing, everyone grows at their own pace. You are where you need to be."   //46
                    };
                    addDialogue(dialogue2);
                }
                else if(a2 == true) {
                    string[] dialogue2 = {                                                                             
                        "'I am deserving of love.'",                                                                                                      
                        "'I strive for progress, not perfection.'",                                                                                       
                        "'I believe in myself.'",                                                                                                         
                        "'This is my life.'",                                                                                                             
                        "No, thank you",                                                                                                               
                        "Alright then, I'd like to take a moment now to thank you for going on this journey with me~ Keep doing what you're doing, everyone grows at their own pace. You are where you need to be." 
                    };
                    addDialogue(dialogue2);
                }
                else if(a3 == true) {
                    string[] dialogue2 = {                                                                                
                        "'I am deserving of love.'",                                                                                                      
                        "'No one is perfect.'",                                                                                                           
                        "'There is no one else like me.'",                                                                                                 
                        "'I am better and stronger than I believe myself to be.'",                                                                        
                        "No, thank you",                                                                                                               
                        "Alright then, I'd like to take a moment now to thank you for going on this journey with me~ Keep doing what you're doing, everyone grows at their own pace. You are where you need to be."
                    };
                    addDialogue(dialogue2);
                } 
                else if(a4 == true) {
                    string[] dialogue2 = {                                                                              
                        "'I am deserving of love.'",                                                                                                       
                        "'Change doesn’t happen in a day, it happens with every little habit.'",                                                           
                        "'My life is a reflection of my mind.'",                                                                                           
                        "'Change means progress, and progress means happiness.'",                                                                          
                        "No, thank you",                                                                                                                
                        "Alright then, I'd like to take a moment now to thank you for going on this journey with me~ Keep doing what you're doing, everyone grows at their own pace. You are where you need to be." 
                    };
                    addDialogue(dialogue2);
                }
            }
            else if(currentDialogue == 38 && textIsAnimating == false  && affirmation == true) 
            {
                int[] arr = {2, 39, 40, 45, 46}; 
                branchingDialogue(arr);
            }
            else if(currentDialogue == 44 && textIsAnimating == false  && affirmation == true) 
            {
                currentDialogue = 45;
            }
            else if(currentDialogue == 46 && textIsAnimating == false  && affirmation == true) 
            {
                enabled = false;
            }
            //-------------------------------------------------------------------------------------------------------------
            else if(currentDialogue == 6 && textIsAnimating == false  && entertainment == true) 
            {
                makeChoice = true;
                dialogueChoice1UI.SetActive(true);
                dialogueChoice2UI.SetActive(true);
                dialogueChoice3UI.SetActive(true);
                dialogueChoice4UI.SetActive(true);
                dialogueChoice1UI.GetComponentInChildren<Text>().text = "Romance/Drama";
                dialogueChoice2UI.GetComponentInChildren<Text>().text = "Action/Adventure";
                dialogueChoice3UI.GetComponentInChildren<Text>().text = "Comedy";
                dialogueChoice4UI.GetComponentInChildren<Text>().text = "Documentary";
                
                if(Input.GetButtonDown("DialogueChoice1")) //Romance/Drama branch - completed
                {
                    print("User chose the first option!");
                    romance = true;
                    string[] dialogue = {
                        "Romance and drama are my favorite!",                                               //7
                        "Are you a hopeless romantic like myself or are you in a relationship?",            //8
                        "Hopeless romantic",                                                                //9
                        "Me too! I’m hoping I meet the one soon...",                                        //10
                        "Me too",                                                                           //11
                        "What are you looking for in a partner?",                                           //12
                        "Those are great traits to look for!",                                              //13
                        "I’m content being single for now",                                                 //14
                        "That’s great! You have so much freedom to enjoy your single life!",                //15
                        "In a relationship",                                                                //16
                        "Did you watch the movie with your SO?",                                            //17
                        "Yes",                                                                              //18
                        "Did they enjoy it?",                                                               //19
                        "Yes, they loved it!",                                                              //20
                        "That’s awesome! I’m glad you could enjoy it together!",                            //21
                        "Not really, they just agreed to watch it because I wanted to",                     //22
                        "That was nice of them!",                                                           //23
                        "No",                                                                               //24
                        "Let me guess, they aren’t really the romantic type?",                              //25
                        "Not at all",                                                                       //26
                        "Can’t always have the same tastes. You know what they say, opposites attract!",    //27
                        "Sometimes they will watch with me, but not always",                                //28
                        "That’s understandable.",                                                           //29
                        "Actually they love the romance they just weren’t around to watch it with me",      //30
                        "Bummer! You should watch it together sometime!",                                   //31
                        "How long have you been together?",                                                 //32
                        "It’s still pretty new!",                                                           //33
                        "That’s so exciting!",                                                              //34
                        "Several months",                                                                   //35
                        "About a year",                                                                     //36
                        "We’ve been together for a while now",                                              //37
                        "That’s awesome!",                                                                  //38
                        "Do you have any advice for single people like me?",                                //39
                        "I will keep that in mind. Thanks!"                                                 //40
                    };                                              
                    setBranch(dialogue);
                }
                else if(Input.GetButtonDown("DialogueChoice2")) //Action/Adventure branch - complete
                {
                    print("User chose the second option!");
                    action = true;
                    string[] dialogue = {
                        "I love action/adventure movies -- especially ones with superheroes!",                                                                                                      //7
                        "I always wanted to have superpowers...",                                                                                                                                   //8
                        "Same!",                                                                                                                                                                    //9
                        "I’d love to have the ability to teleport. Being able to be anywhere in the world -- Paris, the Pyramids, Pizza Hut -- with just a snap of my fingers... What about you?",  //10
                        "Time traveling sounds nice",                                                                                                                                               //11
                        "The past or the future -- which one are you more interested in visiting?",                                                                                                 //12
                        "Past",                                                                                                                                                                     //13
                        "Future",                                                                                                                                                                   //14
                        "Future looks bleak (you know, because of global warming and everything), but I'd personally love to go back in time and visit Leonardo as he paints the Mona Lisa.",       //15
                        "Shapeshifting would be awesome",                                                                                                                                           //16
                        "What would you like to shapeshift into?",                                                                                                                                  //17
                        "Now that you mention it, shapeshifting would be a very nice power to have. Being human can be exhausting -- it’d be nice to have the ability to shapeshift into something like a bird. I got two papers due tomorrow -- I'd love to fly away from these responsibilities.", //18 
                        "I want immortality",                                                                                                                                                       //19
                        "You seriously want to live forever? What for?",                                                                                                                            //20
                        "Interesting... Personally, I know life is short, but I prefer it that way. I don’t want to live forever.",                                                                 //21
                        "I already have a superpower…",                                                                                                                                             //22
                        "Seriously?? What can you do that’s so special?",                                                                                                                           //23
                        "I can fly!",                                                                                                                                                               //24
                        "I can teleport!",                                                                                                                                                          //25
                        "Oh really? Prove it.",                                                                                                                                                     //26
                        "I’m waiting…",                                                                                                                                                             //27
                        "I was joking…",                                                                                                                                                            //28
                        "No, I like being ordinary",                                                                                                                                                //29
                        "Nothing wrong with being ordinary. I feel like society places too much emphasis on being ‘special’ and ‘extraordinary.’",                                                  //30
                        "You know what, I actually like talking to you -- you seem decent. I’m going to tell you something I haven’t told anyone else…",                                            //31
                        "I actually have a superpower; I have the ability to disappear!",                                                                                                           //32
                        "You’ve got to show me!",                                                                                                                                                   //33
                        "I’m no fool, that’s impossible",                                                                                                                                           //34    
                        "Be right back!",                                                                                                                                                           //35
                        "~mwahaha",                                                                                                                                                                 //36
                        "...                              ",                                                                                                                                        //37
                        "Where did she go?",                                                                                                                                                        //38
                        "A glitch in the matrix...?",                                                                                                                                               //39                
                        "~Did you miss me?",                                                                                                                                                        //40
                        "See, I told you! But this is top secret -- this has to stay between you and me."                                                                                           //41
                    };                                         
                    setBranch(dialogue);
                }
                else if(Input.GetButtonDown("DialogueChoice3")) //Comedy branch - complete? kinda short
                {
                    print("User chose the third option!");
                    comedy = true;
                    string[] dialogue = {
                        "I love comedy! Do you know any good jokes?",               //7
                        "Yes",                                                      //8
                        "What is it?",                                              //9
                        "Hahaha, that’s a good one!",                               //10
                        "No",                                                       //11
                        "I’ve got one for you!",                                    //12
                        "Did you hear about the claustrophobic astronaut?",         //13
                        "He just needed a little space.",                           //14
                        "Hahahah! Do you want to hear another?",                    //15
                        "Yes",                                                      //16
                        "What do you call a parade of rabbits hopping backwards?",  //17
                        "A receding hare-line!",                                    //18
                        "No",                                                       //19
                        "Not a big fan of my jokes!? That’s ok, I’m not very punny!",   //20
                        "Do you like stand-up comedy?",                                 //21
                        "Love it!",                                                     //22
                        "Me too! Who is your favorite comedian?",                       //23
                        "That’s awesome! Mine is Kevin Hart",                           //24
                        "One of my favorite Kevin Hart quotes is “Ever argue with a female and, in the middle of the argument, you no longer feel safe because of her actions? She may start pacing back and forth real fast, breathing out her nose. You know what my girl do? When she get mad, she start talking in the third person. That’s scary as hell because that’s her way of telling me that from this point on, she is not responsible for none of her actions.”",  //25
                        "Not really",                                                    //26
                        "Really? You should give Dave Chappelle a chance --  he's one of my favorite comedians. I don't know anyone who doesn't enjoy his skits." //27
                    };
                    setBranch(dialogue);
                }
            }
            else if(currentDialogue == 7 && textIsAnimating == false && comedy == true)
            {
                int[] arr = {2, 8, 9, 11, 12};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 10 && textIsAnimating == false && comedy == true)
            {
                currentDialogue = 11;
            }
            else if(currentDialogue == 15 && textIsAnimating == false && comedy == true)
            {
                int[] arr = {2, 16, 17, 19, 20};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 18 && textIsAnimating == false && comedy == true)
            {
                currentDialogue = 20;
            }
            else if(currentDialogue == 21 && textIsAnimating == false && comedy == true)
            {
                int[] arr = {2, 22, 23, 26, 27};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 27 && textIsAnimating == false && comedy == true)
            {
                enabled = false;
            }
            //-------------------------------------------------------------------------------------------------------------
           else if(currentDialogue == 8 && textIsAnimating == false && action == true)
            {
                int[] arr = {3, 9, 10, 22, 23, 29, 30};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 10 && textIsAnimating == false && action == true)
            {
                int[] arr = {3, 11, 12, 16, 17, 19, 20};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 12 && textIsAnimating == false && action == true)
            {
                int[] arr = {2, 13, 15, 14, 15};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 23 && textIsAnimating == false && action == true)
            {
                int[] arr = {3, 24, 26, 25, 26, 28, 31};
                branchingDialogue(arr);
            }
            else if((currentDialogue == 15 || currentDialogue == 18 || currentDialogue == 21 || currentDialogue == 27 || currentDialogue == 28) && textIsAnimating == false && action == true) 
            {
                currentDialogue = 30;
            }
            else if(currentDialogue == 32 && textIsAnimating == false && action == true)
            {
                int[] arr = {2, 33, 35, 34, 35};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 36 && textIsAnimating == false && action == true)
            {
                girl.SetActive(false);
            }
            else if(currentDialogue == 37 && textIsAnimating == false && action == true)
            {
                int[] arr = {2, 38, 40, 39, 40};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 40 && textIsAnimating == false && action == true)
            {
                girl.SetActive(true);
            }
            else if(currentDialogue == 41 && textIsAnimating == false && action == true)
            {
                enabled = false;
            }
            //-------------------------------------------------------------------------------------------------------------
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
            else if((currentDialogue == 21 || currentDialogue == 23 || currentDialogue == 27 || currentDialogue == 29) && textIsAnimating == false && romance == true) 
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
    //-------------------------------------------------------------------------------------------------------------
    private void addDialogue(string[] dialogue) 
    {
        int index = dialogueArray.Length;
        Array.Resize(ref dialogueArray, dialogueArray.Length + dialogue.Length);
        Array.Copy(dialogue, 0, dialogueArray, index, dialogue.Length);
    }

    //this is used whenever an important choice is made and
    //there is more dialogue that needs to be added to dialogueArray
    private void setBranch(string[] dialogue) 
    {
        makeChoice = false;
        textIsAnimating = true;
        currentDialogue++;
        Array.Resize(ref dialogueArray, dialogueArray.Length + dialogue.Length);
        Array.Copy(dialogue, 0, dialogueArray, currentDialogue, dialogue.Length);
    }
    
    //this is used whenever the user needs to make a decision. so far the game gives the user
    //a max of four choices. index 0 of the input array is the number of choices, and the following
    //are pairs, the first of the pair is the choice and the second is the dialogue that the choice leads to
    private void branchingDialogue(int[] arr)
    {
        makeChoice = true;
        dialogueChoice1UI.SetActive(true);
        dialogueChoice2UI.SetActive(true);
        dialogueChoice1UI.GetComponentInChildren<Text>().text = dialogueArray[arr[1]];
        dialogueChoice2UI.GetComponentInChildren<Text>().text = dialogueArray[arr[3]];
        if(Input.GetButtonDown("DialogueChoice1")) 
        {
            print("User chose the first option!");
            makeChoice = false;
            textIsAnimating = true;
            currentDialogue = arr[2];
        }
        if(Input.GetButtonDown("DialogueChoice2")) 
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
            if(Input.GetButtonDown("DialogueChoice3")) 
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
            if(Input.GetButtonDown("DialogueChoice4")) 
            {
                print("User chose the fourth option!");
                makeChoice = false;
                textIsAnimating = true;
                currentDialogue = arr[8];
            }
        }
    }
}