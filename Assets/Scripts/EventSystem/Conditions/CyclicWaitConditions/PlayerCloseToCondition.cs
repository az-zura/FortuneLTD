using UnityEngine;

namespace EventSystem.Conditions.CyclicWaitConditions
{
    public class PlayerCloseToCondition : ActionExecuteCondition
    {
        private GameObject object1;
        private GameObject object2;
        private float distance;
        public PlayerCloseToCondition(GameObject object1, GameObject object2, float distance)
        {
            this.object1 = object1;
            this.object2 = object2;
            this.distance = distance;
        }

        public override bool EvaluateCondition()
        {
            return Vector3.Distance(object1.transform.position, object2.transform.position) <= distance;
        }
    }
}
