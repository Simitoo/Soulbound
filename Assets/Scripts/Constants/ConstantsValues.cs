namespace Assets.Scripts.Constants
{
    struct ConstantsValues
    {
        public struct StatsValue
        {
            public const float baseEnergy = 100f;
            public const float baseElementDmg = 50f;
            public const float baseEnergyRegenerationRate = 1.2f;
        }

        public struct LevelIncrement
        {
            public const float inceremnt = 1.7f;
        }

        public struct SkillCooldowns
        {
            public const float skillOne = 0.7f;
            public const float skillTwo = 5f;
            public const float ultimateSkill = 120f;
        } 
        
        public struct SkillRequiredEnergy
        {
            public const float skillOne = 10f;
            public const float skillTwo = 30f;
            public const float ultimateSkill = 60f;
        }
    }
}
