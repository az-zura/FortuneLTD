using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventSystem;
using EventSystem.Base;
using NPC.NpcMovement;
using UnityEngine;

public class Conversation : SequentialEvent
{
    public ClickableItemCondition clickable;
    public SpeakerTextManager[] speakersAndText;

    private List<int> ids = new List<int>();
    private int currentConversation;

    public override void OnEventInitialized()
    {
        // make sure only conversations with the same ids are played
        foreach (var st in speakersAndText)
        {
            if (!ids.Contains(st.id))
                ids.Add(st.id);
        }

        // they are talking to each other
        //int n = speakersAndText.Length;
        List<SpecificNpcController> controllers = new List<SpecificNpcController>();
        List<GhostAnimation> animations = new List<GhostAnimation>();
        List<NPC_Locomotion> locomotions = new List<NPC_Locomotion>();
        List<GameObject> activeSpeakers = new List<GameObject>();
        List<SpeakerTextManager> nextST = new List<SpeakerTextManager>();

        int n = ids.Count;
        for (int m = 0; m < n; m++)
        {
            if (ids.Count <= 0)
            {
                return;
            }

            currentConversation = ids.First();
            ids.RemoveAt(0);

            nextST.Clear();
            nextST.AddRange(Array.FindAll(speakersAndText.ToArray(), s => s.id == currentConversation));

            for (int i = 0; i < nextST.Count; i++)
            {
                SpecificNpcController c = nextST[i].speaker.GetComponentInChildren<NpcControllerRandom>();

                if (!controllers.Contains(c) && c != null)
                {
                    controllers.Add(c);
                }

                GhostAnimation a = nextST[i].speaker.GetComponentInChildren<GhostAnimation>();
                if (!animations.Contains(a) && a != null)
                {
                    animations.Add(a);
                }

                NPC_Locomotion l = nextST[i].speaker.GetComponentInChildren<NPC_Locomotion>();
                if (!locomotions.Contains(l) && l != null)
                {
                    locomotions.Add(l);
                }

                GameObject s = nextST[i].speaker;
                if (!activeSpeakers.Contains(s) && s != null)
                {
                    activeSpeakers.Add(s);
                }
            }

            foreach (var s in activeSpeakers)
            {
                AddEventItem(new ToggleActiveAction(s, true));
            }


            foreach (var c in controllers)
            {
                AddEventItem(new SetNpcControllerPossession(true, c));
                AddEventItem(new NPCGotoAction(c.GetLocomotion, ConversationManager.instance.player, 0, 2));
                AddEventItem(new NPCLookAtAction(c.GetAnimation, ConversationManager.instance.player.transform));
            }

            foreach (var st in nextST)
            {
                ClickableItemCondition io = st.speaker.GetComponentInChildren<ClickableItemCondition>();
                if (io)
                    AddEventItem(new ShowOutlineAction(io, true));
            }

            AddEventItem(new EventBasedCondition(clickable.TriggerCondition));

            foreach (var st in nextST)
            {
                ClickableItemCondition io = st.speaker.GetComponentInChildren<ClickableItemCondition>();
                if (io)
                    AddEventItem(new ShowOutlineAction(io, false));
                
                GhostAnimation ani = st.speaker.GetComponentInChildren<GhostAnimation>();
                if (ani != null)
                {
                    AddEventItem(new EmotionAction(ani, st.emotion));
                }

                AddEventItem(new SpeakAction(ConversationManager.instance.speechbubble, st.speaker, st.text));
            }


            foreach (var c in controllers)
            {
                AddEventItem(new NPCLookAtAction(c.GetAnimation));
                //return control
                AddEventItem(new SetNpcControllerPossession(false, c));
            }

            if (m < n - 1) // Don't add click action after last conversation
                AddEventItem(new EventBasedCondition(clickable.TriggerCondition));
        }

        foreach (var s in activeSpeakers)
        {
            ClickableItemCondition io = s.GetComponentInChildren<ClickableItemCondition>();
            if (io)
                AddEventItem(new ShowOutlineAction(io, false));
        }

        StartSequentialEvent();
    }
}

[System.Serializable]
public class SpeakerTextManager
{
    public int id;
    public GameObject speaker;
    public string text;
    public GhostAnimation.Emotion emotion; // emotion before text is said
}