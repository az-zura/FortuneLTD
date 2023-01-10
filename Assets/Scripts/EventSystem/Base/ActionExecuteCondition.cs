using UnityEngine;

namespace EventSystem
{
    public abstract class ActionExecuteCondition : MonoBehaviour
    {
        public abstract bool EvaluateCondition();
    }
}
