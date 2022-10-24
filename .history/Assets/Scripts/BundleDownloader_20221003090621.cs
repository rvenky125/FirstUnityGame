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

    // Start is called before the first frame update
    void Start()
    {
        if (_loadOnLaunch)
        {
            StartCoroutine(DownloadFile(bundleUrl, assetName));
        }
    }

    public IEnumerator DownloadFile(string url, string name, bool loadAll = false)
    {
        //Resetting player to original position
        ResetPlayerPos();

        if (!!previousLoadedGameObject)
        {
            Destroy(previousLoadedGameObject);
        }

        var path = Path.Join(Application.persistentDataPath, name + ".unity3d");
        if (File.Exists(path))
        {
            Debug.Log("Loading asset");
            bundle = AssetBundle.LoadFromFile(path);
            yield return LoadAssetBundle(name, loadAll);
        }
        else
        {
            using (UnityWebRequest web = UnityWebRequest.Get(url))
            {
                Debug.Log("Network request called");
                var operation = web.SendWebRequest();
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
                    File.WriteAllBytes(path, web.downloadHandler.data);
                    bundle = AssetBundle.LoadFromMemory(web.downloadHandler.data);
                    yield return LoadAssetBundle(name, loadAll);
                }
                SetActiveToGameObjects(true);
            }
        }
    }

    IEnumerator LoadAssetBundle(string name, bool loadAll)
    {
        AssetB loadRequest = bundle.LoadAssetAsync(name);
        yield return loadRequest;
        if (loadRequest.isDone)
        {
            var asset = loadRequest.asset as GameObject;
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
}