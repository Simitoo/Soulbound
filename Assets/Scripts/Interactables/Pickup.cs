using Assets.Scripts;
using Assets.Scripts.Models.Items;
using System;
using UnityEngine;

public class Pickup : Interactable
{
    public Item item;

    public override void Interaction()
    {
        base.Interaction();

        PickUp();
    }

    private void PickUp()
    {       
        Debug.Log("Piking up " + item.name);
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }

    public sealed override void CloseInteraction()
    {
        //do nothing
    }
}
