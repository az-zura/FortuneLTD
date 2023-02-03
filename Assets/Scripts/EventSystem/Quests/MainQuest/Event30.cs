using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class Event30 : SequentialEvent
{
    public GameLoop gameLoop;
    public MainQuest mainQuest;
    public GameObject player;
    public Speechbubble bubble;

    public NPCControllerFollow hannahFollow;
    public NPCControllerPositionsRandom hannahRandom;

    public Transform houseGoto;
    public Transform schoolGoto;
    
    public override void OnEventInitialized()
    {
        AddEventItem(new TestAction("starting 30"));
        AddEventItem(new SetEventStateAction(mainQuest,30));
        AddEventItem(new SetNpcControllerPossession(true,hannahFollow));
        AddEventItem(new NPCGotoAction(hannahFollow.GetLocomotion,player.transform.position,2,3));
        //wait unit hanna is next to player
        AddEventItem(new NPCLookAtAction(hannahFollow.GetAnimation,player.transform));
        AddEventItem(new SpeakAction(bubble,player,"So, jetzt aber flott, in 10 Minuten geht’s los."));
        AddEventItem(new SpeakAction(bubble,hannahFollow.gameObject,"Was, wenn ich diesmal keine gute Note schreibe?"));
        
        AddEventItem(new SpeakAction(bubble,player,"Das sagst du jedes mal und jedes mal bist du wieder eine der Besten."));
        AddEventItem(new SpeakAction(bubble,player,"Mach dir nicht so viel Sorgen."));
        AddEventItem(new SpeakAction(bubble,hannahFollow.gameObject,"Ok, Papa."));
        AddEventItem(new SpeakAction(bubble,player,"Und jetzt schnell, die Klausur wartet nicht auf dich."));
        AddEventItem(new SpeakAction(bubble,hannahFollow.gameObject,"Bis später, Papa."));
        AddEventItem(new SpeakAction(bubble,player,"Bis später und viel Erfolg!"));
        
        AddEventItem(new SetEventStateAction(mainQuest,40));
        
        AddEventItem(new NPCLookAtAction(hannahFollow.GetAnimation));
        AddEventItem(new NPCGotoAction(hannahFollow.GetLocomotion,schoolGoto.position));
        
        AddEventItem(new SetNpcControllerPossession(true,hannahFollow));
        AddEventItem(new TeleportAction(hannahRandom.GetLocomotion,houseGoto));
        AddEventItem(new SetNpcControllerPossession(false,hannahRandom));


        StartSequentialEvent();
    }
}
