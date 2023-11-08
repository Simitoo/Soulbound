using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private float baseValue;

    private List<float> modifiers = new List<float>();

    public float GetValue => FinalValueCalculation();

    public void SetBaseValue(float constantValue)
    {
        if (baseValue == 0)
        {
            baseValue += constantValue;
        }
    }

    public void AddModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }

    public void LevelIncrementation(float increment)
    {
        if (increment != 0)
        {
            baseValue *= increment;
        }
    }

    public float GetValueInPercentage()
    {
        float valueInPercentage = GetValue / 100;

        return valueInPercentage;
    }

    private float FinalValueCalculation()
    {
        float finalValue = baseValue;
        modifiers.ForEach(m => finalValue += m);

        return finalValue;
    }
}
