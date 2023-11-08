using Assets.Scripts.Models.Characters;
using Assets.Scripts.Skills;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class CharacterInfoUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI nameField;

        [SerializeField]
        private TextMeshProUGUI elementField;

        [SerializeField]
        private TextMeshProUGUI valueField;

        [SerializeField]
        private Image ultimateSkill;

        //skill icon
        private PlayerStats playerStats;
        private Character character;

        private void Awake()
        {
            playerStats = GetComponent<PlayerStats>();
            character = GetComponent<Character>();
        }

        private void Start()
        {
            nameField.text = character.name;
            elementField.text = character.element.ToString();

            ultimateSkill.sprite = GetComponent<UltimateSkill>().icon;
            ultimateSkill.enabled = true;
        }

        private void Update()
        {
            valueField.text = StatsInfo();
        }

        private string StatsInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{playerStats.attack.GetValue}");
            sb.AppendLine($"{playerStats.health.GetValue}");
            sb.AppendLine($"{playerStats.defence.GetValue}");
            sb.AppendLine($"{playerStats.speed.GetValue}");
            sb.AppendLine($"{playerStats.resist.GetValue:f2}%");
            sb.AppendLine($"{playerStats.fireDmg.GetValue:f2}%");
            sb.AppendLine($"{playerStats.waterDmg.GetValue:f2}%");
            sb.AppendLine($"{playerStats.windDmg.GetValue:f2}%");
            sb.AppendLine($"{playerStats.earthDmg.GetValue:f2}%");
            sb.AppendLine($"{playerStats.voidDmg.GetValue:f2}%");
            sb.AppendLine($"{playerStats.luminosityDmg.GetValue:f2}%");
            sb.AppendLine($"{playerStats.energyRegen.GetValue:f1}%");


            return sb.ToString();
        }
    }
}
