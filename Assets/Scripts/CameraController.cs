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
    
    [Tooltip("When not using multiple raycasts, every part of a specific wall should have the same parent and the parent should also be in the \"Walls\"-layer.")]
    [SerializeField] private bool multipleRaycasts = true;

    [Header("When using the multiple-raycasts-approach:")]
    [SerializeField] private int numOfRaycasts = 100;
    [SerializeField] private Vector2 windowSize = new Vector2(2f, 1f);
    [SerializeField] private Vector2 windowOffset;
 
    void Start()
    {
        oldHitsNumber = 0;

        foreach (CinemachineVirtualCamera cam in cameras)
        {
            cam.Priority = 0;
        }
        SwitchToCamera(currentCam);
    }
 
    private void LateUpdate()
    {
        ViewObstructed();
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
        if (!multipleRaycasts)
        {
            // with one raycast
            // IMPORTANT NOTE: Every part of a specific wall should have the same parent with this approach
            float characterDistance = Vector3.Distance(transform.position, player.transform.position);
            int layerNumber = LayerMask.NameToLayer("Walls");
            int layerMask = 1 << layerNumber;
            List<RaycastHit> hits = new List<RaycastHit>();
            hits.AddRange(Physics.RaycastAll(transform.position, player.position - transform.position,
                characterDistance, layerMask));

            if (hits.Count > 0)
            {
                // Means that some stuff is blocking the view
                int newHits = hits.Count - oldHitsNumber;
                oldHitsNumber = hits.Count;

                if (obstructions != null && obstructions.Count > 0 && newHits < 0)
                {
                    // Repaint all the previous obstructions. Because some of the stuff might be not blocking anymore
                    for (int i = 0; i < obstructions.Count; i++)
                    {
                        MeshRenderer rend = obstructions[i].gameObject.GetComponent<MeshRenderer>();
                        if (rend != null)
                            rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    }

                    obstructions.Clear();
                }

                // collect all obstructions
                for (int i = 0; i < hits.Count; i++)
                {
                    Transform obstruction = hits[i].transform;
                    obstructions.Add(obstruction);
                    
                    Transform parent = obstruction.parent;
                    while (parent.gameObject.layer == LayerMask.NameToLayer("Walls"))
                    {
                        if (!obstructions.Contains(parent))
                        {
                            obstructions.Add(parent);
                            for (int j = 0; j < parent.childCount; j++)
                            {
                                obstructions.Add(parent.GetChild(j));
                            }
                        }

                        parent = parent.parent;
                        if (!parent)
                            break;
                    }
                }

                // Hide the current obstructions
                foreach (Transform obs in obstructions)
                {
                    MeshRenderer rend = obs.gameObject.GetComponent<MeshRenderer>();
                    if (rend != null)
                        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }
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
                            rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    }

                    oldHitsNumber = 0;
                    obstructions.Clear();
                }
            }
        }
        else
        {
            //with many raycasts
            float characterDistance = Vector3.Distance(transform.position, player.transform.position);
            int layerNumber = LayerMask.NameToLayer("Walls");
            int layerMask = 1 << layerNumber;
            List<RaycastHit> hits = new List<RaycastHit>();
            float stepSize = (windowSize.x * windowSize.y) / (float)numOfRaycasts;
            Vector3 right = cameras[currentCam].transform.right;
            Vector3 up = cameras[currentCam].transform.up;

            for (float fx = (-windowSize.x + windowOffset.x) * 0.5f; fx < (windowSize.x + windowOffset.x) * 0.5f; fx += stepSize)
            {
                for (float fy = (-windowSize.y + windowOffset.y) * 0.5f; fy < (windowSize.y + windowOffset.y) * 0.5f; fy += stepSize)
                {
                    Vector3 moveVector = player.position + right * fx + up * fy;
                    hits.AddRange(Physics.RaycastAll(transform.position,
                        (player.position + moveVector) - transform.position,
                        characterDistance, layerMask));
                }
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
                        obstructions[i].gameObject.GetComponent<MeshRenderer>().shadowCastingMode =
                            UnityEngine.Rendering.ShadowCastingMode.On;
                    }
                    
                    obstructions.Clear();
                }
                
                // Hide the current obstructions
                for (int i = 0; i < hits.Count; i++)
                {
                    Transform obstruction = hits[i].transform;
                    obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode =
                        UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
                        obstructions[i].gameObject.GetComponent<MeshRenderer>().shadowCastingMode =
                            UnityEngine.Rendering.ShadowCastingMode.On;
                    }

                    oldHitsNumber = 0;
                    obstructions.Clear();
                }
            }
        }
    }
}