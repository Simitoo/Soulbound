using Assets.Scripts.Reactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ReactionManager : MonoBehaviour
    {
        private ObjectStats myStats;
        private ObjectStats target;

        private CombatSystem combatSystem;
        private ElementalReaction elementalReaction;

        public List<ElementalReaction> reactions = new List<ElementalReaction>();

        public delegate void OnReactionRemoved(ElementalReaction reaction);
        public OnReactionRemoved onReactionRemoved;

        private void Awake()
        {
            myStats = GetComponent<ObjectStats>();
            combatSystem = GetComponent<CombatSystem>();
            elementalReaction = null;
        }

        private void Start()
        {
            combatSystem.onReactionChanged += UpdateElementReactions;
        }

        private void UpdateElementReactions(ElementalReaction reaction, ObjectStats targetStats)
        {
            elementalReaction = reaction;
            target = targetStats;

            StartCoroutine(ApplyReactionEffect(elementalReaction, elementalReaction.CurrentDuration));
        }

        private void ApplyReaction(ElementalReaction reaction)
        {
            if (reactions.Contains(reaction))
            {
                Debug.Log("Reaction is already there");
                reaction.CurrentDuration += reaction.CurrentDuration;
            }
            else
            {
                Debug.Log("Reaction is added");
                reactions.Add(reaction);
            }
        }

        private void RemoveReaction()
        {
            if (reactions.Contains(elementalReaction))
            {
                Debug.Log(elementalReaction.GetType().Name + " is removed!");
                reactions.Remove(elementalReaction);
                onReactionRemoved?.Invoke(elementalReaction);
            }
        }

        private IEnumerator ApplyReactionEffect(ElementalReaction elementalReaction, float duration)
        {
            float randomValue = Random.Range(0f, 1f);
            float chanceRate = 0.15f;

            Debug.Log("Rng is: " + randomValue);

            if (randomValue <= chanceRate)
            {
                ApplyReaction(elementalReaction);

                while (duration >= 0f)
                {
                    elementalReaction.React(myStats, target);

                    yield return new WaitForSeconds(1f);

                    duration -= 1f;

                    elementalReaction.CurrentDuration = duration;
                }

                RemoveReaction();
            }
        }
    }
}
