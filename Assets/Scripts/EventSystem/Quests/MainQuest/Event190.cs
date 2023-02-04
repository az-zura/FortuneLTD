using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using EventSystem.Conditions.CyclicWaitConditions;
using UnityEngine;

public class Event190 : SequentialEvent
{
    public GameLoop gameLoop;
    public MainQuest mainQuest;
    public GameObject player;
    public Speechbubble bubble;

    public Transform bedPos;

    public NPCControllerPositionsRandom pam;

    public GameObject flower;

    public Transform flowersFloor;
    public override void OnEventInitialized()
    {
        AddEventItem(new SetNpcControllerPossession(true,pam));

        AddEventItem(new TeleportAction(pam.GetLocomotion,bedPos.position));
        AddEventItem(new ToggleActiveAction(flower,true));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation,flower.transform));

        AddEventItem(new CyclicCondition(1).AddCondition(new PlayerCloseToCondition(player, pam.gameObject, 5)));
        //player next to pam
        AddEventItem(new SetEventStateAction(mainQuest,190));

        AddEventItem(new SpeakAction(bubble,player,"Hey"));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation,player.transform));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Ich dachte nicht, dass es hier so etwas schönes gibt."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"So viele Jahre bin ich jetzt hier und fast drei Tage vor meiner Passage finde ich sowas."));
        AddEventItem(new SpeakAction(bubble,player,"Pam, wir müssen das jetzt wegmachen"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Lass es mich noch ein Weilchen anschauen"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Findest du es nicht auch unglaublich schön?"));
        AddEventItem(new SpeakAction(bubble,player,"Ja..., schon."));
        AddEventItem(new TimerCondition(1));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Joe, willst du, dass ich ins Jenseits gehe?"));
        AddEventItem(new SpeakAction(bubble,player,"Natürlich, du hast so lange gearbeitet, jeder Geist verdient es, ins Jenseits zu kommen, vor allem du."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Aber willst DU, dass ich ins Jenseits gehe?"));
        AddEventItem(new SpeakAction(bubble,player,"Schon, denke ich, alles andere wäre ja irgendwie egoistisch. So wies die Menschen sind..."));
        AddEventItem(new SpeakAction(bubble,player,"Im moment weiß ich tatsächlich gar nicht mehr, was ich denken soll..."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Wie wär's wenn ich noch ein bisschen bleibe?"));
        AddEventItem(new SpeakAction(bubble,player,"Was redest du? Seit 189 Geisterjahren wartest du jetzt auf deine Passage..."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Da tun 7 Tage mehr auch nicht weh. "));
        AddEventItem(new SpeakAction(bubble,player,"Bist du dir sicher? "));
        
        //spawnFlowers 
        for (int i = 0; i < flowersFloor.childCount; i++)
        {
            var f = flowersFloor.GetChild(i).gameObject;
            AddEventItem(new TimerCondition(Random.Range(0,0.15f)));
            AddEventItem(new ToggleActiveAction(f,true));
        }
        AddEventItem(new TimerCondition(1));

        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Ach wie wunderbar!"));
        AddEventItem(new SpeakAction(bubble,player,"Na gut, ich meine ... wenn es dir so gefällt und du noch ein bisschen bleiben möchtest, dann können wir es meinetwegen da lassen ."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Danke, Joe"));
        AddEventItem(new SpeakAction(bubble,player,"Schlaf gut, Pam"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Du auch"));
        
        AddEventItem(new NPCLookAtAction(pam.GetAnimation));
        AddEventItem(new SetNpcControllerPossession(false,pam));

        AddEventItem(new SetEventStateAction(mainQuest,200));

        StartSequentialEvent();
    }
}
