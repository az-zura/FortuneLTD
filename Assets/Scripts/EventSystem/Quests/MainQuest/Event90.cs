using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using EventSystem.Conditions.CyclicWaitConditions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Event90 : SequentialEvent
{
    public GameLoop gameLoop;
    public MainQuest mainQuest;
    public Speechbubble bubble;

    public GameObject player;
    public NPCControllerPositionsRandom pam;
    public Transform livingRoomGoto;

    public override void OnEventInitialized()
    {
        
        AddEventItem(new CyclicCondition(1).AddCondition(new AfterDateCondition(18,gameLoop)));
        AddEventItem(new SetEventStateAction(mainQuest,90));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation));
        AddEventItem(new SetNpcControllerPossession(true,pam));
        AddEventItem(new NPCGotoAction(pam.GetLocomotion,livingRoomGoto.position));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation,player.transform));
        AddEventItem(new CyclicCondition(1).AddCondition(new PlayerCloseToCondition(player,pam.gameObject,4)));
        AddEventItem(new SpeakAction(bubble,player, "Guten Abend"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject, "Guten Abend"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject, "Die Schule fängt heute anscheinend später an. Ich bringe Hannah dann nachher hin, muss eh noch in die Stadt."));
        AddEventItem(new SpeakAction(bubble,player, "Ok, dann bis später."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject, "Bis später."));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation));
        AddEventItem(new SetNpcControllerPossession(false,pam));
        AddEventItem(new SetEventStateAction(mainQuest,100));

        
        StartSequentialEvent();
    }
    
}
