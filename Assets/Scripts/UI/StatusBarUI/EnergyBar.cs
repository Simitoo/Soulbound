using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public PlayerStats stats;

    public TextMeshProUGUI energyValue;
    public Slider energySlider;

    private void Start()
    {
        energySlider.maxValue = stats.energy.GetValue;
        energySlider.value = stats.energy.GetValue;
    }

    private void Update()
    {
        SetEnergy();
    }

    private void SetEnergy()
    {
        energySlider.value = stats.CurrEnergy;
        energyValue.text = $"{stats.CurrEnergy:f0}/ {stats.energy.GetValue:f0}";
    }
}
