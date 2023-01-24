using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerVegitationSpawner : MonoBehaviour
{
    public GameObject vegetation;
    public LayerMask ignoreLayers;
    private List<GameObject> placedVegetation = new List<GameObject>();

    public float traceTestHeight = 0.5f;
    public float traceTestLength = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Random.Range(0, 100) <= 1)
        //{
        //    PlaceVegetationBeneathPlayer();
        //}
    }

    public GameObject[] getSortetFlowerPos(Vector3 pos)
    {
        return placedVegetation.OrderBy((d) => (d.transform.position - pos).sqrMagnitude).ToArray();
    }

    public void removeFlower(GameObject flower)
    {
        Debug.Log("destory + "  + flower);
        Destroy(flower);
        placedVegetation.Remove(flower);
    }

    public void PlaceVegetationBeneathPlayer()
    {
        Vector3 castFrom = this.transform.position + Vector3.up * traceTestHeight;
        Ray ray = new Ray(castFrom, Vector3.down);
        if (Physics.Raycast(ray, out var hit, traceTestLength,~ignoreLayers) && !hit.transform.gameObject.name.Contains("TracableFlower"))
        {
            PlaceVegetationAt(hit.point);
        }
    }

    public void PlaceVegetationAt(Vector3 pos)
    {
        var placed = Instantiate(vegetation, pos, Quaternion.identity);
        placed.GetComponentInChildren<FlowerTrace>().vegetationSpawner = this;
        placedVegetation.Add(placed);
        
    }
}
