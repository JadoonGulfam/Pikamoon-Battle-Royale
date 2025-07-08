using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEngine.UI;
public class LoadingManager : MonoBehaviour
{
    [System.Serializable]
    public class LoadingIndicator
    {
        public string name;             // e.g. "Default", "LongLoad" …
        public GameObject indicatorObject;
        public Image loadingFillImage;  // optional: bar or radial fill
    }

    [Header("Indicators")]
    public LoadingIndicator[] loadingIndicators;

    public float fakeLoadDuration = 1.0f;

    public static LoadingManager Instance;


    private Image _activeFillImage;        // current bar, null if none
    private Coroutine _fakeLoadRoutine;    // keeps track so we can stop it

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActivateLoading(string loadingName, bool startLoading)
    {
        _activeFillImage = null;

        foreach (var indicator in loadingIndicators)
        {
            bool active = indicator.name == loadingName;
            indicator.indicatorObject.SetActive(active);

            if (active)
            {
                _activeFillImage = indicator.loadingFillImage;
                if (_activeFillImage) _activeFillImage.fillAmount = 0f;
            }
        }
        if (startLoading)
            LoadScene();
    }

    public void DeactivateAll()
    {
        foreach (var indicator in loadingIndicators)
            indicator.indicatorObject.SetActive(false);

        _activeFillImage = null;

        // stop any ongoing fake‑load so we don't overwrite UI after hiding
        if (_fakeLoadRoutine != null)
        {
            StopCoroutine(_fakeLoadRoutine);
            _fakeLoadRoutine = null;
        }
    }
    public void LoadScene(string sceneName = "", Action onComplete = null)
    {
        if (string.IsNullOrEmpty(sceneName))
            _fakeLoadRoutine = StartCoroutine(FakeLoadingCoroutine(onComplete));
        else
            StartCoroutine(LoadSceneAsync(sceneName, onComplete));
    }
    public void LoadSceneAdditive(string sceneName = "", Action onComplete = null)
    {
        if (string.IsNullOrEmpty(sceneName))
            _fakeLoadRoutine = StartCoroutine(FakeLoadingCoroutine(onComplete));
        else
            StartCoroutine(LoadSceneAdditiveAsync(sceneName, onComplete));
    }
    private IEnumerator LoadSceneAsync(string sceneName, Action onComplete)
    {
        if (_activeFillImage) _activeFillImage.fillAmount = 0f;

        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            if (_activeFillImage)
                _activeFillImage.fillAmount = asyncLoad.progress / 0.9f;

            yield return null;
        }

        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone) yield return null;

        onComplete?.Invoke();
    }

    private IEnumerator LoadSceneAdditiveAsync(string sceneName, Action onComplete)
    {
        if (_activeFillImage) _activeFillImage.fillAmount = 0f;

        var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            if (_activeFillImage)
                _activeFillImage.fillAmount = asyncLoad.progress / 0.9f;

            yield return null;
        }

        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone) yield return null;

        onComplete?.Invoke();
    }

    private IEnumerator FakeLoadingCoroutine(Action onComplete)
    {
        if (_activeFillImage) _activeFillImage.fillAmount = 0f;

        float elapsed = 0f;
        while (elapsed < fakeLoadDuration)
        {
            elapsed += Time.deltaTime;
            if (_activeFillImage)
                _activeFillImage.fillAmount = Mathf.Clamp01(elapsed / fakeLoadDuration);

            yield return null;
        }

        if (_activeFillImage) _activeFillImage.fillAmount = 1f;

        DeactivateAll();      // hide UI
        onComplete?.Invoke();
    }
}
