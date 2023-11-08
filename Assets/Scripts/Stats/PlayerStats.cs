using Assets.Scripts.Constants;
using Assets.Scripts.Models;
using UnityEngine;

public class PlayerStats : ObjectStats
{
    private PlayerMovement playerMovement;

    protected override void Awake()
    {        
        energy.SetBaseValue(ConstantsValues.StatsValue.baseEnergy);
        energyRegen.SetBaseValue(ConstantsValues.StatsValue.baseEnergyRegenerationRate);

        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        playerMovement = GetComponent<PlayerMovement>();     
        SpiritManager.instance.onSpiritChanged += AddModifiers;
    }

    public override void BaseStatsIncrementation(float increment)
    {
        base.BaseStatsIncrementation(increment);

        if (attack.GetValue != 0 && health.GetValue != 0 && energy.GetValue != 0)
        {
            attack.LevelIncrementation(increment);
            health.LevelIncrementation(increment);
            energy.LevelIncrementation(increment);
        }
    }

    public override void ReciveDamage(ObjectStats attackerStats, float bonusSkillDmg)
    {
        base.ReciveDamage(attackerStats, bonusSkillDmg);

        playerMovement.ReciveHit();


    }

    public void FullRecover()
    {
        CurrHealth = health.GetValue;
        CurrEnergy = energy.GetValue;
    }

    public override void Die()
    {
        base.Die();

        PlayerManager.instance.KillPlayer();
    }

    private void AddModifiers(Spirit newSpirit, Spirit oldSpirit)
    {
        if (newSpirit != null)
        {
            attack.AddModifier(newSpirit.attackModifier);
            health.AddModifier(newSpirit.healthModifier);
            energyRegen.AddModifier(newSpirit.energyRegeneration);
            defence.AddModifier(newSpirit.defenceModifier);
            speed.AddModifier(newSpirit.speedModifier);
            resist.AddModifier(newSpirit.resistModifier);
            fireDmg.AddModifier(newSpirit.fireDamageModifier);
            waterDmg.AddModifier(newSpirit.waterDamageModifier);
            windDmg.AddModifier(newSpirit.windDamageModifier);
            earthDmg.AddModifier(newSpirit.earthDamageModifier);
        }

        if (oldSpirit != null)
        {
            attack.RemoveModifier(oldSpirit.attackModifier);
            health.RemoveModifier(oldSpirit.healthModifier);
            energyRegen.RemoveModifier(oldSpirit.energyRegeneration);
            defence.RemoveModifier(oldSpirit.defenceModifier);
            speed.RemoveModifier(oldSpirit.speedModifier);
            resist.RemoveModifier(oldSpirit.resistModifier);
            fireDmg.RemoveModifier(oldSpirit.fireDamageModifier);
            waterDmg.RemoveModifier(oldSpirit.waterDamageModifier);
            windDmg.RemoveModifier(oldSpirit.windDamageModifier);
            earthDmg.RemoveModifier(oldSpirit.earthDamageModifier);
        }
    }
}
