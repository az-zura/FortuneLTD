using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// The CameraController should be on the Main Camera in the Scene.
    /// </summary>
    
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

    /// <summary>
    /// Switches the current camera to the camera at index camNum in the cameras-Array.
    /// </summary>
    /// <param name="camNum"> The index of the desired camera in the cameras-Array. </param>
    public void SwitchToCamera(int camNum)
    {
        cameras[currentCam].Priority = 0;
        currentCam = camNum;
        cameras[camNum].Priority = 10;
    }

    /// <summary>
    /// Switches the current camera to the next camera in the cameras-Array.
    /// </summary>
    public void SwitchToNextCamera()
    {
        int camNum = (currentCam + 1) % cameras.Length;
        SwitchToCamera(camNum);
    }

    /// <summary>
    /// Checks if the view on the player is obstructed and makes the obstructions on the Layer "Walls" invisible.
    /// </summary>
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
                        StartCoroutine(FadeMaterials(rend, 1f));
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
                    StartCoroutine(FadeMaterials( rend, 0f));
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
                        StartCoroutine(FadeMaterials(rend, 1f));
                    }
                }

                oldHitsNumber = 0;
                obstructions.Clear();
            }
        }
    }

    private Dictionary<Renderer, float> currentlyFading = new Dictionary<Renderer, float>();
    /// <summary>
    /// Fades the material to a given alpha with a certain speed.
    /// </summary>
    /// <param name="renderer"> Renderer of the material (must be a Key in currentlyFading-Dictionary). </param>
    /// <param name="material"> The Material that should fade. </param>
    /// <param name="fadeSpeed"> The speed in which the material should fade to toalpha. </param>
    /// <returns></returns>
    private IEnumerator FadeMaterial(Renderer renderer, Material material, float fadeSpeed = 1f)
    {
        while (currentlyFading.ContainsKey(renderer) && Math.Abs(material.color.a - currentlyFading[renderer]) > 0.01f)
        {
            if (material.color.a < currentlyFading[renderer])
            {
                // increase alpha
                material.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            }
            else if (material.color.a > currentlyFading[renderer])
            {
                // decrease alpha
                material.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            }

            yield return null;
        }

        //fading done
        material.color = new Color(material.color.r, material.color.g, material.color.b, currentlyFading[renderer]);
    }

    /// <summary>
    /// Fades all materials of a given renderer to a given alpha with a certain speed.
    /// </summary>
    /// <param name="renderer"> Renderer of the material (must be a Key in currentlyFading-Dictionary). </param>
    /// <param name="toalpha"> The alpha, the material should fade to. </param>
    /// <param name="fadeSpeed"> The speed in which the material should fade to toalpha. </param>
    /// <returns></returns>
    private IEnumerator FadeMaterials(Renderer renderer, float toalpha, float fadeSpeed = 1f)
    {
        if (!currentlyFading.ContainsKey(renderer))
        {
            currentlyFading.Add(renderer, toalpha);
        }
        else
        {
            // material is already fading
            currentlyFading[renderer] = toalpha;
            yield break;
        }
        
        float buffer = 0.05f;
        foreach (var material in renderer.materials)
        {
            StartCoroutine(FadeMaterial(renderer, material, fadeSpeed));
        }

        yield return new WaitUntil(() => Math.Abs(renderer.material.color.a - toalpha) <= 0.01f);
        yield return new WaitForSeconds(buffer);
            
        currentlyFading.Remove(renderer);
    }
}