using Assets.Scripts.Constants;
using Assets.Scripts.Models;
using Assets.Scripts.Skills;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private SpiritManager spiritManager;
    private Spirit currentSpirit;
    private PlayerStats playerStats;

    private GameObject skillOnePrefab;
    private GameObject skillTwoPrefab;
    private Skill skillComponent;
    private UltimateSkill ultimateSkill;

    public Transform firePoint;

    public float skillOneCooldown;
    public float skillTwoCooldown;
    public float ultimateCooldown;

    public delegate void OnSkillCast(Skill skill);
    public OnSkillCast onSkillCast;

    private void Start()
    {
        spiritManager = SpiritManager.instance;
        ultimateSkill = GetComponent<UltimateSkill>();
        playerStats = GetComponent<PlayerStats>();

        spiritManager.onSpiritChanged += GetMainSpirit;
    }

    private void Update()
    {
        if(currentSpirit != null)
        {         
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (skillOneCooldown == 0 && IsEnoughEnergy(ConstantsValues.SkillRequiredEnergy.skillOne))
                {
                    SkillOne();
                    skillOneCooldown = ConstantsValues.SkillCooldowns.skillOne;
                }
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (skillTwoCooldown == 0 && IsEnoughEnergy(ConstantsValues.SkillRequiredEnergy.skillTwo))
                {
                    SkillTwo();
                    skillTwoCooldown = ConstantsValues.SkillCooldowns.skillTwo;
                }
            }
        }
        else
        {
            //user message
        }
        

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ultimateCooldown == 0 && IsEnoughEnergy(ConstantsValues.SkillRequiredEnergy.ultimateSkill))
            {
                UltimateSkill();
                ultimateCooldown = ConstantsValues.SkillCooldowns.ultimateSkill;
            }
        }

        UpdateCooldowns();
    }

    private void UltimateSkill()
    {
        if (ultimateSkill != null)
        {
            ultimateSkill.Cast(firePoint);
            ultimateSkill.currentCooldown = ultimateCooldown;
            onSkillCast.Invoke(ultimateSkill);
        }
    }

    private void SkillTwo()
    {
        if (skillTwoPrefab != null)
        {
            skillComponent = skillTwoPrefab.GetComponent<Skill>();
            skillComponent.Cast(firePoint);

            skillComponent.currentCooldown = skillTwoCooldown;
            onSkillCast.Invoke(skillComponent);
        }
    }

    private void SkillOne()
    {
        if (skillOnePrefab != null)
        {
            skillComponent = skillOnePrefab.GetComponent<Skill>();
            skillComponent.Cast(firePoint);

            skillComponent.currentCooldown = skillOneCooldown;
        }
    }

    private void GetMainSpirit(Spirit newSpirit, Spirit oldSpirit)
    {
        currentSpirit = newSpirit;
        if (currentSpirit != null)
        {
            skillOnePrefab = currentSpirit.skillOne;
            skillTwoPrefab = currentSpirit.skillTwo;
        }
    }

    private bool IsEnoughEnergy(float consumption)
    {
        float reducedEnergy = playerStats.CurrEnergy - consumption;

        if (reducedEnergy >= 0)
        {
            playerStats.CurrEnergy -= consumption; 
            return true;
        }

        playerStats.CurrEnergy = 0;
        return false;
    }

    private void UpdateCooldowns()
    {
        if(skillOneCooldown > 0f)
        {
            skillOneCooldown -= Time.deltaTime;
        }

        if(skillTwoCooldown > 0f)
        {
            skillTwoCooldown -= Time.deltaTime;
        }

        if(ultimateCooldown > 0f)
        {
            ultimateCooldown -= Time.deltaTime;
        }

        if(skillOneCooldown <= 0f)
        {
            skillOneCooldown = 0f;
        }

        if(skillTwoCooldown <= 0f)
        {
            skillTwoCooldown = 0f;
        }

        if(ultimateCooldown <= 0f)
        {
            ultimateCooldown = 0f;
        }
    }
}
