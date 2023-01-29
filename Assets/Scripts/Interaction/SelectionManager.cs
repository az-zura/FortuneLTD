using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    /// <summary>
    /// THe SelectionManager needs to be n the Main Camera in the Scene.
    /// Exactly one Selection Manager needs to be in every Scene with InteractableObjects to mak them work.
    /// </summary>
    public static SelectionManager instance; //Singleton --> there is only one Selection Manager in each Scene!

    public static string selectableTag = "Selectable";

    [Tooltip("Highlights the InteractableObject, which the mouse is currently on.")] [SerializeField]
    private Material selectionMaterial;

    [Tooltip("Highlights all InteractableObjects in a radius around the player.")] [SerializeField]
    private Material highlightMaterial; //highlights ALL selectable Objects

    [Tooltip("The player Transform.")] [SerializeField]
    private Transform player;

    [Tooltip("The radius around the player in which InteractableObjects should be highlighted.")] [SerializeField]
    private float radius = 5;

    [HideInInspector] public List<InteractableObject>
        interactableObjs =
            new List<InteractableObject>(); // filled automatically -- contains all InteractableObjects in the Scene

    //private Material[] defaultMaterials; // default material of the current selection
    private InteractableObject _selection; // current selection

    private void Awake()
    {
        // There should only be one SelectionManager in each Scene
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        foreach (var iobj in interactableObjs)
        {
            if (Vector3.Distance(player.position, iobj.transform.position) <= radius && iobj != _selection)
            {
                List<Material> ms = iobj.initialMaterials.ToList();
                ms.Add(highlightMaterial);
                iobj.renderer.materials = ms.ToArray();
            }
            else
            {
                iobj.renderer.materials = iobj.initialMaterials;
            }
        }

        if (_selection != null)
        {
            _selection.renderer.materials = _selection.initialMaterials;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        List<RaycastHit> hits = new List<RaycastHit>();
        hits.AddRange(Physics.RaycastAll(ray));
        if (hits.Count > 0)
        {
            var selection = hits[hits.Count - 1].transform;
            for (int i = 0; i < hits.Count - 1; i++)
            {
                Renderer rend = hits[i].transform.GetComponent<Renderer>();
                if (rend && hits[i].transform.CompareTag(selectableTag))
                {
                    if (!selection.GetComponent<Renderer>())
                    {
                        selection = hits[i].transform;
                    }

                    if (rend.materials[0].color.a > 0.01f)
                    {
                        float dist1 = Vector3.Distance(hits[i].transform.position, Camera.main.transform.position);
                        float dist2 = Vector3.Distance(selection.transform.position, Camera.main.transform.position);
                        if (dist1 < dist2)
                        {
                            selection = hits[i].transform;
                        }
                    }
                }
            }

            if (selection.CompareTag(selectableTag))
            {
                _selection = selection.GetComponent<InteractableObject>();
                List<Material> ms = _selection.initialMaterials.ToList();
                ms.Add(selectionMaterial);
                _selection.renderer.materials = ms.ToArray();
            }

            if (Input.GetMouseButtonDown(0) && _selection)
            {
                InteractableObject iObj = _selection.GetComponent<InteractableObject>();
                if (iObj)
                {
                    iObj.onClick?.Invoke();
                }
            }
        }
    }
}