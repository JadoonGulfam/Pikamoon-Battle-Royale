using System.Collections;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CharacterCustomization
{
    public class AddressableDownloader : MonoBehaviour
    {
        public AddressableMemoryReleaser MemoryManager;
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
        IEnumerator DownloadAddressableObject(string _key, BodyType _type, GameObject _applyOn, bool _applyColor = false)
        {
            GameManager.instance.loader.SetActive(true);
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
                            GameManager.instance.loader.SetActive(false);
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
                                    case BodyType.Hair:
                                        // characterCustomizationManager.ApplyHairPreset(loadAd.Result as GameObject, key, type.ToString());
                                        _applyOn.GetComponent<AvatarBodyParts>().ApplyHairPreset(loadAd.Result as GameObject, _key, BodyPartsType.Hair, _applyColor);
                                        break;
                                    case BodyType.Shirt:
                                        // characterCustomizationManager.ApplyShirtPreset(loadAd.Result as GameObject, key, type.ToString());
                                        _applyOn.GetComponent<AvatarBodyParts>().ApplyShirtPreset(loadAd.Result as GameObject, _key, BodyPartsType.Chest);
                                        break;
                                    case BodyType.Trouser:
                                        // characterCustomizationManager.ApplyTrouserPreset(loadAd.Result as GameObject, key, type.ToString());
                                        _applyOn.GetComponent<AvatarBodyParts>().ApplyTrouserPreset(loadAd.Result as GameObject, _key, BodyPartsType.Hips);
                                        break;
                                    case BodyType.Shoes:
                                        // characterCustomizationManager.ApplyShoesPreset(loadAd.Result as GameObject, key, type.ToString());
                                        _applyOn.GetComponent<AvatarBodyParts>().ApplyShoesPreset(loadAd.Result as GameObject, _key, BodyPartsType.Feet);
                                        break;
                                    case BodyType.Preset:
                                        // characterCustomizationManager.ApplyOnPreset(loadAd.Result as GameObject, key, type.ToString());
                                        _applyOn.GetComponent<AvatarBodyParts>().ApplyOnPreset(loadAd.Result as GameObject, _key, _type);
                                        break;
                                    case BodyType.Arms:
                                        // characterCustomizationManager.ApplyTrouserPreset(loadAd.Result as GameObject, key, type.ToString());
                                        _applyOn.GetComponent<AvatarBodyParts>().ApplyArmsPreset(loadAd.Result as GameObject, _key, BodyPartsType.Arms);
                                        break;
                                    case BodyType.Legs:
                                        // characterCustomizationManager.ApplyTrouserPreset(loadAd.Result as GameObject, key, type.ToString());
                                        _applyOn.GetComponent<AvatarBodyParts>().ApplyLegsPreset(loadAd.Result as GameObject, _key, BodyPartsType.Legs);
                                        break;
                                }
                                GameManager.instance.loader.SetActive(false);
                                MemoryManager.AddToReferenceList(loadAd, _key.ToLower());

                            }
                            yield break;
                        }
                    }
                }
            }
        }
        async Task DownloadAddressableTexture(string key, BodyType _type, GameObject applyOn)
        {
            GameManager.instance.loader.SetActive(true);
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
                            GameManager.instance.loader.SetActive(false);
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
                                switch (_type)
                                {
                                    case BodyType.EyeColor:
                                        applyOn.GetComponent<AvatarBodyParts>().ApplyEyeTexture(loadAd.Result as Texture2D, key);
                                        break;
                                    case BodyType.Eyebrow:
                                        applyOn.GetComponent<AvatarBodyParts>().ApplyEyebrowTexture(loadAd.Result as Texture2D, key);
                                        break;
                                }
                                GameManager.instance.loader.SetActive(false);
                                MemoryManager.AddToReferenceList(loadAd, key.ToLower());

                            }
                            return;
                        }
                    }
                }
            }
        }

    }
}