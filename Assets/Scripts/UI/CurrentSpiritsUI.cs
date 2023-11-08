using Assets.Scripts.Enum;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSpiritsUI : MonoBehaviour
{
    private SpiritManager spiritManager;
    private Spirit mainSpirit;

    public Image mainSlot;
    public Image secondSlot;
    
    void Start()
    {
        spiritManager = SpiritManager.instance;

        spiritManager.onSpiritChanged += GetMainSpirit;
    }

 
    void Update()
    {
        FillSlots();
    }

    private void FillSlots()
    {
        if(mainSpirit != null && mainSpirit == spiritManager.CurrentSpirits[SpiritSlot.Main])
        {
            mainSlot.sprite = mainSpirit.icon;
            mainSlot.enabled = true;

            if(spiritManager.CurrentSpirits[SpiritSlot.Second] != null)
            {
                secondSlot.sprite = spiritManager.CurrentSpirits[SpiritSlot.Second].icon;
                secondSlot.enabled = true;
            }
            else
            {
                secondSlot.enabled = false;
            }
        }
    }

    private void GetMainSpirit(Spirit newSpirit, Spirit oldSpirit)
    {
        mainSpirit = newSpirit;
    }
}
