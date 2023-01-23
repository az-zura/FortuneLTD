using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance; //Singleton --> there is only one Selection Manager in each Scene!
    public static string selectableTag = "Selectable";
    [SerializeField] private Material selectionMaterial;
    [SerializeField] private Material highlightMaterial; //highlights ALL selectable Objects
    [SerializeField] private Transform player;
    [SerializeField] private float radius = 5;

    [HideInInspector] public List<Transform> interactableObjs = new List<Transform>();
    private Material defaultMaterial;
    private Transform _selection;

    private void Awake()
    {
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
            Renderer r = iobj.GetComponent<Renderer>();
            if (r)
            {
                Material m = r.material;
                if (Vector3.Distance(player.position, iobj.position) <= radius && iobj != _selection)
                {
                    r.materials = new[] {m, highlightMaterial};
                }
                else
                {
                    r.materials = new[] {m};
                }
            }
        }
        
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            //selectionRenderer.material = defaultMaterial;
            List<Material> materials = selectionRenderer.materials.ToList();
            materials.Remove(selectionMaterial);
            selectionRenderer.materials = materials.ToArray();//new[] {defaultMaterial};
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
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer)
                {
                    defaultMaterial = selectionRenderer.material;
                    selectionRenderer.materials = new[] {defaultMaterial, selectionMaterial};
                }

                _selection = selection;
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