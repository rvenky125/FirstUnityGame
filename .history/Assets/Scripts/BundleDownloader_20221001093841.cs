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
    private Slider slider = null;

    private AssetBundle bundle = null;
    private GameObject previousLoadedGameObject = null;

    [SerializeField]
    private GameObject[] objectsToHideWhileLoadingAssets;

    [SerializeField]
    private GameObject player;

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
        // Debug.Log("Reset position: " + originalPosition.ToString());
        player.transform.position = MemberwiseClone ;
    }
}

public enum ConditionOperator
{
    // A field is visible/enabled only if all conditions are true.
    And,
    // A field is visible/enabled if at least ONE condition is true.
    Or,
}

public enum ActionOnConditionFail
{
    // If condition(s) are false, don't draw the field at all.
    DontDraw,
    // If condition(s) are false, just set the field as disabled.
    JustDisable,
}