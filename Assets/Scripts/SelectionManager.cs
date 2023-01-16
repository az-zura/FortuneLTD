using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    
    private Material defaultMaterial;
    private Transform _selection;
    
    private void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            //selectionRenderer.material = defaultMaterial;
            selectionRenderer.materials = new[] {defaultMaterial};
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
            for (int i = 0; i < hits.Count; i++)
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
                    //selectionRenderer.material = highlightMaterial;
                    selectionRenderer.materials = new[] {defaultMaterial, highlightMaterial};
                }

                _selection = selection;
            }

            if (Input.GetMouseButtonDown(0))
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