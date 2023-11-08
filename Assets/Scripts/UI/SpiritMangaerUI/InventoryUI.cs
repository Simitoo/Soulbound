using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    private Inventory inventory;
    private InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChanged += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {

    }

    private void UpdateUI()
    {
        Debug.Log("Updating UI...");

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
