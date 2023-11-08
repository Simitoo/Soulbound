namespace Assets.Scripts.Reactions
{
    public class BurnReaction : ElementalReaction
    {
        private float duration = 10f;

        public static BurnReaction instance;

        private void Awake()
        {
            instance = this;

            CurrentDuration = duration;
            type = Enum.ReactionType.Burn;
        }

        public override float CurrentDuration => duration;

        public override void React(ObjectStats attacker, ObjectStats target)
        {
            target.ReciveDamagePerSecond(attacker, attacker.fireDmg.GetValueInPercentage());
        }
    }
}
