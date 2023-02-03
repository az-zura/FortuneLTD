using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using EventSystem.Conditions.CyclicWaitConditions;
using UnityEngine;

public class Event70 : SequentialEvent
{
    public MainQuest mainQuest;
    public GameObject player;
    public Speechbubble bubble;

    public Transform bedroomGoto;
    public Transform windowGoto;
    public Transform lookOutOfWindow;
    public NPCControllerPositionsRandom pam;

    public override void OnEventInitialized()
    {
        AddEventItem(new SetNpcControllerPossession(true, pam));
        AddEventItem(new NPCGotoAction(pam.GetLocomotion,bedroomGoto.position));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation,player.transform));
        
        AddEventItem(new CyclicCondition(0.5f).AddCondition(new PlayerCloseToCondition(player,pam.gameObject,3f)));
        //wait for player to be close to pam
        AddEventItem(new SetEventStateAction(mainQuest,70));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Na wie war dein Tag?"));
        AddEventItem(new SpeakAction(bubble,player,"So wie immer und deiner? "));
        
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Ich habe noch ein Bild gemalt. Ich denke, das wird mein letztes sein."));
        AddEventItem(new SpeakAction(bubble,player,"Dein Meisterwerk?"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Haha, nein, so gut wie “Das halboffene Gartentor” wird es sich nicht verkaufen."));
        AddEventItem(new TimerCondition(1));
        
        AddEventItem(new SpeakAction(bubble,player,"Hätte nie gedacht, dass sich mal ein Bild von dir so gut verkauft."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Meinst du, es hat dir nicht gefallen!?"));
        AddEventItem(new SpeakAction(bubble,player,"Nein, es ist ein wunderschönes Gemälde, nur ist es ja generell schwer, sich als Kunstgeist den Lebensunterhalt zu verdienen."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Ja, da hatte ich wohl echt Glück, dass ein so reicher Geist auf meine Bilder aufmerksam geworden ist."));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation,lookOutOfWindow));
        AddEventItem(new NPCGotoAction(pam.GetLocomotion,windowGoto.position,0,0,false));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Stell dir vor, wenn ich das nicht verkauft hätte, hätte ich wahrscheinlich noch 30/40 Jahre auf meine Passage warten müssen. Am Ende ging dann doch alles sehr schnell."));
        
        AddEventItem(new SpeakAction(bubble,player,"Ja, am Ende ging dann doch alles sehr schnell..."));

        AddEventItem(new SetEventStateAction(mainQuest,80));

        StartSequentialEvent();
    }
}
