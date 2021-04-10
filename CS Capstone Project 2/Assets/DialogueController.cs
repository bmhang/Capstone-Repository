using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

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
        "Good evening, and thank you for joining me here today. It's a pleasure to meet you! I look forward to getting to know you better, especially in this quaint restaurant.",              //0
        "There sure are a great many things going on in the world right now, aren't there? I'm grateful we're able to meet here without having to worry about anything.",                       //1
        "In fact, this is a very unique restaurant in that it customizes itself to your emotions and mood. There's nothing you need to do. Simply relax and let's have a nice conversation."    //2
    };
    private string[] done = {
        "*ding*",
        "Well, it seems like our food is ready!", 
        "I enjoyed our conversation. I'd love to continue it some other time; for now, let's dig in!"
    };
    private int currentDialogue = 0;
    private bool textIsAnimating = true;    //true when the text is animating into the text box
    private bool makeChoice = false;        //true when a choice needs to be made by the player
    private float currentTextPrintAmount = 0;
    private bool affirmation = false;
    private bool a1, a2, a3, a4 = false;
    private bool comfort = false;
    private bool riddles, personality, easy = false;
    private bool entertainment = false;
    private bool romance, action, comedy, documentary = false;
    private bool terminate = false;
    private bool finish = false;
    private bool start = false;
    private bool inEntertainment = false;
    private bool inRelax = false;
    SortedList<string, string[]> d = new SortedList<string, string[]>();
    SortedList<string, string[]> d2 = new SortedList<string, string[]>();

    // Start is called before the first frame update
    void Start()
    {
        //Always hide the advanceTextIndicator at first
        advanceTextIndicator.SetActive(false);
        string[] romanceDialogue = {
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
            "They just weren’t around to watch it with me",                                     //30
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
        d.Add("Romance/Drama", romanceDialogue);
        
        string[] actionDialogue = {
            "I love action/adventure movies -- especially ones with superheroes!",                                                                                                      //7
            "I always wanted to have superpowers...",                                                                                                                                   //8
            "Same!",                                                                                                                                                                    //9
            "I’d love to have the ability to teleport. Being able to be anywhere in the world -- Paris, the Pyramids, Pizza Hut -- with just a snap of my fingers... What about you?",  //10
            "Time traveling sounds nice",                                                                                                                                               //11
            "The past or the future -- which one are you more interested in visiting?",                                                                                                 //12
            "Past",                                                                                                                                                                     //13
            "Future",                                                                                                                                                                   //14
            "Personally, since the future looks bleak (you know, because of global warming and everything), I'd prefer to go back in time and visit Leonardo as he paints the Mona Lisa.",  //15
            "Shapeshifting would be awesome",                                                                                                                                           //16
            "What would you like to shapeshift into?",                                                                                                                                  //17
            "Interesting! Now that you mention it, shapeshifting would be a very nice power to have. Being human can be exhausting -- it’d be nice to have the ability to shapeshift into something like a bird. I got two papers due tomorrow -- I'd love to fly away from these responsibilities.", //18 
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
        d.Add("Action", actionDialogue);
       
        string[] comedyDialogue = {
            "I love comedy! Do you know any good jokes?",               //7
            "Yes",                                                      //8
            "What is it?",                                              //9
            "Hahaha, that’s a good one!",                               //10
            "No",                                                       //11
            "I’ve got one for you!",                                    //12
            "Did you hear about the claustrophobic astronaut?",         //13
            "He just needed a little space!",                           //14
            "That was a good one, am I right? Do you want to hear another?",  //15
            "Yes",                                                      //16
            "What do you call a parade of rabbits hopping backwards?",  //17
            "A receding hare-line!",                                    //18
            "No",                                                       //19
            "Not a big fan of my jokes!? That’s ok, I’m not very punny!",   //20
            "Do you like stand-up comedy?",                                 //21
            "Love it!",                                                     //22
            "Me too! Who is your favorite comedian?",                       //23
            "That’s awesome! Mine is Kevin Hart.",                          //24
            "One of my favorite Kevin Hart quotes is “Ever argue with a female and, in the middle of the argument, you no longer feel safe because of her actions? She may start pacing back and forth real fast, breathing out her nose...",  //25 
            "You know what my girl do? When she get mad, she start talking in the third person. That’s scary as hell because that’s her way of telling me that from this point on, she is not responsible for none of her actions.”",  //26
            "Not really",                                                    //27
            "Really? You should give Dave Chappelle a chance -- he's one of my favorite comedians. I don't know anyone who doesn't enjoy his skits." //28
        };
        d.Add("Comedy", comedyDialogue);

        string[] docDialogue = {
            "What topic do you enjoy watching documentaries about?",
            "History",
            "What is your favorite historical era?",
            "Do you like watching American history documentaries?",
            "Yes, those are my favorite",
            "I really like the Woodstock and 70s era myself. What about you?",
            "Yes I love Woodstock!",
            "So you like music?",
            "Yes",
            "Do you play any instruments?",
            "Yes",
            "What do you play?",
            "That’s awesome!",
            "No",
            "That’s ok, I am more of a listener than a player myself.",
            "Not very much",
            "It’s not for everyone",
            "I’m not very interested in that",
            "I always love looking at the different fashion in historical films. It’s so interesting to see how style has changed over the years!",
            "I love that too",
            "I don’t care much about fashion",
            "What do you enjoy watching and learning about?",
            "Oh wow, that’s cool too!",
            "I prefer watching documentaries about other countries",
            "Do you have a favorite country?",
            "Yes",
            "What is it?",
            "Have you ever been there?",
            "Yes",
            "No", 
            "No",
            "Variety is always good. I can never pick a favorite!",
            "Do you like to travel?",
            "Yes",
            "Where have you been?",
            "That’s awesome! Do you have any recommendations of the best places to visit?",
            "I will keep that in mind! I am planning my next vacation!",
            "No, I am more of a home-body",
            "That’s understandable. There’s plenty of fun to be had at home!",
            "I prefer world history documentaries",
            "Do you tend to be more of a big picture person?",
            "Yeah on a lot of things",
            "That’s good. It’s important to have a well rounded understanding. I know I can get bogged down in the details a bit too much.",
            "Not usually",
            "Are you more detail oriented?",
            "Yes",
            "Me too. I am definitely a perfectionist!",
            "Not so much",
            "I know I can get bogged down in the details a bit too much.",
            "There seem to be a lot of historical documentaries about wars. Do you watch these?",
            "Yes! War documentaries are my favorite!",
            "They are definitely action packed!",
            "No I try to avoid these",
            "Me too. They are so sad! And I don’t like the violence.",
            "Famous people",
            "Who was your favorite documentary about?",
            "Hmmm, I've never heard of them. What are they known for?",
            "Intelligence",
            "Do you consider yourself intelligent?",
            "Yes that is one of my strengths",
            "That will take you far!",
            "Intelligence is not one of my strengths",
            "That’s ok.",
            "Do you like to learn?",
            "I love learning new things!",
            "What is your favorite subject to learn about?",
            "That’s awesome! Math is my favorite subject!",
            "No, I know all I need to know.",
            "It’s just as important to apply what you know!",
            "Artistic",
            "That’s great! I really liked the Taylor Swift documentary!",
            "Do you consider yourself creative or artistic?",
            "Yes",
            "What do you like to make/create?",
            "Wow, that sounds really cool!",
            "No",
            "I am more of a logical thinker myself",
            "What do you enjoy the most?",
            "Music",
            "I love music! Do you play any instruments?",
            "Yes",
            "What do you play?",
            "That’s great!",
            "No",
            "I am more of a listener than a player myself.",
            "Dance",
            "I love dance! Are you a dancer?",
            "Yes",
            "What styles do you dance?",
            "That’s very cool!",
            "No",
            "It’s just as fun to watch!",
            "Art",
            "I love art! Are you an artist?",
            "Yes",
            "What is your primary medium?",
            "That’s very cool!",
            "No",
            "I am not artistically gifted either.",
            "Strong/brave",
            "Are they an inspiration for you?",
            "Yes! Watching this documentary definitely pushed me personally!",
            "That’s great! What is a goal you have created for yourself that was inspired by this person?",
            "That’s a great goal! I know you can do it!",
            "Not really",
            "Who is an inspiration for you?",
            "That’s great!",
            "Social issues",
            "What was the social issue addressed?",
            "Do you consider yourself an activist for this issue?",
            "Yes",
            "Tell me about it. What are some of your best points or advice in support of this issue?",
            "That is very interesting. I can tell you are very passionate about this.",
            "No",
            "Did you learn any new perspectives by watching this documentary?",
            "Yes",
            "That’s great. It’s good to learn and appreciate new ideas.",
            "No",
            "Are you interested in any other social justice issues?",
            "Yes",
            "What are you passionate about?",
            "That’s great. I’d love to learn more about that.",
            "Not really",
            "What is something you are interested in?",
            "That’s great! I’d love to hear more about that!"
        };
        d.Add("Documentary", docDialogue);

        string[] affirmDialogue = {
            "Ah, that's a great place to start! What a fantastic idea. Okay, let's see here...",    //4
            "What affirmation resonates with you the most?"}; 
        d2.Add("Affirmations", affirmDialogue);
        
        string[] comfortDialoglue = {
            "Ah, that's a great place to start! What a fantastic idea. Okay, let's see here...",    //4
            "Which quiz would you like to do?"};
        d2.Add("Comfort topics", comfortDialoglue);
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
            if(currentDialogue == 2 && textIsAnimating == false && start == false)
            {
                start = true;
                Vector3 p = sphere.transform.localPosition;
                //print(p);
                
                if((p.x < 0 && p.y >= 0 && p.z < 0) || (p.x >= 0 && p.y < 0 && p.z >= 0))              
                {   //if stressed or relaxed, go to affirmations/comfort topics
                    string[] dialogue = {
                        "So, now that we're here and waiting for our meal to arrive, what would you like to talk about?"};              //3
                    addDialogue(dialogue, 3);
                }
                else 
                {   //otherwise go to entertainment branch
                    entertainment = true;
                    string[] dialogue = {
                        "Anyways, because of the current state of the world, I find myself with a lot of extra free time on my hands.", //3
                        "Recently I've been watching a lot of movies and need some recommendations.",                                   //4
                        "What is a movie you've watched recently that you really enjoyed?",                                             //5
                        "Very cool! What genre was it?"                                                                                 //6
                    };
                    addDialogue(dialogue, 3);
                }
            }
            else if(currentDialogue == 3 && textIsAnimating == false && entertainment == false)
            {
                makeChoice = true;

                GameObject[] show = {dialogueChoice1UI, dialogueChoice2UI};
                string[] choices = {"DialogueChoice1", "DialogueChoice2"};
                int i = 0;
                var val = d2.Keys.ToList();
                foreach (var key in val) {
                    show[i].SetActive(true);
                    show[i].GetComponentInChildren<Text>().text = key;
                    i++;
                }
                for(int k = 0; k < val.Count; k++) {
                    if(Input.GetButtonDown(choices[k]))
                    {
                        if(val[k] == "Affirmations") 
                        {
                            affirmation = true;                                            
                            setBranch(d2["Affirmations"]);
                            if(d2.ContainsKey("No"))
                                currentDialogue = 5;
                            d2.Remove("Affirmations");
                        }
                        else if(val[k] == "Comfort topics") 
                        {
                            comfort = true;                                         
                            setBranch(d2["Comfort topics"]);
                            if(d2.ContainsKey("No"))
                                currentDialogue = 5;
                            d2.Remove("Comfort topics");
                        }
                        else if(val[k] == "No") 
                        {                                   
                            terminate = true;
                            restartRelaxation();
                        }
                    }
                }
            }
            else if(currentDialogue == 5 && textIsAnimating == false && comfort == true) 
            {
                makeChoice = true;

                dialogueChoice1UI.SetActive(true);
                dialogueChoice2UI.SetActive(true);
                
                dialogueChoice1UI.GetComponentInChildren<Text>().text = "Riddles";
                dialogueChoice2UI.GetComponentInChildren<Text>().text = "Short personality quiz";
                
                if(Input.GetButtonDown("DialogueChoice1")) //Riddles branch -- complete
                {
                    print("User chose the first option!");
                    riddles = true;
                    string[] dialogue = {
                        "I love riddles! Sometimes you can’t get all of them, but it’s just the fun of trying them out that really makes them special, and if you do get them you’re pretty awesome. Hope you enjoy my riddles!",
                        "Please select a level of difficulty:",
                        "Easy",
                        "Hard",
                        "Yes this is going to be so much fun! You have chosen Level: Easy. Even though they’re easy it can also be easy to overthink the answer, so don’t worry if you miss a couple~ Good luck!",
                        "Yes this is going to be so much fun! You’re a brave soul, you have chosen Level: Hard. Good luck!"
                     };
                     setBranch(dialogue);              

                }
                else if(Input.GetButtonDown("DialogueChoice2")) //Short personality quiz branch -- complete
                {
                    print("User chose the second option!");
                    personality = true;
                    string[] dialogue = {
                        "This personality quiz is meant to reveal the hidden aspects of how you relate to others in your life. There are a total of 9 questions.",
                        "Disclaimer: The results of the given test, is a matter for the individual and should not be dictated by the test itself. It only serves as entertainment.",
                        "There are no 'correct' answers, so just let me know what you best identify with. Most importantly, keep an open imagination!",
                        "Q1: You’re walking down a path. What do you see around you?",
                        "Dark forest. The trees are so thick that only a bit of sky shows through.",
                        "This question shows your attitude towards life.",
                        "Your choice was the forest: you are a deep and quiet person. People find you incredibly interesting. You don’t show your true self right away. People like that you are a good listener.",
                        "Cornfield. It stretches as far as you can see, under a bright-blue sky.",
                        "This question shows your attitude towards life.",
                        "Your choice was the cornfield: You are bright, honest, and likable. You make friends easily and rarely get bummed. You’ve got a reputation for being entertaining and fun, and you’re always the center of attention.",
                        "Green rolling hills, dotted with trees. Beyond them you can almost see mountains.",
                        "This question shows your attitude towards life.",
                        "Your choice was the green rolling hills with dotted trees: You’re a practical and down-to-earth type of person. You attract people with your straightforward nature. A great problem solver, and you always listen to both points of view before taking a side.",
                        "Q2: You spot an object by your feet. What is it?",
                        "A mirror",
                        "This question reveals what kind of partner you are looking for.",
                        "You spotted a mirror: You are searching for a person who shares your outlook on life. You have a pretty good idea of what your ideal partner is like, but you will keep an open mind. The perfect romance may be with someone you normally wouldn’t look at twice!",
                        "A ring",
                        "This question reveals what kind of partner you are looking for.",
                        "You spotted a ring: You are a true romantic. When in a relationship, you work hard at keeping things lovey-dovey. You believe true love is forever, and although you might not admit it, you want your partner to care about you.",
                        "A bottle",
                        "This question reveals what kind of partner you are looking for.",
                        "You spotted a bottle: You are looking for someone who is not afraid to show their intelligence. You like those who are ambitious and hard-working.",
                        "Q3: Do you pick it up?",
                        "Yes",
                        "This question tells how ready you are for commitment.",
                        "You picked yes: You are dying to pick the right person.",
                        "No",
                        "This question tells how ready you are for commitment.",
                        "You picked no: You are playing the field for now.",
                        "Q4: Continuing along the path, you come across some water. What form is the water in?",
                        "Calm, clear, serene lake",
                        "This question symbolizes your passion potential.",
                        "You chose a calm, clear, and serene lake: You are not interested in having a superficial relationship. Once you meet someone you are really into, your love will run deep.",
                        "Crashing waterfall",
                        "This question symbolizes your passion potential.",
                        "You chose a waterfall: You’re good at charming people. Those who catch your attention will be trapped by your beauty and character.",
                        "Babbling *Quiet, continuous sound* brook",
                        "This question symbolizes your passion potential.",
                        "You chose a brook: You are a free bird. You do not plan on settling any time soon!",
                        "Q5: You see a key in the water and reach down to pick it up. What does it look like?",
                        "Simple house key",
                        "This question expresses your views on school.",
                        "You saw a house key: Education is less important to you than the world beyond. Secretly, you may be dying to start a career and potentially look into buying your own home.",
                        "Fancy antique key",
                        "This question expresses your views on school.",
                        "You saw an antique key: You consider education to be crucial! You strive to work hard, and absorb as much knowledge as you possibly can.",
                        "Small padlock key",
                        "This question expresses your views on school.",
                        "You saw a padlock key: You may not be too into school, but you still have plenty of great ideas. You follow your intuition, this could lead you into an unique career.",
                        "Q6: As you walk farther along the path, you come to a house. What kind of house is it?",
                        "Hollywood-style mansion",
                        "This question gives clues about your ambitions.",
                        "You chose a mansion: You have a ton of goals! You strive to be the best at whatever you do, and you’re attracted to jobs that let you express your energetic personality.", 
                        "Cottage with a lush green lawn",
                        "This question gives clues about your ambitions.",
                        "You chose a cottage: You have a realistic attitude toward a future profession. Since your feet are planted firmly on the ground, you’ll probably rock at whatever career you pick!",
                        "Beautiful castle in ruins",
                        "This question gives clues about your ambitions.",
                        "You chose a castle: Your career dreams could be a little unrealistic, but not unreachable. Aim for something closer to home first, and work your way to where you want to be! If you stretch yourself too thin you might be disappointed with the results.", 
                        "Q7: What do you do?",
                        "Look in the window",
                        "This question shows your attitude towards success.",
                        "You looked through the window: You’re a little afraid of failing. Taking that first step toward reaching a goal is the most intimidating thing to you. But don’t give up before you’ve actually tried.",
                        "Go inside",
                        "This question shows your attitude towards success.",
                        "You went inside: You feel pretty confident about success. Nothing’s going to stand in your way!",
                        "Walk on",
                        "This question shows your attitude towards success.",
                        "You kept on walking: Success isn’t really a big deal for you. You are content with the simple things, and you would rather be around people you love than spending your life trying to get to the top.",
                        "Q8: Suddenly, something jumps out at you. What is it?",
                        "Bear",
                        "This question reveals your worst fears.",
                        "You chose the bear: You worry about being able to stand on your own two feet. Independence is essential for you.",
                        "Wizard",
                        "This question reveals your worst fears.",
                        "You chose the wizard: You’re scared about things that are often beyond your control.",
                        "Troll",
                        "This question reveals your worst fears.",
                        "You chose the troll: You care about how others see you. It’s crucial that people like you, and that you fit into the crowd. You should trust your own judgement on things.",
                        "Q9: You run down the path until you reach a wall with a door in it. You peek through the keyhole. What’s on the other side?",
                        "The lush gardens of an impressive house",
                        "This question represents your innermost self.",
                        "You saw a garden: You are mature and honest. You believe in speaking your mind, and people rely on you to provide a sensible viewpoint. However, you may come across problems in the future that you’ll have to solve with your heart, and not your head.",
                        "A pond in the middle of the desert",
                        "This question represents your innermost self.",
                        "You saw a desert pond: You have a deep need for your own space. You are always analyzing your thoughts, and you’ve been known to avoid tough situations. Sharing your feelings with someone you trust could help you feel better.",
                        "A sandy beach with crashing waves",
                        "This question represents your innermost self.",
                        "You saw a beach: You are passionate about life and you have strong opinions that you are not afraid to reveal. You can be unpredictable at times and also change those opinions often.",
                        "Thank you for doing this test with me~ I had such a great time! What did you think this revealed about your personality? Was it accurate?",
                        "Very",
                        "Yes that’s awesome!",
                        "Sort of",
                        "Me too, haha!",
                        "Not really",
                        "Dang sorry about that! I’ll look into better ones for the next time."
                    };
                    setBranch(dialogue);
                }
            }
            else if((currentDialogue == 9 || currentDialogue == 19 || currentDialogue == 36
                    || currentDialogue == 46 || currentDialogue == 56 || currentDialogue == 66
                    || currentDialogue == 76 || currentDialogue == 86) && textIsAnimating == false && personality == true) 
            {
                int index = currentDialogue;
                int[] arr = {3, index+1, index+2, index+4, index+5, index+7, index+8};
                branchingDialogue(arr);
            }
            else if((currentDialogue == 12 ||
                    currentDialogue == 22 ||
                    currentDialogue == 39 ||
                    currentDialogue == 49 ||
                    currentDialogue == 59 ||
                    currentDialogue == 69 ||
                    currentDialogue == 79 ||
                    currentDialogue == 89) && textIsAnimating == false && personality == true) 
            {
                int index = currentDialogue;
                currentDialogue = index + 6;
            } 
            else if((currentDialogue == 15 ||
                    currentDialogue == 25 ||
                    currentDialogue == 42 ||
                    currentDialogue == 52 ||
                    currentDialogue == 62 ||
                    currentDialogue == 72 ||
                    currentDialogue == 82 ||
                    currentDialogue == 92) && textIsAnimating == false && personality == true) 
            {
                int index = currentDialogue;
                currentDialogue = index + 3;
            }        
            else if(currentDialogue == 29 && textIsAnimating == false && personality == true) 
            {
                int index = currentDialogue;
                int[] arr = {2, index+1, index+2, index+4, index+5};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 32 && textIsAnimating == false && personality == true) 
            {
                currentDialogue = 35;
            }  
            else if(currentDialogue == 96 && textIsAnimating == false && personality == true) 
            {
                int[] arr = {3, 97, 98, 99, 100, 101, 102}; 
                branchingDialogue(arr);
            }  
            else if((currentDialogue == 98 || currentDialogue == 100 || currentDialogue == 102) 
                    && textIsAnimating == false  && personality == true) 
            {
                Vector3 p = sphere.transform.localPosition;
                if(p.x < 0 && p.y < 0 && p.z < 0 && inEntertainment == false && inRelax == false)   
                    restartEntertainment();
                else 
                    restartRelaxation();
            }
            //-------------------------------------------------------------------------------------------------------------
            else if(currentDialogue == 7 && textIsAnimating == false && riddles == true) 
            {
                int[] arr = {2, 8, 10, 9, 11}; 
                branchingDialogue(arr);
            }
            else if(currentDialogue == 10 && textIsAnimating == false && riddles == true) 
            {
                easy = true;
                string[] dialogue = {
                    "What has to be broken before you can use it?",
                    "Did you get it? Would you like a hint?",
                    "Yes",
                    "Think kitchen, cooking, oval shape…",
                    "Answer: It’s an egg! It needs to be broken BEFORE you can use it.",
                    "No, give me the answer",
                    "Answer: It’s an egg! It needs to be broken BEFORE you can use it.",
                    "I already know the answer",
                    "It’s an egg, good job! It needs to be broken BEFORE you can use it.",
                    "What month of the year has 28 days?",
                    "Would you like a hint?",
                    "Yes",
                    "Jan 28, Feb 28, March 28… Oct 28, Nov 28, Dec 28.",
                    "Answer: All of them! This one got me not at first going to lie.",
                    "No, give me the answer",
                    "Answer: All of them! This one got me at first not going to lie.",
                    "I already know the answer",
                    "Yes, the answer is all of them! Nice job. This one got me at first not going to lie.",
                    "What is always in front of you but can’t be seen?",
                    "Would you like a hint?",
                    "Yes",
                    "Opposite of the past.",
                    "Answer: It’s the future!",
                    "No, give me the answer.",
                    "Answer: It’s the future!",
                    "I already know the answer",
                    "Answer: It’s the future! Great job!",
                    "Last one! You walk into a room that contains a match, a kerosene lamp, a candle and a fireplace. What would you light first?",
                    "Would you like a hint?",
                    "Yes",
                    "It rhymes with batch. 30 more seconds have been added.",
                    "Answer: You would light the match first! This one was funny haha.",
                    "No, give me the answer",
                    "Answer: You would light the match first! This one was funny haha.",
                    "I already know the answer",
                    "Answer: You would light the match first! Awesome job! This one was funny haha.",
                    "End of easy riddles, you did fantastic! Thank you for answering them, I had a great time~"
                };
                addDialogue(dialogue, 12);
                currentDialogue = 11;
            }
            else if(currentDialogue == 11 && textIsAnimating == false && riddles == true && easy == false) 
            {
                string[] dialogue = {
                    "What can be seen once in a minute, twice in a moment, and never in a thousand years?", 
                    "Did you get it? I know I didn’t haha. Would you like a hint?", 
                    "Yes", 
                    "Look very veeerrryyy close to the words.", 
                    "Answer: It’s the letter M!", 
                    "No, give me the answer", 
                    "Answer: It’s the letter M!", 
                    "I already know the answer", 
                    "It’s the letter M. Great job!", 
                    "A truck drove to a village and met 4 cars. How many vehicles were going to the village?", 
                    "Would you like a hint?", 
                    "Yes", 
                    "It’s not what you think it is.", 
                    "Answer: It’s one truck! Very tricky if you ask me.", 
                    "No, give me the answer", 
                    "Answer: It’s one truck! Very tricky if you ask me.", 
                    "I already know the answer", 
                    "Answer: It’s one truck. Nice job! Very tricky if you ask me.", 
                    "Which hand is best for stirring sugar into a cup of tea?", 
                    "Do you get it?? Would you like a hint?", 
                    "Yes", 
                    "Do we use hands?", 
                    "Answer: It’s better to use a SPOON lol. This cracked me up~", 
                    "No, give me the answer", 
                    "Answer: It’s better to use a SPOON lol. This cracked me up~", 
                    "I already know the answer", 
                    "Answer: It’s better to use a SPOON lol. Awesome job! This cracked me up~", 
                    "Last one! What can you catch, but not throw?", 
                    "Would you like a hint?", 
                    "Yes",
                    "It's one of the many symptoms of covid!", 
                    "Answer: It’s a cold!", 
                    "No, give me the answer", 
                    "Answer: It’s a cold!", 
                    "I already know the answer", 
                    "Answer: It’s a cold. Great job! You stay safe out there.", 
                    "End of hard riddles, you did fantastic! Thank you for answering them, I had a great time~"
                }; 
                addDialogue(dialogue, 12);
            }
            else if(currentDialogue == 13 && textIsAnimating == false && riddles == true) 
            {
                int[] arr = {3, 14, 15, 17, 18, 19, 20}; 
                branchingDialogue(arr);
            }
            else if((currentDialogue == 16 || currentDialogue == 18) && textIsAnimating == false  && riddles == true) 
            {
                currentDialogue = 20;
            }
            else if(currentDialogue == 22 && textIsAnimating == false && riddles == true) 
            {
                int[] arr = {3, 23, 24, 26, 27, 28, 29}; 
                branchingDialogue(arr);
            }
            else if((currentDialogue == 25 || currentDialogue == 27) && textIsAnimating == false  && riddles == true) 
            {
                currentDialogue = 29;
            }
            else if(currentDialogue == 31 && textIsAnimating == false && riddles == true) 
            {
                int[] arr = {3, 32, 33, 35, 36, 37, 38}; 
                branchingDialogue(arr);
            }
            else if((currentDialogue == 34 || currentDialogue == 36) && textIsAnimating == false  && riddles == true) 
            {
                currentDialogue = 38;
            }
            else if(currentDialogue == 40 && textIsAnimating == false && riddles == true) 
            {
                int[] arr = {3, 41, 42, 44, 45, 46, 47}; 
                branchingDialogue(arr);
            }
            else if((currentDialogue == 43 || currentDialogue == 45) && textIsAnimating == false  && riddles == true) 
            {
                currentDialogue = 47;
            }
            else if(currentDialogue == 48 && textIsAnimating == false && riddles == true) 
            {
                Vector3 p = sphere.transform.localPosition;
                if(p.x < 0 && p.y < 0 && p.z < 0 && inEntertainment == false && inRelax == false)   
                    restartEntertainment();
                else 
                    restartRelaxation();
            }
            //-------------------------------------------------------------------------------------------------------------
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
                        "Wonderful~ Tell me more about how you practice mindfulness.",                                                                                      //10
                        "I love that! It's important that you're focusing on yourself and deflecting any negative thoughts.",                                               //11
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
                        "By being aware of your thoughts -- and noticing them in a non judgemental way -- you can condition yourself to examine them before reacting to them.",    //26
                        "Give yourself a time limit",                                                                                                                               //27
                        "It’s important to allow yourself time to assess the thing that is causing you stress.",                                                                    //28 
                        "Set a base time of 5 minutes before moving on to the problem and examine the emotions that come with your reflection.",                                    //29
                        "Change your perspective",                                                                                                                                  //30
                        "Remind yourself that you are already remarkably resilient. Also, in order to build and improve your mental stamina, it's important to view things objectively for what they are.", //31
                        "Remember that stress, like situations, come and go -- so whatever you're stressing about now will eventually become a distant memory.",                                            //32
                        "Now then, what are some things you appreciate about yourself?"                                                                                                                     //33
                    };
                    setBranch(dialogue);
                }
                else if(Input.GetButtonDown("DialogueChoice2")) //'I am in charge' branch - complete
                {
                    print("User chose the second option!");
                    a2 = true;
                    string[] dialogue = {
                        "That’s amazing~ It’s so important to remember that while you may not be able to control what happens all the time, you can always control how you feel and react.",
                        "You're doing great because you recognize this affirmation as something important to you.",                                   
                        "What are some ways that you practice choosing happiness?",                                                                                                                       
                        "Choose to focus on what you have, not on what you don’t have",                                                                 
                        "Wonderful~ Tell me more about how you do that.",                                                                       
                        "I love that! It's important that you're focusing on yourself and deflecting any negative thoughts.",                                              
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
                        "Now then, what are some things you appreciate about yourself?"
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
                        "Wonderful~ Tell me more about how you do that.",                                                                       
                        "I love that! It's important that you're focusing on yourself and deflecting any negative thoughts.",                                              
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
                        "Now then, what are some things you appreciate about yourself?"
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
                        "Wonderful~ Tell me more about how you manage to do all that.",                                                        
                        "I love that! It's important that you're focusing on yourself and deflecting any negative thoughts.",                                            
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
                        "Now then, what are some things you appreciate about yourself?"
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
            else if((currentDialogue == 18 || currentDialogue == 20 || currentDialogue == 26 || currentDialogue == 29) && textIsAnimating == false  && affirmation == true) 
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
                    "That’s great to hear~ You have a deep understanding of yourself and what makes you happy.",                               //34
                    "It takes a lot of courage to express yourself out loud. You’re amazing!",                                                 //35
                    "If you don't mind, I'd like for us to take a nice deep breath together before we continue our discussion.",               //36
                    "Ready? Inhale: 1… 2… 3… 4… 5…",                                                                                           //37
                    "Now exhale: 5… 4… 3… 2… 1…",                                                                                              //38            
                    "Would you like for us to go over positive affirmations together?",                                                        //39
                    "Yes please",                                                                                                              //40
                    "I'll be going over four affirmations -- feel free to repeat after me if you’d like.",                                     //41
                };
                addDialogue(dialogue, 34);
                
                if(a1 == true) {
                    string[] dialogue2 = {
                        "'I am deserving of love.'",                                                                                            //42
                        "'I am powerful.'",                                                                                                     //43
                        "'I can achieve anything.'",                                                                                            //44
                        "'I can overcome any obstacle, big or small.'",                                                                         //45
                        "No, thank you",                                                                                                        //46
                        "Alright then, I'd like to take a moment now to thank you for going on this journey with me~ Keep doing what you're doing, everyone grows at their own pace. You are where you need to be."   //47
                    };
                    addDialogue(dialogue2, 42);
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
                    addDialogue(dialogue2, 42);
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
                    addDialogue(dialogue2, 42);
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
                    addDialogue(dialogue2, 42);
                }
            }
            else if(currentDialogue == 39 && textIsAnimating == false  && affirmation == true) 
            {
                int[] arr = {2, 40, 41, 46, 47}; 
                branchingDialogue(arr);
            }
            else if(currentDialogue == 45 && textIsAnimating == false  && affirmation == true) 
            {
                currentDialogue = 46;
            }
            else if(currentDialogue == 47 && textIsAnimating == false && affirmation == true) 
            {
                Vector3 p = sphere.transform.localPosition;
                if(p.x < 0 && p.y < 0 && p.z < 0 && inEntertainment == false && inRelax == false)   
                    restartEntertainment();
                else 
                    restartRelaxation();
            }
            //-------------------------------------------------------------------------------------------------------------
            else if(currentDialogue == 6 && textIsAnimating == false  && entertainment == true) 
            {
                makeChoice = true;
                GameObject[] show = {dialogueChoice1UI, dialogueChoice2UI, dialogueChoice3UI, dialogueChoice4UI};
                string[] choices = {"DialogueChoice1", "DialogueChoice2", "DialogueChoice3", "DialogueChoice4"};
                int i = 0;
                var val = d.Keys.ToList();
                foreach (var key in val) {
                    show[i].SetActive(true);
                    show[i].GetComponentInChildren<Text>().text = key;
                    i++;
                }
                for(int k = 0; k < val.Count; k++) {
                    if(Input.GetButtonDown(choices[k]))
                    {
                        if(val[k] == "Romance/Drama") 
                        {
                            romance = true;                                            
                            setBranch(d["Romance/Drama"]);
                            d.Remove("Romance/Drama");
                        }
                        else if(val[k] == "Action") 
                        {
                            action = true;                                         
                            setBranch(d["Action"]);
                            d.Remove("Action");
                        }
                        else if(val[k] == "Comedy") 
                        {
                            comedy = true;                                         
                            setBranch(d["Comedy"]);
                            d.Remove("Comedy");
                        }
                        else if(val[k] == "Documentary") 
                        {
                            documentary = true;                                         
                            setBranch(d["Documentary"]);
                            d.Remove("Documentary");
                        }
                        else if(val[k] == "No") 
                        {                                   
                            terminate = true;
                            restartEntertainment();
                        }
                    }
                }
            }
            else if(currentDialogue == 7 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {3, 8, 9, 61, 62, 114, 115};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 10 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {3, 11, 12, 30, 31, 46, 47};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 12 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 13, 14, 24, 25};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 14 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 15, 16, 22, 23};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 16 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 17, 18, 20, 21};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 25 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 26, 56, 27, 28};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 31 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 32, 33, 37, 38};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 34 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 35, 39, 36, 39};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 39 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 40, 41, 44, 45};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 47 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 48, 49, 50, 51};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 51 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 52, 53, 54, 55};
                branchingDialogue(arr);
            }
            else if((currentDialogue == 19 || currentDialogue == 21 || currentDialogue == 26 || 
                    currentDialogue == 29 || currentDialogue == 43 || currentDialogue == 44 || currentDialogue == 49 || currentDialogue == 43) 
                    && textIsAnimating == false && documentary == true)
            {
                currentDialogue = 55;
            } 
            else if(currentDialogue == 56 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 57, 58, 59, 60};
                branchingDialogue(arr);
            } 
            else if(currentDialogue == 63 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {3, 64, 65, 76, 77, 106, 107};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 65 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 66, 67, 68, 69};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 67 && textIsAnimating == false && documentary == true)
            {
                currentDialogue = 69;
            }
            else if(currentDialogue == 70 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 71, 72, 74, 75};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 78 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 79, 80, 82, 83};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 81 && textIsAnimating == false && documentary == true)
            {
                currentDialogue = 83;
            }
            else if(currentDialogue == 84 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {3, 85, 86, 92, 93, 99, 100};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 86 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 87, 88, 90, 91};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 93 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 94, 95, 97, 98};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 100 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 101, 102, 104, 105};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 106 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 107, 108, 111, 112};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 116 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 117, 118, 120, 121};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 121 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 122, 123, 124, 125};
                branchingDialogue(arr);
            }
            else if(currentDialogue == 125 && textIsAnimating == false && documentary == true)
            {
                int[] arr = {2, 126, 127, 129, 130};
                branchingDialogue(arr);
            }
            else if((currentDialogue == 58 || currentDialogue == 60 || currentDialogue == 73 || currentDialogue == 75
                    || currentDialogue == 89 || currentDialogue == 91 || currentDialogue == 96 || currentDialogue == 98
                    || currentDialogue == 103 || currentDialogue == 105 || currentDialogue == 110 || currentDialogue == 113
                    || currentDialogue == 119 || currentDialogue == 123 || currentDialogue == 128 || currentDialogue == 131) && textIsAnimating == false && documentary == true)
            {
                Vector3 p = sphere.transform.localPosition;
                if(p.x < 0 && p.y >= 0 && p.z < 0 && inEntertainment == false && inRelax == false)   
                    restartRelaxation();
                else 
                    restartEntertainment();
            }
            //-------------------------------------------------------------------------------------------------------------
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
                int[] arr = {2, 22, 23, 27, 28};
                branchingDialogue(arr);
            }
            else if((currentDialogue == 26 || currentDialogue == 28) && textIsAnimating == false && comedy == true)
            {
                Vector3 p = sphere.transform.localPosition;
                if(p.x < 0 && p.y >= 0 && p.z < 0 && inEntertainment == false && inRelax == false)   
                    restartRelaxation();
                else 
                    restartEntertainment();
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
                Vector3 p = sphere.transform.localPosition;
                if(p.x < 0 && p.y >= 0 && p.z < 0 && inEntertainment == false && inRelax == false)   
                    restartRelaxation();
                else 
                    restartEntertainment();
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
                Vector3 p = sphere.transform.localPosition;
                if(p.x < 0 && p.y >= 0 && p.z < 0 && inEntertainment == false && inRelax == false)   
                    restartRelaxation();
                else 
                    restartEntertainment();
            }
            else if (currentDialogue == 2 && textIsAnimating == false && finish == true) 
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

            if((currentDialogue == 15 || currentDialogue == 24 || currentDialogue == 33 ||
            currentDialogue ==  42) && riddles == true && makeChoice == false && textIsAnimating == false && enabled == true)
            {
                advanceTextIndicator.GetComponent<Text>().text = "Press SPACE for Answer";
                advanceTextIndicator.SetActive(true);

            }
            else if(makeChoice == false && textIsAnimating == false && enabled == true)
            {
                advanceTextIndicator.GetComponent<Text>().text = "Press SPACE to Continue";
                advanceTextIndicator.SetActive(true);

            }
            else
            {
                advanceTextIndicator.SetActive(false);
            }
        }
    }
    //-------------------------------------------------------------------------------------------------------------
    private void restartRelaxation() 
    {
        if(inRelax == false && entertainment == true)
        {
            entertainment = false;
            romance = false;
            action = false;
            comedy = false; 
            documentary = false;
            inRelax = true;
            string[] starting = {
                "", "", "", "Would you like to explore anything else?"};
            currentDialogue = 2;
            Array.Resize(ref dialogueArray, starting.Length);
            Array.Copy(starting, 0, dialogueArray, 0, starting.Length);
        }
        else 
        {
            if(!d2.ContainsKey("No"))
                d2.Add("No", done);
        
            if(d2.Count == 1 || terminate == true) 
            {
                if(terminate == true) 
                {
                    makeChoice = false;
                    textIsAnimating = true;
                    currentDialogue = 0;
                }
                else
                    currentDialogue = -1;
                Array.Resize(ref dialogueArray, done.Length);
                Array.Copy(done, 0, dialogueArray, 0, done.Length);
                finish = true;
            } 
            else 
            {
                currentDialogue = 2;
                string[] dialogue = {"", "", "", "Would you like to explore anything else?"};
                Array.Resize(ref dialogueArray, dialogue.Length);
                Array.Copy(dialogue, 0, dialogueArray, 0, dialogue.Length);
            }
        }
        affirmation = false;
        a1 = false;
        a2 = false;
        a3 = false; 
        a4 = false;
        comfort = false;
        riddles = false;
        personality = false;
        easy = false;
    }
    private void restartEntertainment() 
    {
        if(inEntertainment == false && entertainment == false) {
            entertainment = true;
            inEntertainment = true;
            string[] starting = {
                "", "", "",
                "Anyways, because of the current state of the world, I find myself with a lot of extra free time on my hands.", //3
                "Recently I've been watching a lot of movies and need some recommendations.",                                   //4
                "What is a movie you've watched recently that you really enjoyed?",                                             //5
                "Very cool! What genre was it?"                                                                                 //6
            };
            currentDialogue = 2;
            Array.Resize(ref dialogueArray, starting.Length);
            Array.Copy(starting, 0, dialogueArray, 0, starting.Length);
            affirmation = false;
            a1 = false;
            a2 = false;
            a3 = false; 
            a4 = false;
            comfort = false;
            riddles = false;
            personality = false;
            easy = false;
        }
        else 
        {
            if(!d.ContainsKey("No"))
                d.Add("No", done);
            
            if(d.Count == 1 || terminate == true) 
            {
                if(terminate == true) 
                {
                    makeChoice = false;
                    textIsAnimating = true;
                    currentDialogue = 0;
                }
                else
                    currentDialogue = -1;
                Array.Resize(ref dialogueArray, done.Length);
                Array.Copy(done, 0, dialogueArray, 0, done.Length);
                finish = true;
            } 
            else 
            {
                currentDialogue = 5;
                string[] dialogue = {"", "", "", "", "", "", "Are there any other movie genres you like?"};
                Array.Resize(ref dialogueArray, dialogue.Length);
                Array.Copy(dialogue, 0, dialogueArray, 0, dialogue.Length);
            }
        }
        romance = false;
        action = false;
        comedy = false; 
        documentary = false;
    }

    //this is used whenever more dialogue needs to be added to dialogueArray
    private void addDialogue(string[] dialogue, int index) 
    {
        Array.Resize(ref dialogueArray, dialogueArray.Length + dialogue.Length);
        Array.Copy(dialogue, 0, dialogueArray, index, dialogue.Length);
    }

    //this is used whenever an important choice is made and more dialogue needs to be added to dialogueArray
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