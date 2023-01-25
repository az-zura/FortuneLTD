using EventSystem.Base;
using UnityEngine;

namespace EventSystem.Actions
{
    public class InstantiateAction : ActionBase
    {
        private readonly GameObject gameObject;
        private readonly Vector3 position;
        private readonly Quaternion rotation;
        private GameObject instantiated;

        public InstantiateAction(GameObject gameObject, Vector3 position, Quaternion rotation = default)
        {
            this.gameObject = gameObject;
            this.position = position;
            this.rotation = rotation == default ? Quaternion.identity : rotation;
        }

        public override void OnItemStart()
        {
            instantiated = eventBase.InstantiateActor(gameObject,position,rotation);
            EndEventItem();
        }

        public GameObject GetInstantiated()
        {
            if (instantiated) return instantiated;
            Debug.LogWarning("Game object hasn't been instantiated yet");
            return null;
        }

        public override void OnResumeExecution()
        {
            throw new System.NotImplementedException();
        }
    }
}
