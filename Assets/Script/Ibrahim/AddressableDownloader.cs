using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class AddressableDownloader : MonoBehaviour
{
    public AddressableMemoryReleaser MemoryManager;
    public CharacterCustomizationManager characterCustomizationManager;
    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
            DontDestroyOnLoad(this.gameObject);
            MemoryManager = GetComponent<AddressableMemoryReleaser>();
        //}
       // else
        //{
         //   Destroy(this.gameObject);
        //}
    }
    private void OnEnable()
    {
        Constants.downloadAddressableObject += DownloadAddressableObject;
        Constants.downloadAddressableTexture += DownloadAddressableTexture;
    }
    private void OnDisable()
    {
        Constants.downloadAddressableObject -= DownloadAddressableObject;
        Constants.downloadAddressableTexture -= DownloadAddressableTexture;
    }
    IEnumerator DownloadAddressableObject(string key, bodyType type, GameObject applyOn)
    {
        Debug.Log(type + "    " + key);
        characterCustomizationManager.loader.SetActive(true);
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
                                   // characterCustomizationManager.ApplyHairPreset(loadAd.Result as GameObject, key, type.ToString());
                                    applyOn.GetComponent<AvatarBodyParts>().ApplyHairPreset(loadAd.Result as GameObject, key, type.ToString());
                                    break;
                                case bodyType.Shirt:
                                    // characterCustomizationManager.ApplyShirtPreset(loadAd.Result as GameObject, key, type.ToString());
                                    applyOn.GetComponent<AvatarBodyParts>().ApplyShirtPreset(loadAd.Result as GameObject, key, type.ToString());
                                    break;
                                case bodyType.Trouser:
                                    // characterCustomizationManager.ApplyTrouserPreset(loadAd.Result as GameObject, key, type.ToString());
                                    applyOn.GetComponent<AvatarBodyParts>().ApplyTrouserPreset(loadAd.Result as GameObject, key, type.ToString());
                                    break;
                                case bodyType.Shoes:
                                    // characterCustomizationManager.ApplyShoesPreset(loadAd.Result as GameObject, key, type.ToString());
                                    applyOn.GetComponent<AvatarBodyParts>().ApplyShoesPreset(loadAd.Result as GameObject, key, type.ToString());
                                    break;
                                case bodyType.Preset:
                                    // characterCustomizationManager.ApplyOnPreset(loadAd.Result as GameObject, key, type.ToString());
                                    applyOn.GetComponent<AvatarBodyParts>().ApplyOnPreset(loadAd.Result as GameObject, key, type.ToString());
                                    break;
                            }
                            characterCustomizationManager.loader.SetActive(false);
                            MemoryManager.AddToReferenceList(loadAd, key.ToLower());

                        }
                        yield break;
                    }
                }
            }
        }
    }
    IEnumerator DownloadAddressableTexture(string key, bodyType type, GameObject applyOn)
    {
        Debug.Log(type + "    " + key);
        characterCustomizationManager.loader.SetActive(true);
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
                                    applyOn.GetComponent<AvatarBodyParts>().ApplyEyeTexture(loadAd.Result as Texture2D, key);
                                    break;
                            }
                            characterCustomizationManager.loader.SetActive(false);
                            MemoryManager.AddToReferenceList(loadAd, key.ToLower());

                        }
                        yield break;
                    }
                }
            }
        }
    }

}
