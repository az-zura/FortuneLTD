using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    [SerializeField] private int currentCam = 0;

    private List<Transform> obstructions = new List<Transform>();

    [SerializeField] private Transform player;
    private int oldHitsNumber;

    [Tooltip(
        "When not using multiple raycasts, every part of a specific wall should have the same parent and the parent should also be in the \"Walls\"-layer.")]
    [SerializeField]
    private WallCheckType wallCheck = WallCheckType.boxcast;

    [Header("When using the multiple-raycasts-approach:")] [SerializeField]
    private int numOfRaycasts = 100;

    [SerializeField] private Vector2 windowSize = new Vector2(2f, 1f);
    [SerializeField] private Vector2 windowOffset;
    
    [Header("When using the boxCast-approach:")]
    [SerializeField] private Vector2 boxXYSize = new Vector2(1f, 1f);

    [System.Serializable]
    public enum WallCheckType
    {
        oneRaycast,
        multipleRaycasts,
        boxcast
    }

    private Vector3 oldPos;

    void Start()
    {
        oldHitsNumber = 0;
        oldPos = transform.position;
        ViewObstructed();

        foreach (CinemachineVirtualCamera cam in cameras)
        {
            cam.Priority = 0;
        }

        SwitchToCamera(currentCam);
    }

    private void LateUpdate()
    {
        //check if camera is moving
        if (transform.position != oldPos)
        {
            ViewObstructed();
            oldPos = transform.position;
        }
    }

    public void SwitchToCamera(int camNum)
    {
        cameras[currentCam].Priority = 0;
        currentCam = camNum;
        cameras[camNum].Priority = 10;
    }

    public void SwitchToNextCamera()
    {
        int camNum = (currentCam + 1) % cameras.Length;
        SwitchToCamera(camNum);
    }

    void ViewObstructed()
    {
        float characterDistance = Vector3.Distance(transform.position, player.transform.position);
        int layerNumber = LayerMask.NameToLayer("Walls");
        int layerMask = 1 << layerNumber;
        List<RaycastHit> hits = new List<RaycastHit>();
        switch (wallCheck)
        {
            case WallCheckType.oneRaycast:
                // with one raycast
                // IMPORTANT NOTE: Every part of a specific wall should have the same parent with this approach
                hits.AddRange(Physics.RaycastAll(transform.position, player.position - transform.position,
                    characterDistance, layerMask));
                break;
            case WallCheckType.multipleRaycasts:
                //with many raycasts
                float stepSize = (windowSize.x * windowSize.y) / (float) numOfRaycasts;
                Vector3 right = cameras[currentCam].transform.right;
                Vector3 up = cameras[currentCam].transform.up;

                for (float fx = (-windowSize.x + windowOffset.x) * 0.5f;
                     fx < (windowSize.x + windowOffset.x) * 0.5f;
                     fx += stepSize)
                {
                    for (float fy = (-windowSize.y + windowOffset.y) * 0.5f;
                         fy < (windowSize.y + windowOffset.y) * 0.5f;
                         fy += stepSize)
                    {
                        Vector3 moveVector = player.position + right * fx + up * fy;
                        hits.AddRange(Physics.RaycastAll(transform.position,
                            moveVector - transform.position,
                            characterDistance, layerMask));
                    }
                }
                break;
            case WallCheckType.boxcast:
                Vector3 dir = (player.position - transform.position).normalized;
                hits.AddRange(Physics.BoxCastAll(transform.position,
                    new Vector3(boxXYSize.x, boxXYSize.y, 0.01f) * 0.5f, dir,
                    Quaternion.identity, characterDistance, layerMask));
                break;
        }

        if (hits.Count > 0)
        {
            // Means that some stuff is blocking the view
            int newHits = hits.Count - oldHitsNumber;

            if (obstructions != null && obstructions.Count > 0 && newHits < 0)
            {
                // Repaint all the previous obstructions. Because some of the stuff might be not blocking anymore
                for (int i = 0; i < obstructions.Count; i++)
                {
                    MeshRenderer rend = obstructions[i].gameObject.GetComponent<MeshRenderer>();
                    if (rend != null)
                    {
                        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                        StartCoroutine(FadeMaterial(rend.materials[0], 1f));
                    }
                }

                obstructions.Clear();
            }

            // Hide the current obstructions
            for (int i = 0; i < hits.Count; i++)
            {
                Transform obstruction = hits[i].transform;
                MeshRenderer rend = obstruction.gameObject.GetComponent<MeshRenderer>();
                if (rend != null)
                {
                    rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    StartCoroutine(FadeMaterial( rend.materials[0], 0f));
                }

                obstructions.Add(obstruction);
            }

            oldHitsNumber = hits.Count;
        }
        else
        {
            // Mean that no more stuff is blocking the view and sometimes all the stuff is not blocking as the same time
            if (obstructions != null && obstructions.Count > 0)
            {
                for (int i = 0; i < obstructions.Count; i++)
                {
                    MeshRenderer rend = obstructions[i].gameObject.GetComponent<MeshRenderer>();
                    if (rend != null)
                    {
                        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                        StartCoroutine(FadeMaterial(rend.materials[0], 1f));
                    }
                }

                oldHitsNumber = 0;
                obstructions.Clear();
            }
        }
    }

    private Dictionary<Material, float> currentlyFading = new Dictionary<Material, float>();

    private IEnumerator FadeMaterial(Material material, float toalpha, float fadeSpeed = 1f)
    {
        if (!currentlyFading.ContainsKey(material))
        {
            MaterialExtensions.ToFadeMode(material);
            material.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Transparent;
            currentlyFading.Add(material, toalpha);
        }
        else
        {
            // material is already fading
            currentlyFading[material] = toalpha;
            yield break;
        }

        while (Math.Abs(material.color.a - currentlyFading[material]) > 0.01f)
        {
            if (material.color.a < currentlyFading[material])
            {
                // increase alpha
                material.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            }
            else if (material.color.a > currentlyFading[material])
            {
                // decrease alpha
                material.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            }

            yield return null;
        }

        //fading done
        material.color = new Color(material.color.r, material.color.g, material.color.b, currentlyFading[material]);
        currentlyFading.Remove(material);
    }
}