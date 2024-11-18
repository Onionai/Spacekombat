using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class RemoteAssetLoader : MonoBehaviour
{
    private AsyncOperationHandle<IList<GameObject>> assetHandle;

    [Header("Parameters")]
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private GameObject loadingScreen;

    [Header("References")]
    [SerializeField] private AssetLabelReference assetLabelReference;

    void Start()
    {
        loadingScreen.SetActive(true);
        progressSlider.maxValue = 100.0f;

        SetSliderUI(0);
        LoadRemoteAssetsByLabel();
    }

    private void LoadRemoteAssetsByLabel()
    {
        assetHandle = Addressables.LoadAssetsAsync<GameObject>(assetLabelReference, OnAssetLoaded);
        assetHandle.Completed += OnAllAssetsLoaded;
        
        StartCoroutine(UpdateProgressBar());
    }

    private void SetSliderUI(int value)
    {
        progressSlider.value = value;
        loadingText.text = $"{value}%";
    }

    private IEnumerator UpdateProgressBar()
    {
        while (!assetHandle.IsDone)
        {
            int percentage = Mathf.FloorToInt(assetHandle.PercentComplete * 100);
            SetSliderUI(percentage);
            yield return null;
        }
    }

    private void OnAssetLoaded(GameObject loadedAsset)
    {
        Instantiate(loadedAsset);
        Debug.Log("Remote asset successfully instantiated.");
    }

    private void OnAllAssetsLoaded(AsyncOperationHandle<IList<GameObject>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("All assets with the specified label loaded.");
        }
        else
        {
            Debug.LogError("Failed to load assets by label.");
        }
        loadingScreen.SetActive(false);
    }

    void OnDestroy()
    {
        if (assetHandle.IsValid())
        {
            Addressables.Release(assetHandle);
            Debug.Log("Remote assets released from memory.");
        }
    }
}
