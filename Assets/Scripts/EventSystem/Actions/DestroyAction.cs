using EventSystem.Base;
using UnityEngine;

namespace EventSystem.Actions
{
    public class DestroyAction : ActionBase
    {
        private readonly GameObject gameObject;


        public DestroyAction(GameObject gameObject)
        {
            this.gameObject = gameObject;

        }

        public override void OnItemStart()
        {
            eventBase.DestroyActor(gameObject);
            EndEventItem();
        }

        public override void OnResumeExecution()
        {
            throw new System.NotImplementedException();
        }
    }
}
