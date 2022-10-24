using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneChangeHandler : MonoBehaviour
{
    TMPro.TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<TMPro.TMP_Dropdown>();
        dropdown.onValueChanged()

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
