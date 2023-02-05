using UnityEngine;

namespace EventSystem
{
    public class EventDispatcherTriggerState : MonoBehaviour
    {
        [SerializeField]
        private EventBase eventBase;
        [SerializeField]
        private MainQuest main;
        [SerializeField]
        private int ifQuestStateOrHigher;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("quest state " + main.getMainQuestState());
            if (main.getMainQuestState() >= ifQuestStateOrHigher)
            {
                eventBase.InitializeEvent();
            }
        }
    }
}
