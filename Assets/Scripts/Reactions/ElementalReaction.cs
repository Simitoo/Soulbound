using Assets.Scripts.Enum;

using UnityEngine;

namespace Assets.Scripts.Reactions
{
    public abstract class ElementalReaction : MonoBehaviour
    {
        public ReactionType type;
        public virtual float CurrentDuration { get; set; }
        public abstract void React(ObjectStats attacker, ObjectStats target);
    }
}
