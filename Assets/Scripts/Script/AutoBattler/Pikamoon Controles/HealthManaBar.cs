using UnityEngine;
using UnityEngine.UI;

public class HealthManaBar : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image healthFill;
    [SerializeField] private Image manaFill;

    private void Start()
    {
        UpdateHealthBar(1f);
        UpdateManaBar(1f);
    }

    public void UpdateHealthBar(float fillAmount)
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = Mathf.Clamp01(fillAmount);
            print("fill amount" + healthFill.fillAmount);
        }
    }

    public void UpdateManaBar(float fillAmount)
    {
        if (manaFill != null)
        {
            manaFill.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }
}
