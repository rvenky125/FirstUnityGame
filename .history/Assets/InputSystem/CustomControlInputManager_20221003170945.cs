using System.Collections;
using System.Collections.Generic;
using UnityEngine;
us

public class CustomControlInputManager : MonoBehaviour
{
    private CustomInputs customInputs;
    // Start is called before the firs
    void Awake()
    {
        customInputs = new CustomInputs();
    }

    void Start() {
        customInputs.Custom.Look.started += ctx => StartLook(ctx);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartLook() {

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