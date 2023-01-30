using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Actions;
using EventSystem.Base;
using NPC.NpcMovement;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SequentialEvent))]
public class Speaker : MonoBehaviour
{
    public string[][] texts;
    private int curInteraction = 0;
    private int curTextPart = 0;
    public int id; // Please make sure that each speaker has a unique id!
    private SequentialEvent sequentialEvent;
    
    
    void Start()
    {
        if (SpeakerManager.instance.speakers.Find(s => s.id == id) != null)
        {
            Debug.LogError($"The ID of Speaker on {transform.name} is not unique!");
            return;
        }
        
        curInteraction = PlayerPrefs.GetInt($"alreadySaid{id}", 0);
        sequentialEvent = GetComponent<SequentialEvent>();
        
        LoadCurrentConversation();
    }

    public void LoadCurrentConversation()
    {
        
        string nextText = "";

        SpecificNpcController controller = gameObject.GetComponentInChildren<NpcControllerRandom>();
        //take control of Controller
        sequentialEvent.AddEventItem(new SetNpcControllerPossession(true, controller));
        sequentialEvent.AddEventItem(new NPCLookAtAction(controller.GetAnimation, SpeakerManager.instance.player.transform));
        
        // conversation
        nextText = GetNextText();
        if (nextText != "")
        {
            foreach (var t in texts[curInteraction])
            {
                // Loop durch Texte
                sequentialEvent.AddEventItem(new SpeakAction(SpeakerManager.instance.speechbubble, gameObject, nextText));
            }
        }
        else
        {
            //cleanup
            sequentialEvent.AddEventItem(new NPCLookAtAction(controller.GetAnimation));
            sequentialEvent.AddEventItem(new SetNpcControllerPossession(false, controller));
        }

        sequentialEvent.StartSequentialEvent();
    }
    
    public string GetNextText()
    {
        if (curInteraction >= texts.Length)
        {
            return "";
        }
        
        string s = texts[curInteraction][curTextPart];
        curTextPart++;
        if (curTextPart >= texts[curInteraction].Length)
        {
            curTextPart = 0;
            
            curInteraction++;
            PlayerPrefs.SetInt($"alreadySaid{id}", curInteraction);
        }
        
        //TODO make short conversation event that inherits from sequential event and when last event is dispatched, reset with the following fkt:
        /*
        public void Reset()
        {
            eventItems = new List<EventItem>();
            currentAction = 0;
        }
         */
        
        return s;
    }
}
