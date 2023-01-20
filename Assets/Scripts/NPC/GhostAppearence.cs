using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAppearence : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] glasses;
    public GameObject[] handHeld;

    public Mesh ghostMeshMale;
    public Mesh ghostMeshFemale;


    public float glassesChance;
    public float handHeldChance;

    private GhostGender gender;

    private enum GhostGender
    {
        male,
        female
    }

    void Start()
    {
        //set random ghost gender 
        SkinnedMeshRenderer meshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        if (Random.Range(0, 2) == 0)
        {
            meshRenderer.sharedMesh = ghostMeshMale;
        }
        else
        {
            meshRenderer.sharedMesh = ghostMeshFemale;
        }

        Transform headJoint = gameObject.transform.Find("Armature/base/upper_body");
        Transform handJoint = gameObject.transform.Find("Armature/base/lower_body");

        if (glasses.Length > 0 && Random.Range(0f, 1f) < glassesChance)
        {
            Instantiate(glasses[Random.Range(0, glasses.Length)], headJoint);
        }

        if (handHeld.Length > 0 && Random.Range(0f, 1f) < handHeldChance)
        {
            Instantiate(handHeld[Random.Range(0, handHeld.Length)], handJoint);
        }
    }
}