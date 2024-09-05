using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class AddressableDownloader : MonoBehaviour
{
    public static AddressableDownloader Instance;
    public AddressableMemoryReleaser MemoryManager;
    public CharacterCustomizationManager characterCustomizationManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            MemoryManager = GetComponent<AddressableMemoryReleaser>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public IEnumerator DownloadAddressableObject(string key, bodyType type)
    {
        Debug.Log(type + "    " + key);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (!string.IsNullOrEmpty(key))
            {
                while (true)
                {
                    AsyncOperationHandle loadAd;
                    bool flag = false;
                    loadAd = MemoryManager.GetReferenceIfExist(key.ToLower(), ref flag);
                    if (!flag)
                        loadAd = Addressables.LoadAssetAsync<GameObject>(key.ToLower());
                    yield return loadAd;
                    if (loadAd.Status == AsyncOperationStatus.Failed)
                    {
                        Debug.Log("Fail To load");
                        characterCustomizationManager.loader.SetActive(false);
                        yield break;
                    }
                    else if (loadAd.Status == AsyncOperationStatus.Succeeded)
                    {
                        if (loadAd.Result == null || loadAd.Result.Equals(null))
                        {

                            Addressables.ClearDependencyCacheAsync(key);
                            MemoryManager.RemoveAddressable(key);
                            // else where default items
                        }
                        else
                        {
                            Debug.Log("Success To load");
                            switch (type)
                            {
                                case bodyType.Hair:
                                    characterCustomizationManager.ApplyHairPreset(loadAd.Result as GameObject, key, type.ToString());
                                    break;
                                case bodyType.Shirt:
                                    characterCustomizationManager.ApplyShirtPreset(loadAd.Result as GameObject, key, type.ToString());
                                    break;
                                case bodyType.Trouser:
                                    characterCustomizationManager.ApplyTrouserPreset(loadAd.Result as GameObject, key, type.ToString());
                                    break;
                                case bodyType.Shoes:
                                    characterCustomizationManager.ApplyShoesPreset(loadAd.Result as GameObject, key, type.ToString());
                                    break;
                                case bodyType.Preset:
                                    characterCustomizationManager.ApplyOnPreset(loadAd.Result as GameObject, key, type.ToString());
                                    break;
                            }
                            MemoryManager.AddToReferenceList(loadAd, key.ToLower());

                        }
                        yield break;
                    }
                }
            }
        }
    }
    public IEnumerator DownloadAddressableTexture(string key, bodyType type)
    {
        Debug.Log(type + "    " + key);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (!string.IsNullOrEmpty(key))
            {
                while (true)
                {
                    AsyncOperationHandle loadAd;
                    bool flag = false;
                    loadAd = MemoryManager.GetReferenceIfExist(key.ToLower(), ref flag);
                    if (!flag)
                        loadAd = Addressables.LoadAssetAsync<Texture2D>(key.ToLower());
                    yield return loadAd;
                    if (loadAd.Status == AsyncOperationStatus.Failed)
                    {
                        Debug.Log("Fail To load");
                        characterCustomizationManager.loader.SetActive(false);
                        yield break;
                    }
                    else if (loadAd.Status == AsyncOperationStatus.Succeeded)
                    {
                        if (loadAd.Result == null || loadAd.Result.Equals(null))
                        {

                            Addressables.ClearDependencyCacheAsync(key);
                            MemoryManager.RemoveAddressable(key);
                            // else where default items
                        }
                        else
                        {
                            Debug.Log("Success To load");
                            switch (type)
                            {
                                case bodyType.EyeColor:
                                    characterCustomizationManager.ApplyEyeTexture(loadAd.Result as Texture2D, key);
                                    break;
                            }
                            MemoryManager.AddToReferenceList(loadAd, key.ToLower());

                        }
                        yield break;
                    }
                }
            }
        }
    }

}
