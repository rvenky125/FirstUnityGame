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

    Coroutine coroutine;

    // private string baseUrl = "http://localhost/bundledassets/";
    private string baseUrl = "http://3.111.7.166/playground/";

    // Start is called before the first frame update
    void Awake()
    {
        dropdown.onValueChanged.AddListener(delegate { OnSceneChange(); });
        StartCoroutine(downloader.DownloadFile(baseUrl + "environmentassets", "Environment"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSceneChange()
    {
        StopAllCoroutines();
        var url = "";
        var name = "";
        if (dropdown.value == 0)
        {
            url = baseUrl + "environmentassets";
            name = "Environment";
        }
        else if (dropdown.value == 1)
        {
            url = baseUrl + "environment2";
            name = "Environment2";
        }
        Debug.Log("Sending data:" + url + "," + name);
        coroutine = StartCoroutine(downloader.DownloadFile(url, name));
    }
}
