using Assets.Scripts.Models;
using Assets.Scripts.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SkillPanelUI : MonoBehaviour
    {
        private PlayerManager playerManager;
        private SpiritManager spiritManager;
        private SkillController skillController;

        private Skill ultimateSkill;
        private Skill skillOne;
        private Skill skillTwo;

        public Image ultIcon;
        public Image skillOneIcon;
        public Image skillTwoIcon;

        public Image skillOneHider;
        public Image skillTwoHider;
        public Image ultimateHider;

        public TextMeshProUGUI skillOneCooldownText;
        public TextMeshProUGUI skillTwoCooldownText;
        public TextMeshProUGUI ultimateCooldownText;

        private void Start()
        {
            playerManager = PlayerManager.instance;
            spiritManager = SpiritManager.instance;

            skillController = playerManager.player.GetComponent<SkillController>();

            if (playerManager != null)
            {
                ultimateSkill = playerManager.player.GetComponent<UltimateSkill>();

                ultIcon.sprite = ultimateSkill.icon;
                ultIcon.enabled = true;
            }
           
            spiritManager.onSpiritChanged += GetSpiritSkillInfo;           
        }

        private void Update()
        {
            DisplaySpiritSkills();
            ShowCooldwons();
        }

        private void DisplaySpiritSkills()
        {
            if(skillOne != null && skillTwo != null)
            {
                skillOneIcon.sprite = skillOne.icon;
                skillOneIcon.enabled = true;

                skillTwoIcon.sprite = skillTwo.icon;
                skillTwoIcon.enabled = true;
            }
            else
            {
                skillOneIcon.enabled = false;
                skillOneIcon.enabled = false;
            }
        }

        private void GetSpiritSkillInfo(Spirit newSpirit, Spirit oldSpirit)
        {          
            if(newSpirit != null)
            {
                skillOne = newSpirit.skillOne.GetComponent<Skill>();
                skillTwo = newSpirit.skillTwo.GetComponent<Skill>();
            }
        }

        private void ShowCooldwons()
        {

            if(skillOne != null && skillController.skillOneCooldown > 0f)
            {
                skillOneCooldownText.enabled = true;
                skillOneCooldownText.text = $"{skillController.skillOneCooldown :f0} s";

                skillOneHider.enabled = true;                
            }
            else
            {
                skillOneCooldownText.enabled = false;
                skillOneHider.enabled = false;
            }

            if (skillTwo != null && skillController.skillTwoCooldown > 0f)
            {
                skillTwoCooldownText.enabled = true;
                skillTwoCooldownText.text = $"{skillController.skillTwoCooldown:f0} s";

                skillTwoHider.enabled = true;
            }
            else
            {
                skillTwoCooldownText.enabled = false;
                skillTwoHider.enabled = false;
            }

            if (ultimateSkill != null && skillController.ultimateCooldown > 0f)
            {
                ultimateCooldownText.enabled = true;
                ultimateCooldownText.text = $"{skillController.ultimateCooldown:f0} s";

                ultimateHider.enabled = true;
            }
            else
            {
                ultimateCooldownText.enabled = false;
                ultimateHider.enabled = false;
            }
        }
    }
}
