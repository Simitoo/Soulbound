using Assets.Scripts.Models;
using Assets.Scripts.Stats;

using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class Skill : MonoBehaviour
    {
        [SerializeField]
        protected Spirit spiritOwner;

        protected PlayerManager playerManager;
        protected CombatSystem combatSystem;
        protected PlayerMovement playerMovement;

        protected EnemyStats enemyStats;
        protected Animator skillAnimator;
        protected float skillDmg;

        public string skillName;
        public Sprite icon;
        public float currentCooldown;        

        public float AnimationDuration { get; protected set; }

        protected CombatSystem GetCombatSystem()
        {
            if(combatSystem == null)
            {
                playerManager = PlayerManager.instance;
                combatSystem = playerManager.player.GetComponent<CombatSystem>();               
            }

            return combatSystem;
        }

        protected virtual void Start()
        {
            skillAnimator = GetComponent<Animator>();
            float animationDuration = skillAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            AnimationDuration = animationDuration;

            Invoke(nameof(DestroyObject), animationDuration);
        }

        public virtual void Cast(Transform firePoint)
        {
            Debug.Log("Cast " + skillName);
            GetPlayerMovement();
            playerMovement.StunDuration = AnimationDuration + 1.2f;
            playerMovement.CastSkill();
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }      

        private PlayerMovement GetPlayerMovement()
        {
            if(playerMovement == null)
            {
                playerManager = PlayerManager.instance;
                playerMovement = playerManager.player.GetComponent<PlayerMovement>();
            }

            return playerMovement;
        }
    }
}
