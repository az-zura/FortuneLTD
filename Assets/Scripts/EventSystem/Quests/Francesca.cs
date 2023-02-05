using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;

public class Francesca : SequentialEvent
{
    public GameLoop gameLoop;
    public GameObject player;
    public NPCControllerEmployee karen;
    public GameObject francesca;
    public Speechbubble bubble;

    
    public PlayerInTriggerBoxCondition playerInKitchen;

    public Transform elevator;
    public Transform francescaGoto;
    public Transform karenGoto;

    public GameObject parent;
    
    public override void OnEventInitialized()
    {
        Debug.Log("initsdafkodsflknfda k");
        NPC_Locomotion francescaLocomotion = francesca.GetComponentInChildren<NPC_Locomotion>();
        GhostAnimation francescaAnimation = francesca.GetComponentInChildren<GhostAnimation>();
        AddEventItem(new TestAction("Player triggerd Francesacas event"));
        AddEventItem(new CyclicCondition(1).AddCondition(new IsWorkingHourCondition(gameLoop)).AddCondition(playerInKitchen));
        AddEventItem(new SetNpcControllerPossession(true,karen));
        AddEventItem(new TestAction("Francesacas event "));

        AddEventItem(new ToggleActiveAction(francesca,true));
        AddEventItem(new NPCGotoAction(francescaLocomotion,francescaGoto.position));
        AddEventItem(new NPCLookAtAction(francescaAnimation,player.transform));
        
        AddEventItem(new SpeakAction(bubble,francesca,"Joe, könntest du mit Karen über den Aktentransfer aus dem Sachgebiet L1 sprechen?"));
        AddEventItem(new SpeakAction(bubble,player,"Mach das doch selbst."));
        AddEventItem(new SpeakAction(bubble,francesca,"Ich hab irgendwie nicht so nen guten Draht zu Karen, du kommst besser mit ihr klar"));
        AddEventItem(new SpeakAction(bubble,player,"Ich glaub die ist im Moment einfach sehr beschäftigt"));
        AddEventItem(new SpeakAction(bubble,francesca,"Was macht die eigentlich den ganzen Tag?"));
        AddEventItem(new NPCGotoAction(karen.GetLocomotion,karenGoto.position,0,0,false));
        AddEventItem(new NPCLookAtAction(karen.GetAnimation,francesca.transform));

        AddEventItem(new SpeakAction(bubble,player,"Keine Ahnung, Management Sachen?"));
        AddEventItem(new EmotionAction(francescaAnimation,GhostAnimation.Emotion.Happy));
        AddEventItem(new SpeakAction(bubble,francesca,"Wenn Chai Latte trinken als Arbeit zählen würde wäre sie auf jeden fall die Produktivste in der Abteilung."));
        AddEventItem(new SpeakAction(bubble,karen.gameObject,"Francesca, du bist gefeuert"));
        AddEventItem(new EmotionAction(francescaAnimation,GhostAnimation.Emotion.Sad));
        AddEventItem(new SpeakAction(bubble,francesca,"Ohhhh"));
        
        AddEventItem(new NPCLookAtAction(karen.GetAnimation));
        AddEventItem(new SetNpcControllerPossession(false,karen));
        
        AddEventItem(new NPCLookAtAction(francescaAnimation));
        AddEventItem(new NPCGotoAction(francescaLocomotion,elevator.position));
        AddEventItem(new ToggleActiveAction(parent,false));
        
        StartSequentialEvent();
    }
}
