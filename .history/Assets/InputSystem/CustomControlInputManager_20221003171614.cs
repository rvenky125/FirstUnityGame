using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomControlInputManager : MonoBehaviour
{
    private CustomInputs customInputs;
    // Start is called before the firs
    void Awake()
    {
        customInputs = new CustomInputs();
    }

    void Start()
    {
        customInputs.Custom.Look.started += ctx => StartLook(ctx.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartLook(Vector2 value)
    {
        Debug.Log("Start Look:" + ve)
    }


    void Enable()
    {
        customInputs.Enable();
    }

    void Disable()
    {
        customInputs.Disable();
    }
}
