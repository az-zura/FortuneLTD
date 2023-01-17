using UnityEngine;

namespace EventSystem
{
    public class SequentialEventTest : SequentialEvent
    {
        public ActionExecuteCondition playerInGarageCondition; 
        public ActionExecuteCondition playerInGardenCondition;
        public GhostAnimation playerAnimation;

        public GameObject player;
        public Speechbubble Speechbubble;
        public GameObject ghost;
        public override void OnEventInitialized()
        {

            /*
            AddAction(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Thinking));
            AddAction(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Default,3.0f));
            AddAction(new SpeakAction(Speechbubble,player,"Mal schauen wies dem Auto in der Garage geht",5.0f));
            
            //in garage
            AddAction(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Surprised).AddCondition(playerInGarageCondition));
            AddAction(new SpeakAction(Speechbubble, player, "WO IST MEIN AUTO!??!!?!?", 2.0f));
            AddAction(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Thinking));
            AddAction(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Default,1.5f));
            AddAction(new SpeakAction(Speechbubble, player, "Ah stimmt Geister haben keine Autos", 2.0f));
            
            
            AddAction(new SpeakAction(Speechbubble,player,"",0.5f));
            AddAction(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Thinking));
            AddAction(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Default,3.0f));
            AddAction(new SpeakAction(Speechbubble,player,"Ich sollte auf jeden fall mal den Rasen im Vorgarten mähen",4.0f));
            
            AddAction(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Sad).AddCondition(playerInGardenCondition));;
            AddAction(new EmotionAction(playerAnimation,GhostAnimation.Emotion.Default,5.0f));
            AddAction(new SpeakAction(Speechbubble, player, "Ah stimmt...", 1.5f));
            AddAction(new SpeakAction(Speechbubble, player, "... Rasen haben wir auch nicht", 2.5f));
            */
            
            AddAction(new NPCGotoAction(ghost.GetComponentInChildren<NPC_Locomotion>(),player,6,3));
            AddAction(new NPCLookAtAction(ghost.GetComponentInChildren<GhostAnimation>(), player.transform));
            AddAction(new SpeakAction(Speechbubble,player,"Hello",5.0f));
            AddAction(new SpeakAction(Speechbubble,ghost,"Hi",5.0f));
            AddAction(new NPCLookAtAction(ghost.GetComponentInChildren<GhostAnimation>()));
            StartSequentialEvent();
        }
    }
}
