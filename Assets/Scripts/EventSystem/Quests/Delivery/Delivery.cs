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
    public Speechbubble speechbubble;
    public PlayerInTriggerBoxCondition playerAtNeightbour1House;

    public override void OnEventInitialized()
    {
        SpecificNpcController pamController = pam.GetComponentInChildren<NpcControllerRandom>();
        ClickableItemCondition boxClickable = box.GetComponentInChildren<ClickableItemCondition>();
        GhostAnimation playerAnimation = player.GetComponentInChildren<GhostAnimation>();
        
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
        
        AddEventItem(new ToggleActiveAction(neighbour1,true));
        AddEventItem(new NPCLookAtAction(neighbour1.GetComponentInChildren<GhostAnimation>(),player.transform));
        AddEventItem(new CyclicCondition(1).AddCondition(playerAtNeightbour1House));
        AddEventItem(new SpeakAction(speechbubble,player,"gute Nachmitternacht, uns wurde ein Geisterkind falsch zugestellt, das ist nicht zufällig ihres"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"ganz bestimmt nicht, wenn ich doch schon so ein Talentiertes Geisterkind hab."));
        AddEventItem(new SpeakAction(speechbubble,player,"Wüssten sie denn we…"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"Mit nur 4 Geisterjahren konnte mein kleiner Manfred schon schriftlich dividieren"));
        AddEventItem(new SpeakAction(speechbubble,player,"Schön, aber könnte…"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"Und ab nächster woche macht er ein Praktikum in der Fortune LTD"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"Im management"));
        AddEventItem(new SpeakAction(speechbubble,player,"Wunderbar, aber wüssten sie nicht vom wem das sein könnte"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"Ich denke die von gegenüber könnten noch ein 12. bestellt haben."));
        AddEventItem(new SpeakAction(speechbubble,player,"Danke"));
        AddEventItem(new SpeakAction(speechbubble,neighbour1,"Hab ich ihnen schon von Manfreds Praktikum erzählt?"));


        //TODO : rest
        StartSequentialEvent();
        
    }
}
