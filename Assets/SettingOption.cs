using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingOption : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI optionNameText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button rightArrowButton;

    [Header("Values")]
    [SerializeField] private string optionName;
    [SerializeField] private string[] values; // e.g. { "ON", "OFF" } or { "EASY", "NORMAL", "HARD" }
    [SerializeField] private SettingType settingType;
    private int currentIndex = 0;

    private void OnEnable()
    {
        GameManager.instance.OnSettingsChanged += RefreshUI;
    }

    private void OnDisable()
    {
        GameManager.instance.OnSettingsChanged -= RefreshUI;
    }
    private void RefreshUI()
    {
        string savedValue = GameManager.instance.GetSettingValue(settingType);

        int index = System.Array.FindIndex(values, v =>
            string.Equals(v.Trim(), savedValue.Trim(), System.StringComparison.OrdinalIgnoreCase));

        currentIndex = index >= 0 ? index : 0;
        valueText.text = values[currentIndex];
    }
    private void Start()
    {
        //string savedValue = GameManager.instance.GetSettingValue(settingType);
        //// Match index with saved value (default 0 if not found)
        //int index = System.Array.FindIndex(values, v =>
        //string.Equals(v.Trim(), savedValue.Trim(), System.StringComparison.OrdinalIgnoreCase));
        //currentIndex = index >= 0 ? index : 0;
        // Set initial texts
        RefreshUI();
        optionNameText.text = optionName;
       // valueText.text = values[currentIndex];

        // Hook up buttons
        leftArrowButton.onClick.AddListener(PreviousValue);
        rightArrowButton.onClick.AddListener(NextValue);
    }

    private void UpdateValueText()
    {
        valueText.text = values[currentIndex];
        GameManager.instance.SetSettingValue(settingType, values[currentIndex]);
    }

    private void PreviousValue()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = values.Length - 1;
        UpdateValueText();
    }

    private void NextValue()
    {
        currentIndex++;
        if (currentIndex >= values.Length) currentIndex = 0;
        UpdateValueText();
    }

    // Optional: expose current value
    public string GetCurrentValue()
    {
        return values[currentIndex];
    }
}
