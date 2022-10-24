using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class BundleDownloader : MonoBehaviour
{
    public string bundleUrl = "";
    public string assetName = "";

    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        DownloadFile();
    }

    IEnumerator DownloadFile()
    {
        using (UnityWebRequest web = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl))
        {
            var operation = web.SendWebRequest();
            yield return operation;
            // while (!operation.isDone)
            // {
            //     Debug.Log("Operation is not done yet" + (web.downloadProgress * 100) / 100.0f);
            //     if (slider != null)
            //     {
            //         slider.value = (web.downloadProgress * 100) / 100.0f;
            //         yield return null;
            //     }
            // }

            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download AssetBundle!");
                yield break;
            }
            else
            {
                Debug.Log("Download succeeded");
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(web);
                StartCoroutine(LoadScene(bundle));
            }
        }
    }

    IEnumerator LoadScene(AssetBundle bundle)
    {
        Debug.Log("Load scene executed");
        string[] names = bundle.GetAllAssetNames();
        if (name.Contains(assetName))
        {
            Instantiate(bundle.LoadAsset(assetName));
            bundle.Unload(false);
        }
        else
        {
            Debug.LogError("Please provide valid asset name");
            Debug.Log("Available Asset names are:" + names);
            yield break;
        }
    }
}