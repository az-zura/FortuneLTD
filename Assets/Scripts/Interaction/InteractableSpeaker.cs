using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSpeaker : InteractableObject
{
    // Just a very simple Script
    [SerializeField] private Speechbubble speechBubble;
    [SerializeField] private string[] text;
    [SerializeField] private bool loop;
    private int cur = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.tag = SelectionManager.selectableTag;
        SelectionManager.instance.interactableObjs.Add(transform);

        onClick.AddListener(() => DisplayText());
        speechBubble.btn.onClick.AddListener(() => DisplayText());
    }

    public void DisplayText()
    {
        if (cur < text.Length)
        {
            if (speechBubble.gameObject.activeSelf)
                speechBubble.gameObject.SetActive(true);

            speechBubble.SetBubble(text[cur], Speechbubble.BubbleType.Speech, transform);
            cur++;
        }
        else
        {
            speechBubble.gameObject.SetActive(false);
            if (loop)
            {
                cur = 0;
            }
        }
    }
}
