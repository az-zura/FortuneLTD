using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using EventSystem.Conditions.CyclicWaitConditions;
using NPC.NpcMovement;
using UnityEngine;

public class Event10 : SequentialEvent
{
    public MainQuest mainQuest;
    public NPCControllerPositionsRandom pam;
    public GameObject player;
    public NPCControllerPositionsRandom hannahRandom;
    public NPCControllerFollow hannahFollow;
    public Speechbubble bubble;
    public Transform kitchenCenter;

    public override void OnEventInitialized()
    {
        AddEventItem(new TestAction("lets goooo"));
        AddEventItem(new SetEventStateAction(mainQuest,10));
        AddEventItem(new SetNpcControllerPossession(true, hannahRandom));
        AddEventItem(new SetNpcControllerPossession(true, hannahFollow));
        AddEventItem(new SetNpcControllerPossession(true, pam));
        AddEventItem(new TeleportAction(pam.GetLocomotion, kitchenCenter));
        AddEventItem(new CyclicCondition(0.5f).AddCondition(new PlayerCloseToCondition(player, pam.gameObject, 5f)));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation, player.transform));
        AddEventItem(new SpeakAction(bubble, player,
            "Guten Morgen, na wie fühlt es sich an, nur noch 6 Tage, auf den unaufgeräumten Vorgarten der Ernsts schauen zu müssen."));
        AddEventItem(new SpeakAction(bubble, pam.gameObject,
            "Ganz wunderbar!"));
        AddEventItem(new SpeakAction(bubble, player,
            "Glaub ich"));
        AddEventItem(new SpeakAction(bubble, player,
            " Ich freue mich wirklich riesig für dich, gestern auf dem Heimweg habe ich wieder eine Jenseitswerbung gesehen."));
        AddEventItem(new SpeakAction(bubble, player,
            "Es soll dort riesige Meere bis zum Horizont geben, wie bei den Menschen, nur halt ohne Menschen. "));
        AddEventItem(new SpeakAction(bubble, pam.gameObject,
            "Ach hör auf, das Warten ist echt unerträglich."));
        AddEventItem(new NPCGotoAction(hannahRandom.GetLocomotion,kitchenCenter.position,0,0.1f,false));
        AddEventItem(new SpeakAction(bubble, pam.gameObject,
            "Seit 189 Jahren warte ich jetzt auf meine Passage, wer hätte gedacht, dass die letzten 6 Tage die schlimmsten werden."));
        
        AddEventItem(new NPCLookAtAction(hannahRandom.GetAnimation, player.transform));
        
        AddEventItem(new SpeakAction(bubble, player,
            "Na, hat der schlauste Geist der 5b gut geschlafen?"));
        AddEventItem(new SpeakAction(bubble, hannahRandom.gameObject,
            "Vor Prüfungen schlafe ich immer schlecht"));
        AddEventItem(new SpeakAction(bubble, pam.gameObject,
            "Du hast doch eine ganze Woche lang bis tief in den Tag gelernt."));
        AddEventItem(new SpeakAction(bubble, hannahRandom.gameObject,
            "Aber ein paar Sachen kann ich nicht so gut. Ich hoffe es kommt nicht so viel Geisterwirtschaft dran."));
        AddEventItem(new SpeakAction(bubble, player,
            "Das wird bestimmt gut, wir müssen jetzt aber los, wenn wir zu spät kommen war das ganze Lernen umsonst."));
        AddEventItem(new SpeakAction(bubble, hannahRandom.gameObject,
            "Tschüss Mama!"));
        AddEventItem(new SpeakAction(bubble, pam.gameObject,
            "Bis später, mein Schatz und viel Erfolg.")); 
         
        AddEventItem(new NPCLookAtAction(pam.GetAnimation));
        AddEventItem(new NPCLookAtAction(hannahRandom.GetAnimation));

        AddEventItem(new FollowAction(hannahFollow,player.transform));
        AddEventItem(new SetNpcControllerPossession(false, pam));
        AddEventItem(new SetEventStateAction(mainQuest,20));
        StartSequentialEvent();
    }
}