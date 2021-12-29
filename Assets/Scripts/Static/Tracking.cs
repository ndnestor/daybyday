using System.Collections;
using System.Collections.Generic;
using Computer;
using Game;
using Game.Dialogue;
using Game.Registry;
using UnityEngine;

//This class is a Singleton instance
public class Tracking : MonoBehaviour
{
    public static Tracking Instance { get; private set; }

    public int DayNum { get; private set; }
    public const int MAX_DAYS = 10;
    public const int MAX_TIME = 14;
    public int timeUsed = 0;
    public ArrayList objectUsage = new ArrayList();

    //Used for window lighting
    [SerializeField] private SpriteRenderer windowPaneRenderer;
    [SerializeField] private SpriteRenderer windowLightRenderer;
    [SerializeField] private SpriteRenderer windowFrameHighlightRenderer;
    [SerializeField] private Gradient middayGradient;
    [SerializeField] private Gradient afternoonGradient;
    [SerializeField] private Gradient opacityGradient;

    //Used for sleeping
    [SerializeField] private Transform bedDestination;

    // WaterPlant component for tree to call for update level/day every day
    public WaterPlant treeWater;
    
    //Used for Agenda's day two introduction
    [SerializeField] private GameObject agendasBox;
    [SerializeField] private DialogueGraph agendaDialogue;
    [SerializeField] private GameObject blackOverlay;
    
    // Objects used for dialogue
    private DialogueSystem dialogueSystem;
    private ValueRegistry valueRegistry;
    
    
    private void Start()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        DayNum = 1;

        dialogueSystem = MainInstances.Get<DialogueSystem>();
        valueRegistry = MainInstances.Get<ValueRegistry>();
    }

    //Adds an object to an ArrayList in chronological order of use
    public void AddObject(GameObject obj)
    {
        int listLength = objectUsage.Count;
        objectUsage.Add(obj);
    }

    //Counts the amount of times an object (param obj) has been used
    public int CountObjectUses(GameObject obj)
    {
        int count = 0;

        foreach(GameObject a in objectUsage)
        {
            if(a == obj)
            {
                count++;
            }
        }

        return count;
    }

    /**Adds time units for each object used (Requires a check against MAX_TIME before call)
     * param additionalTime --> timeValue of the object
     * returns total time units used in the day
     */
    public int AddUsedTime(int additionalTime)
    {
        timeUsed += additionalTime;
        UpdateLighting();
        if(timeUsed >= MAX_TIME)
        {
            timeUsed = 0;
            Sleep();
        }

        return timeUsed;
    }

    //Tints the window pane, window light, etc. to reflect time of day changes
    private void UpdateLighting()
    {
        float dayPercentage = (float)timeUsed / MAX_TIME;
        float halfDayPercentage;
        Color lightColor;
        if(dayPercentage < 0.5)
        {
            //Use midday gradient
            halfDayPercentage = timeUsed / ((float)MAX_TIME / 2);
            lightColor = middayGradient.Evaluate(halfDayPercentage);
		}
        else
        {
            //Use afternoon gradient
            halfDayPercentage = (timeUsed - MAX_TIME / 2) / ((float)MAX_TIME / 2);
            lightColor = afternoonGradient.Evaluate(halfDayPercentage);
        }
        windowFrameHighlightRenderer.color = new Color(lightColor.r, lightColor.g, lightColor.b, opacityGradient.Evaluate(dayPercentage).a);
        windowLightRenderer.color = new Color(lightColor.r, lightColor.g, lightColor.b, opacityGradient.Evaluate(dayPercentage).a);
        windowPaneRenderer.color = lightColor;
    }

    //Moves the character to the bed to move on to the next day
    private void Sleep()
    {
        void CallbackAction()
        {
            /* Code in this method will get called when the character is next to the bed and ready to sleep
             * Sleep animation should start playing, sleep theme should start playing, etc
             * Probably should disable the Movement2D.cs script too */
            
            print("Sleeping");
            treeWater.dayUpdate();
            
            //TODO: Uncomment when computer and room scenes get integrated
            //ProfileScreen.Instance.ResetTodaysActivityTimes();
            DayNum++;
            
            InteractionHandler.Instance.UpdateNeglectedSprites();
            ProductivityAid.Instance.UpdateLevel();
            
            // Present Agenda dialogue
            valueRegistry.Set("Day Number", DayNum);
            
            // Set destination to move to upon waking up
            // TODO: Replace with switch case statement
            Vector3 targetPosition;
            if(DayNum == 2)
            {
                targetPosition = agendasBox.transform.position + Vector3.left;
            } else if(DayNum == 3)
            {
                // TODO: Change to appropriate location
                targetPosition = agendasBox.transform.position + Vector3.left;
            } else if(DayNum == 4)
            {
                // TODO: Change to appropriate location
                targetPosition = agendasBox.transform.position + Vector3.left;
            } else if(DayNum == 5)
            {
                // TODO: Change to appropriate location
                targetPosition = agendasBox.transform.position + Vector3.left;
            } else if(DayNum == 6)
            {
                // TODO: Change to appropriate location
                targetPosition = agendasBox.transform.position + Vector3.left;
            } else if(DayNum == 7)
            {
                // TODO: Change to appropriate location
                targetPosition = agendasBox.transform.position + Vector3.left;
            } else
            {
                Debug.LogWarning("Post-sleep target position not set");
                targetPosition = Vector3.zero;
            }
            
            Movement2D.Instance.MoveTo(targetPosition, () => {
                
                // Have agenda speak once Quinn arrives
                dialogueSystem.Present(agendaDialogue);
            });
        }

        Movement2D.Instance.MoveTo(bedDestination.position, CallbackAction);
    }

    private IEnumerator FadeToBlack(int fadeSpeed)
    {
        SpriteRenderer spriteRenderer = blackOverlay.GetComponent<SpriteRenderer>();
        while(spriteRenderer.color.a < 1) {
            Color currColor = spriteRenderer.color;
            spriteRenderer.color = new Color(currColor.r, currColor.g, currColor.b, currColor.a + fadeSpeed);
            yield return null;
        }
    }

    //Used for testing purposes. Should be deleted later
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightControl))
        {
            Debug.Log("Artificially added 1 unit of time");
            AddUsedTime(1);
		}
        if(Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("Artificially moved to the next day");
            DayNum++;
            //TODO: Uncomment when computer and room scenes get integrated
            //ProfileScreen.Instance.ResetTodaysActivityTimes();
        }
    }
}
