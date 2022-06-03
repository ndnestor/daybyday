using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignmentControl : MonoBehaviour
{
    /**
    Function of script in controlling assignments screen:
    1. Control which assignments appear across days
        --> Past due, marked completion, which active
    2. Assignment announcements screen control
    3. Notebook interactability -- notebook automatically not reachable within announcement page
    4. Agenda's goals on non-interactive side -- get from Eric
    **/

    //Have to figure out how to get the days from Tracking

    [SerializeField] Button button1, button2, submitButton;
    [SerializeField] TextMeshProUGUI buttonText1, buttonText2;
    [SerializeField] GameObject announcementPage;
    [SerializeField] TextMeshProUGUI announcementText;
    [SerializeField] TextMeshProUGUI agendaGoals, typerBox;
    public int dayNum;
    int assignment1, assignment2, viewedAssignment, activeAssignment;
    bool activateSubmit;

    public void openActiveAssignment1() {
        viewedAssignment = 1;
        switch(assignment1) {
            case 1:
                announcementText.text = "Happy March, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to: a) Use some hobby items around your room (i.e. read a book, play the piano, etc)and b) on our discussion board about your experience. You can write the full post within an hour, but you decide how much time you spend on exploring your hobbies. I expect to see thoughtful discussion within the week!";
                break;
            case 2:
                if(dayNum == 3) announcementText.text = "Happy May, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to make and present your  goals for the summer. I recommend starting to work on it now so that you can submit it on time next month. Use our class discussion about work-life balances and SMART goals (Specific, Measured, Attainable, Relevant, Time-based) to design your goals for the summer. They can be anything from getting a summer job to recording a song on a keyboard. For this month, you can optionally take an hour to post your work to the discussion board; that way we can all help you make SMART goals that work right for you. I expect to see some well-developed and exciting goals submitted soon!";
                else if(dayNum == 4) announcementText.text = "Happy June, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to make and present your  goals for the summer. Use our class discussion about work-life balances and SMART goals (Specific, Measured, Attainable, Relevant, Time-based) to design your goals for the summer. Plan to use about an hour to present your goals, submit as a discussion post, and reply to your peers’ posts with helpful comments for their own goals. I expect to see constructive criticism throughout our discussions this week!";
                break;
            case 3:
                if(dayNum == 5) announcementText.text = "Happy July, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to find someone in a career field you are interested in and interview them. By now you should have done the interview, and are beginning to write your essay reflecting on the state of your chosen career field, the future implications of working in that field, the necessary skills for work, and some potential avenues for you to acquire it. Also include an analysis of your approach for finding a candidate and the process of the interview itself. Since this is a two month long project, this week, post the introductory paragraph for your paper, and reply to your peers’ posts. This way we can all keep each other accountable to completing the paper, as well as provide each other with excellent feedback! I expect to see constructive criticism for your essays soon!";
                else if(dayNum == 6) announcementText.text = "Happy August, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to find someone in a career field you are interested in and interview them. Then, you should write an essay reflecting on the state of your chosen career field, the future implications of working in that field, the necessary skills for work, and some potential avenues for you to acquire it. Also include an analysis of your approach for finding a candidate and the process of the interview itself. Since this is a two month long project, this week, post a body paragraph you have found particularly tricky to write. Then reply to your peers’ posts about their paragraphs so that we can all provide each other with helpful, last-minute feedback. I expect to see your well-written papers soon!";
                break;
            case 4:
                if(dayNum == 7) announcementText.text = "Happy September, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to find three job ads and analyze them in the context of what we learned for this class. For example, you can explain how you found the job ads, whether or not they fit into your career plans, and if so, how you plan on applying to them. You could also explain your process for finding and communicating with the hiring managers, or deciding on ways to make the best impression you can on your first interview for this position. So long as you apply a class concept of time-management and professional development, there’s no way you can go wrong in this assignment! To help you all brainstorm, post one job description from an ad you find online. Then, reply to another’s post about the ad, using some class concepts to analyze and bring clarity to the description. I expect to see some excellent analysis soon!";
                else if(dayNum == 8) announcementText.text = "Happy October, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to find three job ads and analyze them in the context of what we learned for this class. For example, you can explain how you found the job ads, whether or not they fit into your career plans, and if so, how you plan on applying to them. You could also explain your process for finding and communicating with the hiring managers, or deciding on ways to make the best impression you can on your first interview for this position. So long as you apply a class concept of time-management and professional development, there’s no way you can go wrong in this assignment! I expect to see some excellent analysis soon!";
                break;
            case 5:
                if(dayNum == 9) announcementText.text = "Happy November, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to apply to a job. Tell us about the process of finding the application, restructuring your resume, and interviewing for the position. Also, write about how this job falls into your career plans. Think about the SMART goals we made earlier this year. Are these goals still relevant? Why or why not? To help stave away the worst of the job-hunting jitters, post about the job you have decided to apply to, and reply . I’m looking forward to encouraging you through your job application process soon!";
                else if(dayNum == 10) announcementText.text = "Happy December, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to apply to a job. Tell us about the process of finding the application, restructuring your resume, and interviewing for the position. Also, write about how this job falls into your career plans. Think about the SMART goals we made earlier this year. Are these goals still relevant? Why or why not? I’m looking forward to reading about your job application process soon!";
                break;
        }
        announcementPage.SetActive(true);
    }
    public void openActiveAssignment2() {
        viewedAssignment = 2;
        switch(assignment2) {
            case 3:
                //Assignment 3A
                announcementText.text = "Happy June, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to find someone in a career field you are interested in and interview them. The interview should be 1 hour long. After that, write an essay reflecting on the state of your chosen career field, the future implications of working in that field, the necessary skills for work, and some potential avenues for you to acquire it. Also include an analysis of your approach for finding a candidate and the process of the interview itself. Since this is a two month long project, this week, post a short reflection piece about the interview process and what you learned about your potential future career field. Then reply to your peers’ posts about their interviews. I expect to see exciting revelations about your future jobs soon!";
                break;
        }
        announcementPage.SetActive(true);
    }
    public void closeAnnouncement() {
        announcementPage.SetActive(false);
    }

    public void startAssignment() {
        activateSubmit = true;
        announcementPage.SetActive(false);
    }
    public void assignmentComplete() {
        activateSubmit = false;
        submitButton.gameObject.SetActive(false);
        //typerBox.text = "Assignment Submitted!";
    }
    public bool isAssignmentActive() {
        //Called by notepad controller (toggle)
        //If an assignment is active, returns true and submit button appears
        return activateSubmit;
    }

    void Start() {
        setAssignments();
        setGoals();
    }

    public void setAssignments() {
        //Called upon day change
        switch (dayNum) {
            case 1:
                button1.gameObject.SetActive(true);
                buttonText1.GetComponent<TextMeshProUGUI>().text = "  Assignment 1";
                button2.gameObject.SetActive(false);
                assignment1 = 1;
                assignment2 = 0;
                break;
            case 2:
                button1.gameObject.SetActive(false);
                button2.gameObject.SetActive(false);
                assignment1 = assignment2 = 0;
                break;
            case 3:
                button1.gameObject.SetActive(true);
                buttonText1.GetComponent<TextMeshProUGUI>().text = "  Assignment 2 Part A";
                button2.gameObject.SetActive(false);
                assignment1 = 2;
                assignment2 = 0;
                break;
            case 4:
                button1.gameObject.SetActive(true);
                button2.gameObject.SetActive(true);
                buttonText1.GetComponent<TextMeshProUGUI>().text = "  Assignment 2 Part B";
                buttonText2.GetComponent<TextMeshProUGUI>().text = "  Assignment 3 Part A";
                assignment1 = 2;
                assignment2 = 3;
                break;
            case 5:
                button1.gameObject.SetActive(true);
                buttonText1.GetComponent<TextMeshProUGUI>().text = "  Assignment 3 Part B";
                button2.gameObject.SetActive(false);
                assignment1 = 3;
                assignment2 = 0;
                break;
            case 6:
                button1.gameObject.SetActive(true);
                buttonText1.GetComponent<TextMeshProUGUI>().text = "  Assignment 3 Part C";
                button2.gameObject.SetActive(false);
                assignment1 = 3;
                assignment2 = 0;
                break;
            case 7:
                button1.gameObject.SetActive(true);
                buttonText1.GetComponent<TextMeshProUGUI>().text = "  Assignment 4 Part A";
                button2.gameObject.SetActive(false);
                assignment1 = 4;
                assignment2 = 0;
                break;
            case 8:
                button1.gameObject.SetActive(true);
                buttonText1.GetComponent<TextMeshProUGUI>().text = "  Assignment 4 Part B";
                button2.gameObject.SetActive(false);
                assignment1 = 4;
                assignment2 = 0;
                break;
            case 9:
                button1.gameObject.SetActive(true);
                buttonText1.GetComponent<TextMeshProUGUI>().text = "  Assignment 5 Part A";
                button2.gameObject.SetActive(false);
                assignment1 = 5;
                assignment2 = 0;
                break;
            case 10:
                button1.gameObject.SetActive(true);
                buttonText1.GetComponent<TextMeshProUGUI>().text = "  Assignment 5 Part B";
                button2.gameObject.SetActive(false);
                assignment1 = 5;
                assignment2 = 0;
                break;
        }
    }

    public void setGoals() {
        switch(dayNum) {
            case 1:
                agendaGoals.text = "";
                break;
            case 2:
                agendaGoals.text = "";
                break;
            case 3:
                agendaGoals.text = "";
                break;
            case 4:
                agendaGoals.text = "";
                break;
            case 5:
                agendaGoals.text = "";
                break;
            case 6:
                agendaGoals.text = "";
                break;
            case 7:
                return;
                // TODO: Fix whatever error was going on here
                agendaGoals.text = "";
                break;
            case 8:
                agendaGoals.text = "";
                break;
            case 9:
                agendaGoals.text = "";
                break;
            case 10:
                agendaGoals.text = "";
                break;
        }
    }
}
