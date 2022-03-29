using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inboxMessage : MonoBehaviour
{
    public TextMeshProUGUI message;
    public TextMeshProUGUI response1, response2, response3;
    // separate strings to track most recent message from NPC
    public string msgMom, msgGrandma, msgProf, msgBuddy, msgBrie, msgMatt;
    public GameObject replyBox1, replyBox2, replyBox3;
    //public GameObject sendArrow1, sendArrow2, sendArrow3;
    public int dayShown, personShown;
    public int buddyDay, brieDay;
    public bool brieTalk, buddyTalk; // Should default false
    public int intMom, intGrandma, intProf, intBuddy, intBrie, intMatt;
    public int status;
    public int playerResponse;
    bool buddyRomance;

    /*
    For simplicity:
    - Days are denoted as int 1-10 (eg. Day 1 is "1")
    - People are numbered in order of Inbox UI, as follows:

    1: Mom
    2: Grandma Betsy
    3: Prof. Turner
    4: Buddy
    5: Brie
    6: Cuz Matt

    dayShown and personShown variables store last shown
    changeDay() and changePerson() if day or person changes, respectively

    Day changes daily, should be implemeneted in Tracking script*
    Person changes when message is selected, implemented through Inbox buttons

    *Some chars use independent day vars b/c depends on how many
    days you've been talking to them, no necessarily which day it is in-game
    These indepedent variables will simply increment in the changeDay() method
    Those characters are as follows:
    - Brie
    - Buddy
    These characters use ints "buddyDay" and "brieDay" to track how many days since started
    talking to these characters
    They also use booleans "buddyTalk" and "brieTalk" which start as FALSE and are set to
    TRUE when respective prereqs are satisfied to start conversation. (At this point, begins
    dialogue w/ these NPC's and increments day-counting variables)

    Status changes based on responses, implemented in-game and updated
    through updateStatus() method


    "int" refers to interactiveness, determined on per-character basis for how
    often player has responded to their messages
    1 = isolated - no response
    2 = wallflower (filial) — responds to family messages
    3 = wallflower (friend) - responds to friendly messages
    4 = wallflower (networking) - ignore for now (?)
    5 = butterfly - responds to both family and friendly messages
    "Status" stores # 1-5, outside of newMessage()
    NOTE:
    Counts based on past 3 days' behaviour (>=3 past days)
    (eg. status = 2 means responds to family only - for the past 3 days)

    Note on response boxes:
    Messages have 0-3 built-in responses. Based on how many, response boxes will hide themselves
    Box 1 top, box 2 middle, box 3 bottom
    (eg. 1 possible response shows only box 3; 2 responses shows boxes 2-3)

    #################################################################
    #### EDITOR NOTES FOR PROGRESS/TO-DOS, WILL BE REMOVED LATER ####
    #################################################################

    Note to self: implement a script attached to reply buttons to affect statuses of
    convos, such as intBrie for Brie... also add hide text box mechanic later

    Note to self: Buddy's dialogue is unfinished; certain replies still need to be
    added and written as necessary. Check some other characters do not yet have
    replies written

    Note to self: see that intNpc days are initialized to 1, add methods for
    changing to be linked to buttons later (global script)

    Note to self: replace (NAME) with character name where nec (I think it is relevant
    to Grandma's messages, possibly other characters as well)
    Actually just grandma as of now but be aware

    See extended to-dos and notes in Discord
    */

    void changeDay(int newDay) {
        // Script should be called every time inbox selected
        // newDay should be carried globally from Tracking script in main scene
        dayShown = newDay;
        /*if (buddyTalk == true) {
            buddyDay++;
        }
        if (brieTalk == true) {
            brieDay++;
        } //ADJUSTMENT: Buddy and Brie days only advance if you've responded - in button effect*/
        showMessage(dayShown, personShown, status);
    }
    void changePerson(int newPerson) {
        // This script should be used by active chat buttons (left)
        personShown = newPerson;
        if (personShown == 1) {
            status = intMom;
            message.text = msgMom;
        } else if (personShown == 2) {
            status = intGrandma;
            message.text = msgGrandma;
        } else if (personShown == 3) {
            status = intProf;
            message.text = msgProf;
        } else if (personShown == 4) {
            // status = intBuddy;
            message.text = msgBuddy;
        } else if (personShown == 5) {
            // status = intBrie; looks like Brie's conditions are applied independently
            message.text = msgBrie;
        } else if (personShown == 6) {
            status = intMatt;
            message.text = msgMatt;
        }
        // Set reply boxes all to visible by default; will change based on display
        // i.e. if there's an issue with disappearing boxes look here possibly
        replyBox1.SetActive(true);
        replyBox2.SetActive(true);
        replyBox3.SetActive(true);
        showMessage(dayShown, personShown, status);
    }
    void showMessage(int dayShown, int personShown, int interactiveness) {
        newMessage(dayShown, personShown, interactiveness);
        newResponse (dayShown, personShown);
    }

    //START IS HERE FOR TESTING ONLY - remove later
    void Start() {
        // For testing purposes, fiddle with the values below
        newMessage(4, 2, 0);
    }

    // The method responseSent(int) is for response button use
    public void responseSent(int buttonNum) {
        // This method used by responseB1-B3_effect scripts to indicate
        // which button was pressed
        playerResponse = buttonNum;
        //updateTracking(buttonNum, person);

        switch (personShown) {
            case 2: // Gramma Betsy
                switch (dayShown) {
                    case 3:
                        if(playerResponse == 2) intGrandma = 2;
                        else if(playerResponse == 3) intGrandma = 3;
                        break;
                }
                break;
            case 4: // Buddy
                switch (buddyDay) {
                    case 1:
                        if(playerResponse == 2) intBuddy = 2;
                        break;
                    case 2:
                        if(playerResponse == 2) intBuddy = 3;
                        if(playerResponse == 3) intBuddy = 3; buddyRomance = true;
                        break;
                    case 3:
                        if(playerResponse == 1) break;
                        if(playerResponse == 2 && buddyRomance) intBuddy = 4;
                        if((playerResponse == 2 && !buddyRomance) || playerResponse == 3) intBuddy = 5; buddyRomance = false;
                        break;
                    case 4:
                        if(buddyRomance) { // romance
                            if(playerResponse == 2) buddyInt = 7; buddyRomance = false;
                            if(playerResponse == 3) buddyInt = 6; // --> romance
                            break;
                        } else { //friendly
                            if(playerResponse == 2) buddyInt = 7; // --> friendly
                            if(playerResponse == 3) buddyInt = 7;
                            break;
                        }
                    case 5:
                        if(buddyRomance) {
                            if(playerResponse == 2 || playerResponse == 3) buddyInt = 8; buddyRomance = true;
                            break;
                        } else {
                            if(playerResponse == 2 || playerResponse == 3) buddyInt = 9; buddyRomance = false;
                            break;
                        }
                    case 6:
                        // depends on Brie indie dev path
                        break;
                }
                break;
            case 5: // Brie
                switch (brieDay) {
                    case 1:
                        if(playerResponse == 2) intBrie = 2;
                        else if(playerResponse == 3) intBrie = 3;
                        break;
                    case 2:
                        //Only one response option for each, so if any response advance day
                        if(playerResponse == 3) intBrie = 4;
                        break;
                    case 3:
                        if(playerResponse == 1) intBrie = 5;
                        else if(playerResponse == 2) intBrie = 6;
                        else if(playerResponse == 3) intBrie = 7;
                        break;
                    case 4:
                        switch(intBrie) {
                            case 6:
                                intBrie = 6; // cont romance path for any of 3 answers chosen
                                break;
                            case 7:
                                intBrie = 10;
                                break;
                        }
                        break;
                }
                break;
        }
    }

    //public void updateTracking(int response, int person) {
        // Method not finished, just setting up — should actually be pretty long
        // This method updates status depending on character and response given by player
        // ^ Updates INDIVIDUAL statuses (eg. intBrie)
        // Also uses public inboxMessage vars like brieDay as necessary
        // Called by responseSent() (from same class) every time player responds
    //}

    void newMessage(int day, int person, int status) {
        if (person == 1) {
            Debug.Log("Mom"); // This was for testing purposes, remove later
            Debug.Log("Day is " + day);
            if (day == 2) {
                message.text = "Sweetheart, Is Agenda helping you stay on track with your goals? I’ve heard so much about how it helped other students, and I thought it would be a good present for you, now that you are doing school from home. I’ve felt myself slipping lately - work isn’t what it used to be for me. I am finding my way through. In fact, I’ve taken up knitting. Would you like me to make you a scarf? I promise I can make it super fluffy and warm - just the way you like it. Love, Mom.";
            }
            if (day == 4) {
                message.text = "Sweetheart, remember the pair of bonsai plants we bought together? Mine finally bloomed! It’s so pretty and lush, I’m quite proud of how well I’ve taken care of it. I plan on decorating it for the winter holidays. How about yours? Have you watered it yet? I hope you are doing well and staying on top of your studies. I miss you. Love, Mom.";
            }
            if (day == 6) {
                message.text = "Sweetheart, have you had any sweets lately? I’ve had such a craving for cake the past few weeks. Remember when I mentioned I wanted to get into baking? Well, I started baking banana bread. It’s healthy and super yummy, and I’m quite good at it! The trick is to use whole wheat flour and substitute white sugar for honey. Then you can make a delicious and mostly-healthy treat for just after supper. I would love to chat sometime soon. Maybe I can even bring you a loaf. Wouldn’t that be swell! Love, Mom.";
            }
            if (day == 8) {
                message.text = "Sweetheart, are you staying healthy? I had such a sweet tooth last month that by the end of it I felt like I could never have a treat again! I got myself a yoga mat and started working out. I feel a lot better. I meditate a few minutes every day, I do lots of stretching, and I go for long walks. You should try it sometime! Maybe it will help you relax after a hard day’s work. If you’d like, we could even meditate together! Let me know if you would like that, okay? Love, Mom.";
            }
            if (day == 10) {
                if (status == 0) {
                    message.text = "Honey, I am so glad you’ve kept in touch. I know these past few months have been hard on you, it was hard on us, too. I really miss you. Let me know if I can come visit you. I promise to bring you a treat! Love, Mom.";
                }
                if (status == 1) {
                    message.text = "Honey, Are you okay? I haven’t heard from you lately. I guess Agenda is helping you stay very productive. Let me know if you need anything, okay? I miss you. Love, Mom";
                }
                if (status == 2) {
                    message.text = "Please let me know that you are okay. I love you so much. Stay safe. Love, Mom";
                }
            }
            newResponse(day, person);
        }
        if (person == 2) {
            if (day == 3) {
                message.text = "Dear (NAME), \nSurprise! It is Gramma! It has been so long since we last talked. How are you doing? How is school or are you working now? I miss you so much i wish we could talk more. Your mother said you reply to emails now. I asked my neighbor;s son to teach me how to use it. Let me know if it worked because i cannot really tell. I hope you read this soon. I want you to know that I love you and miss you and I am so proud of you. Your mother tells me you are doing well, even if you are alone. Send me an email if you ever have trouble or want advice. I would love to help! \nWith many hugs and kisses, \nGramma Betsy.";
            }
            if (day == 5) {
                if (intGrandma == 3) { // Organized response to Day 3
                    message.text = "Dear (NAME), \nIt was so nice to hear from you! I am happy the email thing worked. And I am happy that you are doing well! I hope it is sunny where you are. The weather here has not treated me well. My knees hurt, but Rufus is good at reminding me to use my cane. Do you remember Rufus? We adopted the old pug a few years ago. That dog keeps me on my toes! Whenever he wants to walk he hauls my cane and his leash to the door and sits there until I stop what I am doing and we go walk. He is so stubborn, but he is irresistibly cute. How about you? I hope you are finding time to stay connected to the things you like. After all, what is the point of life, if not to enjoy it with those you love? \nWith many hugs and kisses, \nGramma Betsy.";
                }
                if (intGrandma == 2) { // Disorganized response Day 3
                    message.text = "Dear (NAME), \nIt was so nice to hear from you! I am happy the email thing worked. I hope you are doing better now. Whatever is troubling you, I know you can find a way. You are a wonderful person. You can always make this situation work for you. Do you remember Rufus? He misses you. He is a bit upset at me. We did not get to go out for a walk last night because of the storm. I wanted to walk him earlier, but my knees are not what they used to be. I tried taking a short nap but slept all the way until dinnertime! When I woke up, he was already at the door, with my cane and his leash, waiting for me. Since it was storming, I tried to do the best I could. We went for a walk around the house, three times! It was not the same as going outside, but it was better than not walking at all. I have faith in you. I know you will be alright. \nWith many hugs and kisses, \nGramma Betsy.";
                }
            }
            if (day == 6) {
                if (intGrandma == 4 || status == 0) { // if no response Day 3 or ISOLATED state
                    message.text = "Dear (NAME), \nDid my email go through? The neighbor's kid says it did. Can you see this? I miss you lots. Your mother says you have not called her recently either. I hope you know we all love you. It is okay to take time for yourself, but try to keep in touch, even if you can only talk once in a little while. Miss you, love bug. \nWith many hugs and kisses, \nGramma Betsy.";
                }
            }
            if (day == 8) {
                message.text = "Dear (NAME), \nHow are you doing, sweetheart? I can tell the season will soon be over, I can see it in how the leaves are changing on the trees in my backyard. You used to play there when you were little. You liked climbing them, and though you never got far, you looked like you were having fun. Are your classes ending soon? Rufus and I miss you so much. Maybe, once your classes are over, you can come visit! I wonder if you can climb the trees now, perhaps even reach the very top. \nWith many hugs and kisses, \nGramma Betsy";
            }
        }
        if (person == 4) {
            if (buddyDay == 1 && intBuddy == 1) {
                msgBuddy = "Hey, I saw that you submitted a post about trying out piano for the assignment. I thought that was cool. I play piano, too. Let me know if you ever want to talk music";
            }
            if (buddyDay == 2 && intBuddy == 2) {
                msgBuddy = "Thanks for reaching out! No one else did, even though that was the whole point of the assignment lol. I dunno why people can’t just follow the rules the teacher set out. It’s not hard or anything. Yeah I play piano. I mean, I play a lot of instruments, like acoustic guitar and drums and the ocarina. I’m actually trying to learn the ukulele right now. What have you been up to on the piano lately? It’s neat to talk to someone else who also started playing piano recently and I’m curious to see how we each take this differently";
            }
            if (buddyDay == 3 && intBuddy == 3) {
                msgBuddy = "Sorry i’m so late with replying things are really piling up for me. I’m taking a lot of credits this semester. Figured I would just keep myself busy since I’m stuck at home,y’know? But I’m really feeling like I’m over my head. I can’t explain it, it’s like some days I’ll get so keyed up that I’m drowning in work. but uh, speaking of keys - you said you were learning a new song? That’s cool, how’s it going?";
            }
            if (buddyDay == 4) {
                if (intBuddy == 4) { //romance
                    msgBuddy = "Thanks for your message earlier :) I appreciate you offering to go out of your way to help me out. Your advice was really good too. I tried it and it helped. I really sat down and thought about my priorities and it got me thinking about my future, and how school won’t always be what i do. Like I wanna travel, and i want a job that gives back to the community in some way, and i wanna do music. I guess, i shouldnt really worry all the time whether or not i finish something. Its more important to do things with meaning than to just, well, do things. Thanks for reminding me that there’s more to live for than just being productive all the time.";
                }
                if (intBuddy == 5) { //friendly
                    msgBuddy = "Friendly: Thanks for your message earlier. It made me feel better better feeling that i wasn’t alone in my struggles, y’know? Appreciated it. I wouldn’t say im doing better, but im actually finishing things instead of just starting and abandoning projects. Yesterday I spent a full uninterrupted hour just playing piano. I feel like its been months since I got to do just one thing at a time. Im always multitasking. I want to start focusing on just one thing at a time. Thats my goal for this week. What about you?";
                }
            }
            if (buddyDay == 5) {
                if (intBuddy == 6) { // romance
                    msgBuddy = "Hey :) Hope you’re doing well. I’m okay. Been better, been worse. But i think im getting better. Im still stressed about school and stuff, but not like before. It doesnt feel like im drowning all the time. You really helped me. I feel like i owe you one for that haha. Hey, when this is all over, would you want to meet up on campus sometime? I think it would be really cool to chill together. I dunno. Lmk if you want to, kay?";
                }
                if (intBuddy == 7) { //friendly
                    msgBuddy = "Thought id hit yu up and see how yu doin. Im fine. Been better, been worse. Got around to composing my own little song on ukelele. I kinda liked it. Its an easy 1 - 5 - 6 - 4 - 1 progression, but I made it, and I finished it, and thats what matters yknow. Let me know if yu wanna talk music again this week - Im finally at a place where I can make time to do what i like to do :)";
                }
            }
        }
        if (person == 5) {
            // Using brieDay, not day — see above
            if (brieDay == 1 && intBrie == 1) {
                msgBrie = "Hiya! nice to meet yu! myname is brie and i’m suuuuper into gaming. you are too rih, i saww on the class assgnm that yu put Blaster as the game ure plyn rn? thats so cool i play it too! lmk if you ever wanna play tgthr sumtim!";
            }
            if (brieDay == 2 && intBrie == 2) {
                if (intBrie == 2) {
                    msgBrie = "hiya! Wow thnk yu sooo much! i didnt think anyone wuld reply and i sent out like 5 msgs haha so tysm. i just broke my record again in blaster earlier today!! ive been practicin a whooole bunch ever since we all started staying inside. my tag is baeisbrie#0934. hmu whenev n lezzgame!";
                }
                if (intBrie == 3) {
                    msgBrie = "hiya! Wow thnk yu sooo much! i didnt think anyone wuld reply and i sent out like 5 msgs haha so tysm. so liek i've always loved these kinda games, wuld play them aftr skool all th time in highskol. wen i found blaster i jsut couldn't stop myself n then i got this gaming PC and had to emulate it. took me ages to figure it out but i did. its SO worth it. now ppl can just buy the remastered and its just, ugh, i already went through the hard of emulating it. but then it had online and i was just >.> culdnt resist. wbu how'd yu get to playin it?";
                }
            }
            if (brieDay == 3) {
                msgBrie = "hiya! i was wondurin if yuu culd help me w/school? its a lil awkward but uh im actuly not doin too well. uh basically school is like, idk, boring? Or, um, not, not stimulating enough yknow? i  kinda phaze out  alot staring at lectures and picturing me playing Blaster. Idk i like rlly try but ill sit down t do an assignmnt and my brain just clocks out. And ive been sleeping a lot more since i spend all night doing blaster and, well, sleep feels like the only other thing im goodat. Idk how to describe it. but i was wondering if you had tips? Like, study methods that rlly worked for you? or somthin? imean u also dont have to reply like i get it, im just some weird girl whos shit at school hehe, yeah, um but liek im still down 4 Blaster liek whenver so yah just hmu bye";
            }
            if (brieDay == 4) {
                if (intBrie == 6) {
                    msgBrie = "hehe ^///^ ty i appreciate that. thankyu for your kind words and advice - i think i’ll try them out. The to-do list seems like a good idea, i just, im scared ill get too anxious and wont do it. Whenever i get really stressed my brain kinda shuts off and i either sleep or game. its so weird, like i know that i have things to do, and i know i should do them, i just...don’t. and i really try, too! i just cant seem to stay motivated long enough to actually do it. do you experience this too? sometimes it feels im the only one going through stuff like this. everyone online seems fine, and none of my friends have mentioned having trouble like this.";
                }
                if (intBrie == 7) {
                    msgBrie = "oh wow thankyu for ur message!!! i think i get what ure saying. I appreciate yur advice, too, helped me think of ways i could change my mind about things. It gave me new perspective. i reached out to our prof to letthem know im not doin too well. i dunno if itll help my grade but at least theyll know why. i also tried to build myself a schedule. i got some paper and wrote everything i needed to do down. it felt weird just writing things down, normally i type everything. my pinkie was all smudged in lead haha. after i wrote everything down i just, my brain just went silent again looking at the list, but instead of laying down i played with my dog instead. i feel like i have to find different ways of dealing with feeling anxious. i feel like if i can do that, redirect my anxiety to something else, i might start being more productive again. let’s keep talking, okay? ure the first person in a long time to give me valuable advice and i want us to stay friends if we can";
                }
            }
            if (brieDay == 5) {
                if (intBrie == 8) {
                    msgBrie = "it worked!!!! ur suggestions worked!! i mean, sorta, but, i wrote everything down and then i looked at it and i said okay what can i do here right away and i did it! And yeah afterwards i felt exhausted but at least i did sumthin yknow???like yesterday iw asnt doing ANYTHING and now i did SOMEHTING :D tht made me really happy tysm!!!! actually do u maybe wanna video chat sometime? i feel like ovr text we talk a lot about me but i wanna learn more about u!! uve helped me breakthrough a really rough patch and i wanna pay it forward, yknow?  I could deff teach you some Blaster techniques to up ur score, that’s for sure! XD";
                }
                if (intBrie == 9) {
                    msgBrie = "it worked!!!! ur suggestions worked!! i mean, sorta, but, i wrote everything down and then i looked at it and i said okay what can i do here right away and i did it! And yeah afterwards i felt exhausted but at least i did sumthin yknow???like yesterday iw asnt doing ANYTHING and now i did SOMEHTING :D tht made me really happy tysm!!!! actually do u maybe wanna video chat sometime? i had this super cool idea, like, we both like blaster, we're both rlly good at it, but we're bummed they dont have a sequeal. why dont we just... make our own??? ITS WILD I KNOW but like >.> culd be rlly fun?! culd be RLLY cool??!! lmk what yuu think!!!";
                }
            }
            if (brieDay == 6) {
                if (intBrie == 10) {
                    msgBrie = "hiya there :D thanks for replying, it was good hearing from yuu. hows your highscore in blaster coming? beat mine yet? im excited 4 our nxt match ;) ive started going through my to-do list, btw. its super stressful, but im doin it, little by little. i finished my final assignments at least, so that’s good! and i only sleep 10 hrs a day, not 14 hrs. i think thats a great improvement!!!! when i do something good, i reward myself with playing blaster :))) i think ill do some research bout productivity styles and systems and see if that can give me more information. i also wanna say thank you because i dont think i would have made it this far without your help. Tysm 4 ur advice and ur perspective and helping me grow as a person ^.^ ik schul is out but let’s keep in touch!! ure a gr8 friend, and ure pretty decent at blaster too ;P";
                }
                if (intBrie == 11) {
                    msgBrie = "hiya there :D thanks for replying, it was good hearing from yuu. hows your highscore in blaster coming? beat mine yet? im excited 4 our nxt match ;) ive started going through my to-do list, btw. its super stressful, but im doin it, little by little. i finished my final assignments at least, so that’s good! and i only sleep 10 hrs a day, not 14 hrs. i think thats a great improvement!!!! i think ill do some research bout productivity styles and systems and see if that helps. i also wanna say thank you because i dont think i would have made it this far without your help. actually, i had this idea the other day, and i thought it would be really cool to work wit you on it. we're both super experienced gamers w/ blaster, rih? so, why dont we just, make our own blaster? idk i think that would be real neat! we could put music and make more challenging levels and its like, ive been really motivated lately about it, ive got all this stuff set up and just let me know if you're interested, ok? ive got lots to show you!";
                }
            }
        }
        if (person == 6) {
            if (day == 3) {
                msgMatt = "Yo, Wassup. Its ur cuz Matt. How ya doin? Ma said ur livin alone rn, how ya managing? Must be different to be alone all the time. Im still with family. Sometimes i wish i was totally alone but ik tht id prolly go bananas if i was by myself. Hang in there, cuz. Lets talk again soon.";
            }
            if (day == 5) { 
                msgMatt = "Yo, Wassup? Heard you got the latest Agenda thing, dats pretty cool. Sposed to help yu stay on top of schoolwork, right? does it actually help tho? i figured its one of those habit-trackers but idk if a tool can help you be more productive if you aint committed to it. But idk tho. Im taking HS online rn an it sukz but i know that if i keep steady i can get to where i wanna go. does Agenda help remind you of why ya workin hard or does it just force you to get the work done?";
            }
            if (day == 7) {
                //msgMatt = ""
            }
            if (day == 9) {
                //msgMatt = ""
                //revisit once we figure out the metric for "organization" (?) which is mentioned in dialogue but not laid out
            }
        }
    }



    void newResponse (int day, int person) {
        if (person == 1) {
            if (day == 2) {
                replyBox1.SetActive(false);
                response2.text = "Hi mom! Thanks for the Agenda! It's been helping out a lot.";
                response3.text = "Hey mom. Thanks for the Agenda. Very busy- talk soon!";
            }
            if (day == 4) {
                response1.text = "Oh no, I forgot about it!";
                response2.text = "Im doing well, and the plant is, too!";
                response3.text = "The plant is okay. I'm trying my best with other things, too.";
            }
            if (day == 6) {
                response1.text = "Mm, banana bread. Bring me some!";
                response2.text = "That sounds great! When I'm done with school, I can come over and help.";
                response3.text = "Oh, that's nice. Yeah. Good for you.";
            }
            if (day == 8) {
                response1.text = "I have a yoga mat too and I use it all the time!";
                response2.text = "I'm too busy for yoga, but I'm happy it works for you.";
                response3.text = "How are you always doing better, and I'm doing worse? I don't understand...";
            }
            if (day == 10) {
                if (status == 0) {
                    replyBox1.SetActive(false);
                    response2.text = "I'm too busy for a visit, but let's chat soon.";
                    response3.text = "I'd love to see you. I miss you.";
                }
                if (status == 1) {
                    replyBox1.SetActive(false);
                    response2.text = "I hate Agenda. I hate this. Nothing I do helps. Everything sucks.";
                    response3.text = "I'm sorry I never messaged you. I was so busy.";
                }
                if (status == 2) {
                    replyBox1.SetActive(false);
                    response2.text = "Please help. I'm so alone.";
                    response3.text = "Don't contact me again.";
                }
            }
        }
        if (person == 2) {
            if (day == 3) {
                replyBox1.SetActive(false);
                response2.text = "hi gramma! how are you? i'm okay…";
                response3.text = "hi gramma! how are you? i'm good!";
            }
            if (day == 5) {
                response1.text = "I'm working hard now so I can do what I like later. you have to work to get to do what you love.";
                response2.text = "Sometimes, I feel like I'm spending too much time doing things that won't help my future...";
                response3.text = "I try to work and play—it’s hard, but I get to do what I need and what I want!";
            }
            if (day == 6) {
                replyBox1.SetActive(false);
                response2.text = "sorry, gramma. i'm better now. how are you?";
                response3.text = "sorry, gramma. i'm okay, i just don't think i can talk right now.";
            }
            if (day == 8) {
                replyBox1.SetActive(false);
                response2.text = "I’ll visit you and Rufus as soon as I can!";
                response3.text = "I can’t come now, but let’s stay in touch.";
            }
        }
        if (person == 4) {
            if (buddyDay == 1) {
                replyBox1.SetActive(false);
                response2.text = "lets talk music bro :)";
                response3.text = "only just started playing piano. when did you start? ";
            }
            if (buddyDay == 2) {
                replyBox1.SetActive(false);
                response2.text = "idk a piano just exists in my room and i figured, eh, why not?";
                response3.text = "i tried practicing and it's a bit awkward, but like i'm kinda feelin' it";
            }
            if (buddyDay == 3) {
                response1.text = "maybe like, get a life? school is for tools. git gud.";
                response2.text = "sounds rough, man. i hope you feel better soon. i'm always here to chat if you need it. if you try taking more breaks, that could help with the stress.";
                response3.text = "been there bro, it sucks. i focused on prioritizing me-time, and balanced my work w/ my health. that's the best thing to do, dude.";
            }
            if (buddyDay == 4) {
                if (buddyRomance) {
                    replyBox1.SetActive(false);
                    response2.text = "maybe there's a way you can decide on your top priorities? then do the things that matter most.";
                    response3.text = "no problem. i'm always happy to help :) maybe you could make a list of your priorities? that could help you decide on what to do in a given day.";
                }
                if (!buddyromance) {
                    replyBox1.SetActive(false);
                    response2.text = "maybe try and take a full break this time. total relaxation, no thinking about work at all.";
                    response3.text = "i feel you. maybe focus on one thing at a time instead of everything at once? maybe do a priorities list or something to focus on just the one thing.";
                }
            }
            if (buddyDay == 5) {
                if (buddyRomance) {
                    replyBox1.SetActive(false);
                    response2.text = "i'd love that :)";
                    response3.text = "yeah! we could jam together too!";
                }
                if (!buddyRomance) {
                    replyBox1.SetActive(false);
                    response2.text = "so happy to hear you're feeling like yourself more!";
                    response3.text = "i'm doing alright. piano's going well. school is too. just, y'know, tryna make it.";
                }
            }
        }
        if (person == 5) {
            if (brieDay == 1) {
                replyBox1.SetActive(false);
                response2.text = "lezzgo play online rn just unlocked it, my high score is....";
                response3.text = "hahaha heck yeah lets play sometime. how'd you get to playing the game?";
            }
            if (brieDay == 2 && intBrie == 2) {
                replyBox1.SetActive(false);
                replyBox2.SetActive(false);
                response3.text = "my tag is itsjustagame#6969, hmu rn, lezzdoeit";
            }
            if (brieDay == 2 && intBrie == 3) {
                replyBox1.SetActive(false);
                replyBox2.SetActive(false);
                response3.text = "it was just in my computer like randomly bro idek…";
            }
            if (brieDay == 3) {
                response1.text = "you should just stop playing blaster. period. oh and stop sleeping so much. Duh.";
                response2.text = "sounds like you're going through a rough time. and that's ok :) you're not weird, promise. [Emphasize that she isn’t just 'some weird girl', instead that she is struggling, and that’s okay. Offer advice later.]";
                response3.text = "ok so when i had issues focusing i would exercise, and that could help with sleep too. i also suggest...[Focus on actions. Offer advice, agree things are tough]";
            }
            if (brieDay == 4) {
                if (intBrie == 6) {
                    response1.text = "not really, but that's okay, your experiences are unique. keep trying the techniques and see if it helps. have you tried a reward system with gaming?";
                    response2.text = "not really, but that's okay. i'm grateful you trust me with this :) ill do my best to help";
                    response3.text = "yes i totally get what you mean! sometimes, when its rlly bad for me, it feels like...";
                }
                if (intBrie == 7) {
                    replyBox1.SetActive(false);
                    response2.text = "lets deff keep talking, it helps me to connect with someone else about this stuff too.";
                    response3.text = "hope things work themselves out. hang in there.";
                }
            }
            if (brieDay == 5) {
                if (intBrie == 8) {
                    replyBox1.SetActive(false);
                    response2.text = "i'd love that :)";
                    response3.text = "sure! we should find some time to game together, too.";                    
                }
                if (intBrie == 9) {
                    replyBox1.SetActive(false);
                    response2.text = "make a game? SIGN ME UP";
                    response3.text = "got too much on my plate rn. maybe another time!";
                }
            }
            if (brieDay == 6) {
                if (intBrie == 10) {
                    replyBox1.SetActive(false);
                    replyBox2.SetActive(false);
                    response3.text = "also reached out to someone, like a professional, to help me out with my anxiety. i think i need more help on this than i can give myself.";
                }
                if (intBrie == 11) {
                    replyBox1.SetActive(false);
                    response2.text = "heck yeah, lets do it! show me wat you got";
                    response3.text = "i think rn ima focus on school but thanks for the offer tho!";
                }
            }
            if (brieDay == 7) {
                if (intBrie == 12) {
                    replyBox1.SetActive(false);
                    replyBox2.SetActive(false);
                    response3.text = "hiya! here's something quick i whipped up for the game. wanna play it and check it out?";
                }
            }
        }
    }
}
