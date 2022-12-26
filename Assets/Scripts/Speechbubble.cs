using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Speechbubble : MonoBehaviour
{
    [SerializeField] private Image talkIndicator;
    [SerializeField] private Transform player;
    [SerializeField] private bool scaleWithPlayer;
    [SerializeField] private float minScale = 0.1f;
    [SerializeField] private float scaleFactor = 10f;

    private Vector3 playerSize;
    private Camera camera;
    private RectTransform rectTrans;
    private CanvasGroup canvasGroup;
    private Vector2 initialPos;
    private Vector2 initialSize;
    private Vector2 initialSizeIndicator;

    private void Start()
    {
        camera = Camera.main;
        playerSize = player.GetComponent<MeshRenderer>().bounds.size;
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        //initialOffsets[0] = rectTrans.offsetMin;
        //initialOffsets[1] = rectTrans.offsetMax;
        initialPos = rectTrans.position;
        initialSize = rectTrans.sizeDelta;
        initialSizeIndicator = talkIndicator.rectTransform.sizeDelta;
    }

    private void Update()
    {
        //project player position onto canvas
        Vector3 worldPos = player.position + camera.transform.up * playerSize.y * 0.7f;
        Vector3 projectedPos = RectTransformUtility.WorldToScreenPoint(camera, worldPos);
        transform.position =  new Vector2(initialPos.x, projectedPos.y);
        projectedPos = RectTransformUtility.WorldToScreenPoint(camera, player.position);
        talkIndicator.transform.position = new Vector2(projectedPos.x, 0);;

        if (scaleWithPlayer)
        {
            float dist = Vector3.Distance(player.position, camera.transform.position);
            
            if (dist == 0)
            {
                dist = float.MinValue;
            }

            float scaleTemp = scaleFactor / dist;
            Vector3 newScale = (scaleTemp >= minScale ? scaleTemp : minScale) * Vector3.one;

            rectTrans.sizeDelta = initialSize * newScale;
            talkIndicator.rectTransform.sizeDelta = initialSizeIndicator * newScale;
        }
        
        // set y - position of Speechbubble and indicator
        rectTrans.position += Vector3.up * (rectTrans.sizeDelta.y * 0.5f + talkIndicator.rectTransform.sizeDelta.y);
        talkIndicator.rectTransform.localPosition = new Vector3(talkIndicator.rectTransform.localPosition.x, -(rectTrans.sizeDelta.y + talkIndicator.rectTransform.sizeDelta.y) * 0.5f);

        float alpha = talkIndicator.transform.localPosition.x < 0 ?
            Math.Abs(talkIndicator.transform.position.x - (rectTrans.position.x - rectTrans.sizeDelta.x * 0.5f)) /
            (rectTrans.sizeDelta.x * 0.5f) : Math.Abs(talkIndicator.transform.position.x - (rectTrans.position.x+rectTrans.sizeDelta.x * 0.5f)) /
                                             (rectTrans.sizeDelta.x * 0.5f);
        canvasGroup.alpha = (talkIndicator.transform.position.x >= rectTrans.position.x-rectTrans.sizeDelta.x * 0.5f && talkIndicator.transform.position.x <= rectTrans.position.x+rectTrans.sizeDelta.x * 0.5f) ? alpha : 0;
    }
}