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
    private int currentIndex = 0;

    private void Start()
    {
        // Set initial texts
        optionNameText.text = optionName;
        UpdateValueText();

        // Hook up buttons
        leftArrowButton.onClick.AddListener(PreviousValue);
        rightArrowButton.onClick.AddListener(NextValue);
    }

    private void UpdateValueText()
    {
        valueText.text = values[currentIndex];
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
