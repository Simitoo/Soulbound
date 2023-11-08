using Assets.Scripts.Constants;
using Assets.Scripts.Enum;
using Assets.Scripts.Models.Characters;
using UnityEngine;

public class ObjectStats : MonoBehaviour
{
    private Character character;

    public Element element;
    public Stat attack;
    public Stat health;
    public Stat energy;
    public Stat defence;
    public Stat speed;
    public Stat resist;
    public Stat fireDmg;
    public Stat waterDmg;
    public Stat windDmg;
    public Stat earthDmg;
    public Stat voidDmg;
    public Stat luminosityDmg;
    public Stat energyRegen;   

    public float CurrAttack { get; set; }
    public float CurrHealth { get; set; }
    public float CurrEnergy { get; set; }
    public float CurrSpeed { get; set; }

    protected virtual void Awake()
    {
        character = GetComponent<Character>();
        element = character.element;

        CurrAttack = attack.GetValue;
        CurrHealth = health.GetValue;
        CurrEnergy = energy.GetValue;
        CurrSpeed = speed.GetValue;       
    }

    protected virtual void Start()
    {
        SetBaseElementDamageValue();
    }

    public virtual void BaseStatsIncrementation(float increment)
    {
        Debug.Log($"Increment stat with {increment}");       
    }

    public virtual void ReciveDamage(ObjectStats attackerStats, float bonusSkillDmg)
    {
        float attackerDamage = (attackerStats.attack.GetValue + bonusSkillDmg) - defence.GetValue;
        attackerDamage = Mathf.Clamp(attackerDamage, 0, float.MaxValue);

        CurrHealth -= attackerDamage;
        Debug.Log($"{transform.name} recives {attackerDamage} damage");

        if (CurrHealth <= 0)
        {
            Die();
        }
    }

    public void ReciveDamagePerSecond(ObjectStats attackerStats, float elementalDmgBonus)
    {
        float additionalDmg = (attackerStats.attack.GetValue - defence.GetValue) * (elementalDmgBonus - resist.GetValueInPercentage());
        additionalDmg = Mathf.Clamp(additionalDmg, 0, float.MaxValue);

        Debug.Log($"{transform.name} recives {additionalDmg} additional damage per second");

        CurrHealth -= additionalDmg;
        if (CurrHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(float amount)
    {

    }

    public void EnergyRegeneration()
    {
        if(CurrEnergy == energy.GetValue)
        {
            CurrEnergy = energy.GetValue;
        }
        else
        {
            CurrEnergy += energyRegen.GetValueInPercentage();
        }
        
    }

    public virtual void Die()
    {
        Debug.Log($"{transform.name} died!");
    }

    private void SetBaseElementDamageValue()
    {
        switch (character.element)
        {
            case Element.Fire:
                fireDmg.SetBaseValue(ConstantsValues.StatsValue.baseElementDmg);
                break;
            case Element.Water:
                waterDmg.SetBaseValue(ConstantsValues.StatsValue.baseElementDmg);
                break;
            case Element.Wind:
                windDmg.SetBaseValue(ConstantsValues.StatsValue.baseElementDmg);
                break;
            case Element.Earth:
                earthDmg.SetBaseValue(ConstantsValues.StatsValue.baseElementDmg);
                break;
            case Element.Void:
                voidDmg.SetBaseValue(ConstantsValues.StatsValue.baseElementDmg);
                break;
            case Element.Luminosity:
                luminosityDmg.SetBaseValue(ConstantsValues.StatsValue.baseElementDmg);
                break;
        }
    }
}
