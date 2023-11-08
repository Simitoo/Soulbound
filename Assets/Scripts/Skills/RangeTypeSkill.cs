using Assets.Scripts.Stats;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class RangeTypeSkill : Skill
    {
        protected override void Start()
        {
            base.Start();
        }

        public override void Cast(Transform firePoint)
        {
            float distance = 4.5f;

            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, distance);

            if (hit.collider != null)
            {
                enemyStats = hit.collider.GetComponent<EnemyStats>();

                Instantiate(gameObject, hit.point, Quaternion.identity);

                if(enemyStats != null)
                {
                    GetCombatSystem().Attack(enemyStats, skillDmg);
                    GetCombatSystem().DetermineElementalReaction(spiritOwner.element, enemyStats);
                }                                             
            }
            else
            {
                Instantiate(gameObject, firePoint.position, Quaternion.identity);
            }
        }
    }
}
