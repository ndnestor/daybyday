using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Dialogue;
using UnityEngine;

public class BonsaiTree : MonoBehaviour
{
    [SerializeField] private GameObject waterCan;
    [SerializeField] private GameObject wateringObj;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BonsaiTreeState[] bonsaiTreeStates;
    [SerializeField] private AudioClip[] songs;
    [SerializeField] private DialogueGraph dialogue;

    private DialogueSystem dialogueSystem;
    private bool wasWatered;
    private int woodLevel;
    private int leafLevel;

    [HideInInspector] public int level = 1;
    [HideInInspector] public int day = 1;

    // Start is called before the first frame update
    private void Start() {
        InteractionHandler.Instance.RegisterObject("Bonsai Tree", WaterBonsai, 1);
        dialogueSystem = MainInstances.Get<DialogueSystem>();
    }

    private IEnumerator WaterBonsai()
    {
        Movement2D.Instance.SetPlayerControl(false);
        InteractionHandler.Instance.canInteract = false;
        
        var waterCanInstance = Instantiate(waterCan, new Vector3(0.93f, -1.58f, 0.0f), Quaternion.identity);
        var wateringObjInstance = Instantiate(wateringObj, new Vector3(-0.05f, -1.50f, 0.0f), Quaternion.identity);
        wasWatered = true;

        MusicPlayer.Instance.StopMusic();
        
        yield return new WaitUntil(() => !MusicPlayer.Instance.audioSource.isPlaying);
        
        var songIndex = Math.Min(woodLevel + leafLevel, songs.Length - 1);
        MusicPlayer.Instance.audioSource.clip = songs[songIndex];
        MusicPlayer.Instance.PlayMusic(false);
        Tracking.Instance.QueueRoomTheme();

        yield return new WaitUntil(() => !waterCanInstance && !wateringObjInstance);
        
        dialogueSystem.Present(dialogue, () =>
        {
            Movement2D.Instance.SetPlayerControl(true);
            InteractionHandler.Instance.canInteract = true; 
        });
    }
    
    public void DayUpdate() {
        if (wasWatered) {
            woodLevel++;
            leafLevel++;
            if (woodLevel > 3)
                woodLevel = 3;
            if (leafLevel > 3)
                leafLevel = 3;
        } else {
            leafLevel--;
            if (leafLevel < 1)
                leafLevel = 1;
        }

        wasWatered = false;

        foreach (var state in bonsaiTreeStates)
            if (state.earliestDay <= Tracking.Instance.DayNum && state.latestDay >= Tracking.Instance.DayNum)
                if (state.woodLevel == woodLevel && state.leafLevel == leafLevel)
                    spriteRenderer.sprite = state.sprite;
    }
}
