using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Actions;
using EventSystem.Base;
using NPC.NpcMovement;
using UnityEngine;
using UnityEngine.Serialization;

public class Delivery : SequentialEvent
{
    public GameObject player;
    public GameObject pam;
    public GameObject childGhost;
    public GameObject box;
    public GameObject neighbour1;
    public GameObject neighbour2;
    public GameObject neighbourChild1;
    public GameObject neighbourChild2;
    public GameObject neighbourChild3;
    public GameObject boxAtNeighbourClosed;
    public GameObject boxAtNeighbourOpened;
    public GameObject newChildGhostAtNeighbour;
    public GameObject quest;
    public Transform nighoburDadGoto;
    public Transform nighoburHouseGoto;
    public Speechbubble speechbubble;
    public PlayerInTriggerBoxCondition playerAtNeightbour1House;
    public PlayerInTriggerBoxCondition playerAtNeightbour2House;
    public PlayerNotInTriggerBoxCondition playerNotAtNeightbour2House;
    
    public override void OnEventInitialized()
    {
        SpecificNpcController pamController = pam.GetComponentInChildren<NpcControllerRandom>();

        GhostAnimation neighbour2Animation = neighbour2.GetComponentInChildren<GhostAnimation>();
        NPC_Locomotion neighbour2Locomotion = neighbour2.GetComponentInChildren<NPC_Locomotion>();
        
        GhostAnimation neighbourChild1Animation = neighbourChild1.GetComponentInChildren<GhostAnimation>();
        GhostAnimation neighbourChild2Animation = neighbourChild2.GetComponentInChildren<GhostAnimation>();
        GhostAnimation neighbourChild3Animation = neighbourChild3.GetComponentInChildren<GhostAnimation>();
                
       NPC_Locomotion neighbourChild1NPC_Locomotion = neighbourChild1.GetComponentInChildren<NPC_Locomotion>();
       NPC_Locomotion neighbourChild2NPC_Locomotion = neighbourChild2.GetComponentInChildren<NPC_Locomotion>();
       NPC_Locomotion neighbourChild3NPC_Locomotion = neighbourChild3.GetComponentInChildren<NPC_Locomotion>();
       NPC_Locomotion newGhostAtNeighbourNPC_Locomotion = newChildGhostAtNeighbour.GetComponentInChildren<NPC_Locomotion>();
        
        
        ClickableItemCondition boxClickable = box.GetComponentInChildren<ClickableItemCondition>();
        GhostAnimation playerAnimation = player.GetComponentInChildren<GhostAnimation>();
        
        
        AddEventItem(new ToggleActiveAction(neighbourChild1,true));
        AddEventItem(new ToggleActiveAction(neighbourChild2,true));
        AddEventItem(new ToggleActiveAction(neighbourChild3,true));
        AddEventItem(new ToggleActiveAction(neighbour1,true));

        AddEventItem(new ToggleActiveAction( box,true));
        
        //take controll of pams NPC controller
        AddEventItem(new SetNpcControllerPossession(true, pamController));
        
        //pam goto player
        AddEventItem(new NPCGotoAction(pamController.GetLocomotion,player,0,2));
        //pam look at player
        AddEventItem(new NPCLookAtAction(pamController.GetAnimation,player.transform));
        
        
        //conversation 1
        AddEventItem(new SpeakAction(speechbubble,pam,"Schatz, hast du was bestellt?"));
        AddEventItem(new SpeakAction(speechbubble,player,"Nein, wiso?"));
        AddEventItem(new SpeakAction(speechbubble,pam,"Da steht n paket vor der Tür, dachte es wäre für dich."));
        AddEventItem(new SpeakAction(speechbubble,player,"Also ich hab nix bestellt"));
        AddEventItem(new SpeakAction(speechbubble,pam,"Schau mal obs richtig zugestellt wurde"));
        
        //wait for user click
        AddEventItem(new EventBasedCondition(boxClickable.TriggerCondition));
        AddEventItem(new ToggleActiveAction( box,false));

        AddEventItem(new ToggleActiveAction( childGhost,true));
        AddEventItem(new NPCLookAtAction(childGhost.GetComponentInChildren<GhostAnimation>(),player.transform));
        
        AddEventItem(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Surprised));
        AddEventItem(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Default,1));
        
        AddEventItem(new SpeakAction(speechbubble,childGhost,"Hallo"));
        AddEventItem(new SpeakAction(speechbubble,player,"Noch eins? Als ob wir mit Hannah nicht schon genung um die Ohren hätten."));
        AddEventItem(new SpeakAction(speechbubble,childGhost,"Papa?"));
        
        AddEventItem(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Thinking));
        AddEventItem(new SpeakAction(speechbubble,player,"Nein warte ich muss das kurz absprechen"));
        AddEventItem(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Default,5.0f));
        AddEventItem(new SpeakAction(speechbubble,player,"PAM!!!!"));
        AddEventItem(new SpeakAction(speechbubble,player,"Hast du noch ein Kind bestellt?"));
        AddEventItem(new SpeakAction(speechbubble,pam,"Nein wieso?"));
        AddEventItem(new SpeakAction(speechbubble,player,"Weil in dem paket eins drin war!")); 
        AddEventItem(new SpeakAction(speechbubble,pam,"Als ob wir mit Hanna nicht schon genung um die Ohren hätten."));
        AddEventItem(new SpeakAction(speechbubble,pam,"Frag mal bei den Nachbarn obs denen gehört, sonst bring ichs morgen Vormitternacht auf die Post"));
        
        AddEventItem(new NPCLookAtAction(neighbour1.GetComponentInChildren<GhostAnimation>(),player.transform));
        AddEventItem(new CyclicCondition(1).AddCondition(playerAtNeightbour1House));
        //at neighbour 1 house
        
        


        
        AddEventItem(new SpeakAction(speechbubble,player,"gute Nachmitternacht, uns wurde ein Geisterkind falsch zugestellt, das ist nicht zufällig ihres"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"ganz bestimmt nicht, wenn ich doch schon so ein Talentiertes Geisterkind hab."));
        AddEventItem(new SpeakAction(speechbubble,player,"Wüssten sie denn we…"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"Mit nur 4 Geisterjahren konnte mein kleiner Manfred schon schriftlich dividieren"));
        AddEventItem(new SpeakAction(speechbubble,player,"Schön, aber könnte…"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"Und ab nächster woche macht er ein Praktikum in der Fortune LTD"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"Im management"));
        AddEventItem(new SpeakAction(speechbubble,player,"Wunderbar, aber wüssten sie nicht vom wem das sein könnte"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"Ich denke die von nebenan könnten noch ein 12. bestellt haben."));
        AddEventItem(new SpeakAction(speechbubble,player,"Danke"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"Hab ich ihnen schon von Manfreds Praktikum erzählt?"));
        
        
        AddEventItem(new CyclicCondition(1).AddCondition(playerAtNeightbour2House));
        //player at second house
        AddEventItem(new ToggleActiveAction(childGhost,false));
        AddEventItem(new ToggleActiveAction(box,false));

        
        
        AddEventItem(new NPCLookAtAction(neighbourChild1Animation,player.transform));
        AddEventItem(new NPCLookAtAction(neighbourChild2Animation,player.transform));
        AddEventItem(new NPCLookAtAction(neighbourChild3Animation,player.transform));
        
        AddEventItem(new SpeakAction(speechbubble,neighbourChild1,"Er ist Da!!!"));
        AddEventItem(new SpeakAction(speechbubble,neighbourChild2,"Endlich"));
        AddEventItem(new NPCGotoAction(neighbour2Locomotion,nighoburDadGoto.position));
        AddEventItem(new NPCLookAtAction(neighbour2Animation,neighbourChild3.transform));
        AddEventItem(new SpeakAction(speechbubble,neighbour2," Hab ihr euch noch einen Bruder bestellt? "));
        AddEventItem(new SpeakAction(speechbubble,neighbourChild3,"Ja, die Steine im Garten agucken ist zumehrt viel besser!"));
        AddEventItem(new EmotionAction(neighbour2Animation,GhostAnimation.Emotion.Thinking));
        AddEventItem(new EmotionAction(neighbour2Animation,GhostAnimation.Emotion.Angry,1));
        AddEventItem(new EmotionAction(neighbour2Animation,GhostAnimation.Emotion.Default,2));
        AddEventItem(new SpeakAction(speechbubble,neighbour2,"Da hast du recht Frank..."));
        AddEventItem(new SpeakAction(speechbubble,neighbourChild3,"Tommy"));
        AddEventItem(new SpeakAction(speechbubble,neighbour2,"...Tommy, das ist aber jetzt wirklich das allerletzte mal dass ich euch das durchgehen lasse."));
        AddEventItem(new NPCLookAtAction(neighbour2Animation,neighbourChild1.transform));
        AddEventItem(new SpeakAction(speechbubble,neighbourChild1,"Klar doch Papa"));
        
        AddEventItem(new NPCLookAtAction(neighbour2Animation,player.transform));
        AddEventItem(new SpeakAction(speechbubble,neighbour2,"Stellen Sie das Paket einfach auf den Rasen und vielen Dank fürs vorbeibringen"));
        AddEventItem(new ToggleActiveAction(boxAtNeighbourClosed,true));
        AddEventItem(new SpeakAction(speechbubble,neighbour2,"Na gut dann packt euren Bruder aus und dann ab ins Bett, bald geht die Sonne auf."));
        
        AddEventItem(new NPCLookAtAction(neighbour2Animation));

        AddEventItem(new NPCGotoAction(neighbour2Locomotion,nighoburHouseGoto.transform.position,false));
        AddEventItem(new TimerCondition(2));
        AddEventItem(new ToggleActiveAction(boxAtNeighbourClosed,false));
        AddEventItem(new ToggleActiveAction(boxAtNeighbourOpened,true));
        AddEventItem(new ToggleActiveAction(newChildGhostAtNeighbour,true));
        
        AddEventItem(new SpeakAction(speechbubble,newChildGhostAtNeighbour,"Hallo"));
        AddEventItem(new SpeakAction(speechbubble,neighbourChild2,"Hallo, wir sind deine Geschwister"));
        AddEventItem(new SpeakAction(speechbubble,newChildGhostAtNeighbour,"Ok"));
        
        
        
        AddEventItem(new NPCLookAtAction(neighbourChild1Animation));
        AddEventItem(new NPCLookAtAction(neighbourChild2Animation));
        AddEventItem(new NPCLookAtAction(neighbourChild3Animation));

        AddEventItem(new NPCGotoAction(neighbourChild1NPC_Locomotion,nighoburHouseGoto.transform.position,false));
        AddEventItem(new NPCGotoAction(neighbourChild2NPC_Locomotion,nighoburHouseGoto.transform.position,false));
        AddEventItem(new NPCGotoAction(neighbourChild3NPC_Locomotion,nighoburHouseGoto.transform.position,false));
        AddEventItem(new NPCGotoAction(newGhostAtNeighbourNPC_Locomotion,nighoburHouseGoto.transform.position,false));
        AddEventItem(new CyclicCondition(1).AddCondition(playerNotAtNeightbour2House));
        
        //cleanup
        //disable event parent
        AddEventItem(new ToggleActiveAction(quest,false));
        
        
        AddEventItem(new NPCLookAtAction(pamController.GetAnimation));
        //retrun controll to pams npc controller
        AddEventItem(new SetNpcControllerPossession(false, pamController));
        
        


        //TODO : rest
        StartSequentialEvent();
        
    }
}
