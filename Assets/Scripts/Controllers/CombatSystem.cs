using Assets.Scripts.Enum;
using Assets.Scripts.Reactions;
using System.Collections;

using UnityEngine;

[RequireComponent(typeof(ObjectStats))]
public class CombatSystem : MonoBehaviour
{
    public ObjectStats myStats;
    private CombatSystem targetCombatSystem;

    private float attackCooldown = 0f;

    public float attackSpeed = 1f;
    public float attackDelay = 0.6f;

    public event System.Action OnAttack;

    private ElementalReaction currReaction;

    public delegate void OnReactionChanged(ElementalReaction newElementalReaction, ObjectStats targetStats);
    public OnReactionChanged onReactionChanged;

    private void Awake()
    {
        myStats = GetComponent<ObjectStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        StartCoroutine(EnergyRegenerationDelay());
    }

    public virtual void Attack(ObjectStats targetStats, float bonusSKillDmg)
    {    
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay, bonusSKillDmg));

            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCooldown = 1f / attackSpeed;
        }
    }

    private IEnumerator DoDamage(ObjectStats stats, float delay, float skillDmg)
    {
        yield return new WaitForSeconds(delay);

        if (stats != null)
        {
            stats.ReciveDamage(myStats, skillDmg);
        }
    }

    private IEnumerator EnergyRegenerationDelay()
    {
        yield return new WaitForSeconds(attackSpeed);

        if (myStats.CurrEnergy < myStats.energy.GetValue)
        {           
            myStats.EnergyRegeneration();
        }
    }

    public void DetermineElementalReaction(Element attacker, ObjectStats target)
    {
        targetCombatSystem = target.transform.GetComponent<CombatSystem>();

        if (attacker == Element.Fire && target.element == Element.Earth)
        {
            currReaction = BurnReaction.instance;

            Debug.Log("Current Reaction is: " + currReaction.GetType().Name);

            if(targetCombatSystem.onReactionChanged != null)
            {
                targetCombatSystem.onReactionChanged(currReaction, target);
            }           
        }
    }
}
