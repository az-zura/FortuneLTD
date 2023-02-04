using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;

public class Event170 : SequentialEvent
{
    public GameLoop gameLoop;
    public MainQuest mainQuest;
    public GameObject player;
    public Speechbubble bubble;

    public NPCControllerFollow hannahFollow;
    public NPCControllerPositionsRandom hannahRandom;

    public Transform houseGoto;
    public Transform fireHydrant;
    public Transform fireHydrantPos;
    public PlayerNotInTriggerBoxCondition playerNotInTriggerBoxCondition;

    public override void OnEventInitialized()
    {
        AddEventItem(new TestAction("starting 170"));
        AddEventItem(new SetEventStateAction(mainQuest,170));
        AddEventItem(new SetNpcControllerPossession(true,hannahFollow));
        AddEventItem(new NPCGotoAction(hannahFollow.GetLocomotion,player.transform.position,2,3));
        //wait unit hanna is next to player
        
        AddEventItem(new NPCLookAtAction(hannahFollow.GetAnimation,player.transform));
        
        AddEventItem(new SpeakAction(bubble,hannahFollow.gameObject,"Papa, wieso ist denn noch keiner da?"));
        AddEventItem(new SpeakAction(bubble,player,"Hm, komisch..."));
        AddEventItem(new SpeakAction(bubble,hannahFollow.gameObject,"Soll ich wieder nach Hause gehen?"));
        AddEventItem(new SpeakAction(bubble,player,"NEIN!!!!"));
        AddEventItem(new SpeakAction(bubble,player,"nein!"));
        AddEventItem(new SpeakAction(bubble,player,"Du kannst den Hydrant angucken, das macht unglaublich viel Spaß,hab ich in deinem Alter auch gemacht."));
        AddEventItem(new NPCGotoAction(hannahFollow.GetLocomotion,fireHydrantPos.position));
        AddEventItem(new NPCLookAtAction(hannahFollow.GetAnimation,fireHydrant));

        AddEventItem(new SpeakAction(bubble,hannahFollow.gameObject,"Wow, stimmt Papa, wie aufregend!"));
        AddEventItem(new SpeakAction(bubble,player,"Und später erzählst du mir wie's war."));
        AddEventItem(new SpeakAction(bubble,hannahFollow.gameObject,"Alles klar papa, bis nachher."));
        AddEventItem(new SpeakAction(bubble,player,"Bis später."));

        
        AddEventItem(new CyclicCondition(1).AddCondition(playerNotInTriggerBoxCondition));
        
        AddEventItem(new SetEventStateAction(mainQuest,180));
        AddEventItem(new NPCLookAtAction(hannahFollow.GetAnimation));
        AddEventItem(new SetNpcControllerPossession(true,hannahFollow));
        AddEventItem(new TeleportAction(hannahRandom.GetLocomotion,houseGoto));
        AddEventItem(new SetNpcControllerPossession(false,hannahRandom));


        StartSequentialEvent();
    }
}
