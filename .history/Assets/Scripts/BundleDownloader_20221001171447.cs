using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using System.IO;
public class BundleDownloader : MonoBehaviour
{
    [SerializeField]
    private bool _loadOnLaunch = true;

    //This is required only if the _loadOnLaunchIsTrue
    public string bundleUrl = "";
    //This is required only if the _loadOnLaunchIsTrue
    public string assetName = "";

    [SerializeField]
    private Slider slider = null;

    private AssetBundle bundle = null;
    private GameObject previousLoadedGameObject = null;

    [SerializeField]
    private GameObject[] objectsToHideWhileLoadingAssets;

    [SerializeField]
    private CharacterController player;

    // private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        // originalPosition = player.transform.position;
        if (_loadOnLaunch)
        {
            StartCoroutine(DownloadFile(bundleUrl, assetName));
        }
    }

    public IEnumerator DownloadFile(string url, string name)
    {
        try {

        }
        File.
        var reader = new StreamReader(Application.persistentDataPath + "/" + name);
        var bufferLength = Buffer.ByteLength(reader.ReadToEnd().ToCharArray());
        Debug.Log(bufferLength);
        //Restting player to original position
        ResetPlayerPos();

        if (!!previousLoadedGameObject)
        {
            Destroy(previousLoadedGameObject);
        }

        using (UnityWebRequest web = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            var operation = web.SendWebRequest();
            // yield return operation;

            slider.gameObject.SetActive(true);
            slider.value = 0f;

            SetActiveToGameObjects(false);

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
                bundle = DownloadHandlerAssetBundle.GetContent(web);
                var buffer = DownloadHandlerBuffer.GetContent(web);
                SaveBundle(name, buffer);
                yield return LoadAssetBundle(name);
            }
            SetActiveToGameObjects(true);
        }
    }

    IEnumerator LoadAssetBundle(string name)
    {
        var loadRequest = bundle.LoadAssetAsync(name);
        yield return loadRequest;
        if (loadRequest.isDone)
        {
            var asset = loadRequest.asset as GameObject;
            // asset.transform.SetPositionAndRotation(new Vector3(0, 0, 0), asset.transform.rotation);
            previousLoadedGameObject = Instantiate(asset);
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

    void SetActiveToGameObjects(bool isActive)
    {
        for (int i = 0; i < objectsToHideWhileLoadingAssets.Length; i++)
        {
            objectsToHideWhileLoadingAssets[i].SetActive(isActive);
        }
    }

    void ResetPlayerPos()
    {
        Debug.Log("Reset position: ");
        player.enabled = false;
        player.transform.position = new Vector3(1.0f, 1.0f, -25.0f);
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        player.enabled = true;
    }


    void SaveBundle(string name, string buffer)
    {
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + name);
        writer.Write(buffer);
        writer.Flush();
        writer.Close();
    }
}