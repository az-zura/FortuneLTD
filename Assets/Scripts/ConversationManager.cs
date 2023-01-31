using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    public static ConversationManager instance;
    public Conversation[] conversations;
    public GameObject player;
    public Speechbubble speechbubble;

    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
