using System;
using UnityEngine;

namespace NPC.NpcMovement
{
    public abstract class SpecificNpcController : MonoBehaviour
    {
        protected NPC_Locomotion Locomotion;
        protected GhostAnimation Animation;
        public bool isControllingNpc = true;

        public NPC_Locomotion GetLocomotion => Locomotion;

        public GhostAnimation GetAnimation => Animation;

        public bool IsControllingNpc
        {
            get => isControllingNpc;
        }

        public void StopControllingNpc()
        {
            isControllingNpc = false;
            OnStopControlling();
            Locomotion.getNavmeshAgent().ResetPath();
        }

        public void StartControllingNpc()
        {
            isControllingNpc = true;
            OnStartControlling();
        }

        void Awake()
        {
            Locomotion = this.GetComponent<NPC_Locomotion>();
            Animation = this.GetComponentInChildren<GhostAnimation>();
            if (!Locomotion)
            {
                Debug.LogError("Couldn't get Locomotion in NPC_Movement_Controller!");
                return;
            }
            
            if (!Animation)
            {
                Debug.LogError("Couldn't get Animation in NPC_Movement_Controller!");
                return;
            }
            Locomotion.PathEndReached += onPathEndHidden;
            if(isControllingNpc) OnStartControlling();
        }
        
        

        private void OnDestroy()
        {
            Locomotion.PathEndReached -= onPathEndHidden;
        }

        private void onPathEndHidden(object caller, EventArgs args)
        {
            if (isControllingNpc)  OnPathEnd();
        }

        protected abstract void OnStartControlling();

        protected abstract void OnPathEnd();

        protected abstract void OnStopControlling();

    }
}
