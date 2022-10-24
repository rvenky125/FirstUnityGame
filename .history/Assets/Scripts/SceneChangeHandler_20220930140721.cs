using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U

public class SceneChangeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSceneChange(int sceneIndex)
    {
        Debug.Log(sceneIndex);
    }
}
