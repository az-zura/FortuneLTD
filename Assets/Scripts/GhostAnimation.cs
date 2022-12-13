using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimation : MonoBehaviour
{
    public CharacterController controller;
    public float idleBobHeight = 0.4f;
 
    static Vector3 _onlyHorizontal = new Vector3(1, 1, 0).normalized; 

    void Update()
    {
        Vector3 v = new Vector3(controller.velocity.x,0,controller.velocity.z).normalized;
        if (!(v.x == 0 && v.z == 0))
        {
            gameObject.transform.forward = v;
        }

        var o = gameObject;
        var localPosition = o.transform.localPosition;
        localPosition = new Vector3(localPosition.x,Mathf.Cos(Time.time)*idleBobHeight,localPosition.z);
        o.transform.localPosition = localPosition;
    }
}
