using Assets.Scripts.Models;
using Assets.Scripts.Skills;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Repository.UI
{
    public class SpiritInfoUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI nameField;

        [SerializeField]
        private TextMeshProUGUI elementField;

        [SerializeField]
        private TextMeshProUGUI statsField;

        [SerializeField]
        private Image spiritImage;

        [SerializeField]
        private Image skillOne;

        [SerializeField]
        private Image skillTwo;
        
        public void DisplaySpiritInfo(Spirit spirit)
        {
            nameField.text = spirit.name;

            elementField.text = $"element: {spirit.element}";

            spiritImage.sprite = spirit.icon;
            spiritImage.enabled = true;

            skillOne.sprite = spirit.skillOne.GetComponent<Skill>().icon;
            skillOne.enabled = true;

            skillTwo.sprite = spirit.skillTwo.GetComponent<Skill>().icon;
            skillTwo.enabled = true;

            statsField.text = StatsVisualizator(spirit);
        }
        
        private string StatsVisualizator(Spirit spirit)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(spirit.attackModifier != 0f ? $"{spirit.attackModifier:f1}" : "N/A");
            sb.AppendLine(spirit.healthModifier != 0f ? $"{spirit.healthModifier:f1}" : "N/A");
            sb.AppendLine(spirit.defenceModifier != 0f ? $"{spirit.defenceModifier:f1}" : "N/A");
            sb.AppendLine(spirit.speedModifier != 0f ? $"{spirit.speedModifier:f1}" : "N/A");
            sb.AppendLine(spirit.resistModifier != 0f ? $"{spirit.resistModifier:f1}" : "N/A");
            sb.AppendLine(spirit.fireDamageModifier != 0f ? $"{spirit.fireDamageModifier:f1}" : "N/A");
            sb.AppendLine(spirit.waterDamageModifier != 0f ? $"{spirit.waterDamageModifier:f1}" : "N/A");
            sb.AppendLine(spirit.windDamageModifier != 0f ? $"{spirit.windDamageModifier:f1}" : "N/A");
            sb.AppendLine(spirit.earthDamageModifier != 0f ? $"{spirit.earthDamageModifier:f1}" : "N/A");
            sb.AppendLine(spirit.energyRegeneration != 0f ? $"{spirit.energyRegeneration:f1}%" : "N/A");

            return sb.ToString();
        }
    }
}
