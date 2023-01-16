using UnityEngine;

namespace EventSystem
{
    public class EventDispatcherTrigger : MonoBehaviour
    {
        [SerializeField]
        private EventBase eventBase;
        private void OnTriggerEnter(Collider other)
        {
            eventBase.InitializeEvent();
        }
    }
}
