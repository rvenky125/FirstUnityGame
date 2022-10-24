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
        StartCoroutine(DownloadFile());
    }

    IEnumerator DownloadFile()
    {
        using (UnityWebRequest web = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl))
        {
            var operation = web.SendWebRequest();
            // yield return operation;
            while (!operation.isDone)
            {
                Debug.Log("Operation is not done yet" + (web.downloadProgress * 100) / 100.0f);
                if (slider != null)
                {
                    slider.value = (web.downloadProgress * 100) / 100.0f;
                    yield return null;
                }
            }

            if (web.result != UnityWebRequest.Result.Success)
            {
                yield break;
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(web);
                yield return LoadAssetBundle(bundle);
            }
        }
    }

    IEnumerator LoadAssetBundle(AssetBundle bundle)
    {
        var loadRequest = bundle.LoadAssetAsync(assetName);
        yield return loadRequest;
        if (loadRequest.isDone)
        {
            Instantiate(loadRequest.asset);
            bundle.Unload(false);
            slider.value = 1.0f;
            slider.gameObject.SetActive(false);
            slider.value = 0f;
            yield break;
        }
    }
}