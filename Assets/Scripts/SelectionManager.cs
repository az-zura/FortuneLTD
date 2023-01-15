using Unity.VisualScripting;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
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
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    defaultMaterial = selectionRenderer.material;
                    //selectionRenderer.material = highlightMaterial;
                    selectionRenderer.materials = new[] {defaultMaterial, highlightMaterial};
                }

                _selection = selection;
            }
        }
    }
}