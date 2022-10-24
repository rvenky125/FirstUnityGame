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
        downloader = BundleDownloader
        dropdown.onValueChanged.AddListener(delegate { OnSceneChange(dropdown); });
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSceneChange(TMPro.TMP_Dropdown change)
    {
        var url = "";
        var name = "";
        if (change.value == 0)
        {
            url = "http://localhost/bundledassets/environmentassetbundle";
            name = "Environment";
        }
        else if (change.value == 1)
        {
            url = "http://localhost/bundledassets/environment2";
            name = "Environment2";
        }
        StartCoroutine(downloader.DownloadFile(url, name));
    }
}
