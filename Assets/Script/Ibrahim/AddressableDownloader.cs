using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class AddressableDownloader : MonoBehaviour
{
    public AddressableMemoryReleaser MemoryManager;
    public CharacterCustomizationManager characterCustomizationManager;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        MemoryManager = GetComponent<AddressableMemoryReleaser>();
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
    IEnumerator DownloadAddressableObject(string _key, bodyType _type, GameObject _applyOn, bool _applyColor = false)
    {
        Debug.Log(_type + "    " + _key);
        characterCustomizationManager.loader.SetActive(true);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (!string.IsNullOrEmpty(_key))
            {
                while (true)
                {
                    AsyncOperationHandle loadAd;
                    bool flag = false;
                    loadAd = MemoryManager.GetReferenceIfExist(_key.ToLower(), ref flag);
                    if (!flag)
                        loadAd = Addressables.LoadAssetAsync<GameObject>(_key.ToLower());
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

                            Addressables.ClearDependencyCacheAsync(_key);
                            MemoryManager.RemoveAddressable(_key);
                            // else where default items
                        }
                        else
                        {
                            Debug.Log("Success To load");
                            switch (_type)
                            {
                                case bodyType.Hair:
                                    // characterCustomizationManager.ApplyHairPreset(loadAd.Result as GameObject, key, type.ToString());
                                    _applyOn.GetComponent<AvatarBodyParts>().ApplyHairPreset(loadAd.Result as GameObject, _key, _type.ToString(), _applyColor);
                                    break;
                                case bodyType.Shirt:
                                    // characterCustomizationManager.ApplyShirtPreset(loadAd.Result as GameObject, key, type.ToString());
                                    _applyOn.GetComponent<AvatarBodyParts>().ApplyShirtPreset(loadAd.Result as GameObject, _key, _type.ToString());
                                    break;
                                case bodyType.Trouser:
                                    // characterCustomizationManager.ApplyTrouserPreset(loadAd.Result as GameObject, key, type.ToString());
                                    _applyOn.GetComponent<AvatarBodyParts>().ApplyTrouserPreset(loadAd.Result as GameObject, _key, _type.ToString());
                                    break;
                                case bodyType.Shoes:
                                    // characterCustomizationManager.ApplyShoesPreset(loadAd.Result as GameObject, key, type.ToString());
                                    _applyOn.GetComponent<AvatarBodyParts>().ApplyShoesPreset(loadAd.Result as GameObject, _key, _type.ToString());
                                    break;
                                case bodyType.Preset:
                                    // characterCustomizationManager.ApplyOnPreset(loadAd.Result as GameObject, key, type.ToString());
                                    _applyOn.GetComponent<AvatarBodyParts>().ApplyOnPreset(loadAd.Result as GameObject, _key, _type.ToString());
                                    break;
                                case bodyType.Arms:
                                    // characterCustomizationManager.ApplyTrouserPreset(loadAd.Result as GameObject, key, type.ToString());
                                    _applyOn.GetComponent<AvatarBodyParts>().ApplyArmsPreset(loadAd.Result as GameObject, _key, _type.ToString());
                                    break;
                                case bodyType.Legs:
                                    // characterCustomizationManager.ApplyTrouserPreset(loadAd.Result as GameObject, key, type.ToString());
                                    _applyOn.GetComponent<AvatarBodyParts>().ApplyLegsPreset(loadAd.Result as GameObject, _key, _type.ToString());
                                    break;
                            }
                            characterCustomizationManager.loader.SetActive(false);
                            MemoryManager.AddToReferenceList(loadAd, _key.ToLower());

                        }
                        yield break;
                    }
                }
            }
        }
    }
    async Task DownloadAddressableTexture(string key, bodyType type, GameObject applyOn)
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
                    await loadAd.Task;
                    if (loadAd.Status == AsyncOperationStatus.Failed)
                    {
                        Debug.Log("Fail To load");
                        characterCustomizationManager.loader.SetActive(false);
                        return;
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
                        return;
                    }
                }
            }
        }
    }

}
