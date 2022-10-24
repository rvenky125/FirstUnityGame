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
    }

    IEnumerator DownloadFile()
    {
        using (UnityWebRequest web = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl))
        {
            var operation = web.SendWebRequest();

            while (!operation.isDone)
            {
                slider.value = (web.downloadProgress * 100);
            }

            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download AssetBundle!");
                yield break;
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(web);
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
    }
}