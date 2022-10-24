using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneChangeHandler : MonoBehaviour
{
    TMPro.Dr dropdown;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        dropdown.onValueChanged.AddListener(delegate { OnSceneChange()});
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSceneChange(Dropdown change)
    {
        Debug.Log(sceneIndex);
    }
}
