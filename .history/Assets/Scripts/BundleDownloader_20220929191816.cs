using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class BundleDownloader : MonoBehaviour
{
    public string bundleUrl = "";
    public string assetName = "";

    // Start is called before the first frame update
    IEnumerator Start()
    {
        using (UnityWebRequest web = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl, ))
        {
            yield return web.SendWebRequest();
            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download AssetBundle!");
                yield break;
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(web);
                String[] names = bundle.GetAllAssetNames();
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