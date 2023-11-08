using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerStats stats;

    public TextMeshProUGUI healthValue;
    public Slider hpSlider;

    private void Start()
    {       
        hpSlider.value = stats.health.GetValue;
    }

    private void Update()
    {
        SetHealth();
    }

    private void SetHealth()
    {
        hpSlider.maxValue = stats.health.GetValue;
        hpSlider.value = stats.CurrHealth;
        healthValue.text = $"{stats.CurrHealth:f0}/ {stats.health.GetValue:f0}";
    }
}
