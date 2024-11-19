using UnityEngine;
using CharacterCustomization;
public class LoadingManager : MonoBehaviour
{
    [System.Serializable]
    public class LoadingIndicator
    {
        public string name;
        public GameObject indicatorObject;
    }

    public static LoadingManager Instance; // Singleton instance
    public LoadingIndicator[] loadingIndicators;

    private void Awake()
    {
        // Ensure there's only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Activates the specified loading indicator by name and deactivates others.
    /// </summary>
    public void ActivateLoading(string loadingName)
    {
        foreach (var indicator in loadingIndicators)
        {
            if (indicator.name == loadingName)
            {
                indicator.indicatorObject.SetActive(true);
            }
            else
            {
                indicator.indicatorObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Deactivates all loading indicators.
    /// </summary>
    public void DeactivateAll()
    {
        foreach (var indicator in loadingIndicators)
        {
            indicator.indicatorObject.SetActive(false);
        }
    }
}
