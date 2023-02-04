using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using EventSystem.Conditions.CyclicWaitConditions;
using UnityEngine;

public class Event130 : SequentialEvent
{
    
    public GameLoop gameLoop;
    public MainQuest mainQuest;
    public Speechbubble bubble;
    public GameObject player;
    public NPCControllerPositionsRandom pam;
    public Transform pamBedroomPos;
    public Transform windowPos;
    public Transform windowLookOut;
    

    public override void OnEventInitialized()
    {
        AddEventItem(new CyclicCondition(1).AddCondition(new IsNotWorkingHourCondition(gameLoop)));
        AddEventItem(new SetEventStateAction(mainQuest,130));
        AddEventItem(new SetNpcControllerPossession(true,pam));
        AddEventItem(new NPCGotoAction(pam.GetLocomotion,pamBedroomPos.position));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation,player.transform));
        AddEventItem(new CyclicCondition(1).AddCondition(new PlayerCloseToCondition(player,pam.gameObject,4)));

        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Ist irgendwas, du bist irgendwie schweigsamer als sonst"));
        AddEventItem(new SpeakAction(bubble,player,"Es ist..."));
        AddEventItem(new SpeakAction(bubble,player," Bis heute habe ich nicht so viel über die Menschen nachgedacht weißt du"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Was soll man da auch nachdenken?"));
        AddEventItem(new SpeakAction(bubble,player,"Naja, wir holen sie andauernd in die Geisterwelt, aber irgendwie habe ich noch nie darüber nachgedacht, wie das für die Menschen ist."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Ist das denn wichtig, es ist ja unabdingbar für uns dass es neue Geister gibt."));
        AddEventItem(new SpeakAction(bubble,player,"Ja, das verstehe ich ja auch und wir haben das ja auch immer schon so gemacht. Aber.."));
        AddEventItem(new TimerCondition(1.5f));
        AddEventItem(new SpeakAction(bubble,player,"Ich weiß nicht, ob ich dir das erzählen sollte."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject," Mach ruhig, in einer Woche bin ich eh im Jenseits."));
        AddEventItem(new SpeakAction(bubble,player,"Deswegen meine ich ja..."));
        AddEventItem(new SpeakAction(bubble,player,"Ich habe mal den Anhang der Akten gelesen, weißt du. Und jetzt weiß ich nicht so genau was ich denken soll"));
        AddEventItem(new SpeakAction(bubble,player,"Anscheinend werden Menschen traurig wenn einer von ihnen weg ist."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Was für ein Unsinn, wenn sie doch eine Chance auf ihren Platz im Jenseits bekommen."));
        AddEventItem(new TimerCondition(1.5f));
        AddEventItem(new SpeakAction(bubble,player,"Ich weiß nicht, es ist wie wenn man etwas hat und dann ist es nicht mehr da, aber man will es trotzdem noch."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"So wie vermissen?"));
        AddEventItem(new SpeakAction(bubble,player,"Ja vermissen, das ist das Wort was ich gesucht habe "));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Ein bisschen egoistisch von den Menschen, sich zu wünschen, dass ein anderer da bleibt, obwohl er auch ins Jenseits gehen könnte."));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation));
        AddEventItem(new NPCGotoAction(pam.GetLocomotion,windowPos.position,0,0,false));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation,windowLookOut));
        
        AddEventItem(new SpeakAction(bubble,player,"Ja, da hast du recht..."));
        AddEventItem(new SpeakAction(bubble,player,"Das wäre wohl egoistisch."));
        
        AddEventItem(new SetEventStateAction(mainQuest,140));
        

        StartSequentialEvent();
    }
}
