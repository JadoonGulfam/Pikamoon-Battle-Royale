using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
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
    public void LoadScene(string sceneName, Action onComplete = null)
    {
        StartCoroutine(LoadSceneAsync(sceneName, onComplete));
    }

    private IEnumerator LoadSceneAsync(string sceneName, Action onComplete)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        onComplete?.Invoke(); // Hide splash/loading panel after loading
    }
    public void LoadSceneAdditive(string sceneName, Action onComplete = null)
    {
        StartCoroutine(LoadSceneAdditiveAsync(sceneName, onComplete));
    }

    private IEnumerator LoadSceneAdditiveAsync(string sceneName, Action onComplete)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        onComplete?.Invoke();
    }
}
