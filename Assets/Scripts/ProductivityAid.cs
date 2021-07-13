using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using Game.Dialogue;
using Game.Registry;
using UnityEngine;

public class ProductivityAid : MonoBehaviour
{

    [SerializeField] private DialogueGraph dialogueGraph;
    [SerializeField] private int[] levelIncrementDays;

    [HideInInspector] public static ProductivityAid Instance { get; private set; }

    private const string StringRegistryId = "Productivity Aid Prompt";
    
    private StringRegistry stringRegistry;
    private DialogueSystem dialogueSystem;
    private int numGoalsToComplete = 0;
    private int level = 0;

    private void Start()
    {
        Instance = this;
        dialogueSystem = MainInstances.Get<DialogueSystem>();
        stringRegistry = MainInstances.Get<StringRegistry>();
    }

    //Called at the start of each day
    public void UpdateLevel()
    {
        if(levelIncrementDays.Contains(Tracking.Instance.DayNum))
        {
            level++;
        }
    }
    
    //Shows relevant information about an interaction before starting it
    public void Prompt(string activityName, int activityTime, int proValue, int socValue, int casValue) {
        
        //Create the message to send depending on current level
        string message = "";
        if(level >= 1)
        {
            message += $"The {activityName} will take {activityTime} hours to complete";
        }
        if(level >= 2)
        {
            message += $"\nProfessional value: {proValue}\nSocial value: {socValue}\nCasual value: {casValue}";
        }
        if(level == 3)
        {
            if(socValue > 0 || casValue > 0 && numGoalsToComplete > 0)
            {
                message += "\nYou cannot do social or casual activities until you have completed all of Agenda's goals";
            } else {
                //TODO: Possibly change the message otherwise
            }
        } else if(level == 4) {
            // TODO: Ask Eric about this
        }
        
        //Set productivity aid string value in the string registry
        stringRegistry.Set(StringRegistryId, message);
        
        //Display the prompt
        dialogueSystem.Present(dialogueGraph);

    }
}
