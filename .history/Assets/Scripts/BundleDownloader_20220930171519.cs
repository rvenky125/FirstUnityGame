using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class BundleDownloader : MonoBehaviour
{
    [SerializeField]
    private bool _loadOnLaunch = true;
    public string bundleUrl = "";
    public string assetName = "";

    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        if (_loadOnLaunch)
        {
            StartCoroutine(DownloadFile(bundleUrl, assetName));
        }
    }

    public IEnumerator DownloadFile(string url, string name)
    {
        Debug.Log("Download file called:" + url + "," + name);
        using (UnityWebRequest web = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            var operation = web.SendWebRequest();
            // yield return operation;
            while (!operation.isDone)
            {
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
        var loadRequest = bundle.LoadAssetAsync(name);
        yield return loadRequest;
        if (loadRequest.isDone)
        {

            // asset.transform.SetPositionAndRotation(new Vector3(0, 0, 0), asset.transform.rotation);
            Instantiate(loadRequest.asset);
            bundle.Unload(false);
            if (slider != null)
            {
                slider.value = 1.0f;
                slider.gameObject.SetActive(false);
                slider.value = 0f;
            }
            yield break;
        }
    }
}