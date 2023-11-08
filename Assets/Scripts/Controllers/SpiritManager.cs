using Assets.Scripts.Constants;
using Assets.Scripts.Enum;
using Assets.Scripts.Models;
using Assets.Scripts.Repository.UI;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour
{
    public static SpiritManager instance;

    public delegate void OnSpiritChanged (Spirit newSpirit, Spirit oldSpirit);
    public OnSpiritChanged onSpiritChanged;

    private SpiritInfoUI spiritInfoUI;
    private SelectionSlot[] selectionSlots;
    private PopUp popUpMessage;

    private Spirit[] currSpirits;

    public Transform parentOfSelectionSlots;

    private bool isFull;
    private Spirit oldSpirit;

    public Dictionary<SpiritSlot, Spirit> CurrentSpirits => SpiritMapper();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        int numOfSlots = System.Enum.GetNames(typeof(SpiritSlot)).Length;
        currSpirits = new Spirit[numOfSlots];

        spiritInfoUI = GetComponent<SpiritInfoUI>();
        selectionSlots = parentOfSelectionSlots.GetComponentsInChildren<SelectionSlot>();
        popUpMessage = GetComponent<PopUp>();
    }

    public void Equip(Spirit newSpirit)
    {
        int slotIndex = 0;      

        if (IsAlreadyEquiped(newSpirit))
        {
            popUpMessage.Show(string.Format(UserMessages.AlreadyEquiped, newSpirit.name));
            return;
        }

        if (IsCurrentCollectionFull())
        {
            popUpMessage.Show(string.Format(UserMessages.FullSelectionSlots));
        }

        for (int i = 0; i < currSpirits.Length; i++)
        {
            if (currSpirits[i] == null)
            {
                currSpirits[i] = newSpirit;
                slotIndex = i;

                selectionSlots[slotIndex].AddItem(newSpirit);              

                break;
            }            
        }

        onSpiritChanged?.Invoke(newSpirit, null);

        ShowSpiritInfo(currSpirits[slotIndex]);
    }

    public void Unequip(Spirit targetSpirit)
    {
        for (int i = 0; i < currSpirits.Length; i++)
        {
            if (currSpirits[i] == targetSpirit)
            {
                currSpirits[i] = null;

                onSpiritChanged?.Invoke(null, targetSpirit);
            }
        }

        oldSpirit = targetSpirit;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < selectionSlots.Length; i++)
        {
            selectionSlots[i].ClearSlot();          
        }

        SetToDefault();
    }

    private void ShowSpiritInfo(Spirit spirit)
    {
        spiritInfoUI.DisplaySpiritInfo(spirit);
    }

    private bool IsAlreadyEquiped(Spirit targetSpirit)
    {
        for (int i = 0; i < currSpirits.Length; i++)
        {
            //////////////////////////
            if (currSpirits[i] != null && currSpirits[i].name == targetSpirit.name)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsCurrentCollectionFull()
    {
        for(int i = 0;i < currSpirits.Length; i++)
        {
            if (currSpirits[i] != null)
            {
                isFull = true;
            }
            else
            {
                isFull = false;
            }
        }

        return isFull;
    }

    private Dictionary<SpiritSlot, Spirit> SpiritMapper()
    {
        if(currSpirits != null)
        {
            Dictionary<SpiritSlot, Spirit> mapper = new Dictionary<SpiritSlot, Spirit>
            {
                {SpiritSlot.Main, currSpirits[0] },
                {SpiritSlot.Second, currSpirits[1] }
            };

            return mapper;
        }
        
        return null;
    }

    private void SetToDefault()
    {
        for( int i = 0; i < currSpirits.Length; i++)
        {
            onSpiritChanged?.Invoke(null, currSpirits[i]);
        }
    }
}
