using Assets.Scripts.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusBar : MonoBehaviour
{
    public EnemyStats stats;
    public Slider hpSlider;
    public TextMeshProUGUI enemyNameText;
    public GameObject reactionsPanel;

    private void Start()
    {
        enemyNameText.text = stats.gameObject.name;
        hpSlider.value = stats.health.GetValue;
    }

    private void Update()
    {               
        if(stats != null) 
        {
            gameObject.SetActive(true);
            enemyNameText.enabled = true;
            SetHealth();
        }
        else
        {
            gameObject.SetActive(false);
            enemyNameText.enabled = false;
            reactionsPanel.SetActive(false);
        }
    }

    private void SetHealth()
    {
        hpSlider.maxValue = stats.health.GetValue;
        hpSlider.value = stats.CurrHealth;
    }
}
