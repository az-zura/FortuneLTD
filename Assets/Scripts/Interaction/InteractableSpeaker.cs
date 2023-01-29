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
        renderer = GetComponent<Renderer>();
        initialMaterials = renderer.materials;
        SelectionManager.instance.interactableObjs.Add(this);

        onClick.AddListener(() => DisplayText());
        speechBubble.btn.onClick.AddListener(() => DisplayText());
    }
    
    private void OnDestroy()
    {
        SelectionManager.instance.interactableObjs.Remove(this);
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
            speechBubble.btn.onClick.RemoveListener(() => DisplayText());
            speechBubble.gameObject.SetActive(false);
            if (loop)
            {
                cur = 0;
            }
        }
    }
}
