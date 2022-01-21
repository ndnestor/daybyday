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

    [SerializeField] Button button1, button2;
    [SerializeField] GameObject announcementPage;
    [SerializeField] TextMeshProUGUI announcementText;
    [SerializeField] TextMeshProUGUI agendaGoals;
    int dayNum;
    int assignment1, assignment2;

    public void openActiveAssignment1() {
        switch(assignment1) {
            case 1:
                announcementText.text = "Happy March, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to: a) Use some hobby items around your room (i.e. read a book, play the piano, etc)and b) on our discussion board about your experience. You can write the full post within an hour, but you decide how much time you spend on exploring your hobbies. I expect to see thoughtful discussion within the week!";
                break;
            case 2:
                announcementText.text = "Happy June, students! It’s your friendly neighborhood professor here to remind you that you have an assignment due soon. To complete this assignment, you need to make and present your  goals for the summer. Use our class discussion about work-life balances and SMART goals (Specific, Measured, Attainable, Relevant, Time-based) to design your goals for the summer. Plan to use about an hour to present your goals, submit as a discussion post, and reply to your peers’ posts with helpful comments for their own goals. I expect to see constructive criticism throughout our discussions this week!";
                break;
            case 3:
                announcementText.text = "";
                break;
        }
        announcementPage.SetActive(true);
    }
    public void openActiveAssignment2() {
        announcementPage.SetActive(true);
    }
    public void closeAnnouncement() {
        announcementPage.SetActive(false);
    }

    public void Start() {
        setAssignments();
        setGoals();
    }

    public void setAssignments() {
        //Called upon day change
        switch (dayNum) {
            case 1:
                button1.gameObject.SetActive(true);
                button1.GetComponent<TextMeshProUGUI>().text = "Assignment 1";
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
                button1.GetComponent<TextMeshProUGUI>().text = "Assignment 2 Part A";
                button2.gameObject.SetActive(false);
                assignment1 = 2;
                assignment2 = 0;
                break;
            case 4:
                button1.gameObject.SetActive(true);
                button2.gameObject.SetActive(true);
                button1.GetComponent<TextMeshProUGUI>().text = "Assignment 2 Part B";
                button2.GetComponent<TextMeshProUGUI>().text = "Assignment 3 Part A";
                assignment1 = 2;
                assignment2 = 3;
                break;
            case 5:
                button1.gameObject.SetActive(true);
                button1.GetComponent<TextMeshProUGUI>().text = "Assignment 3 Part B";
                button2.gameObject.SetActive(false);
                assignment1 = 3;
                assignment2 = 0;
                break;
            case 6:
                button1.gameObject.SetActive(true);
                button1.GetComponent<TextMeshProUGUI>().text = "Assignment 3 Part C";
                button2.gameObject.SetActive(false);
                assignment1 = 3;
                assignment2 = 0;
                break;
            case 7:
                button1.gameObject.SetActive(true);
                button1.GetComponent<TextMeshProUGUI>().text = "Assignment 4 Part A";
                button2.gameObject.SetActive(false);
                assignment1 = 4;
                assignment2 = 0;
                break;
            case 8:
                button1.gameObject.SetActive(true);
                button1.GetComponent<TextMeshProUGUI>().text = "Assignment 4 Part B";
                button2.gameObject.SetActive(false);
                assignment1 = 4;
                assignment2 = 0;
                break;
            case 9:
                button1.gameObject.SetActive(true);
                button1.GetComponent<TextMeshProUGUI>().text = "Assignment 5 Part A";
                button2.gameObject.SetActive(false);
                assignment1 = 5;
                assignment2 = 0;
                break;
            case 10:
                button1.gameObject.SetActive(true);
                button1.GetComponent<TextMeshProUGUI>().text = "Assignment 5 Part B";
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
