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

    // Start is called before the first frame update
    void Awake()
    {
        dropdown.onValueChanged.AddListener(delegate { OnSceneChange(); });
    }

    void OnStart() 

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
            url = "http://localhost/bundledassets/environmentassetbundle";
            name = "Environment";
        }
        else if (dropdown.value == 1)
        {
            url = "http://localhost/bundledassets/environment2";
            name = "Environment2";
        }
        Debug.Log("Sending data:" + url + "," + name);
        coroutine = StartCoroutine(downloader.DownloadFile(url, name));
    }
}
