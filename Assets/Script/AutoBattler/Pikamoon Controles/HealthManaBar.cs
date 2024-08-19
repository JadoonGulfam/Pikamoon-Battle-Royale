using UnityEngine;
using UnityEngine.UI;

public class HealthManaBar : MonoBehaviour
{
    [SerializeField]
    private Image healthFill;
    [SerializeField]
    private Image manaFill;

    private void Start()
    {
        UpdateHealthBar(1);
        UpdateManaBar(1);
    }
    public void UpdateHealthBar(float fillAmount)
    {
        healthFill.fillAmount = 0.5f;
    }

    public void UpdateManaBar(float fillAmount)
    {
        manaFill.fillAmount = 0.8f;
    }
}
