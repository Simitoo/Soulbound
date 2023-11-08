using Assets.Scripts.Stats;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(CombatSystem))]
    public class EnemyController : MonoBehaviour
    {
        private Transform player;
        private AIPath aiPath;
        private EnemyStats mystats;
        private Animator enemyAnim;
        private CombatSystem combat;

        [SerializeField]
        private float detectionRange;

        private float followRange;

        private float rayDistance = 1.2f;
        private float stunTimer = 0f;
        private float stunDuration = 1.2f;

        private Vector2 enemyStartPosition;
        private Vector2 rayDirection;

        private bool isPlayerAbove;
        private bool isStunned;


        private void Start()
        {
            player = PlayerManager.instance.player.transform;

            aiPath = GetComponent<AIPath>();
            mystats = GetComponent<EnemyStats>();

            enemyAnim = GetComponentInChildren<Animator>();

            combat = GetComponent<CombatSystem>();

            enemyStartPosition = transform.position;

            if (combat != null)
            {
                combat.OnAttack += SetAttackAnimation;
            }

            followRange = detectionRange;
        }

        private void Update()
        {
            if (isStunned)
            {
                stunTimer += Time.deltaTime;
                if (stunTimer >= stunDuration)
                {
                    isStunned = false;
                    stunTimer = 0f;
                }
            }
            else
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

                if (distanceToPlayer <= detectionRange)
                {
                    aiPath.maxSpeed = mystats.speed.GetValue;

                    aiPath.destination = player.transform.position;

                    ReachTarget();

                }
                else
                {
                    ReturnToStartPosition();
                }

                Flip();

                SetWalkAnimation(aiPath.maxSpeed);
            }          
        }

        private void ReachTarget()
        {
            int playerLayer = LayerMask.NameToLayer("Player");
            int layerMask = 1 << playerLayer;

            PlayerAbove(layerMask);
            PlayerInFront(layerMask);
        }

        private void StartAttack()
        {
            ObjectStats targetStats = player.GetComponent<ObjectStats>();

            if (targetStats != null)
            {
                combat.Attack(targetStats, 0);
                combat.DetermineElementalReaction(mystats.element, targetStats);
            }
        }

        private void ReturnToStartPosition()
        {
            aiPath.destination = enemyStartPosition;

            float distanceToStartPosition = Vector2.Distance(transform.position, enemyStartPosition);

            if (distanceToStartPosition <= 0.5f)
            {
                aiPath.maxSpeed = 0;
                mystats.CurrHealth = mystats.health.GetValue;
            }
        }

        private void SetWalkAnimation(float speed)
        {
            enemyAnim.SetFloat("Speed", speed);
        }

        private void SetAttackAnimation()
        {
            if (!isPlayerAbove && aiPath.maxSpeed == 0)
            {
                enemyAnim.SetTrigger("Attack");
            }
        }

        private void PlayerAbove(int layerMask)
        {
            isPlayerAbove = false;

            Vector2 rayStartPoint = transform.position;
            Vector2 rayDirection = Vector2.up;

            RaycastHit2D hit;

            hit = Physics2D.Raycast(rayStartPoint, rayDirection, rayDistance, layerMask);
            Debug.DrawRay(rayStartPoint, rayDirection * rayDistance, Color.red);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    aiPath.maxSpeed = 0;
                    isPlayerAbove = true;
                }
            }
        }

        private void PlayerInFront(int layerMask)
        {
            RaycastHit2D hit;

            hit = Physics2D.Raycast(transform.position, rayDirection, rayDistance, layerMask);
            Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.blue);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player") && !isPlayerAbove)
                {
                    aiPath.maxSpeed = 0;
                    StartAttack();
                }
            }
        }

        public void ReciveHit()
        {
            isStunned = true;
            aiPath.maxSpeed = 0;
            enemyAnim.SetTrigger("Hit");
        }

        private void Flip()
        {
            if (aiPath.desiredVelocity.x >= 0.01f) //right
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                rayDirection = Vector2.right;
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                rayDirection = Vector2.left;
            }
        }
    }
}
