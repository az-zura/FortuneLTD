using EventSystem.Base;
using UnityEngine;

namespace EventSystem.Actions
{
    public class InstantiateAction : ActionBase
    {
        private readonly GameObject gameObject;
        private readonly Vector3 position;
        private readonly Quaternion rotation;

        public InstantiateAction(GameObject gameObject, Vector3 position, Quaternion rotation = default)
        {
            this.gameObject = gameObject;
            this.position = position;
            this.rotation = rotation == default ? Quaternion.identity : rotation;
        }

        public override void OnItemStart()
        {
            eventBase.InstantiateActor(gameObject,position,rotation);
            EndEventItem();
        }

        public override void OnResumeExecution()
        {
            throw new System.NotImplementedException();
        }
    }
}
