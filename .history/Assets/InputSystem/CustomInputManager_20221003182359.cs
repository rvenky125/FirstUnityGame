using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInputManager : MonoBehaviour
{
    private CustomInputs customInputs;
    // Start is called before the firs
    void Awake()
    {
        customInputs = new CustomInputs();
    }

    void Start()
    {
        customInputs.Custom.Look.performed += ctx => StartLook();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartLook(Vector2 value)
    {
        
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
