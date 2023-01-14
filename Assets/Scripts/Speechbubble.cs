using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

[RequireComponent(typeof(CanvasGroup))]
public class Speechbubble : MonoBehaviour
{
    [System.Serializable]
    public enum BubbleType
    {
        Speech,
        Thought,
        Dismissed
    }
    
    [SerializeField] private TextMeshProUGUI bubbleText;
    [SerializeField] private Image talkIndicator;
    [SerializeField] private BubbleType bubbleType = BubbleType.Speech;
    [SerializeField] [Tooltip("[0]: speech, [1]: thought")] private Sprite[] typeSprites; 
    [SerializeField] private Transform speaker; // the game object currently that talks
    [SerializeField] private bool scaleWithSpeaker;
    [SerializeField] private float minScale = 0.1f;
    [SerializeField] private float scaleFactor = 10f;

    private Vector3 playerSize;
    private Camera camera;
    private RectTransform rectTrans;
    private CanvasGroup canvasGroup;
    private Vector2 initialPos; // Initial position of the bubble
    private Vector2 initialSize; // Initial size of the bubble
    private Vector2 initialSizeIndicator; // Initial size of the position indicator

    private void Start()
    {
        if (speaker)
        {
            ChangeSpeaker(speaker);
        }
        camera = Camera.main;

        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        initialPos = rectTrans.position;
        initialSize = rectTrans.sizeDelta;
        initialSizeIndicator = talkIndicator.rectTransform.sizeDelta;

        talkIndicator.sprite =
            typeSprites[(bubbleType.GetHashCode() < typeSprites.Length) ? bubbleType.GetHashCode() : 0];
        //StartCoroutine(Test());
    }

    private void Update()
    {
        if (bubbleType == BubbleType.Dismissed)
        {
            this.gameObject.SetActive(false);
            return;
        }
        if (scaleWithSpeaker)
        {
            // scale depending on distance to player
            float dist = Vector3.Distance(speaker.position, camera.transform.position);
            
            if (dist == 0)
            {
                dist = float.MinValue;
            }

            float scaleTemp = scaleFactor / dist;
            Vector3 newScale = (scaleTemp >= minScale ? scaleTemp : minScale) * Vector3.one;

            rectTrans.sizeDelta = initialSize * newScale;
            talkIndicator.rectTransform.sizeDelta = initialSizeIndicator * newScale;
        }
        
        //project player position onto canvas
        Vector3 worldPos = speaker.position + camera.transform.up * (playerSize.y * 0.7f);
        Vector3 projectedPos = RectTransformUtility.WorldToScreenPoint(camera, worldPos);
        transform.position =  new Vector2(initialPos.x, projectedPos.y);
        projectedPos = RectTransformUtility.WorldToScreenPoint(camera, speaker.position);
        talkIndicator.transform.position = new Vector2(projectedPos.x, 0);;
        
        // set y - position of speech bubble and indicator
        rectTrans.position += Vector3.up * (rectTrans.sizeDelta.y * 0.5f + talkIndicator.rectTransform.sizeDelta.y);
        talkIndicator.rectTransform.localPosition = new Vector3(talkIndicator.rectTransform.localPosition.x, -(rectTrans.sizeDelta.y + talkIndicator.rectTransform.sizeDelta.y) * 0.5f);

        // set speech bubble alpha depending on position
        float alpha = 1f;
        if (talkIndicator.transform.localPosition.x < rectTrans.sizeDelta.x * 0.3f)
        {
            alpha = Math.Abs(talkIndicator.transform.position.x - (rectTrans.position.x - rectTrans.sizeDelta.x * 0.5f)) /
                    (rectTrans.sizeDelta.x * 0.3f);
        }
        else if (talkIndicator.transform.localPosition.x > rectTrans.sizeDelta.x * 0.3f)
        {
            alpha = Math.Abs(talkIndicator.transform.position.x - (rectTrans.position.x+rectTrans.sizeDelta.x * 0.5f)) /
                    (rectTrans.sizeDelta.x * 0.3f);
        }
        canvasGroup.alpha = (talkIndicator.transform.position.x >= rectTrans.position.x-rectTrans.sizeDelta.x * 0.5f && talkIndicator.transform.position.x <= rectTrans.position.x+rectTrans.sizeDelta.x * 0.5f) ? alpha : 0;
    }

    public void ChangeSpeaker(Transform speaker)
    {
        this.speaker = speaker;
        playerSize = speaker.GetComponentInChildren<Renderer>().bounds.size;

    }
    
    public void ChangeBubbleType(BubbleType type)
    {
        switch (type)
        {
            case BubbleType.Speech:
            case BubbleType.Thought:
                bubbleType = type;
                talkIndicator.sprite = typeSprites[type.GetHashCode()];
                this.gameObject.SetActive(true);
                break;
            case BubbleType.Dismissed:
                this.gameObject.SetActive(false);
                break;
            default:
                Debug.LogError($"Bubble type {type} does not exist.");
                break;
        }    }

    public void SetBubbleText(string text)
    {
        bubbleText.text = text;
    }

    public void SetBubble(string text, BubbleType type, Transform speaker)
    {
        SetBubbleText(text);
        ChangeBubbleType(type);
        ChangeSpeaker(speaker);
    }
}