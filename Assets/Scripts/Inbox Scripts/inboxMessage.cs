using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inboxMessage : MonoBehaviour
{
    public TextMeshProUGUI message;
    public TextMeshProUGUI response1, response2, response3;
    public string msgMom, msgGrandma, msgProf, msgBuddy, msgBrie, msgMatt;
    public GameObject replyBox1, replyBox2, replyBox3;
    public int dayShown, personShown;
    public int buddyDay, brieDay;
    public bool brieTalk = false; // only incr brieDay if convo started w/ Brie, start false set true
    public int intMom, intGrandma, intProf, intBuddy, intBrie, intMatt;
    public int status;
    public int playerResponse;

    /*
    For simplicity:
    - Days are denoted as int 1-10 (eg. Day 1 is "1")
    - People are numbered in order of Inbox UI, as follows:

    1: Mom
    2: Grandma Betty
    3: Prof. Turner
    4: Buddy
    5: Brie
    6: Cuz Matt

    dayShown and personShown variables store last shown
    changeDay() and changePerson() if day or person changes, respectively

    Day changes daily, should be implemeneted in Tracking script*
    Person changes when message is selected, implemented through Inbox buttons

    *Some chars use independent day vars (eg. brie uses brieDay) b/c depends on how many
    days you've been talking to them, no necessarily which day it is in-game
    These indepedent variables will simply increment in the changeDay() method
    Those characters are as follows:
    - Brie
    - [add as nec]

    Status changes based on responses, implemented in-game and updated
    through updateStatus() method
    Status is: "isolated", "allResponse", or "noResponse"

    "int" refers to interactiveness, determined on per-character basis for how
    often player has responded to their messages
    0 = isolated
    1 = no response
    2 = all response
    "Status" stores # 0-2 based on which character, outside of newMessage()

    Note on response boxes:
    Messages have 0-3 built-in responses. Based on how many, response boxes will hide themselves
    Box 1 top, box 2 middle, box 3 bottom
    eg. 1 possible response shows only box 3; 2 responses shows boxes 2-3

    #################################################################
    #### EDITOR NOTES FOR PROGRESS/TO-DOS, WILL BE REMOVED LATER ####
    #################################################################

    Note to self: implement a script attached to reply buttons to affect statuses of
    convos, such as intBrie for Brie... also add hide text box mechanic later

    Note to self: Buddy's dialogue is unfinished; certain replies still need to be
    added and written as necessary.

    Note to self: see that intNpc days are initialized to 1, add methods for
    changing to be linked to buttons later (global script)

    See extended to-dos and notes in Discord
    */

    void changeDay(int newDay) {
        dayShown = newDay;
        /** if (brieTalk == true) {
            brieDay++;
        } */
        showMessage(dayShown, personShown, status);
    }
    void changePerson(int newPerson) {
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
        newMessage(2, 1, 0);
    }

    // The following method is for response button use
    public void responseSent(int buttonNum) {
        playerResponse = buttonNum;
    }

    void newMessage(int day, int person, int status) {
        if (person == 1) {
            Debug.Log("Mom"); // This was for testing purposes, remove later
            Debug.Log("Day is " + day);
            if (day == 2) {
                message.text = "Sweetheart, Is Agenda helping you stay on track with your goals? I’ve heard so much about how it helped other students, and I thought it would be a good present for you, now that you are doing school from home. I’ve felt myself slipping lately - work isn’t what it used to be for me. I am finding my way through. In fact, I’ve taken up knitting. Would you like me to make you a scarf? I promise I can make it super fluffy and warm - just the way you like it. Love, Mom.";
            }
            if (day == 4) {
                message.text = "Sweetheart, remember the pair of bonsai plants we bought together? Mine finally bloomed! It’s so pretty and lush, I’m quite proud of how well I’ve taken care of it. I plan on decorating it for the winter holidays. How about yours? Have you watered it yet? I hope you are doing well and staying on top of your studies. I miss you. Love, Mom";
            }
            if (day == 7) {
                message.text = "Sweetheart, have you had any sweets lately? I’ve had such a craving for cake the past few weeks. Remember when I mentioned I wanted to get into baking? Well, I started baking banana bread. It’s healthy and super yummy, and I’m quite good at it! The trick is to use whole wheat flour and substitute white sugar for honey. Then you can make a delicious and mostly-healthy treat for just after supper. I would love to chat sometime soon. Maybe I can even bring you a loaf. Wouldn’t that be swell! Love, Mom.";
            }
            if (day == 9) {
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
            if (day == 4) {

            }
            if (day == 6) {

            }
            if (day == 10) {

            }
        }
        if (person == 4) {
            if (buddyDay == 1 && intBrie == 1) {
                msgBuddy = "Hey, I saw that you submitted a post about trying out piano for the assignment. I thought that was cool. I play piano, too. Let me know if you ever want to talk music";
            }
            if (buddyDay == 2 && intBuddy == 2) {
                msgBuddy = "Thanks for reaching out! No one else did, even though that was the whole point of the assignment lol. I dunno why people can’t just follow the rules the teacher set out. It’s not hard or anything. Yeah I play piano. I mean, I play a lot of instruments, like acoustic guitar and drums and the ocarina. I’m actually trying to learn the ukulele right now. What have you been up to on the piano lately? It’s neat to talk to someone else who also started playing piano recently and I’m curious to see how we each take this differently";
            }
            if (buddyDay == 3 && intBuddy == 3) {
                msgBuddy = "Sorry i’m so late with replying things are really piling up for me. I’m taking a lot of credits this semester. Figured I would just keep myself busy since I’m stuck at home,y’know? But I’m really feeling like I’m over my head. I can’t explain it, it’s like some days I’ll get so keyed up that I’m drowning in work. but uh, speaking of keys - you said you were learning a new song? That’s cool, how’s it going?";
            }
            if (buddyDay == 4) {
                if (intBuddy == 4) {
                    msgBuddy = "Thanks for your message earlier :) I appreciate you offering to go out of your way to help me out. Your advice was really good too. I tried it and it helped. I really sat down and thought about my priorities and it got me thinking about my future, and how school won’t always be what i do. Like I wanna travel, and i want a job that gives back to the community in some way, and i wanna do music. I guess, i shouldnt really worry all the time whether or not i finish something. Its more important to do things with meaning than to just, well, do things. Thanks for reminding me that there’s more to live for than just being productive all the time.";
                }
                if (intBuddy == 5) {
                    msgBuddy = "Friendly: Thanks for your message earlier. It made me feel better better feeling that i wasn’t alone in my struggles, y’know? Appreciated it. I wouldn’t say im doing better, but im actually finishing things instead of just starting and abandoning projects. Yesterday I spent a full uninterrupted hour just playing piano. I feel like its been months since I got to do just one thing at a time. Im always multitasking. I want to start focusing on just one thing at a time. Thats my goal for this week. What about you?";
                }
            }
            if (buddyDay == 5) {
                if (intBuddy == 6) {
                    msgBuddy = "Hey :) Hope you’re doing well. I’m okay. Been better, been worse. But i think im getting better. Im still stressed about school and stuff, but not like before. It doesnt feel like im drowning all the time. You really helped me. I feel like i owe you one for that haha. Hey, when this is all over, would you want to meet up on campus sometime? I think it would be really cool to chill together. I dunno. Lmk if you want to, kay?";
                }
                if (intBuddy == 7) {
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
            }
            if (brieDay == 5) {
                if (intBrie == 7) {
                    msgBrie = "oh wow thankyu for ur message!!! i think i get what ure saying. I appreciate yur advice, too, helped me think of ways i could change my mind about things. It gave me new perspective. i reached out to our prof to letthem know im not doin too well. i dunno if itll help my grade but at least theyll know why. i also tried to build myself a schedule. i got some paper and wrote everything i needed to do down. it felt weird just writing things down, normally i type everything. my pinkie was all smudged in lead haha. after i wrote everything down i just, my brain just went silent again looking at the list, but instead of laying down i played with my dog instead. i feel like i have to find different ways of dealing with feeling anxious. i feel like if i can do that, redirect my anxiety to something else, i might start being more productive again. let’s keep talking, okay? ure the first person in a long time to give me valuable advice and i want us to stay friends if we can";
                }
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

            }
            if (day == 5) { 

            }
            if (day == 7) {

            }
            if (day == 9) {
                
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
                response1.text = "MOM DAY 4 RESPONSE 1 FILLER TEXT";
                response2.text = "MOM DAY 4 RESPONSE 2 FILLER TEXT";
            }
            if (day == 7) {
                response1.text = "MOM DAY 7 RESPONSE 1 FILLER TEXT";
                response2.text = "MOM DAY 7 RESPONSE 2 FILLER TEXT";
            }
            if (day == 9) {
                response1.text = "MOM DAY 9 RESPONSE 1 FILLER TEXT";
                response2.text = "MOM DAY 9 RESPONSE 2 FILLER TEXT";
            }
            if (day == 10) {
                response1.text = "MOM DAY 10 RESPONSE 1 FILLER TEXT";
                response2.text = "MOM DAY 10 RESPONSE 2 FILLER TEXT";
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
            if (brieDay == 4 && intBrie == 6) {
                response1.text = "not really, but that's okay, your experiences are unique. keep trying the techniques and see if it helps. have you tried a reward system with gaming?";
                response2.text = "not really, but that's okay. i'm grateful you trust me with this :) ill do my best to help";
                response3.text = "yes i totally get what you mean! sometimes, when its rlly bad for me, it feels like...";
            }
            if (brieDay == 5) {
                if (intBrie == 7) {
                    replyBox1.SetActive(false);
                    response2.text = "lets deff keep talking, it helps me to connect with someone else about this stuff too.";
                    response3.text = "hope things work themselves out. hang in there.";
                }
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
                if (intBrie == 1) {
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
