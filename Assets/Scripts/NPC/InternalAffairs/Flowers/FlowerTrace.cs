using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTrace : InteractableObject
{
    public PlayerVegitationSpawner vegetationSpawner;
    public Mesh[] flowerModels;

    private void Start()
    {
        base.Start();
        this.GetComponent<MeshFilter>().sharedMesh = flowerModels[Random.Range(0, flowerModels.Length)];

    }
    public void Destroy()
    {
        vegetationSpawner.removeFlower(this.gameObject);
    }
}
