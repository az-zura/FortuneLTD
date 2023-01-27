using UnityEngine;

namespace EventSystem
{
    public class PlayerNotInTriggerBoxCondition : ActionExecuteCondition
    {
        private bool isPlayerInTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player")) isPlayerInTrigger = true;
        }

        private void OnTriggerExit(Collider other)
        {   
            if(other.CompareTag("Player")) isPlayerInTrigger = false;
            
        }

        public override bool EvaluateCondition()
        {
            return !isPlayerInTrigger;
        }
    }
}
