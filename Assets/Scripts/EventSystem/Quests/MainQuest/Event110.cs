using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;

public class Event110 : SequentialEvent
{
    public GameLoop gameLoop;
    public MainQuest mainQuest;
    public Speechbubble bubble;

    public PopulateCity populateCity;
    public GameObject player;

    public Transform parentGhosts;
    public GhostAnimation flowerGhostAnimation;
    public GhostAnimation policeGhostAnimation;
    public NPC_Locomotion policeNPC_Locomotion;
    public Transform parenPolice;

    public Transform center;
    public GameObject flower;

    public GameObject parent;

    public PlayerNotInTriggerBoxCondition playerNotInTriggerBoxCondition;


    public override void OnEventInitialized()
    {
        AddEventItem(new SetEventStateAction(mainQuest,110));

        AddEventItem(new ActivateCityPopulationAction(populateCity,true));
        for (int i = 0; i < parentGhosts.childCount; i++)
        {
            var g = parentGhosts.GetChild(i);
            AddEventItem(new NPCLookAtAction(g.GetComponentInChildren<GhostAnimation>(), flowerGhostAnimation.gameObject.transform));
        }
            
        for (int i = 0; i < parenPolice.childCount; i++)
        {
            var g = parenPolice.GetChild(i);
            AddEventItem(new EmotionAction(g.GetComponentInChildren<GhostAnimation>(), GhostAnimation.Emotion.Angry));
        }
        AddEventItem(new EmotionAction(policeGhostAnimation, GhostAnimation.Emotion.Angry));

        AddEventItem(new EmotionAction(flowerGhostAnimation,GhostAnimation.Emotion.Surprised));
        AddEventItem(new SpeakAction(bubble,policeGhostAnimation.gameObject,"Unternehmen Sie keinen Fluchtversuch, sie sind umstellt!"));
        AddEventItem(new SpeakAction(bubble,flowerGhostAnimation.gameObject,"Was ist das? Ich verstehe nicht!!"));
        AddEventItem(new SpeakAction(bubble,policeGhostAnimation.gameObject,"Leisten sie keinen Wiederstand!"));
        AddEventItem(new SpeakAction(bubble,flowerGhostAnimation.gameObject,"Ich habe nichts verbrochen!"));
        AddEventItem(new SpeakAction(bubble,policeGhostAnimation.gameObject,"Der Gegenstand am Boden ist höchst gefährlich, kommen Sie uns nicht zu nahe!"));
        AddEventItem(new SpeakAction(bubble,flowerGhostAnimation.gameObject,"Bitte, ich verstehe nicht was hier passiert, das gehört mir nicht!"));
        AddEventItem(new SpeakAction(bubble,policeGhostAnimation.gameObject,"Wir werden sie zu Ihrer eigenen Sicherheit mitnehmen müssen"));
        AddEventItem(new SpeakAction(bubble,flowerGhostAnimation.gameObject,"Nein, bitte nicht, es muss sich um ein Missverständnis handeln!!!"));
        AddEventItem(new TimerCondition(1.5f));
        AddEventItem(new ToggleActiveAction(flowerGhostAnimation.gameObject,false));
        AddEventItem(new NPCGotoAction(policeNPC_Locomotion,center.position));
        AddEventItem(new ToggleActiveAction(flower,false));
        AddEventItem(new NPCLookAtAction(policeGhostAnimation,player.transform));
        AddEventItem(new SpeakAction(bubble,policeGhostAnimation.gameObject,"Weitergehen hier gibt es nichts zu sehen"));
        AddEventItem(new SpeakAction(bubble,policeGhostAnimation.gameObject,"WEITERGEHEN!!!!!"));
        AddEventItem(new ActivateCityPopulationAction(populateCity,false));
        
        AddEventItem(new CyclicCondition(1).AddCondition(playerNotInTriggerBoxCondition));
        AddEventItem(new ToggleActiveAction(parent,false));
        AddEventItem(new SetEventStateAction(mainQuest,120));
        StartSequentialEvent();
    }
}
