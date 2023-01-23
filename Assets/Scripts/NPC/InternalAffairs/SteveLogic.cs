using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SteveLogic : MonoBehaviour
{
    private NPC_Locomotion locomotion;
    public GameObject player;
    public float loosePlayerDistance = 20;  
    public float looseFlowerDistance = 10;  
    public float destroyFlowerDistance = 3;

    public float viewAngle = 0.6f; 



    private Transform playerHeadPos;
    private Transform steveHeadPos;

    private LayerMask layerMaskVisiblePlayer;
    private LayerMask layerMaskVisiblePlant;

    private Vector3 flowerPos;
    private Vector3 lastKnownPlayerPos;
    
    private bool hasReachedLastPlayerPos = true;

    private PlayerVegitationSpawner vegetationSpawner;

    private Vector3 moveToPos;

    

    void Start()
    {
        locomotion = this.GetComponent<NPC_Locomotion>();
        steveHeadPos = transform.Find("Ghost/ghost/Armature/base/upper_body");
        playerHeadPos = player.transform.Find("Ghost/ghost/Armature/base/upper_body");
        vegetationSpawner = player.GetComponent<PlayerVegitationSpawner>();
        
        if(!steveHeadPos) Debug.LogError("Couldn't get upper body of steve");
        if(!playerHeadPos) Debug.LogError("Couldn't get upper body of player");
        if(!locomotion) Debug.LogError("Couldn't locomotion in steve");
        if(!locomotion) Debug.LogError("Couldn't get  vegetationSpawner in player");
        
        layerMaskVisiblePlayer  = LayerMask.GetMask("Default","Player","OnlyClickable");
        layerMaskVisiblePlant  = LayerMask.GetMask("Default","OnlyClickable");
    }
    
    
    void Update()
    {
        //remove close flowers
        var flowerSorted = vegetationSpawner.getSortetFlowerPos(transform.position);
        foreach (var flower in flowerSorted)
        {
            if (Vector3.Distance(flower.transform.position, transform.position) > destroyFlowerDistance)
            {
                break;
            }
            vegetationSpawner.removeFlower(flower);
        }
        
        //1. sees player?
        if (hasLoSToPlayer())
        {
            hasReachedLastPlayerPos = false;
            lastKnownPlayerPos = player.transform.position;
            moveSteveToPos(lastKnownPlayerPos);
            Debug.DrawRay(lastKnownPlayerPos,Vector3.up*5,Color.green,0.1f);

            return;   
        }

        //2. has reached last known player pos? 
        if (!hasReachedLastPlayerPos && Vector3.Distance(lastKnownPlayerPos, this.transform.position) > 3)
        {
            hasReachedLastPlayerPos = false;
            moveSteveToPos(lastKnownPlayerPos);
            Debug.DrawRay(lastKnownPlayerPos,Vector3.up*5,Color.green,0.1f);
            return;
        }
        hasReachedLastPlayerPos = true;
        
        //sees flower?
        var closestFlower = hasLoSToFlower(flowerSorted);
        if (closestFlower)
        {

            moveSteveToPos(closestFlower.transform.position);
            return;
        }
        
        
        //random search 
        if (Vector3.Distance(moveToPos, locomotion.getNavMeshDestination()) < 3 || locomotion.navigationState == NPC_Locomotion.NPCNavigationState.idle)
        {
            //Debug.Log("random search");
            if (locomotion.MoveTo(gameObject.transform.position, 10, 20))
            {
                moveToPos = locomotion.getNavMeshDestination();
            }
            
        }

    }

    private void moveSteveToPos(Vector3 pos)
    {
        if (Vector3.Distance(pos, moveToPos) > 0.2f)
        {
            moveToPos = pos;
            locomotion.MoveTo(pos);
        }
    }

    private bool hasLoSToPlayer()
    {
        return canSeeObject(player,loosePlayerDistance,"PlayerCollider");
    }
    
    private GameObject hasLoSToFlower(GameObject[] flowers)
    {
        foreach (GameObject flower in flowers)
        {
            if (flower)
            {
                if (Vector3.Distance(flower.transform.position, this.transform.position) > looseFlowerDistance)
                {
                    return null;
                }

                if (canSeeObject(flower, looseFlowerDistance, "TracableFlower"))
                {
                    return flower;
                }
            }
        }

        return null;
    }

    private bool canSeeObject(GameObject test, float maxDistance, string checkForName = default)
    {
        Vector3 direction = test.transform.position - steveHeadPos.position;
        float distance = direction.magnitude;
        
        if (distance > maxDistance)
        {
            return false;
        }
        
        direction = direction.normalized;
        Debug.DrawRay(steveHeadPos.position,-steveHeadPos.forward*5f,Color.blue,0.1f);
        float dot = Vector3.Dot(-steveHeadPos.forward, direction);
        if (dot < viewAngle)
        {
            return false;
        }
            
        
        if (Physics.Raycast(new Ray(steveHeadPos.position, direction), out var hit, distance+2,
                layerMaskVisiblePlayer))
        {
            Debug.DrawLine(steveHeadPos.position,hit.point,Color.red,0.1f);
            //Debug.Log("safd" + hit.collider.gameObject.tag);
            if (checkForName != default)
            {
                //Debug.Log(hit.collider.gameObject.name + " - > " + hit.collider.gameObject.name.Contains(checkForName));
                return (hit.collider.gameObject.name.Contains(checkForName));
            }
            else
            {
                return (hit.collider.gameObject == test);
            }
        }
        //Debug.Log("hits : " + hits.Length);
        return false;
    }
    
}
