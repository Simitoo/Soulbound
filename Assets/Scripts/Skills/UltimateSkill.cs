using Assets.Scripts.Stats;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class UltimateSkill : Skill
    {
        private LayerMask layerMask;
        private PlayerStats player;

        public CinemachineZoomController cinemachineZoomController;

        protected override void Start()
        {
            player = GetComponent<PlayerStats>();
            skillAnimator = GetComponent<Animator>();
            float animationDuration = skillAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            AnimationDuration = animationDuration;           

            layerMask = LayerMask.GetMask("Player", "Ground", "Untagged");

            skillDmg = 20f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer != layerMask && collision.CompareTag("Enemy"))
            {
                enemyStats = collision.transform.GetComponent<EnemyStats>();

                if (enemyStats != null)
                {
                    GetCombatSystem().Attack(enemyStats, skillDmg);
                    GetCombatSystem().DetermineElementalReaction(player.element, enemyStats);
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
                    GetCombatSystem().DetermineElementalReaction(player.element, enemyStats);
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
                    GetCombatSystem().DetermineElementalReaction(player.element, enemyStats);
                }
            }
        }

        public override void Cast(Transform firePoint)
        {
            base.Cast(firePoint);

            skillAnimator.SetTrigger("UltimateAttack");
            cinemachineZoomController.ZoomIn(AnimationDuration);
        }
    }
}
