using Assets.Scripts.Constants;
using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.Stats
{
    public class EnemyStats : ObjectStats
    {
        private EnemyController enemyController;

        protected override void Awake()
        {
            base.Awake();

            energy.SetBaseValue(ConstantsValues.StatsValue.baseEnergy);
            energyRegen.SetBaseValue(ConstantsValues.StatsValue.baseEnergyRegenerationRate);
        }

        protected override void Start()
        {
            base.Start();
            enemyController = GetComponent<EnemyController>();
        }

        public override void ReciveDamage(ObjectStats attackerStats, float bonusSkillDmg)
        {
            base.ReciveDamage(attackerStats, bonusSkillDmg);
            enemyController.ReciveHit();

            Debug.Log("Enemy recives damage from " + attackerStats.transform.name);
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

        public override void Die()
        {
            base.Die();

            // die animation
            //Respawn after some time

            Destroy(gameObject);
        }
    }
}
