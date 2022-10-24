using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneChangeHandler : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Dropdown dropdown;

    [SerializeField]
    BundleDownloader downloader;

    // Start is called before the first frame update
    void Awake()
    {
        dropdown.onValueChanged.AddListener(delegate { OnSceneChange(dropdown); });
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSceneChange(TMPro.TMP_Dropdown change)
    {
        if (change.value == 0)
        {
            StartCoroutine()
            downloader.DownloadFile("", "");
        }
        else if (change.value == 1)
        {
            downloader.DownloadFile("", "");
        }
    }
}
