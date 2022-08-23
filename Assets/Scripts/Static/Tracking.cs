using System.Collections;
using System.Collections.Generic;
using Computer;
using Game;
using Game.Dialogue;
using Game.Registry;
using UnityEngine;
using UnityEngine.Serialization;

//This class is a Singleton instance
public class Tracking : MonoBehaviour
{
    public static Tracking Instance
    {
        get
        {
            if(instance == null)
                instance = new Tracking();
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private static Tracking instance;

    private int dayNum;
    public int DayNum {
        get => dayNum;
        private set
        {
            dayNum = value;
            PersistentDataSaver.Instance.Set("dayNum", value);
        }
    }
    public const int MAX_DAYS = 10;
    public const int MAX_TIME = 14;
    private float timeUsed;
    public float TimeUsed
    {
        get => timeUsed;
        private set
        {
            timeUsed = value;
            PersistentDataSaver.Instance.Set("timeUsed", value);
        }
    }
    public ArrayList objectUsage = new ArrayList();

    //Used for window lighting
    [SerializeField] private SpriteRenderer windowPaneRenderer;
    [SerializeField] private SpriteRenderer windowLightRenderer;
    [SerializeField] private SpriteRenderer windowFrameHighlightRenderer;
    [SerializeField] private Gradient middayGradient;
    [SerializeField] private Gradient afternoonGradient;
    [SerializeField] private Gradient opacityGradient;
    [SerializeField] private Vector2 wakeUpPosition;

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

        dialogueSystem = MainInstances.Get<DialogueSystem>();
        valueRegistry = MainInstances.Get<ValueRegistry>();

        DayNum = PersistentDataSaver.Instance.TryGet("dayNum", 1);
        TimeUsed = PersistentDataSaver.Instance.TryGet("timeUsed", 0);

        print("======================");
        print(dayNum);
        print("======================");
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
    public float AddUsedTime(float additionalTime)
    {
        TimeUsed += additionalTime;
        UpdateLighting();
        if(TimeUsed >= MAX_TIME)
        {
            TimeUsed = 0;
            Sleep();
        }

        return TimeUsed;
    }
    
    // NOTE: Used for testing purposes only
    // TODO: Remove
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            AddUsedTime(1);
        }
    }

    //Tints the window pane, window light, etc. to reflect time of day changes
    public void UpdateLighting()
    {
        float dayPercentage = TimeUsed / MAX_TIME;
        float halfDayPercentage;
        Color lightColor;
        if(dayPercentage < 0.5)
        {
            //Use midday gradient
            halfDayPercentage = TimeUsed / ((float)MAX_TIME / 2);
            lightColor = middayGradient.Evaluate(halfDayPercentage);
		}
        else
        {
            //Use afternoon gradient
            halfDayPercentage = (TimeUsed - MAX_TIME / 2) / ((float)MAX_TIME / 2);
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
            treeWater.DayUpdate();
            
            //TODO: Uncomment when computer and room scenes get integrated
            //ProfileScreen.Instance.ResetTodaysActivityTimes();
            DayNum++;
            
            InteractionHandler.Instance.UpdateNeglectedSprites();
            ProductivityAid.Instance.UpdateLevel();
            
            // Present Agenda dialogue
            valueRegistry.Set("Day Number", DayNum);
            
            // Set destination to move to upon waking up
            Vector3 targetPosition;
            if (DayNum == 2)
            {
                agendasBox.SetActive(true);
                targetPosition = agendasBox.transform.position + Vector3.left;
            } else
                targetPosition = wakeUpPosition;

            Movement2D.Instance.MoveTo(targetPosition, () => {
                
                // Have agenda speak once Quinn arrives
                dialogueSystem.Present(agendaDialogue, () =>
                {
                    if(DayNum == 2)
                        Destroy(agendasBox);
                });
            });
        }

        Movement2D.Instance.MoveTo(bedDestination.position, CallbackAction);
    }
}
