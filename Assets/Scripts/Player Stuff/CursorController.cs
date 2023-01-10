using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private PlayerInput input;

    private void Awake()
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        input.Enable();
    }
}
