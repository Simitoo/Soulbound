using Assets.Scripts.Models.Items;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> items = new List<Item>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChanged;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of Inventory!");
            return;
        }

        instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);

        onItemChanged?.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        onItemChanged?.Invoke();
    }

}
