using Assets.Scripts.Stats;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class ColliderSkill : Skill
    {
        [SerializeField]
        private Rigidbody2D rb;
        private float speed = 6.5f;

        private LayerMask layerMask;

        protected override void Start()
        {
            base.Start();

            rb.velocity = transform.right * speed;
            layerMask = LayerMask.GetMask("Player", "Ground", "Untagged");

            skillDmg = 8.5f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer != layerMask && collision.CompareTag("Enemy"))
            {
                enemyStats = collision.transform.GetComponent<EnemyStats>();    
                
                if(enemyStats != null)
                {
                    GetCombatSystem().Attack(enemyStats, skillDmg);
                    GetCombatSystem().DetermineElementalReaction(spiritOwner.element, enemyStats);
                }
            }            
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.layer != layerMask && collision.CompareTag("Enemy"))
            {
                enemyStats = collision.transform.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    GetCombatSystem().Attack(enemyStats, skillDmg);
                    GetCombatSystem().DetermineElementalReaction(spiritOwner.element, enemyStats);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer != layerMask && collision.CompareTag("Enemy"))
            {
                enemyStats = collision.transform.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    GetCombatSystem().Attack(enemyStats, skillDmg);
                    GetCombatSystem().DetermineElementalReaction(spiritOwner.element, enemyStats);
                }
            }
        }

        public override void Cast(Transform firePoint)
        {
            Instantiate(gameObject, firePoint.position, firePoint.rotation);

            base.Cast(firePoint);                
        }
    }
}
