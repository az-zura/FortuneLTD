using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using NPC.NpcMovement;
using UnityEngine;

public class Conversation : SequentialEvent
{
    public SpeakerTextManager[] speakersAndText;
    
    public override void OnEventInitialized()
    {
        // they are talking to each other
        int n = speakersAndText.Length;
        List<SpecificNpcController> controllers = new List<SpecificNpcController>();
        List<GhostAnimation> animations = new List<GhostAnimation>(); 
        List<NPC_Locomotion> locomotions = new List<NPC_Locomotion>();

        List<GameObject> activeSpeakers = new List<GameObject>();
        for (int i = 0; i < n; i++)
        {
            SpecificNpcController c = speakersAndText[i].speaker.GetComponentInChildren<NpcControllerRandom>();
            if (!controllers.Contains(c) && c != null)
            {
                controllers.Add(c);
            }
            
            GhostAnimation a = speakersAndText[i].speaker.GetComponentInChildren<GhostAnimation>();
            if (!animations.Contains(a) && a != null)
            {
                animations.Add(a);
            }
            
            NPC_Locomotion l = speakersAndText[i].speaker.GetComponentInChildren<NPC_Locomotion>();
            if (!locomotions.Contains(l) && l != null)
            {
                locomotions.Add(l);
            }

            GameObject s = speakersAndText[i].speaker;
            if (!activeSpeakers.Contains(s) && s != null)
            {
                activeSpeakers.Add(s);
            }
        }

        foreach (var s in activeSpeakers)
        {
            AddEventItem(new ToggleActiveAction(s,true));
        }

        foreach (var c in controllers)
        {
            AddEventItem(new SetNpcControllerPossession(true, c));
            AddEventItem(new NPCLookAtAction(c.GetAnimation, ConversationManager.instance.player.transform));
        }

        foreach (var st in speakersAndText)
        {
            GhostAnimation ani = st.speaker.GetComponentInChildren<GhostAnimation>();
            if (ani != null)
            {
                AddEventItem(new EmotionAction(ani, st.emotion));
            }

            AddEventItem(new SpeakAction(ConversationManager.instance.speechbubble,st.speaker,st.text));
        }
        
        //cleanup
        foreach (var s in activeSpeakers)
        {
            AddEventItem(new ToggleActiveAction(s,false));
        }
        
        //disable event parent
        AddEventItem(new ToggleActiveAction(gameObject,false));
        
        foreach (var c in controllers)
        {
            AddEventItem(new NPCLookAtAction(c.GetAnimation));
            //retrun controll to pams npc controller
            AddEventItem(new SetNpcControllerPossession(false, c));
        }

        StartSequentialEvent();
    }
}

[System.Serializable]
public class SpeakerTextManager
{
    public GameObject speaker;
    public string text;
    
    public GhostAnimation.Emotion emotion; // emotion before text is said
}