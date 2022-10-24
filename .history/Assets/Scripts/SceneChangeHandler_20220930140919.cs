using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeHandler : MonoBehaviour
{
    Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        dropdown.onValueChanged.AddListener
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
